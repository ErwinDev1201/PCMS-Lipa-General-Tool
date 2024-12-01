using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmEmailFormat : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly EmailFormatDB emailDB = new();
		//private readonly MailSender mailSender = new MailSender();						
		public string accessLevel;
		public string EmpName;

		public frmEmailFormat()
		{
			InitializeComponent();
			ViewEmailFormat();
		}

		private void ViewEmailFormat()
		{
			dgEmail.BestFitColumns(BestFitColumnMode.AllCells);
			var dataTable = emailDB.ViewEmailFormatList(EmpName, out string lblCount);

			dgEmail.DataSource = dataTable;

			lblSearchCount.Text = lblCount;
		}


		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//
		//}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var dlgEmailFormat = new frmModifyEmailformat
			{
				Text = "New Email Format",
				btnSave = { Text = "Save " },
				empName = EmpName
			};
			dlgEmailFormat.GetDBID();
			dlgEmailFormat.txtEmailFormat.Focus();
			dlgEmailFormat.ShowDialog();
			ViewEmailFormat();
		}

		private void frmAdjusterinformation_Load(object sender, EventArgs e)
		{
			dgEmail.BestFitColumns(BestFitColumnMode.AllCells);
			//this.dgBillDiagnosis.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;			
			//this.dgEmail.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgEmail.ReadOnly = true;
		}

		private void dgEmail_MouseDoubleClick(object sender, MouseEventArgs e)
		{

			if (dgEmail.SelectedRows.Count > 0)
			{
				var dlgModEmail = new frmModifyEmailformat();
				emailDB.FillEmailInfo(dgEmail, dlgModEmail.txtIntID, dlgModEmail.txtInsuranceName, dlgModEmail.txtEmailFormat, dlgModEmail.txtRemarks, EmpName);
				if (accessLevel == "User")
				{
					//dlgModEmail.btnDelete.Visible = false;
					dlgModEmail.btnSave.Visible = false;
					dlgModEmail.Text = "View Diagnosis Info";

				}
				else
				{
					dlgModEmail.btnSave.Text = "Update";
					dlgModEmail.Text = "View/Modify Diagnosis Info";
				}
				dlgModEmail.ShowDialog();

			}
			ViewEmailFormat();
		}

		private void frmEmailFormat_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			task.SearchTwoColumnOneFieldText(dgEmail, "[Insurance Email Format]", "[Insurance]", "[Email Format]", txtSearch, lblSearchCount, EmpName);
		}
	}
}
