
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmMPN : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly MPN mpn = new();
		public string accessLevel;
		public string EmpName;

		public frmMPN()
		{
			InitializeComponent();
			ShowMpnList();

		}

		private void ShowMpnList()
		{
			task.ViewDataTable(dgMPN, "[MPN Information]", lblSearchCount, EmpName);
		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}

		private void OpenLink(string webLink)
		{
			if (webLink.Contains("viiad.com") == false)
			{
				Process.Start(webLink);
			}
			else
			{
				RadMessageBox.Show("This website should be open in RD WEb Internet Explorer or RD Google Chrome", "Notification",
					MessageBoxButtons.OK, RadMessageIcon.Info);

			}
		}

		private void dgMPN_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Click Yes to Open the Link and No to View more details and modify the info, Click X to close", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				mpn.FillUpMPNWeblink(dgMPN, txtLink, EmpName);
				OpenLink(txtLink.Text);
			}
			else
			{
				if (dgMPN.SelectedRows.Count > 0)
				{
					var dlgModMPN = new frmModMPN();
					mpn.FillMPNData(dgMPN, dlgModMPN.txtIntID, dlgModMPN.txtInsuranceName, dlgModMPN.txtMPNName, dlgModMPN.txtMPNUserName, dlgModMPN.txtPassword, dlgModMPN.txtWebLink, dlgModMPN.txtRemarks, EmpName);
					if (accessLevel == "User")
					{
						dlgModMPN.btnDelete.Visible = false;
						dlgModMPN.btnUpdateSave.Visible = false;
						dlgModMPN.Text = "View Diagnosis Info";

					}
					else
					{
						dlgModMPN.btnUpdateSave.Text = "Update";
						dlgModMPN.Text = "View/Modify Diagnosis Info";
					}
					dlgModMPN.ShowDialog();

				}
				ShowMpnList();
			}
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var dlgModMPN = new frmModMPN
			{
				txtIntID = { ReadOnly = true },
				btnDelete = { Visible = false },
				btnUpdateSave = { Text = "Save" },
				Text = "New MPN"
			};
			dlgModMPN.txtInsuranceName.Focus();
			dlgModMPN.GetDBID();
			dlgModMPN.ShowDialog();
			ShowMpnList();
		}

		private void frmMPN_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void frmMPN_Load(object sender, EventArgs e)
		{
			dgMPN.BestFitColumns(BestFitColumnMode.AllCells);
			txtLink.Visible = false;
			dgMPN.ReadOnly = true;
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			task.SearchTwoColumnOneFieldText(dgMPN, "[MPN Information]", "[Insurance Name]", "[Remarks]", txtSearch, lblSearchCount, EmpName);
		}
	}
}
