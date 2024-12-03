using DocumentFormat.OpenXml.Drawing;
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
			var dataTable = user.ViewEmployeeInformationUser(EmpName, out string lblCount, "EmpInfo");
			dgEmpInfo.DataSource = dataTable;
			lblSearchCount.Text = lblCount;
		}

		private void AutoFillEmp()
		{
			try
			{
				if (dgEmpInfo.SelectedRows.Count == 0)
					return;

				var selectedRow = dgEmpInfo.SelectedRows[0];
				{
					txtEmpID.Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty;
					txtEmpName.Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty;
					txtemailAdd.Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty;
					txtBVNo.Text = selectedRow.Cells[3].Value?.ToString() ?? string.Empty;
					cmbUserDept.Text = selectedRow.Cells[4].Value?.ToString() ?? string.Empty;
					cmbUserPosition.Text = selectedRow.Cells[5].Value?.ToString() ?? string.Empty;
					cmbempStatus.Text = selectedRow.Cells[6].Value?.ToString() ?? string.Empty;
					txtRemarks.Text = selectedRow.Cells[7].Value?.ToString() ?? string.Empty;
				}
			}
			catch (Exception ex)
			{
				task.LogError("AutoFillEmp", EmpName, "frmEmployeeInfo", txtEmpID.Text, ex);
			}
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
				
				LoadEmployeeInformation();
			}
			else
			{
				task.SearchThreeColumnOneFieldText(dgEmpInfo, "[User Information]", "[Employee Name]", "[Broadvoice No.]", "[Remarks]", txtSearch, lblSearchCount, EmpName);
			}
		}
	}
}
