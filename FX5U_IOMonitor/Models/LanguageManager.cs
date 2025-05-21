using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            { "zh-TW", "繁體中文" },
            { "en-US", "English" }
        };

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

        public static string Translate(string key)
        {
            return _currentLanguageMap.TryGetValue(key, out var value) ? value : key;
        }
    }
}
