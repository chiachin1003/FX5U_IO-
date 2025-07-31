
using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Resources;
using FX5U_IO元件監控;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
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
            
            this.Load += UserLoginForm_Load;

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
            Saw_Info add_saw_Form = new Saw_Info();

            add_saw_Form.Show();
        }
        private static System.Timers.Timer? _syncTimer;
        private static readonly SemaphoreSlim _syncLock = new(1, 1);
        private static ApplicationDB? _SysLocal;
        private static CloudDbContext? _SysCloud;
        private bool _isSwitching = false;


        private async void chk_toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_isSwitching)
            {
                chk_toggle.CheckedChanged -= chk_toggle_CheckedChanged;
                chk_toggle.Checked = !chk_toggle.Checked; // 反轉回來
                chk_toggle.CheckedChanged += chk_toggle_CheckedChanged;
                chk_toggle.Text = "Switch...";

                return;
            }
            _isSwitching = true;
            bool connected = false;
            chk_toggle.CheckedChanged -= chk_toggle_CheckedChanged;

            try
            {
                SetToggleState(connected: chk_toggle.Checked, enabled: false); 

                if (chk_toggle.Checked) // 開啟同步
                {
                    if (_SysCloud == null)
                    {
                        DbConfig.LoadFromJson("DbConfig.json");
                        CloudDbProvider.Init();
                        _SysCloud = CloudDbProvider.GetContext();
                    }

                    if (_SysCloud != null)
                    {

                        await StopAutoSyncAsync();
                        await TableSync.SyncCloudToLocalAllTables(_SysLocal, _SysCloud);
                        await TableSync.SyncLocalToCloudAllTables(_SysLocal, _SysCloud);
                        connected = true;

                        StartAutoSync(); 
                    }
                    else
                    {
                        MessageBox.Show("❌ 無法建立與雲端的連線！");
                        connected = false;
                        return;
                    }

                }
                else // 關閉同步
                {
                    await StopAutoSyncAsync();
                    if (_SysCloud != null)
                    {
                        await TableSync.SyncCloudToLocalAllTables(_SysLocal, _SysCloud);
                        SetToggleState(connected: false, enabled: false);

                    }
                    connected = false;
                }
            }
            finally
            {
                chk_toggle.CheckedChanged += chk_toggle_CheckedChanged;
                SetToggleState(connected, enabled: true);
                _isSwitching = false;

            }

        }



        private static void StartAutoSync()
        {
            if (_SysLocal == null || _SysCloud == null)
                return;

            _syncTimer = new System.Timers.Timer(30_000); // 30秒
            _syncTimer.Elapsed += async (s, e) =>
            {
                _syncTimer.Enabled = false; // 避免重複註冊
                await _syncLock.WaitAsync(); // 等待排程同步鎖

                try
                {
                    await TableSync.SyncLocalToCloudAllTables(_SysLocal, _SysCloud);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"❌ 同步時發生錯誤：{ex.Message}");
                }
                finally
                {
                    _syncLock.Release(); 
                    _syncTimer.Enabled = true;
                }
            };
            _syncTimer.AutoReset = true;
            _syncTimer.Enabled = true;
        }
        private static async Task StopAutoSyncAsync()
        {
            if (_syncTimer != null)
            {
                _syncTimer.Stop();
                _syncTimer.Dispose();
                _syncTimer = null;

                // 等待目前同步完成（若還在同步中）
                await _syncLock.WaitAsync();
              
                _syncLock.Release();

            }
        }
        private async void UserLoginForm_Load(object sender, EventArgs e)
        {
            //_SysCloud = CloudDbProvider.GetContext();
            //_SysLocal = new ApplicationDB();
            //if (_SysCloud == null)
            //{
            //    SetToggleState(false, enabled: true);
            //}
            //else
            //{
            //    SetToggleState(connected: true, enabled: false);
            //    await TableSync.SyncCloudToLocalAllTables(_SysLocal, _SysCloud);
            //    await TableSync.SyncLocalToCloudAllTables(_SysLocal, _SysCloud);
            //    SetToggleState(connected: true, enabled: true);

            //    StartAutoSync();


            //}




        }

        private void SetToggleState(bool connected, bool enabled)
        {
            if (connected)
            {
                chk_toggle.Text = "Connect";
                chk_toggle.BackColor = Color.DodgerBlue;
                chk_toggle.ForeColor = Color.White;
            }
            else
            {
                chk_toggle.Text = "DisConnect";
                chk_toggle.BackColor = Color.LightBlue; // 淺藍：未連線
                chk_toggle.ForeColor = Color.Black;
            }

            chk_toggle.Enabled = enabled;
        }
    }
}
