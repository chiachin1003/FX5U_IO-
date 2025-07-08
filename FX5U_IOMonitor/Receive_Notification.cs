
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Login;
using Microsoft.AspNetCore.Identity;
using FX5U_IOMonitor;




namespace FX5U_IOMonitor
{
    public partial class Receive_Notification : Form
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUser _currentUser;

        public Receive_Notification(UserManager<ApplicationUser> userManager, ApplicationUser currentUser)
        {
            InitializeComponent();
            _userManager = userManager;
            _currentUser = currentUser;
            UpdateLanguage();
        }

        private void UpdateLanguage()
        {
            this.Text = LanguageManager.Translate("Receive_Notification");
            lab_CurrentUser.Text = LanguageManager.Translate("Receive_Notification_User") +"："+_currentUser.UserName;
        }


        private async void Receive_Notification_Load(object sender, EventArgs e)
        {
            var latestUser = await _userManager.FindByIdAsync(_currentUser.Id);
            var result = await _userManager.UpdateAsync(latestUser);
            if (result.Succeeded)
            {
                if (latestUser.LineNotifyToken == "")
                {
                    latestUser.NotifyByLine = false;
                    _currentUser.NotifyByLine = false;

                    check_Line.Checked = false;
                    check_Line.Enabled = false;
                }
                if (latestUser.Email == "")
                {
                    latestUser.NotifyByEmail = false;
                    _currentUser.NotifyByEmail = false;

                    check_Email.Checked = false;
                    check_Email.Enabled = false;
                }

                check_Email.Checked = latestUser.NotifyByEmail;
                check_Line.Checked = latestUser.NotifyByLine;
            }

        }
        private async void btn_Confirm_Click(object sender, EventArgs e)
        {
            var dbUser = await _userManager.FindByIdAsync(_currentUser.Id);
            if (dbUser.LineNotifyToken == "")
            {
                dbUser.NotifyByLine = false;
                check_Line.Enabled = false;
            }

            dbUser.NotifyByEmail = check_Email.Checked;
            dbUser.NotifyByLine = check_Line.Checked;

            var result = await _userManager.UpdateAsync(dbUser);
            if (result.Succeeded)
            {
                _currentUser.NotifyByEmail = dbUser.NotifyByEmail;
                _currentUser.NotifyByLine = dbUser.NotifyByLine;
                MessageBox.Show(LanguageManager.Translate("Receive_Notification_success"));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(LanguageManager.Translate("Receive_Notification_error") + string.Join("\n", result.Errors.Select(e => e.Description)));
            }
        }

    }
}
