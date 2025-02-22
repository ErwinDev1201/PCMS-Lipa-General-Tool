using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmHearingRep : Telerik.WinControls.UI.RadForm
	{
		private static readonly Notification notif = new();
		private readonly HearingRep hearing = new();
		public string accessLevel;
		public string EmpName;


		public frmHearingRep()
		{
			InitializeComponent();
			ShowHearingRepList();
		}

		private void ShowHearingRepList()
		{
			dgHearingRep.BestFitColumns(BestFitColumnMode.DisplayedCells);
			var dataTable = hearing.ViewHearinRepList(EmpName, out string lblCount);
			dgHearingRep.DataSource = dataTable;
			lblSearchCount.Text = lblCount;
			
		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}
		//
		private void dgHearingRep_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgHearingRep.SelectedRows.Count == 0)
					return;

				var selectedRow = dgHearingRep.SelectedRows[0];
				var modHearingRep = new frmModHearingRep
				{
					txtIntID = { Text = selectedRow.Cells["Rep ID"].Value?.ToString() ?? string.Empty },
					txtBoard = { Text = selectedRow.Cells["Board"].Value?.ToString() ?? string.Empty },
					txtHearingRep = { Text = selectedRow.Cells["Name"].Value?.ToString() ?? string.Empty },
					txtEmailAdd = { Text = selectedRow.Cells["Email"].Value?.ToString() ?? string.Empty },
					txtPhoneNo = { Text = selectedRow.Cells["Phone No."].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty }
				};

				if (accessLevel != "User")
				{
					modHearingRep.Text = "View/Update Hearing Rep Information";
					modHearingRep.btnUpdateSave.Text = "Update";
				}
				else
				{
					modHearingRep.Text = "View Hearing Rep Information";
					modHearingRep.btnDelete.Visible = false;
					modHearingRep.btnUpdateSave.Visible = false;
				}

				modHearingRep.ShowDialog();
				ShowHearingRepList();
			}
			catch (Exception ex)
			{
				notif.LogError("dgHearingRep_MouseDoubleClick", EmpName, "frmHearingRep", null, ex);
			}
		}

		private void frmHearingRep_Load(object sender, EventArgs e)
		{
			dgHearingRep.BestFitColumns(BestFitColumnMode.DisplayedCells);
			dgHearingRep.ReadOnly = true;
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var dlgModEPHearing = new frmModHearingRep
			{
				btnUpdateSave = { Text = "Save" },
				Text = "New Easy Print Denial",
				btnDelete = { Visible = false },
				empName = EmpName
			};
			hearing.GetDBID(out string ID, EmpName);
			dlgModEPHearing.txtIntID.Text = ID;
			//dlgModEPHearing.GetDBID();
			dlgModEPHearing.ShowDialog();
			ShowHearingRepList();
		}

		private void frmHearingRep_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{

			try
			{
				dgHearingRep.BestFitColumns(BestFitColumnMode.DisplayedCells);	
				DataTable resultTable = hearing.SearchData(
				txtSearch.Text,
				out string searchcount, EmpName);
				dgHearingRep.DataSource = resultTable;
				lblSearchCount.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("txtSearch_TextChanged", EmpName, "frmHearingRep", null, ex);
			}
			//task.SearchTwoColumnOneFieldText(dgHearingRep, "[Hearing Representative]", "[Name]", "[Remarks]", txtSearch, lblSearchCount, EmpName);
		}
	}
}
