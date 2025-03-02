using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmEmployeeInfo : RadForm
	{
		private static readonly Notification notif = new();
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
					///txtEmpID.Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty;
					///txtEmpName.Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty;
					///txtemailAdd.Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty;
					///txtBVNo.Text = selectedRow.Cells[3].Value?.ToString() ?? string.Empty;
					///cmbUserDept.Text = selectedRow.Cells[4].Value?.ToString() ?? string.Empty;
					///cmbUserPosition.Text = selectedRow.Cells[5].Value?.ToString() ?? string.Empty;
					///cmbempStatus.Text = selectedRow.Cells[6].Value?.ToString() ?? string.Empty;
					///txtRemarks.Text = selectedRow.Cells[7].Value?.ToString() ?? string.Empty;

					// Safely extract each value and check for nulls
					txtEmpID.Text = selectedRow.Cells["Employee ID"]?.Value?.ToString() ?? string.Empty;
					txtEmpName.Text = selectedRow.Cells["Employee Name"]?.Value?.ToString() ?? string.Empty;
					cmbUserPosition.Text = selectedRow.Cells["Position"]?.Value?.ToString() ?? string.Empty;
					cmbUserDept.Text = selectedRow.Cells["Department"]?.Value?.ToString() ?? string.Empty;
					cmbempStatus.Text = selectedRow.Cells["Status"]?.Value?.ToString() ?? string.Empty;
					txtemailAdd.Text = selectedRow.Cells["Email Address"]?.Value?.ToString() ?? string.Empty;
					cmbOfficeLoc.Text = selectedRow.Cells["Office"]?.Value?.ToString() ?? string.Empty;	
					txtBVNo.Text = selectedRow.Cells["Broadvoice No."]?.Value?.ToString() ?? string.Empty;
				}
			}
			catch (Exception ex)
			{
				notif.LogError("AutoFillEmp", EmpName, "frmEmployeeInfo", txtEmpID.Text, ex);
			}
			//user.FillEmployeeData(dgEmpInfo, txtEmpID, txtEmpName, txtemailAdd, txtBVNo, cmbUserDept, cmbUserPosition, cmbOfficeLoc, cmbempStatus, txtRemarks, EmpName);
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
			dgEmpInfo.BestFitColumns(BestFitColumnMode.DisplayedCells);
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
			try
			{
				if (!string.IsNullOrEmpty(txtSearch.Text))
				{
					DataTable resultTable = user.GetSearch(txtSearch.Text, "Active", out string searchCount, EmpName, "EmpInfo");
					dgEmpInfo.DataSource = resultTable;
					lblSearchCount.Text = searchCount;
				}
				else
				{
					LoadEmployeeInformation();
				}
				
				//DataTable
				//DataTable resultTable = user.SearchData(
				//txtSearch.Text,
				//out string searchcount, EmpName);
				//
				//dgEmpInfo.DataSource = resultTable;
				//lblSearchCount
				//	.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("txtSearch_TextChanged", EmpName, "frmAdjusterInfo", null, ex);
			}
			//if (txtSearch.Text == "")
			//{
			//	
			//	LoadEmployeeInformation();
			//}
			//else
			//{
			//
			//	
			//	//task.SearchThreeColumnOneFieldText(dgEmpInfo, "[User Information]", "[Employee Name]", "[Broadvoice No.]", "[Remarks]", txtSearch, lblSearchCount, EmpName);
			//}
		}
	}
}
