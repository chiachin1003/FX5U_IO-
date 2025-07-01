
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Login;




namespace FX5U_IOMonitor
{
	public partial class Receive_Notification : Form
	{
		public Receive_Notification()
		{
			InitializeComponent();
			UpdateLanguage();
		}

		private void UpdateLanguage()
		{
			this.Text = LanguageManager.Translate("Receive_Notification");
           
        }

     
	}
}
