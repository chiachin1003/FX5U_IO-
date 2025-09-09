using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Collections;
using static FX5U_IOMonitor.Message.Message_function;



namespace FX5U_IOMonitor
{
    internal static class Program
	{

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
                LocalDbProvider.Init(); 
                using var userService = LocalDbProvider.GetUserService();
                userService.CreateDefaultUserAsync().Wait();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Translate("Message_InitalError") + $"InitMachineInfoDatabase ：{ex.Message}", 
                    LanguageManager.Translate("Message_InitalError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
             
                //var importResult = LanguageImportHelper.ImportLanguage("language.csv",true);
                string lang = Properties.Settings.Default.LanguageSetting;
                LanguageManager.LoadLanguageFromDatabase(lang);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Translate("File_Settings_InputFailed") +$"Language：{ex.Message}",
                    LanguageManager.Translate("Message_InitalError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            // 啟動每日各項排程
            Scheduling.DailyTask.StartAllSchedulers();
          
            Application.Run( new Main() );

        }
    }
}