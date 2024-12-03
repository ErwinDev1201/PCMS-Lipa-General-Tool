
using DiscordMessenger;
using DocumentFormat.OpenXml.Drawing;
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
			var dataTable = mpn.ViewMPNList(EmpName, out string lblCount);
			dgMPN.DataSource = dataTable;
			lblSearchCount.Text = lblCount;
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
			try
			{
				var modMPN = new frmModMPN();
				var selectedRow = dgMPN.SelectedRows[0];
				if (DialogResult.Yes == RadMessageBox.Show("Click Yes to Open the Link and No to View more details and modify the info, Click X to close", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					string link = selectedRow.Cells[5].Value?.ToString() ?? string.Empty;
					OpenLink(link);
				}
				else
				{
					modMPN.txtIntID.Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty;
					modMPN.txtInsuranceName.Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty;
					modMPN.txtMPNName.Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty;
					modMPN.txtMPNUserName.Text = selectedRow.Cells[3].Value?.ToString() ?? string.Empty;
					modMPN.txtPassword.Text = selectedRow.Cells[4].Value?.ToString() ?? string.Empty;
					modMPN.txtWebLink.Text = selectedRow.Cells[5].Value?.ToString() ?? string.Empty;
					modMPN.txtRemarks.Text = selectedRow.Cells[6].Value?.ToString() ?? string.Empty;

					if (accessLevel != "User")
					{
						modMPN.Text = "View/Update MPN Information";
						modMPN.btnUpdateSave.Text = "Update";
					}
					else
					{
						modMPN.Text = "View MPN Information";
						modMPN.btnDelete.Visible = false;
						modMPN.btnUpdateSave.Visible = false;
					}
					modMPN.ShowDialog();
					ShowMpnList();
				}
					
			}
			catch (Exception ex)
			{
				task.LogError("dgMPN_MouseDoubleClick", EmpName, "frmMPN", null, ex);
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
