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

			if (dgHearingRep.SelectedRows.Count > 0)
			{
				var dlgModHeariRep = new frmModHearingRep();
				hearing.FillHearingRep(dgHearingRep, dlgModHeariRep.txtIntID, dlgModHeariRep.txtBoard, dlgModHeariRep.txtHearingRep, dlgModHeariRep.txtEmailAdd, dlgModHeariRep.txtPhoneNo, dlgModHeariRep.txtRemarks, EmpName);
				if (accessLevel == "User")
				{
					dlgModHeariRep.btnDelete.Visible = false;
					dlgModHeariRep.btnUpdateSave.Visible = false;
					dlgModHeariRep.Text = "View Hearing Rep Info";

				}
				else
				{
					dlgModHeariRep.btnUpdateSave.Text = "Update";
					dlgModHeariRep.Text = "View/Modify Hearing Rep Info";
				}
				dlgModHeariRep.ShowDialog();

			}
			ShowHearingRepList();
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
