using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmResetPassword : Telerik.WinControls.UI.RadForm
	{

		private readonly User user = new();
		private readonly FEWinForm fe = new();
		public string cmbLevel;
		public string empName;
		public string changePassword;


		public frmResetPassword()
		{
			InitializeComponent();
			lblIDValidation.Visible = false;
			lblPasswordValidation.Visible = false;
			lblValidation.Visible = false;

			//show eye
			this.txtNewPassword.UseSystemPasswordChar = true;
			RadButtonElement btn = new();
			btn.Click += btn_Click;
			this.txtNewPassword.RightButtonItems.Add(btn);
			btn.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
			btn.BorderElement.Visibility = ElementVisibility.Collapsed;
			var font1 = ThemeResolutionService.GetCustomFont("Font Awesome 5 Free Regular");
			btn.CustomFont = font1.Name;
			btn.CustomFontSize = 5;
			//btn.Text = "\ue13D";
			btn.Text = "\ue052";
		}

		private void btn_Click(object sender, EventArgs e)
		{
			this.txtNewPassword.UseSystemPasswordChar = !this.txtNewPassword.UseSystemPasswordChar;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			//string adminMessage = "Password successfully updated, and the password is sent to the provided email.";
			//string nonadminMessage = "Please log in with the new password the next time. A copy of your new password is sent via email";
			radPanel1.Enabled = false;
			//btnOK.Enabled = false;
			if (changePassword == "forgot")
			{
				bool isSuccess = user.UpdateUserPassword(
					txtEmpID.Text,
					txtWorkEmail.Text,
					txtNewPassword.Text,
					"forgot",
					empName,
					out string message,
					out string Status);
				if (isSuccess)
				{
					fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					fe.SendToastNotifDesktop(message, "Failed");
				}
				radPanel1.Enabled = true;
				Hide();
			}

			else if (changePassword == "Yes")
			{
				bool isSuccess = user.UpdateUserPassword(
					txtEmpID.Text,
					txtWorkEmail.Text,
					txtNewPassword.Text,
					"Yes",
					empName,
					out string message,
					out string Status);
				if (isSuccess)
				{
					fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					fe.SendToastNotifDesktop(message, "Failed");
				}
				radPanel1.Enabled = true;
				Hide();
				radPanel1.Enabled =  true;
			}
			var login = new FrmLogin();
			login.txtPassword.Clear();
			login.Show();
		}


		private void frmResetPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtWorkEmail_TextChanged(object sender, EventArgs e)
		{
			if (!user.IsValidEmail(txtWorkEmail.Text))
			{
				lblValidation.Visible = true;
				lblValidation.Text = "Invalid Email";
				btnOK.Enabled = false;
			}
		}

		private void clearValidationmessage()
		{
			lblPasswordValidation.Text = "";
			lblIDValidation.Text = "";
			lblValidation.Text = "";

		}


		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void txtNewPassword_TextChanged(object sender, EventArgs e)
		{
			if (txtNewPassword.Text.Length < 8)
			{
				lblPasswordValidation.Visible = true;
				lblPasswordValidation.Text = "Password Requirements didn't meet. Password character count should be greater than 8";
				btnOK.Enabled = false;
			}
			else
			{
				lblPasswordValidation.Visible = false;
				//lblPasswordValidation.Text = "Password Requirements didn't meet. Password character count should be greater than 8";
				btnOK.Enabled = true;
			}
		}

		private void txtEmpID_TextChanged(object sender, EventArgs e)
		{
			if (txtEmpID.Text == "")
			{
				lblIDValidation.Text = "This a required field";
				btnOK.Enabled = false;
			}
		}
	}
}
