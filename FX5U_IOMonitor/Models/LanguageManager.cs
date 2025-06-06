using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Login;
using Microsoft.EntityFrameworkCore;

namespace FX5U_IOMonitor.Models
{
    public static class LanguageManager
    {
        private static Dictionary<string, string> _currentLanguageMap = new Dictionary<string, string>();
        private static List<Dictionary<string, string>> _languageList = new List<Dictionary<string, string>>();
        private static List<string> _headers = new List<string>();
        public static event Action<string>? LanguageChanged;

        public static Dictionary<string, string> LanguageMap = new Dictionary<string, string>
        {
            { "TW", "繁體中文" },
            { "US", "English" }
        };
        public static void SetLanguage(string cultureCode)
        {
            LoadLanguageFromDatabase(cultureCode);

            // 儲存到系統設定
            Properties.Settings.Default.LanguageSetting = cultureCode;
            Properties.Settings.Default.Save();
        }
        public static void LoadLanguageCSV(string csvPath, string cultureName)
        {
            try
            {
                if (!File.Exists(csvPath))
                {
                    MessageBox.Show($"語言檔案不存在：{csvPath}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var reader = new StreamReader(csvPath, Encoding.UTF8);
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.Trim(),
                    MissingFieldFound = null,
                    HeaderValidated = null // ✅ 防止欄位對應錯誤時中斷
                };

                using var csv = new CsvReader(reader, config);

                if (!csv.Read() || !csv.ReadHeader())
                {
                    MessageBox.Show("語言檔案格式錯誤，請檢查 CSV 標頭。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _headers = csv.HeaderRecord?.ToList() ?? new List<string>();

                if (_headers.Count == 0 || !_headers.Contains("Key"))
                {
                    MessageBox.Show("語言檔案缺少必要欄位 'Key'。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                _languageList.Clear();

                while (csv.Read())
                {
                    var dict = new Dictionary<string, string>();
                    foreach (var header in _headers)
                    {
                        dict[header] = csv.GetField(header) ?? ""; // ✅ 空值防呆
                    }
                    _languageList.Add(dict);
                }

                _currentLanguageMap = _languageList
                    .Where(d => d.ContainsKey("Key"))
                    .ToDictionary(
                        d => d["Key"],
                        d => d.ContainsKey(cultureName) && !string.IsNullOrWhiteSpace(d[cultureName])
                                ? d[cultureName]
                                : d["Key"]
                    );

                // ✅ 語言變更事件
                LanguageChanged?.Invoke(cultureName);
              

            }
            catch (Exception ex)
            {
                MessageBox.Show($"載入語言檔案時發生錯誤：\n{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //更改語系
        public static string Translate(string key)
        {
            return _currentLanguageMap.TryGetValue(key, out var value) ? value : key;
        }
        //更改語系內含有數值型態的資料
        public static string TranslateFormat(string key, params object[] args)
        {
            var format = Translate(key);
            return string.Format(format, args);
        }
        /// <summary>
        /// 從資料庫載入語言對應表
        /// </summary>
        /// <param name="cultureCode">TW, US, JP, KR...等語言代碼</param>
        public static void LoadLanguageFromDatabase(string cultureCode)
        {
            try
            {
                using var context = new ApplicationDB();

                // 確認資料表中是否有該語系欄位
                var supportedColumns = typeof(Language)
                    .GetProperties()
                    .Select(p => p.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                if (!supportedColumns.Contains(cultureCode))
                {
                    MessageBox.Show($"❌ 不支援的語言代碼：{cultureCode}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 從資料庫載入語言資料
                var languageData = context.Language
                    .AsNoTracking()
                    .ToList()
                    .Where(l => !string.IsNullOrWhiteSpace(l.Key)) // 避免空Key
                    .ToDictionary(
                        l => l.Key,
                        l =>
                        {
                            var prop = typeof(Language).GetProperty(cultureCode);
                            return prop?.GetValue(l)?.ToString() ?? l.Key;
                        });

                _currentLanguageMap = languageData;

                LanguageChanged?.Invoke(cultureCode);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ 載入語系失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
