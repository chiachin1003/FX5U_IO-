using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Models;
using Microsoft.Extensions.Logging;
using static FX5U_IOMonitor.Email.Message_function;
using FX5U_IOMonitor.Login;


namespace FX5U_IOMonitor
{
    internal static class Program
	{
        public static DatabaseSyncService SyncService { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DbConfig.LoadFromJson("DbConfig.json");
            try
            {
                DBfunction.InitMachineInfoDatabase();
                using var userService = new UserService<ApplicationDB>();
                userService.CreateDefaultUserAsync().Wait();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"InitMachineInfoDatabase 初始化失敗：{ex.Message}", "初始化錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ///雲端資料庫更新
            //var syncService = new DatabaseSyncService();
            //syncService.CurrentSyncMode = SyncMode.CompleteSync;
            //syncService.Start();

            // 啟動每日各項排程
            Email.DailyTask.StartAllSchedulers();
           
            //Email.DailyTask.StartAlarmScheduler();
            //Email.DailyTask.StartElementScheduler();
            //Email.DailyTask.StartParam_historyTaskScheduler();

            try
            {
                var importResult = LanguageImportHelper.ImportLanguage("language.csv");
                string lang = Properties.Settings.Default.LanguageSetting;
                LanguageManager.LoadLanguageFromDatabase(lang);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"語言檔匯入失敗：{ex.Message}", "初始化錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            bool loginSucceeded = false;
            // 登入
            while (!loginSucceeded)
            {
                var loginForm = new UserLoginForm();
                var result = loginForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var currentUser = loginForm.CurrentUser;
                    if (currentUser != null)
                    {
                        Application.Run(new Main());
                    }
                    loginSucceeded = true;
                }
                else
                {
                    var retry = MessageBox.Show(LanguageManager.Translate("User_Login_Form_Message"), LanguageManager.Translate("User_Login_Form_hint"), MessageBoxButtons.YesNo);
                    if (retry == DialogResult.No)
                    {
                        return;
                    }
                    else 
                    {
                        Application.Exit();
                    }
                }
            }
          
            //Application.Run( new Main() );
            SyncService?.Dispose();

        }
    }
}