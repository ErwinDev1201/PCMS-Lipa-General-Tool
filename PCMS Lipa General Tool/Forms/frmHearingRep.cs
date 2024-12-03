using PCMS_Lipa_General_Tool.Class;
using System;
using System.Configuration;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmHearingRep : Telerik.WinControls.UI.RadForm
	{
		private static readonly string wcsupport = ConfigurationManager.AppSettings["wcsupportpath"];
		private readonly CommonTask task = new();
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
					txtIntID = { Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty },
					txtBoard = { Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty },
					txtHearingRep = { Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty },
					txtEmailAdd = { Text = selectedRow.Cells[3].Value?.ToString() ?? string.Empty },
					txtPhoneNo = { Text = selectedRow.Cells[4].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells[5].Value?.ToString() ?? string.Empty }
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
				task.LogError("dgHearingRep_MouseDoubleClick", EmpName, "frmHearingRep", null, ex);
			}
		}

		private void frmHearingRep_Load(object sender, EventArgs e)
		{
			dgHearingRep.BestFitColumns(BestFitColumnMode.AllCells);
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
			dlgModEPHearing.GetDBID();
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
			task.SearchTwoColumnOneFieldText(dgHearingRep, "[Hearing Representative]", "[Name]", "[Remarks]", txtSearch, lblSearchCount, EmpName);
		}
	}
}
