using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmEasyPrint : Telerik.WinControls.UI.RadForm
	{
		private const string frmTitle = "New Easy Print Denial";
		private readonly CommonTask task = new();
		private readonly EasyPrint easyPrint = new();
		//private readonly MailSender mailSender = new MailSender();						
		public string accessLevel;
		public string EmpName;

		public frmEasyPrint()
		{
			InitializeComponent();
			ShowEpDenRes();
			this.txtSearch.TextBoxElement.ToolTipText = "Search Only the Easy Print Denial code";
		}



		private void ShowEpDenRes()
		{
			var dataTable = easyPrint.ViewEasyPrintList(EmpName, out string lblCount);

			dgEasyPrint.DataSource = dataTable;

			lblCountResult.Text = lblCount;
		}
		//The code does not have any bugs.


		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}
		//The code does not check for any potential SQL injection attacks. It should use parameterized queries instead of string concatenation to prevent malicious input.

		private void btnNew_Click(object sender, EventArgs e)
		{
			var dlgEPDenial = new frmModEasyPrintDenial
			{
				btnDelete = { Visible = false },
				btnUpdateSave = { Text = "Save" },
				Text = frmTitle,
				empName = EmpName
			};
			dlgEPDenial.GetDBID();
			dlgEPDenial.btnUpdateSave.Text = "Save";
			dlgEPDenial.ShowDialog();
			ShowEpDenRes();
		}

		private void frmEasyPrint_Load(object sender, EventArgs e)
		{
			dgEasyPrint.BestFitColumns(BestFitColumnMode.AllCells);
			//this.dgEasyPrint.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgEasyPrint.ReadOnly = true;
		}

		private void dgEasyPrint_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dgEasyPrint.SelectedRows.Count > 0)
			{
				var dlgModEasyPrint = new frmModEasyPrintDenial();
				easyPrint.FillEasyPrint(dgEasyPrint, dlgModEasyPrint.txtIntID, dlgModEasyPrint.txtEPCode, dlgModEasyPrint.txtInsuranceName, dlgModEasyPrint.txtDenialDescrption, dlgModEasyPrint.txtPossibleSolution, dlgModEasyPrint.txtRemarks, EmpName);
				if (accessLevel == "User")
				{
					dlgModEasyPrint.btnDelete.Visible = false;
					dlgModEasyPrint.btnUpdateSave.Visible = false;
					dlgModEasyPrint.Text = "View Easy Print Denial Info";

				}
				else
				{
					dlgModEasyPrint.btnUpdateSave.Text = "Update";
					dlgModEasyPrint.Text = "View/Modify asy Print DenialInfo";
				}
				dlgModEasyPrint.ShowDialog();

			}
			ShowEpDenRes();
		}

		private void frmEasyPrint_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			task.SearchTwoColumnOneFieldText(dgEasyPrint, "[Easy Print Denial]", "[Easy Print Code]", "REMARKS", txtSearch, lblCountResult, EmpName);
		}
	}
}
