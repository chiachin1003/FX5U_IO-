
using System.Text.Json;

namespace FX5U_IOMonitor.Utilization
{
    public static class UtilizationConfigLoader
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        public static List<UtilizationConfig> LoadMachines(string path = "Utilization_machine.json")
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"找不到機台設定檔: {path}");

            var json = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<List<UtilizationConfig>>(json, _jsonOptions)
                       ?? new List<UtilizationConfig>();

            // 基本驗證
            foreach (var m in data)
            {
                if (string.IsNullOrWhiteSpace(m.Machine))
                    throw new InvalidDataException("machines.json 內有 Machine 為空值");
                if (string.IsNullOrWhiteSpace(m.ReadBitAddress))
                    throw new InvalidDataException($"機台 {m.Machine} 的 ReadBitAddress 為空值");
            }

            // Optional：檢查機台名稱重複
            var dup = data.GroupBy(x => x.Machine).FirstOrDefault(g => g.Count() > 1);
            if (dup != null)
                throw new InvalidDataException($"machines.json 機台名稱重複：{dup.Key}");

            return data;
        }

        public static ShiftsFile LoadShifts(string path = "UtilizationShiftConfig.json")
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"找不到班別設定檔: {path}");

            var json = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<ShiftsFile>(json, _jsonOptions)
                       ?? new ShiftsFile();

            if (data.Shifts == null || data.Shifts.Count == 0)
                throw new InvalidDataException("shifts.json 沒有任何 Shifts 設定");

            foreach (var s in data.Shifts)
            {
                if (s.ShiftNo < 1 || s.ShiftNo > 4)
                    throw new InvalidDataException($"Shifts.ShiftNo 僅支援 1/2/3/4，取得 {s.ShiftNo}");
                if (string.IsNullOrWhiteSpace(s.Start) || string.IsNullOrWhiteSpace(s.End))
                    throw new InvalidDataException($"ShiftNo={s.ShiftNo} 的 Start/End 不可為空");
            }

            return data;
        }
    }
}
