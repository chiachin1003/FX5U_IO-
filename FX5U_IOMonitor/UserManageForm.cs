using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FX5U_IOMonitor
{
    public partial class UserManageForm : Form
    {
        public UserManageForm()
        {
            InitializeComponent();
            InitRolesComboBox();
            this.Shown += UserManageForm_Shown;
            UpdateLanguage();
        }
        public class UserDisplay
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Token { get; set; }
        }
        private void UpdateLanguage()
        {
            this.Text = LanguageManager.Translate("UserManageForm_Title");
            _lblAccount.Text = LanguageManager.Translate("UserManageForm_Lbl_Name");
            _lblPassword.Text = LanguageManager.Translate("UserManageForm_Lbl_Password");
            _lblConfirmPassword.Text = LanguageManager.Translate("UserManageForm_Lbl_ConfirmPassword");
            _lblRole.Text = LanguageManager.Translate("UserManageForm_Lbl_Role");
            _lblSelectedRole.Text = LanguageManager.Translate("UserManageForm_Lbl_Role");
            _btnAdd.Text = LanguageManager.Translate("UserManageForm_Btn_Add");
            _btnDelete.Text = LanguageManager.Translate("UserManageForm_Btn_Delete");
            lab_emailsetting.Text = LanguageManager.Translate("UserManageForm_Lab_mail");
            lab_hint.Text = LanguageManager.Translate("UserManageForm_lab_hint");
        }

        private async void UserManageForm_Shown(object? sender, EventArgs e)
        {
            await UpdateDGV();
            Notification_Settings.LoadUserImageTo(pictureBox_Official_Account);
        }

        private async Task UpdateDGV()
        {
            string curSelectedRole = _cbSelectedRole.Text;
            using var userService = LocalDbProvider.GetUserService();
            var userNameList = await userService.GetAllAsync(curSelectedRole);

            var filteredUserList = userNameList.Select(u => new UserDisplay
            {
                UserName = u.UserName,
                Email = u.Email,
                Token = u.LineNotifyToken

            }).ToList();

            _dgvUsers.DataSource = filteredUserList.ToList();
            _dgvUsers.Columns["UserName"].HeaderText = LanguageManager.Translate("UserManageForm_Lbl_Name");
            _dgvUsers.Columns["UserName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _dgvUsers.Columns["Scheduling"].HeaderText = LanguageManager.Translate("UserManageForm_Lab_mail");
            _dgvUsers.Columns["Scheduling"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _dgvUsers.Columns["Token"].HeaderText = "Line Notify";
            _dgvUsers.Columns["Token"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            

            // _dgvUsers.DataSource = new BindingList<ApplicationUser>(userNameList);
        }

        void InitRolesComboBox()
        {
            _cbRole.Items.AddRange(new object[] { SD.Role_User, SD.Role_Operator, SD.Role_Admin });
            _cbRole.SelectedIndex = 0;
            _cbSelectedRole.Items.AddRange(new object[] { SD.Role_User, SD.Role_Operator, SD.Role_Admin });
            _cbSelectedRole.SelectedIndex = 0;
        }

        private async void _cbSelectedRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            await UpdateDGV();
        }

        private async void _btnAdd_Click(object sender, EventArgs e)
        {
            // Check if the user has entered a valid account and password
            if (string.IsNullOrEmpty(_txtAccount.Text) || string.IsNullOrEmpty(_txtPassword.Text))
            {
                MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_InvalidInput"),
                    LanguageManager.Translate("UserManageForm_Msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the user already exists
            using (var userService = LocalDbProvider.GetUserService())
            {
                if (userService.CheckUserExist(_txtAccount.Text))
                {
                    MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_UserNameAlreadyExist"),
                        LanguageManager.Translate("UserManageForm_Msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check password == confirm password
                if (_txtPassword.Text != _txtConfirmPassword.Text)
                {
                    MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_PasswordNotMatch"),
                        LanguageManager.Translate("UserManageForm_Msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await userService.CreateUserAsync(_txtAccount.Text, _txtPassword.Text, _cbRole.Text, _txtEmail.Text, _txt_Line.Text);
            }

            await UpdateDGV();
        }

        private async void _btnDelete_Click(object sender, EventArgs e)
        {
            // Get the selected dgv row
            if (_dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_SelectRowFirst"));
                return;
            }

            var selectedUser = _dgvUsers.SelectedRows[0].DataBoundItem as UserDisplay;
            if (selectedUser == null)
            {
                MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_DataNotFound"));
                return;
            }

            // Check if the user is admin
            if (selectedUser.UserName == SD.Admin_Account)
            {
                MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_CanNotDeleteUser"));
                return;
            }

            // Delete user from database
            using (var userService = LocalDbProvider.GetUserService())
            {
                await userService.DeleteUserAsync(selectedUser.UserName);
            }

            await UpdateDGV();
        }

        private async void _dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // 標題列無效
            using var userService = LocalDbProvider.GetUserService();

            var selectedUser = _dgvUsers.Rows[e.RowIndex].DataBoundItem as UserDisplay;
            var fullUser = await userService.UserManager.FindByNameAsync(selectedUser.UserName);
            if (fullUser == null)
            {
                MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_DataNotFound"));
                return;
            }

            using (var form = new Receive_Notification(userService.UserManager, fullUser))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }

        }
    }
}
