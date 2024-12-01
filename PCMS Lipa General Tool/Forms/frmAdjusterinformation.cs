using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class FrmAdjusterinformation : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly Adjuster adj = new();
		//private readonly MailSender mailSender = new MailSender();						
		public string accessLevel;
		public string EmpName;


		public FrmAdjusterinformation()
		{
			InitializeComponent();
			ShowAdjInfo();
		}

		private void ShowAdjInfo()
		{
			var dataTable = adj.ViewAdjusterList(EmpName, out string lblCount);

			dgAdjusterInfo.DataSource = dataTable;

			lblCountSearch.Text = lblCount;
		}



		private void dgAdjusterInfo_DoubleClick(object sender, EventArgs e)
		{

			if (dgAdjusterInfo.SelectedRows.Count > 0)
			{
				var modAdj = new frmModifyAdjusterInfo();
				adj.FillAdjusterInfo(dgAdjusterInfo, modAdj.txtIntID, modAdj.txtInsuranceName, modAdj.txtAdjusterName, modAdj.txtphoneno, modAdj.txtExtension, modAdj.txtFax, modAdj.txtEmailAdd, modAdj.txtSupervisor, modAdj.txtRemarks, EmpName);
				if (accessLevel != "User")
				{

					modAdj.Text = "View/Update Adjuster Information";
					//modAdj.btnDelete.Visible = false;
					modAdj.btnUpdateSave.Text = "Update";
				}
				else
				{
					modAdj.Text = "View Adjuster Information";
					modAdj.btnDelete.Visible = false;
					modAdj.btnUpdateSave.Visible = false;
				}
				modAdj.ShowDialog();
			}
			ShowAdjInfo();

		}
		//No bugs found.

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//
		//}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var modifyAdjInfo = new frmModifyAdjusterInfo
			{
				txtIntID = { ReadOnly = true },
				btnDelete = { Visible = false },
				btnUpdateSave = { Text = "Save" },
				Text = "New Adjuster Information",
				empName = EmpName
			};
			modifyAdjInfo.txtInsuranceName.Focus();
			modifyAdjInfo.GetDBID();
			modifyAdjInfo.ShowDialog();
			ShowAdjInfo();

		}

		private void frmAdjusterinformation_Load(object sender, EventArgs e)
		{
			dgAdjusterInfo.BestFitColumns(BestFitColumnMode.AllCells);
			//this.dgAdjusterInfo.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgAdjusterInfo.ReadOnly = true;
		}

		private void frmAdjusterinformation_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			task.SearchTwoColumnOneFieldText(dgAdjusterInfo, "[Adjuster Information]", "[Insurance Name]", "[Remarks]", txtSearch, lblCountSearch, EmpName);
		}
	}
}
