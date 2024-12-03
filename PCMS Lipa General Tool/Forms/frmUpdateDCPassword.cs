using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmUpdateDCPassword : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		readonly Discord dc = new();
		public string empName;


		public frmUpdateDCPassword()
		{
			InitializeComponent();
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
			btnOK.Enabled = false;
		}

		private void btn_Click(object sender, EventArgs e)
		{
			this.txtNewPassword.UseSystemPasswordChar = !this.txtNewPassword.UseSystemPasswordChar;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			dc.UpdateDCPassword(txtUsername.Text, txtNewPassword.Text, empName);
			task.AddActivityLog(empName + " his/her their Discord password for " + txtUsername.Text, empName, txtUsername.Text + " is updated", "PASSWORD UPDATE");
		}

		private void frmResetPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtNewPassword_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtNewPassword.Text))
			{
				btnOK.Enabled = true;
			}
		}
	}
}
