using FX5U_IOMonitor.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static FX5U_IOMonitor.Models.Email;


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

            // 啟動警告通知排程器
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var scheduler = new AlarmDailySummaryScheduler(new TimeSpan(8, 0, 0));
            scheduler.Start();
            ///雲端資料庫更新
            var syncService = new DatabaseSyncService();
            syncService.CurrentSyncMode = SyncMode.CompleteSync;
            syncService.Start();

            Application.Run( new Main() );
            SyncService?.Dispose();

        }
    }
}