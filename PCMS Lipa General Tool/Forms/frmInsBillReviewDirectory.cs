using PCMS_Lipa_General_Tool.Class;
using System;
using System.Configuration;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmInsBillReviewDirectory : Telerik.WinControls.UI.RadForm
	{
		private static readonly string wcsupport = ConfigurationManager.AppSettings["wcsupportpath"];
		private readonly CommonTask task = new();
		private readonly BillReview bill = new();

		//private readonly MailSender mailSender = new MailSender();						
		public string accessLevel;
		public string EmpName;

		public frmInsBillReviewDirectory()
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
			if (dgBillReview.SelectedRows.Count > 0)
			{
				var dlgModBR = new frmModBillReviewInfo();
				bill.FillBillReview(dgBillReview, dlgModBR.txtIntID, dlgModBR.txtInsuranceName, dlgModBR.txtPhoneNo, dlgModBR.txtFax, dlgModBR.txtURPhone, dlgModBR.txtURFaxNo, dlgModBR.txtBRPhoneNo, dlgModBR.txtBRFaxNo, dlgModBR.txtOnlineEmail, dlgModBR.txtRemarks, EmpName);
				if (accessLevel == "User")
				{
					dlgModBR.btnDelete.Visible = false;
					dlgModBR.btnUpdateSave.Visible = false;
					dlgModBR.Text = "View Bill Reviewer Info";

				}
				else
				{
					dlgModBR.btnUpdateSave.Text = "Update";
					dlgModBR.Text = "View/Modify Bill Reviewer Info";
				}
				dlgModBR.ShowDialog();

			}
			ShowInsuranceInfo();
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

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			task.SearchTwoColumnOneFieldText(dgBillReview, "[Bill Review Directory]", "[Insurance]", ",[Remarks]", txtSearch, lblSearchCount, EmpName);
		}
	}
}
