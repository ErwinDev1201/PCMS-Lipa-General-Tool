using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmBillReviewDirectory : RadForm
	{
		private readonly BillReview bill = new();
		private static readonly Notification notif = new();

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
			dgBillReview.BestFitColumns(BestFitColumnMode.DisplayedCells);
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
					txtIntID = { Text = selectedRow.Cells["Reviewer ID"].Value?.ToString() ?? string.Empty },
					txtInsuranceName = { Text = selectedRow.Cells["Insurance Name"].Value?.ToString() ?? string.Empty },
					txtPhoneNo = { Text = selectedRow.Cells["Phone No."].Value?.ToString() ?? string.Empty },
					txtFax = { Text = selectedRow.Cells["Fax No."].Value?.ToString() ?? string.Empty },
					txtURPhone = { Text = selectedRow.Cells["UR Phone No."].Value?.ToString() ?? string.Empty },
					txtURFaxNo = { Text = selectedRow.Cells["UR Fax No."].Value?.ToString() ?? string.Empty },
					txtBRPhoneNo = { Text = selectedRow.Cells["BR Phone No."].Value?.ToString() ?? string.Empty },
					txtBRFaxNo = { Text = selectedRow.Cells["BR Fax No."].Value?.ToString() ?? string.Empty },
					txtOnlineEmail = { Text = selectedRow.Cells["Email"].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty }
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
				notif.LogError("dgAdjusterInfo_DoubleClick", EmpName, "frmAdjusterInfo", null, ex);
			}
		}

		private void frmInsBillReviewDirectory_Load(object sender, EventArgs e)
		{
			dgBillReview.BestFitColumns(BestFitColumnMode.DisplayedCells);
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
			bill.GetDBID(out string ID, EmpName);
			dlgBillReview.txtIntID.Text = ID;
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
				dgBillReview.BestFitColumns(BestFitColumnMode.DisplayedCells);
				lblSearchCount.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("txtSearch_TextChanged", EmpName, "frmBillReviewDirectory", null, ex);
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			LoadSearchandFilter();
			//task.	(dgBillReview, "[Bill Review Directory]", "[Insurance]", ",[Remarks]", txtSearch, lblSearchCount, EmpName);
		}
	}
}
