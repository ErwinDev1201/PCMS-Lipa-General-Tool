using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmDiagnosis : RadForm
	{
		private readonly Diagnosis dx = new();
		public string EmpName;
		public string accessLevel;
		private static readonly Notification notif = new();


		public frmDiagnosis()
		{
			InitializeComponent();
			ShowMePTDxCodes();
			//chkBodyParts.Checked = true;
		}

		private void ShowMePTDxCodes()
		{
			var dataTable = dx.ViewDxList(EmpName, out string lblCount);
			dgBillDiagnosis.DataSource = dataTable;
			dgBillDiagnosis.BestFitColumns(BestFitColumnMode.DisplayedDataCells);	
			lblSearchCount.Text = lblCount;

		}

		//private void txtBodyParts_TextChanged(object sender, EventArgs e)
		//{
		//	try
		//	{
		//		if (chkDiagnosis.Checked == true && chkBodyParts.Checked == true)
		//		{
		//			mainProcess.SearchTwoColumnTwoFieldText(dgBillDiagnosis, "Diagnosis", "[Diagnosis]", "[Body Parts]", txtBodyParts, txtDiagnosis, lblSearchCount, EmpName);
		//		}
		//		else
		//		{
		//			mainProcess.SearchTwoColumnOneFieldText(dgBillDiagnosis, "Diagnosis", "[Body Parts]", "[Remarks]", txtBodyParts, lblSearchCount, EmpName);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		var ErrorMessage = ex.Message + "\n\n Module: frmBillDiagnosis \n Process: txtBodyParts_TextChanged \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//}

		//private void txtDiagnosis_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var diagnosis = new frmModDiagnosis
			{
				btnDelete = { Visible = false },
				btnUpdateSave = { Text = "Save" },
				Text = "New Diagnosis",
				empName = EmpName
			};
			dx.GetDBID(out string ID, EmpName);
			diagnosis.txtIntID.Text = ID;
			diagnosis.txtICD10.Focus();
			diagnosis.ShowDialog();
			ShowMePTDxCodes();
		}

		private void dgBillDiagnosis_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgBillDiagnosis.SelectedRows.Count == 0)
					return;

				var selectedRow = dgBillDiagnosis.SelectedRows[0];
				var modDx = new frmModDiagnosis
				{
					txtIntID = { Text = selectedRow.Cells["Diagnosis ID"].Value?.ToString() ?? string.Empty },
					txtDiagnosis = { Text = selectedRow.Cells["ICD-10"].Value?.ToString() ?? string.Empty },
					txtICD10 = { Text = selectedRow.Cells["ICD-9"].Value?.ToString() ?? string.Empty },
					txtICD9 = { Text = selectedRow.Cells["Diagnosis"].Value?.ToString() ?? string.Empty },
					txtBodyPart = { Text = selectedRow.Cells["Body Parts"].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty }
				};

				if (accessLevel != "User")
				{
					modDx.Text = "View/Update Diagnosis Information";
					modDx.btnUpdateSave.Text = "Update";
				}
				else
				{
					modDx.Text = "View Adjuster Information";
					modDx.btnDelete.Visible = false;
					modDx.btnUpdateSave.Visible = false;
				}

				modDx.ShowDialog();
				ShowMePTDxCodes();
			}
			catch (Exception ex)
			{
				notif.LogError("dgBillDiagnosis_MouseDoubleClick", EmpName, "frmDiagnosis", null, ex);
			}
			
		}

		private void frmBillDiagnosis_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void frmBillDiagnosis_Load(object sender, EventArgs e)
		{
			dgBillDiagnosis.BestFitColumns(BestFitColumnMode.DisplayedDataCells);
			dgBillDiagnosis.ReadOnly = true;
		}

		private void dgBillDiagnosis_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Escape)
			{
				Close();
			}
		}

		private void LoadSearchandFilter()
		{
			try
			{
				DataTable resultTable = dx.SearchData(
				txtDiagnosis.Text,
				txtBodyParts.Text,
				out string searchcount, EmpName);
				dgBillDiagnosis.DataSource = resultTable;
				dgBillDiagnosis.BestFitColumns(BestFitColumnMode.DisplayedDataCells);
				lblSearchCount.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("LoadSearchandFilter", EmpName, "frmDiagnosis", null, ex);
			}
		}

		private void txtDiagnosis_TextChanged(object sender, EventArgs e)
		{
			LoadSearchandFilter();
		}


		private void txtBodyParts_TextChanged(object sender, EventArgs e)
		{
			LoadSearchandFilter();
			//if (txtDiagnosis.Text != "")
			//{
			//	task.SearchTwoColumnTwoFieldText(dgBillDiagnosis, "Diagnosis", "[Diagnosis]", "[Body Parts]", txtBodyParts, txtDiagnosis, lblSearchCount, EmpName);
			//}
			//else
			//{
			//	task.SearchTwoColumnOneFieldText(dgBillDiagnosis, "Diagnosis", "[Diagnosis]", "[Remarks]", txtBodyParts, lblSearchCount, EmpName);
			//}
		}
	}
}
