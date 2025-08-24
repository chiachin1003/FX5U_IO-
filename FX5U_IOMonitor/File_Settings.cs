using CsvHelper;
using CsvHelper.Configuration;
using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Properties;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using static FX5U_IOMonitor.Models.UI_Display;

namespace FX5U_IOMonitor.Resources
{
    public partial class File_Settings : Form
    {


        public File_Settings()
        {
            InitializeComponent();
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            LanguageManager.LanguageChanged -= OnLanguageChanged;
            LanguageManager.LanguageChanged += OnLanguageChanged;
            SwitchLanguage();

            //var TableList = new List<DisplayValuePair<string>>
            //{
            //    new DisplayValuePair<string>("警告資料表", "alarm"),
            //    new DisplayValuePair<string>("鋸帶材質資料表", "Blade_brand"),
            //    new DisplayValuePair<string>("鋸帶尺數資料表", "Blade_brand_TPI"),
            //    new DisplayValuePair<string>("語系資料表", "Language")

            //};
            //foreach (var item in TableList)
            //{
            //    comb_datatable.Items.Add(item);  // item.ToString() 會顯示 Display 值
            //}

            //var SaveList = new List<DisplayValuePair<string>>
            //{
            //    new DisplayValuePair<string>("自動儲存至下載", "auto"),
            //    new DisplayValuePair<string>("使用者自選", "manual"),
            //};
            //foreach (var item in SaveList)
            //{
            //    comb_select.Items.Add(item);  // item.ToString() 會顯示 Display 值
            //}




        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }


        private void btn_setting_Click(object sender, EventArgs e)
        {
            lab_cloudstatus.Text = "";
            string? tableName = ComboBoxHelper.GetSelectedValue<string>(comb_datatable);
            string? saveMode = ComboBoxHelper.GetSelectedValue<string>(comb_select);

            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(saveMode))
            {
                Message_Config.LogMessage(LanguageManager.Translate("File_Settings_Message_Check"));
                return;
            }

            try
            {
                if (tableName == "Language")
                {
                    LanguageImportHelper.ExportLanguageTemplate(saveMode);
                }
                else if (tableName == "alarm")
                {
                    TableImportExportManager.Export_AlarmToCSV(saveMode);
                }
                else
                {
                    TableImportExportManager.ExportTableToCsv(tableName, saveMode);
                }
            }
            catch (Exception ex)
            {
                Message_Config.LogMessage($"[匯出失敗] 資料表：{tableName}，模式：{saveMode}，錯誤：{ex.Message}");
            }          
        }

