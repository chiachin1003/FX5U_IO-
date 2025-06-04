
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
			this.Text = ResMapper.GetLocalizedString( "UserLoginForm::Title" );
			_lblAccount.Text = ResMapper.GetLocalizedString( "UserManageForm::Lbl::blade_TPI_name" );
			_lblPassword.Text = ResMapper.GetLocalizedString( "UserManageForm::Lbl::Password" );
			_btnLogin.Text = ResMapper.GetLocalizedString( "UserLoginForm::Btn::Login" );
		}

		private async void _btnLogin_Click( object sender, EventArgs e )
		{
			// Check if the user has entered a valid account and password
			if( string.IsNullOrEmpty( _txtAccount.Text ) || string.IsNullOrEmpty( _txtPassword.Text ) ) {
				MessageBox.Show( ResMapper.GetLocalizedString( "UserManageForm::Msg::InvalidInput" ),
					ResMapper.GetLocalizedString( "UserManageForm::Msg::Error" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
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
					MessageBox.Show( ResMapper.GetLocalizedString( "UserLoginForm::Msg::AccountDoesNotExist" ),
						ResMapper.GetLocalizedString( "UserManageForm::Msg::Error" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
					break;

				case UserErrorCode.PasswordError:
					MessageBox.Show( ResMapper.GetLocalizedString( "UserLoginForm::Msg::PasswordError" ),
						ResMapper.GetLocalizedString( "UserManageForm::Msg::Error" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
					break;

			}
		}
	}
}
