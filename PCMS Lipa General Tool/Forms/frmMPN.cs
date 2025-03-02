using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmMPN : RadForm
	{
		private static readonly Notification notif = new();
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
					modMPN.txtIntID.Text = selectedRow.Cells["MPN ID"].Value?.ToString() ?? string.Empty;
					modMPN.txtInsuranceName.Text = selectedRow.Cells["Insurance Name"].Value?.ToString() ?? string.Empty;
					modMPN.txtMPNName.Text = selectedRow.Cells["MPN"].Value?.ToString() ?? string.Empty;
					modMPN.txtMPNUserName.Text = selectedRow.Cells["Username"].Value?.ToString() ?? string.Empty;
					modMPN.txtPassword.Text = selectedRow.Cells["Password"].Value?.ToString() ?? string.Empty;
					modMPN.txtWebLink.Text = selectedRow.Cells["Website"].Value?.ToString() ?? string.Empty;
					modMPN.txtRemarks.Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty;

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
				notif.LogError("dgMPN_MouseDoubleClick", EmpName, "frmMPN", null, ex);
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
			mpn.GetDBID(out string ID, EmpName);
			dlgModMPN.txtIntID.Text = ID;
			dlgModMPN.txtInsuranceName.Focus();
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
			dgMPN.BestFitColumns(BestFitColumnMode.DisplayedCells);
			txtLink.Visible = false;
			dgMPN.ReadOnly = true;
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{

			try
			{
				DataTable resultTable = mpn.SearchData(
				txtSearch.Text,
				out string searchcount, EmpName);

				dgMPN.DataSource = resultTable;
				lblSearchCount.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("txtSearch_TextChanged", EmpName, "frmAdjusterInfo", null, ex);
			}
			///task.SearchTwoColumnOneFieldText(dgMPN, "[MPN Information]", "[Insurance Name]", "[Remarks]", txtSearch, lblSearchCount, EmpName);
		}
	}
}
