using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmEmployeeInfo : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly User user = new();
		public string EmpName;
		public string accessLevel;

		public frmEmployeeInfo()
		{
			InitializeComponent();
			LoadEmployeeInformation();
		}

		private void LoadEmployeeInformation()
		{
			var dataTable = user.ViewEmployeeInformationUser(EmpName, out string lblCount);

			dgEmpInfo.DataSource = dataTable;

			lblSearchCount.Text = lblCount;
		}

		private void AutoFillEmp()
		{
			user.FillUpEmpTxtBox(dgEmpInfo, txtEmpID, txtEmpName, txtemailAdd, txtBVNo, cmbUserDept, cmbUserPosition, cmbOfficeLoc, cmbempStatus, txtRemarks, EmpName);
		}


		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}

		private void dgEmpInfo_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			AutoFillEmp();
		}

		private void dgEmpInfo_KeyUp(object sender, KeyEventArgs e)
		{
			AutoFillEmp();
		}

		private void frmEmployeeInfo_Load(object sender, EventArgs e)
		{
			dgEmpInfo.BestFitColumns(BestFitColumnMode.AllCells);
			//this.dgBillDiagnosis.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;			
			//this.dgEmpInfo.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgEmpInfo.ReadOnly = true;
		}

		private void frmEmployeeInfo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			if (txtSearch.Text == "")
			{
				user.ViewEmployeeInformationUser(dgEmpInfo, lblSearchCount, EmpName);
			}
			else
			{
				task.SearchThreeColumnOneFieldText(dgEmpInfo, "[User Information]", "[Employee Name]", "[Broadvoice No.]", "[Remarks]", txtSearch, lblSearchCount, EmpName);
			}
		}
	}
}
