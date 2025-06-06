using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Net.Mail;
using FX5U_IOMonitor.Properties;
using static FX5U_IOMonitor.Models.UI_Display;

namespace FX5U_IOMonitor.Resources
{
    public partial class File_Settings : Form
    {


        public File_Settings()
        {
            InitializeComponent();
            var TableList = new List<DisplayValuePair<string>>
            {
                new DisplayValuePair<string>("警告資料表", "alarm"),
                new DisplayValuePair<string>("鋸帶材質資料表", "Blade_brand"),
                new DisplayValuePair<string>("鋸帶尺數資料表", "Blade_brand_TPI"),
                new DisplayValuePair<string>("語系資料表", "Language")

            };

            var SaveList = new List<DisplayValuePair<string>>
            {
                new DisplayValuePair<string>("自動儲存至下載", "auto"),
                new DisplayValuePair<string>("使用者自選", "manual"),
            };

            foreach (var item in TableList)
            {
                comb_datatable.Items.Add(item);  // item.ToString() 會顯示 Display 值
            }
            comb_datatable.SelectedIndex = 0;
            Text_design.SetComboBoxCenteredDraw(comb_datatable);

            foreach (var item in SaveList)
            {
                comb_select.Items.Add(item);  // item.ToString() 會顯示 Display 值
            }
            comb_select.SelectedIndex = 0;
            Text_design.SetComboBoxCenteredDraw(comb_select);
        }



        private void btn_setting_Click(object sender, EventArgs e)
        {
            if (comb_datatable.SelectedItem is DisplayValuePair<string> tableItem &&
                comb_select.SelectedItem is DisplayValuePair<string> saveModeItem)
            {
                string tableName = tableItem.Value;     // 內部資料表名稱
                string saveMode = saveModeItem.Value;   // "auto" or "manual"

                if (tableName == "Language")
                {
                    LanguageImportHelper.ExportLanguageTemplate(saveMode);
                }
                else 
                {
                    TableImportExportManager.ExportTableToCsv(tableName, saveMode);  // 呼叫對應函數
                }


            }
            else
            {
                MessageBox.Show("❌ 請確認已選擇資料表與儲存模式！");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (comb_datatable.SelectedItem is DisplayValuePair<string> tableItem)
            {
                string tableName = tableItem.Value;     // 內部資料表名稱

                if (tableName == "Language")
                {
                    var result = LanguageImportHelper.ImportLanguage();
                    if (result != null)
                    {
                        Console.WriteLine($"匯入完成：新增 {result.InsertCount}，更新 {result.UpdateCount}，刪除 {result.DeleteCount}");
                        string lang = Properties.Settings.Default.LanguageSetting;
                        LanguageManager.LoadLanguageFromDatabase(lang);
                    }
                }
                else 
                {
                    Csv2Db.UpdateTable(tableName);
                }
              

            }
            else
            {
                MessageBox.Show("❌ 請確認已選擇資料表與儲存模式！");
            }
        }



        }

        


        
    
}
