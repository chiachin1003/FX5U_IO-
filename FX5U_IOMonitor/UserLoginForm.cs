
using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Resources;
using FX5U_IO元件監控;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Windows.Forms;
using static FX5U_IOMonitor.Models.Test_;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;




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
            using (var userService = LocalDbProvider.GetUserService())
            {
                errorCode = await userService.LoginAsync(_txtAccount.Text, _txtPassword.Text);
            }

            switch (errorCode)
            {
                case UserErrorCode.None:
                    using (var userService = LocalDbProvider.GetUserService())
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
            //Main_form_test add_sawband_Form = new Main_form_test();
            //Machine_monitoring_interface_card add_saw_Form = new Machine_monitoring_interface_card(ScheduleFrequency.Weekly);
            Saw_Info add_saw_Form = new Saw_Info();

            add_saw_Form.Show();
            Sawband_Info saw_Form = new Sawband_Info();

            saw_Form.Show();
        }
       
    }
}
