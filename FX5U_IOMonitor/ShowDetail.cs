using System.Data;
using System.Text;
using static FX5U_IOMonitor.Main;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using static FX5U_IOMonitor.Models.UpdateData;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Xml.Linq;
using static FX5U_IOMonitor.Models.panel_design_Setting;


namespace FX5U_IOMonitor
{
    public enum ShowDetailPage
    {
        Default,
        MainInfo,
        LifeSetting,
        History
    }
    public partial class ShowDetail : Form
    {
        private string equipmentTag;


        public ShowDetail(string Tag, ShowDetailPage defaultPage = ShowDetailPage.Default)
        {
            InitializeComponent();
            equipmentTag = Tag;
            
            switch (defaultPage)
            {
                case ShowDetailPage.MainInfo:
                    btn_showmain_Click(null, EventArgs.Empty);
                    break;
                case ShowDetailPage.LifeSetting:
                    btn_lifeSetting_Click(null, EventArgs.Empty);
                    break;
                case ShowDetailPage.History:
                    btn_history_Click(null, EventArgs.Empty);
                    break;
                default:
                  
                        string dbtable = DBfunction.FindTableWithAddress(equipmentTag);
                        if (dbtable == "") return;

                        string numberReplace = DBfunction.GetHistoryBySourceAndAddress(dbtable, equipmentTag).Count.ToString();
                        string StartTime = DBfunction.GetMountTimeByAddress(dbtable, equipmentTag) + "；第" + numberReplace + "次";

                        var usagePanel = panel_design.CreateShowMainPanel(
                            equipmentTag,
                            DBfunction.Get_Decription_ByAddress(dbtable, equipmentTag),
                            DBfunction.Get_MaxLife_ByAddress(dbtable, equipmentTag),
                            DBfunction.Get_use_ByAddress(dbtable, equipmentTag),
                            DBfunction.Get_Comment_ByAddress(dbtable, equipmentTag),
                            StartTime
                        );

                        panel_main.Controls.Add(usagePanel);

                    
                    break;
            }

        }


        private void btn_history_Click(object sender, EventArgs e)
        {
            panel_main.Controls.Clear();
            string dbtable = DBfunction.FindTableWithAddress(equipmentTag);
            if (dbtable == "") return;

            List<History> history = DBfunction.GetHistoryBySourceAndAddress(dbtable, equipmentTag);

            var usagePanel = panel_design.CreateUsagePanel(
                equipmentTag,
                DBfunction.Get_Decription_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_MaxLife_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_use_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_Comment_ByAddress(dbtable, equipmentTag), history

            );
            panel_main.Controls.Add(usagePanel);


        }

        private void btn_timereset_Click(object sender, EventArgs e)
        {

            //要新增確認更換元件確認鈕

            string dbtable = DBfunction.FindTableWithAddress(equipmentTag);
            if (dbtable == "") return;

            // 當前使用次數
            int equipment_use = DBfunction.Get_use_ByAddress(dbtable, equipmentTag);

            // 移除元件時間更新
            DBfunction.SetCurrentTimeAsUnmountTime(dbtable, equipmentTag);
            // 寫入元件歷史資料
            DBfunction.SetMachineIOToHistory(dbtable, equipmentTag, equipment_use);
            // 更新元件啟用時間資料
            DBfunction.SetCurrentTimeAsMountTime(dbtable, equipmentTag);
            // 顯示當前設備啟用時間
            string Part_InstallationTime = (DBfunction.GetMountTimeByAddress(dbtable, equipmentTag)).ToString();

            string numberReplace = DBfunction.GetHistoryBySourceAndAddress(dbtable, equipmentTag).Count.ToString();

            // 更新元件使用次數 
            DBfunction.Set_use_ByAddress(dbtable, equipmentTag,0);


            //更新頁面
            panel_main.Controls.Clear();

            string StartTime = DBfunction.GetMountTimeByAddress(dbtable, equipmentTag) + "；第" + numberReplace + "次";
            var usagePanel = panel_design.CreateShowMainPanel(
                equipmentTag,
                DBfunction.Get_Decription_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_MaxLife_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_use_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_Comment_ByAddress(dbtable, equipmentTag), StartTime
                );


            panel_main.Controls.Add(usagePanel);

          


        }

        private void btn_lifeSetting_Click(object sender, EventArgs e)
        {
            panel_main.Controls.Clear();
            string dbtable = DBfunction.FindTableWithAddress(equipmentTag);
            if (dbtable == "") return;

            Panel Settings = panel_design_Setting.CreateSettingPanel(equipmentTag);
            panel_main.Controls.Add(Settings);
        }

        private void btn_showmain_Click(object sender, EventArgs e)
        {
            panel_main.Controls.Clear();

            string dbtable = DBfunction.FindTableWithAddress(equipmentTag);
            if (dbtable == "") return;

            string numberReplace = DBfunction.GetHistoryBySourceAndAddress(dbtable, equipmentTag).Count.ToString();
            string StartTime = DBfunction.GetMountTimeByAddress(dbtable, equipmentTag) + "；第" + numberReplace + "次";

            var usagePanel = panel_design.CreateShowMainPanel(
                equipmentTag,
                DBfunction.Get_Decription_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_MaxLife_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_use_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_Comment_ByAddress(dbtable, equipmentTag),
                StartTime
            );

            panel_main.Controls.Add(usagePanel);

        }






    }
}
