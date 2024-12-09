using PCMS_Lipa_General_Tool.Class;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmBillReviewDirectory : Telerik.WinControls.UI.RadForm
	{
		private static readonly string wcsupport = ConfigurationManager.AppSettings["wcsupportpath"];
		private readonly CommonTask task = new();
		private readonly BillReview bill = new();

		//private readonly MailSender mailSender = new MailSender();						
		public string accessLevel;
		public string EmpName;

		public frmBillReviewDirectory()
		{
			InitializeComponent();
			ShowInsuranceInfo();
		}

		private void ShowInsuranceInfo()
		{
			var dataTable = bill.ViewBillReviewList(EmpName, out string lblCount);

			dgBillReview.DataSource = dataTable;

			lblSearchCount.Text = lblCount;
		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}

		private void dgBillReview_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgBillReview.SelectedRows.Count == 0)
					return;

				var selectedRow = dgBillReview.SelectedRows[0];
				var modBR = new frmModBillReviewInfo
				{
					txtIntID = { Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty },
					txtInsuranceName = { Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty },
					txtPhoneNo = { Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty },
					txtFax = { Text = selectedRow.Cells[3].Value?.ToString() ?? string.Empty },
					txtURPhone = { Text = selectedRow.Cells[4].Value?.ToString() ?? string.Empty },
					txtURFaxNo = { Text = selectedRow.Cells[5].Value?.ToString() ?? string.Empty },
					txtBRPhoneNo = { Text = selectedRow.Cells[6].Value?.ToString() ?? string.Empty },
					txtBRFaxNo = { Text = selectedRow.Cells[7].Value?.ToString() ?? string.Empty },
					txtOnlineEmail = { Text = selectedRow.Cells[8].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells[9].Value?.ToString() ?? string.Empty }
				};

				if (accessLevel != "User")
				{
					modBR.Text = "View/Update Adjuster Information";
					modBR.btnUpdateSave.Text = "Update";
				}
				else
				{
					modBR.Text = "View Adjuster Information";
					modBR.btnDelete.Visible = false;
					modBR.btnUpdateSave.Visible = false;
				}

				modBR.ShowDialog();
				ShowInsuranceInfo();
			}
			catch (Exception ex)
			{
				task.LogError("dgAdjusterInfo_DoubleClick", EmpName, "frmAdjusterInfo", null, ex);
			}
		}

		private void frmInsBillReviewDirectory_Load(object sender, EventArgs e)
		{
			dgBillReview.BestFitColumns(BestFitColumnMode.AllCells);
			//this.dgBillDiagnosis.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;			
			dgBillReview.ReadOnly = true;
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var dlgBillReview = new frmModBillReviewInfo
			{
				btnDelete = { Visible = false },
				btnUpdateSave = { Text = "Save" },
				Text = "New Bill Review",
				empName = EmpName

			};
			dlgBillReview.GetDBID();
			dlgBillReview.ShowDialog();
			ShowInsuranceInfo();
		}

		private void frmInsBillReviewDirectory_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void dgBillReview_Click(object sender, EventArgs e)
		{

		}

		private void LoadSearchandFilter()
		{
			try
			{
				DataTable resultTable = bill.SearchData(
				txtSearch.Text,
				out string searchcount, EmpName);

				dgBillReview.DataSource = resultTable;
				lblSearchCount.Text = searchcount;

			}
			catch (Exception ex)
			{
				task.LogError("txtSearch_TextChanged", EmpName, "frmBillReviewDirectory", null, ex);
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			LoadSearchandFilter();
			//task.	(dgBillReview, "[Bill Review Directory]", "[Insurance]", ",[Remarks]", txtSearch, lblSearchCount, EmpName);
		}
	}
}
