using PCMS_Lipa_General_Tool.Class;
using System;
using System.Configuration;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmInsuranceInfo : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly Insurance insurance = new();
		//private readonly MailSender mailSender = new MailSender();				
		public string txtID;
		public string accessLevel;
		public string EmpName;


		public frmInsuranceInfo()
		{
			InitializeComponent();
			ShowInsInfo();

		}

		private void frmAdjusterinformation_Load(object sender, EventArgs e)
		{
			dgInsuranceInfo.BestFitColumns(BestFitColumnMode.AllCells);
			dgInsuranceInfo.ReadOnly = true;
		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}

		private void ShowInsInfo()
		{

			var dataTable = insurance.ViewInsuraceList(EmpName, out string lblCount);

			dgInsuranceInfo.DataSource = dataTable;

			lblcountSearchResult.Text = lblCount;
		}

		private void dgInsuranceInfo_MouseDoubleClick(object sender, MouseEventArgs e)
		{

			if (dgInsuranceInfo.SelectedRows.Count > 0)
			{
				var dlgModInsInfo = new frmModPrivateInfoIns();
				insurance.FillInsuranceInfo(dgInsuranceInfo, dlgModInsInfo.txtIntID, dlgModInsInfo.txtInsuranceName, dlgModInsInfo.txtInsCode, dlgModInsInfo.txtInsuranceAddress, dlgModInsInfo.txtPayerID, dlgModInsInfo.txtRemarks, EmpName);
				if (accessLevel == "User")
				{
					dlgModInsInfo.btnDelete.Visible = false;
					dlgModInsInfo.btnUpdateSave.Visible = false;
					dlgModInsInfo.Text = "View Insurance Info";

				}
				else
				{
					dlgModInsInfo.btnUpdateSave.Text = "Update";
					dlgModInsInfo.Text = "View/Modify Insurance Info";
				}
				dlgModInsInfo.ShowDialog();

			}
			ShowInsInfo();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var dlgInsInfor = new frmModPrivateInfoIns
			{
				btnUpdateSave = { Text = "Save" },
				btnDelete = { Visible = false },
				Text = "New Insurance Info",
				empName = EmpName
			};
			dlgInsInfor.GetDBID();
			dlgInsInfor.txtInsCode.Focus();
			dlgInsInfor.ShowDialog();
			ShowInsInfo();
		}

		private void frmInsuranceInfo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			task.SearchTwoColumnOneFieldText(dgInsuranceInfo, "[Insurance Info]", "[Insurance Name]", "[Address]", txtSearch, lblcountSearchResult, EmpName);
		}
	}
}
