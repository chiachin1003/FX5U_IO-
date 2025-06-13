using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Newtonsoft.Json;
using FX5U_IOMonitor.Models;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using FX5U_IOMonitor.Login;
using System.ComponentModel.DataAnnotations.Schema;

// 語系實體類別
public class Language : SyncableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }
    public string Key { get; set; }  // 介面名稱
    public string? TW { get; set; }
    public string? US { get; set; }
    // 未來可以新增：public string? JP { get; set; }
    // 未來可以新增：public string? KR { get; set; }
    // 等等...
}

// 動態 CSV 模型
public class LanguageCsvRecord : DynamicObject
{
    private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

    public int Id { get; set; }
    public string Key { get; set; }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        return _values.TryGetValue(binder.Name, out result);
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        _values[binder.Name] = value;
        return true;
    }

    public Dictionary<string, object> GetAllValues()
    {
        var result = new Dictionary<string, object>
        {
            ["Id"] = Id,
            ["Key"] = Key
        };

        foreach (var kvp in _values)
        {
            result[kvp.Key] = kvp.Value;
        }

        return result;
    }

    public void SetLanguageValue(string languageCode, string value)
    {
        _values[languageCode] = value;
    }

    public string GetLanguageValue(string languageCode)
    {
        return _values.TryGetValue(languageCode, out var value) ? value?.ToString() : null;
    }
}

// 語系匯入服務
public class LanguageImportService
{
    private readonly ApplicationDB _context;

    public LanguageImportService(ApplicationDB context)
    {
        _context = context;
    }
   
