using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmWCTool : Telerik.WinControls.UI.RadForm
	{
		private static readonly string wcsupport = ConfigurationManager.AppSettings["wcsupportpath"];
		private static readonly Notification notif = new();

		public string empName;
		public string accessLevel;

		public frmWCTool()
		{
			InitializeComponent();
		}


		private void btnStateofBar_Click(object sender, EventArgs e)
		{
			Process.Start(
				"http://members.calbar.ca.gov/fal/LicenseeSearch/QuickSearch?ResultType=0&SearchType=0&SoundsLike=False");
		}

		private void btnEAMSEarch_Click(object sender, EventArgs e)
		{
			Process.Start("https://eams.dwc.ca.gov/WebEnhancement/RequesterInformationCaptureScreen.jsp");
		}

		private void btnWCInq_Click(object sender, EventArgs e)
		{
			Process.Start("https://caworkcompcoverage.com/Search");
		}

		private void btnEAMSAdmin_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.dir.ca.gov/ftproot/eamsclaimsadmins.txt");
		}

		private void btnICD10_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.icd10data.com/");
		}

		private void btnPharmacyFS_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.dir.ca.gov/dwc/pharmfeesched/PFS.asp");
		}

		private void btnEAMSInt_Click(object sender, EventArgs e)
		{
			Process.Start("iexplore.exe", "https://eams.dwc.ca.gov/external/logon.jsp");
			//https://eams.dwc.ca.gov/WebEnhancement/
		}

		private void btnOAPractiseMate_Click(object sender, EventArgs e)
		{
			Process.Start("https://pm.officeally.com/PM/Login.aspx?ReturnUrl=%2fpm");
		}

		private void btnFeeSched_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\WC Fee Sched by Mayor.xltm";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				notif.LogError("btnFeeSched_Click", empName, "frmWCTool", "N/A", ex);
			}
		}

		private void btnICD9_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.icd9data.com/");
		}

		private void btnInterest_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.webmath.com/simpinterest.html");
		}

		private void btnLienClaim_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.dir.ca.gov/ftproot/EAMSLienClaimants.txt");
		}

		private void btnApproveMPN_Click(object sender, EventArgs e)
		{
			var filepath = wcsupport + @"\ListApprovedMPN.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				notif.LogError("btnApproveMPN_Click", empName, "frmWCTool", "N/A", ex);
			}
		}

		private void btnCCHISearch_Click(object sender, EventArgs e)
		{
			Process.Start("https://cchi.learningbuilder.com/Public/PractitionerLookup/Search");
		}

		private void frmWCTool_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}

}
