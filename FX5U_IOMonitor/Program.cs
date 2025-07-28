using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static FX5U_IOMonitor.Email.Message_function;
using static FX5U_IOMonitor.Models.Test_;


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
                CloudDbProvider.Init();
                LocalDbProvider.Init(); 
                using var userService = LocalDbProvider.GetUserService();

                userService.CreateDefaultUserAsync().Wait();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"InitMachineInfoDatabase ��l�ƥ��ѡG{ex.Message}", "��l�ƿ��~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ///���ݸ�Ʈw��s
            //var syncService = new DatabaseSyncService();
            //syncService.CurrentSyncMode = SyncMode.CompleteSync;
            //syncService.Start();

            // �ҰʨC��U���Ƶ{
            //Email.DailyTask.StartAllSchedulers();
           
            try
            {
             
                //var importResult = LanguageImportHelper.ImportLanguage("language.csv");
                string lang = Properties.Settings.Default.LanguageSetting;
                LanguageManager.LoadLanguageFromDatabase(lang);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�y���ɶפJ���ѡG{ex.Message}", "��l�ƿ��~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            bool loginSucceeded = false;
            // �n�J
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