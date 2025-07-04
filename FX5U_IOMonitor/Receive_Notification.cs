
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

        }


        private async void Receive_Notification_Load(object sender, EventArgs e)
        {
            var latestUser = await _userManager.FindByIdAsync(_currentUser.Id);
         
            check_Email.Checked = _currentUser.NotifyByEmail;
            check_Line.Checked = _currentUser.NotifyByLine;
        }
        private async void btn_Confirm_Click(object sender, EventArgs e)
        {
            var dbUser = await _userManager.FindByIdAsync(_currentUser.Id);

            dbUser.NotifyByEmail = check_Email.Checked;
            dbUser.NotifyByLine = check_Line.Checked;

            var result = await _userManager.UpdateAsync(dbUser);
            if (result.Succeeded)
            {
                _currentUser.NotifyByEmail = dbUser.NotifyByEmail;
                _currentUser.NotifyByLine = dbUser.NotifyByLine;
                MessageBox.Show("通知設定已更新");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("更新失敗：" + string.Join("\n", result.Errors.Select(e => e.Description)));
            }
        }

    }
}
