
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Login;




namespace FX5U_IOMonitor
{
	public partial class UserLoginForm : Form
	{
		public UserLoginForm()
		{
			InitializeComponent();
			//UpdateLanguage();
		}

		private void UpdateLanguage()
		{
			this.Text = LanguageManager.Translate("User_Login_Form");
            //_lblAccount.Text = ResMapper.GetLocalizedString( "UserManageForm::Lbl::blade_TPI_name" );
			
            _lblPassword.Text = LanguageManager.Translate("UserManageForm_Lbl_Password");

            _btnLogin.Text = LanguageManager.Translate("User_Login_Btn_Login");

        }

        private async void _btnLogin_Click( object sender, EventArgs e )
		{
			// Check if the user has entered a valid account and password
			if( string.IsNullOrEmpty( _txtAccount.Text ) || string.IsNullOrEmpty( _txtPassword.Text ) ) {
				MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_InvalidInput"),
                    LanguageManager.Translate("UserManageForm_Msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error );
				return;
			}

			UserErrorCode errorCode;
			using( var userService = new UserService<ApplicationDB>() ) {
				errorCode = await userService.LoginAsync( _txtAccount.Text, _txtPassword.Text );
			}

			switch( errorCode ) {
				case UserErrorCode.None:
					DialogResult = DialogResult.OK;
					break;

				case UserErrorCode.NotExist:
					MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_AccountDoesNotExist"),
                         LanguageManager.Translate("UserManageForm_Msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error );
					break;

				case UserErrorCode.PasswordError:
					MessageBox.Show(LanguageManager.Translate("UserManageForm_Msg_PasswordError"),
                         LanguageManager.Translate("UserManageForm_Msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error );
					break;

			}
		}
	}
}