        private async void btn_update_Click(object sender, EventArgs e)
        {
            using var Cloud_context = new CloudDbContext();
            using var Local_context = new ApplicationDB();
            lab_cloudstatus.Text = "";
            string? tableName = ComboBoxHelper.GetSelectedValue<string>(comb_datatable);

            if (string.IsNullOrWhiteSpace(tableName))
            {
                Message_Config.LogMessage(LanguageManager.Translate("File_Settings_Message_Check"));
                return;
            }

            if (tableName == "Language")
            {
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
                    try
                    {
                        lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUpload");
                        lab_cloudstatus.ForeColor = Color.Gray;
                        if (Cloud_context == null)
                        {
                            lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUploadFailed");
                            lab_cloudstatus.ForeColor = Color.Red;
                        }
                        else
                        {
                            var Language = await TableSync.SyncFromLocalToCloud<Language>(Local_context, Cloud_context, "Language");
                            TableSync.LogSyncResult("", Language);
                            // ✅ 檢查是否真的有更新成功
                            if (Language.Added > 0 || Language.Updated > 0)
                            {
                                lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUploadSuccess");
                                lab_cloudstatus.ForeColor = Color.Green;
                            }
                            else
                            {
                                lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUploadFailed");
                                lab_cloudstatus.ForeColor = Color.Orange;
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUploadFailed");
                        lab_cloudstatus.ForeColor = Color.Red;

                    }
                }

            }
            else if (tableName == "MachineParameters")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "CSV 檔案 (*.csv)|*.csv",
                    Title = $"選擇要匯入的 {tableName} CSV 檔案"
                };

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                try
                {
                    Csv2Db.Initialization_MachineprameterFromCSV(openFileDialog.FileName);
                    lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUpload");
                    lab_cloudstatus.ForeColor = Color.Gray;
                    var prameter = await TableSync.SyncFromLocalToCloud<MachineParameter>(Local_context, Cloud_context, "MachineParameters");
                    lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUploadSuccess");
                    lab_cloudstatus.ForeColor = Color.Green;

                }
                catch (Exception ex)
                {
                    lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUploadFailed");
                    lab_cloudstatus.ForeColor = Color.Red;
                }
            }
            else
            {
                Csv2Db.UpdateTable(tableName);
                try
                {
                    lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUpload");
                    lab_cloudstatus.ForeColor = Color.Gray;
                    if (Cloud_context == null)
                    {
                        throw new Exception("Cloud context 為 null");
                    }
                    switch (tableName)
                    {
                        case "Blade_brand_TPI":
                            var Blade_brand_TPI = await TableSync.SyncFromLocalToCloud<Blade_brand_TPI>(Local_context, Cloud_context, "Blade_brand_TPI");
                            TableSync.LogSyncResult("", Blade_brand_TPI);

                            break;
                        case "Blade_brand":
                            var Blade_brand = await TableSync.SyncFromLocalToCloud<Blade_brand>(Local_context, Cloud_context, "Blade_brand");
                            TableSync.LogSyncResult("", Blade_brand);
                            break;

                        case "alarm":
                            var alarm = await TableSync.SyncFromLocalToCloud<Alarm>(Local_context, Cloud_context, "alarm", "IPC_table");
                            var AlarmTranslation = await TableSync.SyncFromLocalToCloud<AlarmTranslation>(Local_context, Cloud_context, "AlarmTranslation", "AlarmId", "Id");
                            TableSync.LogSyncResult("", alarm);
                            TableSync.LogSyncResult("", AlarmTranslation);
                            break;
                        default:
                            throw new NotSupportedException($"未支援的 tableName: {tableName}");

                    }
                    lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUploadSuccess");
                    lab_cloudstatus.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldUploadFailed");
                    lab_cloudstatus.ForeColor = Color.Red;
                }
            }

        }

        private async void btn_cloud_Click(object sender, EventArgs e)
        {
            using var Cloud_context = new CloudDbContext();
            using var Local_context = new ApplicationDB();
            lab_cloudstatus.Text = "";
            string? tableName = ComboBoxHelper.GetSelectedValue<string>(comb_datatable);

            if (string.IsNullOrWhiteSpace(tableName))
            {
                Message_Config.LogMessage(LanguageManager.Translate("File_Settings_Message_Check"));
                return;
            }
            try
            {
                lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldDownloading");
                lab_cloudstatus.ForeColor = Color.Gray;
                if (Cloud_context == null)
                {
                    lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldDownloadFailed");
                    lab_cloudstatus.ForeColor = Color.Red;
                }
                else
                {
                    if (Cloud_context == null)
                    {
                        throw new Exception("Cloud context 為 null");
                    }
                    switch (tableName)
                    {
                        case "Language":
                            var Language = await TableSync.SyncFromCloudToLocal<Language>(Local_context, Cloud_context, tableName);
                            TableSync.LogSyncResult("", Language);

                            break;

                        case "Blade_brand_TPI":
                            var Blade_brand_TPI = await TableSync.SyncFromCloudToLocal<Blade_brand_TPI>(Local_context, Cloud_context, "Blade_brand_TPI");
                            TableSync.LogSyncResult("", Blade_brand_TPI);

                            break;
                        case "Blade_brand":
                            var Blade_brand = await TableSync.SyncFromCloudToLocal<Blade_brand>(Local_context, Cloud_context, "Blade_brand");
                            TableSync.LogSyncResult("", Blade_brand);

                            break;


                        case "alarm":
                            var alarm = await TableSync.SyncFromCloudToLocal<Alarm>(Local_context, Cloud_context, "alarm", "IPC_table");
                            var AlarmTranslation = await TableSync.SyncFromCloudToLocal<AlarmTranslation>(Local_context, Cloud_context, "AlarmTranslation", "AlarmId", "Id");
                            TableSync.LogSyncResult("", alarm);
                            TableSync.LogSyncResult("", AlarmTranslation);
                            break;

                        default:
                            throw new NotSupportedException($"未支援的 tableName: {tableName}");

                    }
                    lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldDownloadSuccess");
                    lab_cloudstatus.ForeColor = Color.Green;

                }

            }
            catch (Exception ex)
            {
                lab_cloudstatus.Text = LanguageManager.Translate("File_Settings_Message_ClouldDownloadFailed");
                lab_cloudstatus.ForeColor = Color.Red;
            }


        }
        private void SwitchLanguage()
        {
            this.Text = LanguageManager.Translate("File_Settings_Title");
            btn_download.Text = LanguageManager.Translate("File_Settings_Button_download");
            btn_update.Text = LanguageManager.Translate("File_Settings_Button_update");
            btn_cloud.Text = LanguageManager.Translate("File_Settings_Button_cloud");
            lab_TableSelect.Text = LanguageManager.Translate("File_Settings_lab_TableSelect");
            lab_save_location.Text = LanguageManager.Translate("File_Settings_lab_save_location");

            Text_design.SafeAdjustFont(btn_download, btn_download.Text);
            Text_design.SafeAdjustFont(lab_TableSelect, lab_TableSelect.Text);
            Text_design.SafeAdjustFont(lab_save_location, lab_save_location.Text);

            var items = new List<(string, string)>
            {
                (LanguageManager.Translate("File_Settings_AlarmTable"), "alarm"),
                (LanguageManager.Translate("File_Settings_MaterialTable"), "Blade_brand"),
                (LanguageManager.Translate("File_Settings_TPI_Table"), "Blade_brand_TPI"),
                (LanguageManager.Translate("File_Settings_LanguageTable"), "Language")
            };

            if (UserService<ApplicationDB>.CurrentRole == SD.Role_Admin)
            {
                items.Add((LanguageManager.Translate("File_Settings_Machineprameter"), "MachineParameters"));
            }

            ComboBoxHelper.BindDisplayValueItems<string>(comb_datatable, items);

            comb_datatable.SelectedIndex = 0;
            Text_design.SetComboBoxCenteredDraw(comb_datatable);

            ComboBoxHelper.BindDisplayValueItems<string>(comb_select, new[]
            {
                (LanguageManager.Translate("File_Settings_AutoSave"), "auto"),
                (LanguageManager.Translate("File_Settings_UserSelection"), "manual"),
            });
            comb_select.SelectedIndex = 0;
            Text_design.SetComboBoxCenteredDraw(comb_select);
        }

        
    }

}
