
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Login;




namespace FX5U_IOMonitor
{
    public partial class Unit_Setting : Form
    {
        public ApplicationUser CurrentUser { get; private set; }

        public Unit_Setting()
        {
            InitializeComponent();
            UpdateLanguage();
            InitUnitComboBox();

        }

        private void UpdateLanguage()
        {
            InitUnitComboBox();
            this.Text = LanguageManager.Translate("Unit_Setting_Setting");

        }

       
        private void btn_unit_Click(object sender, EventArgs e)
        {
            if (comb_unit.SelectedValue is string unitValue)
            {
                UnitManager.SetUnit(unitValue); // ✅ 使用事件通知所有訂閱者
            }
        }
        private void InitUnitComboBox()
        {

            var unitOptions = new List<KeyValuePair<string, string>>
            {
                new("Metric", LanguageManager.Translate("Unit_Setting_Metric")),  // 公制
                new("Imperial", LanguageManager.Translate("Unit_Setting_Imperial"))  // 英制
            };

            comb_unit.DisplayMember = "Value"; // 顯示用
            comb_unit.ValueMember = "Key";     // 儲存值用
            comb_unit.DataSource = unitOptions;

            // 顯示標題（如按鈕標籤）
            btn_unit.Text = LanguageManager.Translate("Unit_Setting_unitChange");

            // 設定選取值（從設定檔）
            string unit = Properties.Settings.Default.UnitSetting;
            comb_unit.SelectedValue = unit;
        }

    }
}
