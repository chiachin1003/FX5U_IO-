using System.ComponentModel;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Login;

namespace FX5U_IOMonitor
{
	public partial class UserManageForm : Form
	{
		public UserManageForm()
		{
			InitializeComponent();
			InitRolesComboBox();
			this.Shown += UserManageForm_Shown;
			//UpdateLanguage();
		}

		private void UpdateLanguage()
		{
			this.Text = ResMapper.GetLocalizedString( "UserManageForm::Title" );
			_lblAccount.Text = ResMapper.GetLocalizedString( "UserManageForm::Lbl::Name" );
			_lblPassword.Text = ResMapper.GetLocalizedString( "UserManageForm::Lbl::Password" );
			_lblConfirmPassword.Text = ResMapper.GetLocalizedString( "UserManageForm::Lbl::ConfirmPassword" );
			_lblRole.Text = ResMapper.GetLocalizedString( "UserManageForm::Lbl::Role" );
			_lblSelectedRole.Text = ResMapper.GetLocalizedString( "UserManageForm::Lbl::Role" );
			_btnAdd.Text = ResMapper.GetLocalizedString( "UserManageForm::Btn::Add" );
			_btnDelete.Text = ResMapper.GetLocalizedString( "UserManageForm::Btn::Delete" );
		}

		private async void UserManageForm_Shown( object? sender, EventArgs e )
		{
			await UpdateDGV();
		}

		private async Task UpdateDGV()
		{
			string curSelectedRole = _cbSelectedRole.Text;
			using var userService = new UserService<ApplicationDB>();
			var userNameList = await userService.GetAllAsync( curSelectedRole );
			_dgvUsers.DataSource = new BindingList<ApplicationUser>( userNameList );
		}

		void InitRolesComboBox()
		{
			_cbRole.Items.AddRange( new object[] { SD.Role_User, SD.Role_Operator, SD.Role_Admin } );
			_cbRole.SelectedIndex = 0;
			_cbSelectedRole.Items.AddRange( new object[] { SD.Role_User, SD.Role_Operator, SD.Role_Admin } );
			_cbSelectedRole.SelectedIndex = 0;
		}

		private async void _cbSelectedRole_SelectedIndexChanged( object sender, EventArgs e )
		{
			await UpdateDGV();
		}

		private async void _btnAdd_Click( object sender, EventArgs e )
		{
			// Check if the user has entered a valid account and password
			if( string.IsNullOrEmpty( _txtAccount.Text ) || string.IsNullOrEmpty( _txtPassword.Text ) ) {
				MessageBox.Show( ResMapper.GetLocalizedString( "UserManageForm::Msg::InvalidInput" ),
					ResMapper.GetLocalizedString( "UserManageForm::Msg::Error" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
				return;
			}

			// Check if the user already exists
			using( var userService = new UserService<ApplicationDB>() ) {
				if( userService.CheckUserExist( _txtAccount.Text ) ) {
					MessageBox.Show( ResMapper.GetLocalizedString( "UserManageForm::Msg::UserNameAlreadyExist" ),
						ResMapper.GetLocalizedString( "UserManageForm::Msg::Error" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
					return;
				}

				// Check password == confirm password
				if( _txtPassword.Text != _txtConfirmPassword.Text ) {
					MessageBox.Show( ResMapper.GetLocalizedString( "UserManageForm::Msg::PasswordNotMatch" ),
						ResMapper.GetLocalizedString( "UserManageForm::Msg::Error" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
					return;
				}

				await userService.CreateUserAsync( _txtAccount.Text, _txtPassword.Text, _cbRole.Text );
			}

			await UpdateDGV();
		}

		private async void _btnDelete_Click( object sender, EventArgs e )
		{
			// Get the selected dgv row
			if( _dgvUsers.SelectedRows.Count == 0 ) {
				MessageBox.Show( ResMapper.GetLocalizedString( "UserManageForm::Msg::SelectRowFirst" ) );
				return;
			}

			var selectedUser = _dgvUsers.SelectedRows[ 0 ].DataBoundItem as ApplicationUser;
			if( selectedUser == null ) {
				MessageBox.Show( ResMapper.GetLocalizedString( "UserManageForm::Msg::DataNotFound" ) );
				return;
			}

			// Check if the user is admin
			if( selectedUser.UserName == SD.Admin_Account ) {
				MessageBox.Show( ResMapper.GetLocalizedString( "UserManageForm::Msg::CanNotDeleteUser" ) );
				return;
			}

			// Delete user from database
			using( var userService = new UserService<ApplicationDB>() ) {
				await userService.DeleteUserAsync( selectedUser.UserName );
			}

			await UpdateDGV();
		}
	}
}
