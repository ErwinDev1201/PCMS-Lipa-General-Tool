using PCMS_Lipa_General_Tool.Services;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmWCTrainingtools : Telerik.WinControls.UI.RadForm
	{
		private static readonly string wcsupport = ConfigurationManager.AppSettings["wcsupportpath"];
		//private readonly MailSender mailSender = new MailSender();

		public string empName;
		public string accessLevel;
		private static readonly Notification notif = new();


		public frmWCTrainingtools()
		{
			InitializeComponent();
		}

		private void btnTerms_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\3. Terms and Definition.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				notif.LogError("btnTerms_Click", empName, "frmWCTrainingTool", filepath, ex);
				
			}
		}

		private void btnWCProcess_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\4. Wc Process.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnWCProcess_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnFlow_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\5. Billing Flow, Denial And Forms.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnFlow_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnCorres_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\6. Correspondence.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnCorres_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}
		private void btnParPaidBills_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\7. Partially Paid Bills.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnParPaidBills_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnSBR_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\8. Sbr Appeals.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnSBR_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnPIClaims_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\Personal Injury Claim.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnPIClaims_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnCaseSettlements_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\9. Case Settlements.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnCaseSettlements_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnTypeofHearing_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\10. Types Of Hearings.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnTypeofHearing_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnRebut_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\11. Rebuttals.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnTerms_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnChroma_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\Chromatography.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnChroma_Click", empName, "frmWCTrainingTool", filepath, ex);

			}
		}

		private void btnExhibits_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\Exhibits.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnExhibits_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnDeclare_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\Lien Declaration.ppt";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnBillingColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnDeclare_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void btnMemo_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\MEMO_Collection Guideline.12.21.18.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: WorkCompWindow \n Process: btnMemoColl_Click \n\n Detailed Error: " + ex.ToString();
				notif.LogError("btnMemo_Click", empName, "frmWCTrainingTool", filepath, ex);
			}
		}

		private void frmWCTrainingtools_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}