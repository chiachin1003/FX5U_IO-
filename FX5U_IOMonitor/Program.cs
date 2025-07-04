using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Models;
using Microsoft.Extensions.Logging;
using static FX5U_IOMonitor.Email.email;
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
            var syncService = new DatabaseSyncService();
            syncService.CurrentSyncMode = SyncMode.CompleteSync;
            syncService.Start();

            // 啟動每日各項排程
            //Email.DailyTask.StartAlarmScheduler();
            //Email.DailyTask.StartElementScheduler();
            //Email.DailyTask.StartParam_historyTaskScheduler();

            // 先顯示登入畫面
            using (var loginForm = new UserLoginForm())
            {
                var result = loginForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Application.Run(new Main());
                }
                else
                {
                    Application.Exit();
                }
            }

            //Application.Run( new Main() );
            SyncService?.Dispose();

        }
    }
}