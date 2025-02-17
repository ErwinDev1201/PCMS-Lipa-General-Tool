using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmEasyPrint : Telerik.WinControls.UI.RadForm
	{
		private const string frmTitle = "New Easy Print Denial";
		private readonly EasyPrint easyPrint = new();
		private static readonly Notification notif = new();
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
			easyPrint.GetDBID(out string ID, EmpName);
			dlgEPDenial.txtIntID.Text = ID;
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
			try
			{
				if (dgEasyPrint.SelectedRows.Count == 0)
					return;

				var selectedRow = dgEasyPrint.SelectedRows[0];
				var modEP = new frmModEasyPrintDenial
				{
					txtIntID = { Text = selectedRow.Cells["EP ID"].Value?.ToString() ?? string.Empty },
					txtEPCode = { Text = selectedRow.Cells["Easy Print Code"].Value?.ToString() ?? string.Empty },
					txtInsuranceName = { Text = selectedRow.Cells["Insurance"].Value?.ToString() ?? string.Empty },
					txtDenialDescrption = { Text = selectedRow.Cells["Description"].Value?.ToString() ?? string.Empty },
					txtPossibleSolution = { Text = selectedRow.Cells["Possible Resolution"].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty }
				};

				if (accessLevel != "User")
				{
					modEP.Text = "View/Update Adjuster Information";
					modEP.btnUpdateSave.Text = "Update";
				}
				else
				{
					modEP.Text = "View Adjuster Information";
					modEP.btnDelete.Visible = false;
					modEP.btnUpdateSave.Visible = false;
				}

				modEP.ShowDialog();
				ShowEpDenRes();
			}
			catch (Exception ex)
			{
				notif.LogError("dgEasyPrint_MouseDoubleClick", EmpName, "frmEasyPrint", null, ex);
			}
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
			try
			{
				DataTable resultTable = easyPrint.SearchData(
				txtSearch.Text,
				out string searchcount, EmpName);

				dgEasyPrint.DataSource = resultTable;
				lblCountResult.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("txtSearch_TextChanged", EmpName, "frmEasyprint", null, ex);
			}
		}
	}
}
