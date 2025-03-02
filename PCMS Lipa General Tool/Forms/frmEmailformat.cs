using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmEmailFormat : RadForm
	{
		private readonly EmailFormatDB emailDB = new();
		private static readonly Notification notif = new();
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
			dgEmail.BestFitColumns(BestFitColumnMode.DisplayedCells);
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
			var dlgEmailFormat = new frmModEmailformat
			{
				Text = "New Email Format",
				btnSave = { Text = "Save " },
				empName = EmpName
			};

			//dlgEmailFormat.GetDBID();
			emailDB.GetDBID(out string ID, EmpName);
			dlgEmailFormat.txtIntID.Text = ID;
			dlgEmailFormat.txtEmailFormat.Focus();
			dlgEmailFormat.ShowDialog();
			ViewEmailFormat();
		}

		private void frmAdjusterinformation_Load(object sender, EventArgs e)
		{
			dgEmail.BestFitColumns(BestFitColumnMode.DisplayedCells);
			//this.dgBillDiagnosis.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;			
			//this.dgEmail.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgEmail.ReadOnly = true;
		}

		private void dgEmail_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgEmail.SelectedRows.Count == 0)
					return;

				var selectedRow = dgEmail.SelectedRows[0];
				var modEmail = new frmModEmailformat
				{
					txtIntID = { Text = selectedRow.Cells["Format ID"].Value?.ToString() ?? string.Empty },
					txtInsuranceName = { Text = selectedRow.Cells["Insurance"].Value?.ToString() ?? string.Empty },
					txtEmailFormat = { Text = selectedRow.Cells["Email Format"].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty }
				};

				if (accessLevel != "User")
				{
					modEmail.Text = "View/Update Adjuster Information";
					modEmail.btnSave.Text = "Update";
				}
				else
				{
					modEmail.Text = "View Adjuster Information";
					modEmail.btnSave.Visible = false;
				}

				modEmail.ShowDialog();
				ViewEmailFormat();
			}
			catch (Exception ex)
			{
				notif.LogError("dgEmail_MouseDoubleClick", EmpName, "frmEmailormat", null, ex);
			}
			
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
			try
			{
				dgEmail.BestFitColumns(BestFitColumnMode.DisplayedCells);
				DataTable resultTable = emailDB.SearchData(
				txtSearch.Text,
				out string searchcount, EmpName);
				dgEmail.DataSource = resultTable;
				lblSearchCount.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("txtSearch_TextChanged", EmpName, "frmEmailFormat", null, ex);
			}
		}
	}
}
