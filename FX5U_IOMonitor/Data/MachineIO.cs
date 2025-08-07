using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX5U_IOMonitor.Models;

namespace FX5U_IOMonitor.Data
{

    public class MachineIO : SyncableEntity
    {
        [Key]
        public int Id { get; set; }

        public required string address { get; set; }
        [Required]
        public string Machine_name { get; set; }
        public string baseType  { get; set; } // 實體元件的地址值型態

        public bool IOType { get; set; }
        public RelayType RelayType { get; set; }
        public string ClassTag { get; set; } //分組類別

        public string Description { get; set; } //設備更換料號
        public string Comment { get; set; } // 當前資料

        // 私有欄位 + 自動同步邏輯
        private int _equipment_use;
        private int _maxLife;

        public int equipment_use
        {
            get => _equipment_use;
            set
            {
                _equipment_use = value;
                UpdateRUL();
            }
        }

        public int MaxLife
        {
            get => _maxLife;
            set
            {
                _maxLife = value;
                UpdateRUL();
            }
        }

        private void UpdateRUL()
        {
            RUL = (double)RemainingLifeTime;
        }
        public double RUL { get; set; } // 剩餘壽命


        public int Setting_green { get; set; } //使用者設定健康健康狀態百分比
        public int Setting_yellow { get; set; }//使用者設定健康黃燈百分比
        public int Setting_red { get; set; } //使用者設定健康異警百分比
        public double percent { get; set; }
        public bool Breakdown { get; set; }
        public ICollection<MachineIOTranslation> Translations { get; set; } = new List<MachineIOTranslation>();
        public ICollection<History> Histories { get; set; } = new List<History>();        //連動歷史資料
        [NotMapped]
        public float RemainingLifeTime => (float)Math.Round(Math.Max(0, (1 - (float)equipment_use / MaxLife) * 100), 2);
        public bool? current_single { get; set; } //當前讀取數值or信號

        public DateTime MountTime { get; set; }
        public DateTime UnmountTime { get; set; }

        public string GetComment(string languageCode = "US")
        {
            return Translations?.FirstOrDefault(t => t.LanguageCode == languageCode)?.Comment ?? "";
        }

        public void SetComment(string languageCode, string comment)
        {
            var translation = Translations.FirstOrDefault(t => t.LanguageCode == languageCode);
            if (translation != null)
            {
                translation.Comment = comment;
            }
            else
            {
                Translations.Add(new MachineIOTranslation
                {
                    LanguageCode = languageCode,
                    Comment = comment,
                });
            }
        }

    }

    public class Machine_number : SyncableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? IP_address { get; set; }

        public int Port { get; set; }

        public string MC_Type { get; set; }

        [NotMapped] 
        public McFrame mcFrame
        {
            get => Enum.TryParse<McFrame>(MC_Type, out var val) ? val : McFrame.MC1E;
            set => MC_Type = value.ToString();
        }
    }

    public enum RelayType
    {
        Electronic = 0,  // False
        Machanical = 1  // True
    }
    public enum McFrame
    {
        MC1E,
        MC3E,
    }
}
