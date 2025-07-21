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
        public static Dictionary<string, string> LanguageMap { get; private set; }
                = new Dictionary<string, string>(); 
      
      
        public static void SetLanguage(string cultureCode)
        {
            // 儲存到系統設定
            Properties.Settings.Default.LanguageSetting = cultureCode;
            Properties.Settings.Default.Save();
            LoadLanguageFromDatabase(Properties.Settings.Default.LanguageSetting);

          
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
                var langProp = typeof(Language).GetProperty(cultureCode);
                if (langProp == null)
                {
                    MessageBox.Show($"❌ 無法取得語系欄位屬性：{cultureCode}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!supportedColumns.Contains(cultureCode))
                {
                    return;
                    MessageBox.Show($"❌ 不支援的語言代碼：{cultureCode}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                var rows = context.Language.AsNoTracking().ToList();
                // 判斷是否有 Null 翻譯
                var nullKeys = rows
                    .Where(l => !string.IsNullOrWhiteSpace(l.Key))
                    .Where(l => string.IsNullOrWhiteSpace(langProp.GetValue(l)?.ToString()))
                    .Select(l => l.Key)
                    .ToList();

                if (nullKeys.Any())
                {
                    MessageBox.Show(
                        $"⚠️ 發現 {nullKeys.Count} 筆語系資料（{cultureCode}）尚未翻譯。\n例如：{string.Join(", ", nullKeys.Take(5))}...",
                        "語系資料不完整",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    var result = LanguageImportHelper.ImportLanguage();
                    if (result != null)
                    {
                        // 5. 顯示結果
                        MessageBox.Show(
                            $"語系資料匯入完成：\n" +
                            $"新增 {result.InsertCount} 筆\n" +
                            $"更新 {result.UpdateCount} 筆\n" +
                            $"刪除 {result.DeleteCount} 筆 匯入成功");
                        string lang = Properties.Settings.Default.LanguageSetting;
                        LanguageManager.LoadLanguageFromDatabase(lang);
                        LanguageManager.SetLanguage(lang); // ✅ 自動載入 + 儲存 + 觸發事件
                        LanguageManager.SyncAvailableLanguages(LanguageManager.Currentlanguge);
                    }

                }

                // 建立翻譯對應表
                _currentLanguageMap = rows
                    .Where(l => !string.IsNullOrWhiteSpace(l.Key))
                    .ToDictionary(
                        l => l.Key,
                        l =>
                        {
                            var val = langProp.GetValue(l)?.ToString();
                            return !string.IsNullOrWhiteSpace(val) ? val : l.Key;
                        });

                LanguageChanged?.Invoke(cultureCode);
            
                //// 從資料庫載入語言資料
                //var languageData = context.Language
                //    .AsNoTracking()
                //    .ToList()
                //    .Where(l => !string.IsNullOrWhiteSpace(l.Key)) // 避免空Key
                //    .ToDictionary(
                //        l => l.Key,
                //        l =>
                //        {
                //            var prop = typeof(Language).GetProperty(cultureCode);
                //            return prop?.GetValue(l)?.ToString() ?? l.Key;
                //        });

            //_currentLanguageMap = languageData;

            //LanguageChanged?.Invoke(cultureCode);

            }
            catch (Exception ex)
            {
                return;
                MessageBox.Show($"❌ 載入語系失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string Currentlanguge => Properties.Settings.Default.LanguageSetting;
        /// <summary>
        /// 選取語系資料
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetAvailableLanguages(string displayCultureCode = "US")
        {
            using var context = new ApplicationDB();

            // 取出 LangName_xx 開頭的 Key
            var allLangRows = context.Language
                .AsNoTracking()
                .Where(l => l.Key.StartsWith("LangName_"))
                .ToList();

            // 建立語言代碼與顯示名稱（欄位）對應
            var langMap = new Dictionary<string, string>();
            foreach (var row in allLangRows)
            {
                string langCode = row.Key.Replace("LangName_", "");
                // 用反射取得指定欄位（預設 TW）作為顯示名稱
                var prop = typeof(Language).GetProperty(displayCultureCode);
                string displayName = prop?.GetValue(row)?.ToString() ?? langCode;
                langMap[langCode] = displayName;
            }

            return langMap;
        }
        /// <summary>
        /// 自動從資料庫載入支援的語系欄位與名稱，更新 LanguageMap
        /// </summary>
        public static void SyncAvailableLanguages(string displayCultureCode = "TW")
        {
            try
            {
                using var context = new ApplicationDB();

                // 1️⃣ 取得語系資料表的所有語系欄位（排除 Id, Key）
                var languageColumns = typeof(Language).GetProperties()
                    .Where(p => p.Name != "Id" && p.Name != "Key" && p.PropertyType == typeof(string))
                    .Select(p => p.Name)
                    .ToList();

                // 2️⃣ 查詢所有 LangName_ 開頭的 Key 作為顯示名稱參考
                var langNameRows = context.Language
                    .AsNoTracking()
                    .Where(l => l.Key.StartsWith("LangName_"))
                    .ToDictionary(l => l.Key, l => l);

                var newMap = new Dictionary<string, string>();

                foreach (var code in languageColumns)
                {
                    // e.g., "LangName_JP"
                    var key = $"LangName_{code}";
                    if (langNameRows.TryGetValue(key, out var row))
                    {
                        var prop = typeof(Language).GetProperty(displayCultureCode);
                        string displayName = prop?.GetValue(row)?.ToString() ?? code;
                        newMap[code] = displayName;
                    }
                    else
                    {
                        // fallback: 顯示語系代碼
                        newMap[code] = code;
                    }
                }

                LanguageMap = newMap;
            }
            catch (Exception ex)
            {
                return;
                MessageBox.Show($"❌ 語系同步失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
