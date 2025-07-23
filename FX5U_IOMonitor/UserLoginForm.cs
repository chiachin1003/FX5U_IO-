
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static FX5U_IOMonitor.Models.Test_;




namespace FX5U_IOMonitor
{
    public partial class UserLoginForm : Form
    {
        public ApplicationUser CurrentUser { get; private set; }

        public UserLoginForm()
        {
            InitializeComponent();
            UpdateLanguage();
        }

        private void UpdateLanguage()
        {
            this.Text = LanguageManager.Translate("User_Login_Form_title");
            _lblAccount.Text = LanguageManager.Translate("User_Login_Form_Account");

            _lblPassword.Text = LanguageManager.Translate("UserManageForm_Lbl_Password");

            _btnLogin.Text = LanguageManager.Translate("User_Login_Btn_Login");

        }

        private async void _btnLogin_Click(object sender, EventArgs e)
        {
            // Check if the user has entered a valid account and password
            if (string.IsNullOrEmpty(_txtAccount.Text) || string.IsNullOrEmpty(_txtPassword.Text))
            {
                MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_InvalidInput"),
                    LanguageManager.Translate("UserManageForm_Msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserErrorCode errorCode;
            using (var userService = new UserService<ApplicationDB>())
            {
                errorCode = await userService.LoginAsync(_txtAccount.Text, _txtPassword.Text);
            }

            switch (errorCode)
            {
                case UserErrorCode.None:
                    using (var userService = new UserService<ApplicationDB>())
                    {
                        CurrentUser = await userService.GetUserByNameAsync(_txtAccount.Text);
                    }
                    DialogResult = DialogResult.OK;
                    break;

                case UserErrorCode.NotExist:
                    MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_AccountDoesNotExist"),
                         LanguageManager.Translate("UserManageForm_Msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case UserErrorCode.PasswordError:
                    MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_PasswordError"),
                         LanguageManager.Translate("UserManageForm_Msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //var localData = await local.Set<Alarm>().AsNoTracking().ToListAsync();同步資料對其

            //foreach (var item in localData)
            //{
            //    var prop = item.GetType().GetProperty("IsSynced");
            //    if (prop != null)
            //    {
            //        prop.SetValue(item, false); //預設為 false
            //    }
            //}
            try
            {
                using var local = new ApplicationDB();

                var cloud = CloudDbProvider.GetContext();

                //1.Machine_number
                var Machine = await TableSyncHelper.SyncFromLocalToCloud<Machine_number>(local, cloud, "Machine");
                MessageBox.Show(Machine.ToString(), "✅ 同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////2.History
                //var Histories = await TableSyncHelper.SyncFromLocalToCloud<History>(local, cloud, "Histories");
                //MessageBox.Show(Histories.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //////3.MachineParameter
                //var MachineParameters = await TableSyncHelper.SyncFromLocalToCloud<MachineParameter>(local, cloud, "MachineParameters");
                //MessageBox.Show(MachineParameters.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //////4.
                //var Blade_brand = await TableSyncHelper.SyncFromLocalToCloud<Blade_brand>(local, cloud, "Blade_brand");
                //MessageBox.Show(Blade_brand.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //////5.
                //var Blade_brand_TPI = await TableSyncHelper.SyncFromLocalToCloud<Blade_brand_TPI>(local, cloud, "Blade_brand_TPI");
                //MessageBox.Show(Blade_brand_TPI.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //////6.
                //var Language = await TableSyncHelper.SyncFromLocalToCloud<Language>(local, cloud, "Language");
                //MessageBox.Show(Language.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //7.
                //var alarm = await TableSyncHelper.SyncFromLocalToCloud<Alarm>(local, cloud, "alarm");
                //MessageBox.Show(alarm.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //8.
                //var AlarmHistories = await TableSyncHelper.SyncFromLocalToCloud<AlarmHistory>(local, cloud, "AlarmHistories");
                //MessageBox.Show(AlarmHistories.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////9.
                //var Machine_IO = await TableSyncHelper.SyncFromLocalToCloud<MachineIO>(local, cloud, "Machine_IO");
                //MessageBox.Show(Machine_IO.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ////10.
                //var MachineParameterHistoryRecode = await TableSyncHelper.SyncFromLocalToCloud<MachineParameterHistoryRecode>(local, cloud, "MachineParameterHistoryRecodes");
                //MessageBox.Show(MachineParameterHistoryRecode.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ////11.
                //var MachineIOTranslations = await TableSyncHelper.SyncFromLocalToCloud<MachineIOTranslation>(local, cloud, "MachineIOTranslation");
                //MessageBox.Show(MachineIOTranslations.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ////12.
                //var AlarmTranslation = await TableSyncHelper.SyncFromLocalToCloud<AlarmTranslation>(local, cloud, "AlarmTranslation");
                //MessageBox.Show(AlarmTranslation.ToString(), "同步結果", MessageBoxButtons.OK, MessageBoxIcon.Information);



            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ 同步失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
