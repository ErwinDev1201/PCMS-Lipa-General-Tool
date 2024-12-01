using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmBillDiagnosis : Telerik.WinControls.UI.RadForm
	{
		private readonly Diagnosis dx = new();
		private readonly CommonTask task = new();
		public string EmpName;
		public string accessLevel;

		public frmBillDiagnosis()
		{
			InitializeComponent();
			ShowMePTDxCodes();
			//chkBodyParts.Checked = true;
		}

		private void ShowMePTDxCodes()
		{
			string lblCount;
			var dataTable = dx.ViewDxList(EmpName, out lblCount);

			dgBillDiagnosis.DataSource = dataTable;

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
			var diagnosis = new frmModifyDiagnosis
			{
				btnDelete = { Visible = false },
				btnUpdateSave = { Text = "Save" },
				Text = "New Diagnosis",
				empName = EmpName
			};
			diagnosis.GetDBID();
			diagnosis.txtICD10.Focus();
			diagnosis.ShowDialog();
			ShowMePTDxCodes();
		}

		private void dgBillDiagnosis_MouseDoubleClick(object sender, MouseEventArgs e)
		{

			if (dgBillDiagnosis.SelectedRows.Count > 0)
			{
				var dlgModDx = new frmModifyDiagnosis();
				dx.FillBillDiagnosisInfo(dgBillDiagnosis, dlgModDx.txtIntID, dlgModDx.txtICD10, dlgModDx.txtICD9, dlgModDx.txtDiagnosis, dlgModDx.txtBodyPart, dlgModDx.txtRemarks, EmpName);
				if (accessLevel == "User")
				{
					dlgModDx.btnDelete.Visible = false;
					dlgModDx.btnUpdateSave.Visible = false;
					dlgModDx.Text = "View Diagnosis Info";

				}
				else
				{
					dlgModDx.btnUpdateSave.Text = "Update";
					dlgModDx.Text = "View/Modify Diagnosis Info";
				}
				dlgModDx.ShowDialog();

			}
			ShowMePTDxCodes();
			
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
			dgBillDiagnosis.BestFitColumns(BestFitColumnMode.AllCells);
			dgBillDiagnosis.ReadOnly = true;
		}

		private void dgBillDiagnosis_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Escape)
			{
				Close();
			}
		}

		private void txtDiagnosis_TextChanged(object sender, EventArgs e)
		{
			if (txtBodyParts.Text != "")
			{
				task.SearchTwoColumnTwoFieldText(dgBillDiagnosis, "Diagnosis", "[Diagnosis]", "[Body Parts]", txtBodyParts, txtDiagnosis, lblSearchCount, EmpName);
			}
			else
			{
				task.SearchTwoColumnOneFieldText(dgBillDiagnosis, "Diagnosis", "[Diagnosis]", "[Remarks]", txtBodyParts, lblSearchCount, EmpName);
			}
		}


		private void txtBodyParts_TextChanged(object sender, EventArgs e)
		{
			if (txtDiagnosis.Text != "")
			{
				task.SearchTwoColumnTwoFieldText(dgBillDiagnosis, "Diagnosis", "[Diagnosis]", "[Body Parts]", txtBodyParts, txtDiagnosis, lblSearchCount, EmpName);
			}
			else
			{
				task.SearchTwoColumnOneFieldText(dgBillDiagnosis, "Diagnosis", "[Diagnosis]", "[Remarks]", txtBodyParts, lblSearchCount, EmpName);
			}
		}
	}
}