    /// <summary>
    /// 匯入語系 CSV 檔案
    /// </summary>
    /// <returns>匯入結果</returns>
    public ImportResult ImportLanguageCsv(string? filepath = null)
    {
        if (string.IsNullOrWhiteSpace(filepath))
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV 檔案 (*.csv)|*.csv",
                Title = "選擇要匯入的語系 CSV 檔案"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return null;

            filepath = openFileDialog.FileName;
        }
        try
        {
            // 1. 讀取 CSV 檔案並分析結構
            var csvData = ReadCsvWithDynamicColumns(filepath);
            if (!csvData.Any())
            {
                MessageBox.Show("❌ CSV 檔案沒有資料", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            // 2. 檢查並更新資料庫結構
            var languageColumns = GetLanguageColumnsFromCsv(csvData);
            CheckAndUpdateDatabaseSchema(languageColumns);

            // 3. 處理 ID 驗證
            var processedData = ProcessLanguageIds(csvData);
            if (processedData == null) return null;

            // 4. 執行匯入
            var result = ExecuteLanguageImport(processedData, languageColumns);


            return result;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"❌ 語系資料匯入失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    /// <summary>
    /// 讀取 CSV 檔案（支援動態欄位）
    /// </summary>
    private List<LanguageCsvRecord> ReadCsvWithDynamicColumns(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Encoding = System.Text.Encoding.UTF8,
            PrepareHeaderForMatch = args => args.Header.Trim(),
            MissingFieldFound = null,
            HeaderValidated = null
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config);

        // 讀取標題行
        csv.Read();
        csv.ReadHeader();
        var headers = csv.HeaderRecord;

        var records = new List<LanguageCsvRecord>();

        while (csv.Read())
        {
            var record = new LanguageCsvRecord();

            // 處理固定欄位
            if (headers.Contains("Id"))
            {
                var idField = csv.GetField("Id");
                if (int.TryParse(idField, out int parsedId))
                {
                    record.Id = parsedId;
                }
                else
                {
                    record.Id = 0;
                }
            }

            if (headers.Contains("Key"))
                record.Key = csv.GetField<string>("Key");

            // 處理動態語系欄位
            foreach (var header in headers.Where(h => h != "Id" && h != "Key"))
            {
                var value = csv.GetField<string>(header);
                record.SetLanguageValue(header, value);
            }

            records.Add(record);
        }

        return records;
    }

    /// <summary>
    /// 從 CSV 取得語系欄位清單
    /// </summary>
    private List<string> GetLanguageColumnsFromCsv(List<LanguageCsvRecord> csvData)
    {
        var languageColumns = new HashSet<string>();

        foreach (var record in csvData)
        {
            var values = record.GetAllValues();
            foreach (var key in values.Keys.Where(k => k != "Id" && k != "Key"))
            {
                languageColumns.Add(key);
            }
        }

        return languageColumns.ToList();
    }

    /// <summary>
    /// 檢查並更新資料庫結構（如果需要新增語系欄位）
    /// </summary>
    private void CheckAndUpdateDatabaseSchema(List<string> requiredColumns)
    {
        var existingColumns = GetExistingLanguageColumns();
        var newColumns = requiredColumns.Except(existingColumns).ToList();

        if (newColumns.Any())
        {
            var result = MessageBox.Show(
                $"⚠️ 偵測到新的語系欄位：{string.Join(", ", newColumns)}\n" +
                "系統將自動在資料庫中新增這些欄位，是否繼續？",
                "新增語系欄位",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                throw new OperationCanceledException("使用者取消新增語系欄位");

            AddLanguageColumnsToDatabase(newColumns);
        }
    }

    /// <summary>
    /// 取得現有的語系欄位
    /// </summary>
    private List<string> GetExistingLanguageColumns()
    {
        var languageType = typeof(Language);
        var properties = languageType.GetProperties()
            .Where(p => p.Name != "Id" && p.Name != "Key" && p.PropertyType == typeof(string))
            .Select(p => p.Name)
            .ToList();

        return properties;
    }

    /// <summary>
    /// 在資料庫中新增語系欄位
    /// </summary>
    private void AddLanguageColumnsToDatabase(List<string> newColumns)
    {
        // 這裡需要使用 Raw SQL 來新增欄位
        // 注意：實際環境中建議使用 Entity Framework Migrations
        foreach (var column in newColumns)
        {
            var sql = $"ALTER TABLE \"Language\" ADD COLUMN \"{column}\" TEXT";
            _context.Database.ExecuteSqlRaw(sql);
        }

        // 重新載入 DbContext 以反映結構變更
        _context.ChangeTracker.Clear();
    }

    /// <summary>
    /// 處理 ID 驗證
    /// </summary>
    private List<LanguageCsvRecord> ProcessLanguageIds(List<LanguageCsvRecord> records)
    {
        // 取得資料庫最大 ID
        int maxDbId = _context.Language.Any() ? _context.Language.Max(l => l.Id) : 0;
        int nextId = maxDbId + 1;

        // 檢查並補齊 ID
        var idSet = new HashSet<int>();
        foreach (var record in records)
        {
            if (record.Id <= 0)
            {
                record.Id = nextId++;
            }

            if (!idSet.Add(record.Id))
            {
                MessageBox.Show($"❌ 匯入失敗：CSV 中出現重複的 Id: {record.Id}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        return records;
    }

    /// <summary>
    /// 執行語系資料匯入
    /// </summary>
    private ImportResult ExecuteLanguageImport(List<LanguageCsvRecord> records, List<string> languageColumns, bool enableDelete = false)
    {
        var result = new ImportResult();

        var existingData = _context.Language.ToDictionary(l => l.Key);

        foreach (var record in records)
        {
            if (string.IsNullOrWhiteSpace(record.Key))
                continue;

            if (existingData.TryGetValue(record.Key, out var existing))
            {
                if (UpdateLanguageEntity(existing, record, languageColumns))
                    result.UpdateCount++;
            }
            else
            {
                var newEntity = CreateLanguageEntity(record, languageColumns);
                _context.Language.Add(newEntity);
                result.InsertCount++;
            }
        }

        //  條件式選擇是否刪除資料庫中不存在於 CSV 的 Key
        if (enableDelete)
        {
            var csvKeys = records.Select(r => r.Key).ToHashSet();
            var toDelete = existingData.Values.Where(e => !csvKeys.Contains(e.Key)).ToList();

            if (toDelete.Any())
            {
                _context.Language.RemoveRange(toDelete);
                result.DeleteCount = toDelete.Count;
            }
        }

        _context.SaveChanges();
        return result;
    }

    /// <summary>
    /// 更新語系實體
    /// </summary>
    private bool UpdateLanguageEntity(Language existing, LanguageCsvRecord csvRecord, List<string> languageColumns)
    {
        bool hasChanges = false;

        // 更新 Key
        if (existing.Key != csvRecord.Key)
        {
            existing.Key = csvRecord.Key;
            hasChanges = true;
        }

        // 更新語系欄位
        var languageType = typeof(Language);
        foreach (var column in languageColumns)
        {
            var property = languageType.GetProperty(column);
            if (property != null)
            {
                var newValue = csvRecord.GetLanguageValue(column);
                var existingValue = property.GetValue(existing)?.ToString();

                if (existingValue != newValue)
                {
                    property.SetValue(existing, newValue);
                    hasChanges = true;
                }
            }
        }

        return hasChanges;
    }

    /// <summary>
    /// 創建新的語系實體
    /// </summary>
    private Language CreateLanguageEntity(LanguageCsvRecord csvRecord, List<string> languageColumns)
    {
        var entity = new Language
        {
            Key = csvRecord.Key
        };

        // 設定語系欄位
        var languageType = typeof(Language);
        foreach (var column in languageColumns)
        {
            var property = languageType.GetProperty(column);
            if (property != null)
            {
                var value = csvRecord.GetLanguageValue(column);
                property.SetValue(entity, value);
            }
        }

        return entity;
    }
}

// 簡化使用介面
public static class LanguageImportHelper
{
    /// <summary>
    /// 匯入語系資料
    /// </summary>
    /// <returns>匯入結果</returns>
    public static ImportResult ImportLanguage(string? filepath = null)
    {
        using var context = new ApplicationDB();
        var importService = new LanguageImportService(context);
        return importService.ImportLanguageCsv(filepath);
    }

    /// <summary>
    /// 匯出語系資料為 CSV 範本
    /// </summary>
    /// <param name="includeData">是否包含現有資料</param>
    public static void ExportLanguageTemplate(string mode = "auto")
    {
        try
        {
            SaveFileDialog saveFileDialog;
            string filePath;
            if (mode.ToLower() == "manual")
            {
                // 手動選擇儲存位置
                saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV 檔案 (*.csv)|*.csv",
                    FileName = "language.csv",
                    Title = "語系儲存"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                filePath = saveFileDialog.FileName;
            }
            else
            {
                // 自動儲存到下載資料夾
                string downloadFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Downloads"
                );

                Directory.CreateDirectory(downloadFolder); // 保險起見建立資料夾
                filePath = Path.Combine(downloadFolder, $"language.csv");
            }

            using var context = new ApplicationDB();
            using var writer = new StreamWriter(filePath, false, new UTF8Encoding(true));

            // 取得所有語系欄位
            var languageColumns = typeof(Language).GetProperties()
                .Where(p => p.Name != "Id" && p.Name != "Key" && p.PropertyType == typeof(string))
                .Select(p => p.Name)
                .ToList();

            // 寫入標題
            writer.WriteLine($"Id,Key,{string.Join(",", languageColumns)}");

           
            // 寫入現有資料
            var languages = context.Language.ToList();
            foreach (var lang in languages)
            {
                var values = new List<string> { lang.Id.ToString(), lang.Key };

                foreach (var column in languageColumns)
                {
                    var property = typeof(Language).GetProperty(column);
                    var value = property?.GetValue(lang)?.ToString() ?? "";
                    values.Add($"\"{value}\"");
                }

                writer.WriteLine(string.Join(",", values));
            }
            

            MessageBox.Show($"✅ 語系範本匯出完成：\n📄 {filePath}", "匯出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"❌ 匯出失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}


