using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmSuggestion : Telerik.WinControls.UI.RadForm
	{
		public string EmpName;
		public string accessLevel;
		readonly WinDiscordAPI dc = new();

		public frmSuggestion()
		{
			InitializeComponent();
		}


		private void btnSendSuggestion_Click(object sender, EventArgs e)
		{
			dc.PublishtoDiscord("PCMS Lipa General Tool", "Suggestion From " + EmpName, txtSuggestion.Text, "", "https://discord.com/api/webhooks/1069454422150750339/moaqxm8foLT5qanroGQuyj5aDeVmOCvyyua7j9fC_mm3fsHHcSIRWoMKerL844av_nX9", "https://discord.gg/C7ssm9ze");
			RadMessageBox.Show("Thank you for you suggestion, Erwin will take a review of the possibility of the suggestion and implement it.\n Keep Suggesting to help our product to improve.", "Thank you");
			this.Close();
		}

		private void frmSuggestion_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
