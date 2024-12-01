using PCMS_Lipa_General_Tool.Class;
using System;
using System.Configuration;
using System.Data.SqlClient;
using YourmeeAppLibrary.Security;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmDeveloperAccess : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly SecurityEncryption secEnc = new();
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		public string empName;
		public string accessLevel;

		public frmDeveloperAccess()
		{
			InitializeComponent();
			txtcurrPassword.ReadOnly = true;
			LoadCurrentPassword();

		}

		private void LoadCurrentPassword()
		{
			using var conSQL = new SqlConnection(_dbConnection);
			conSQL.Open();
			var checkDevPassword = "SELECT DeveloperAccess FROM [User Information] WHERE Username LIKE 'Erwin'";
			using var cmdSQLDevPass = new SqlCommand(checkDevPassword, conSQL);
			using var readerDevPassword = cmdSQLDevPass.ExecuteReader();
			if (readerDevPassword.Read())
			{
				txtcurrPassword.Text = readerDevPassword.GetString(0);
			}
		}

		private void btnUpdateDevPassword_Click(object sender, EventArgs e)
		{
			var conquery = "UPDATE [User Information] SET DeveloperAccess = '" + txtNewPassword.Text + "' WHERE USERNAME = 'Erwin'";
			task.UpdateValues(conquery, empName, "Password successfully updated");
		}
	}
}
