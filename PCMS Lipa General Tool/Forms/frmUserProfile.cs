using System;
using System.Windows.Forms;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmUserProfile : Telerik.WinControls.UI.RadForm
	{
		public string empName;


		public frmUserProfile()
		{
			InitializeComponent();
			ReadOnly();
		}

		private void frmUserProfile_Load(object sender, EventArgs e)
		{
			txtIntID.ShowEmbeddedLabel = true; txtIntID.EmbeddedLabelText = "Employee ID";
			txtEmpName.ShowEmbeddedLabel = true; txtEmpName.EmbeddedLabelText = "Employee Name";
			txtBVNo.ShowEmbeddedLabel = true; txtBVNo.EmbeddedLabelText = "Broadvoice No.";
			txtDateofBirth.ShowEmbeddedLabel = true; txtDateofBirth.EmbeddedLabelText = "Date of Birth";
			txtUsername.ShowEmbeddedLabel = true; txtUsername.EmbeddedLabelText = "Username";
			txtLytecPassword.ShowEmbeddedLabel = true; txtLytecPassword.EmbeddedLabelText = "Lytec Password";
			txtLytecUsername.ShowEmbeddedLabel = true; txtLytecUsername.EmbeddedLabelText = "Lytec Username";
			txtRDWebPassword.ShowEmbeddedLabel = true; txtRDWebPassword.EmbeddedLabelText = "RDWeb Username";
			txtRDWebUsername.ShowEmbeddedLabel = true; txtRDWebUsername.EmbeddedLabelText = "RDWeb Password";
			txtUserAccess.ShowEmbeddedLabel = true; txtUserAccess.EmbeddedLabelText = "User Access";
			txtUserPosition.ShowEmbeddedLabel = true; txtUserPosition.EmbeddedLabelText = "User Position";
			txtWorkEmail.ShowEmbeddedLabel = true; txtWorkEmail.EmbeddedLabelText = "Work Email";

			// new version 05.23
			txtDiscordUsername.ShowEmbeddedLabel = true; txtDiscordUsername.EmbeddedLabelText = "Discord Username";
			txtDiscordPassword.ShowEmbeddedLabel = true; txtDiscordPassword.EmbeddedLabelText = "Discord Password";
		}

		private void ReadOnly()
		{
			txtIntID.ReadOnly = true;
			txtEmpName.ReadOnly = true;
			txtBVNo.ReadOnly = true;
			txtDateofBirth.ReadOnly = true;
			txtUsername.ReadOnly = true;
			txtLytecPassword.ReadOnly = true;
			txtLytecUsername.ReadOnly = true;
			txtRDWebPassword.ReadOnly = true;
			txtRDWebUsername.ReadOnly = true;
			txtUserAccess.ReadOnly = true;
			txtUserPosition.ReadOnly = true;
			txtWorkEmail.ReadOnly = true;
		}

		private void frmUserProfile_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void radButton1_Click(object sender, EventArgs e)
		{
			var updateDCPassword = new frmUpdateDCPassword()
			{
				Text = "Update Discord Password",
				empName = empName
			};
			updateDCPassword.txtUsername.Text = txtDiscordUsername.Text;
			updateDCPassword.txtUsername.Enabled = false;
		}

		private void btnChangedPassword_Click(object sender, EventArgs e)
		{
			var resetPassword = new frmResetPassword
			{
				Text = "Change Password"
			};
			resetPassword.txtEmpID.Enabled = false;
			resetPassword.changePassword = "Yes";
			resetPassword.txtWorkEmail.Enabled = false;
			resetPassword.txtEmpID.Text = txtUsername.Text;
			resetPassword.btnOK.Text = "Change Password";
			resetPassword.txtWorkEmail.Text = txtWorkEmail.Text;
			resetPassword.ShowDialog();
		}
	}
}
