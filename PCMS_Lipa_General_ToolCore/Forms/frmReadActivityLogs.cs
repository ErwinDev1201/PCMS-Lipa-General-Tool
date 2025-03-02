using System.Windows.Forms;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmReadActivityLogs : Telerik.WinControls.UI.RadForm
	{
		//private readonly MailSender mailSender = new MailSender();
		public string txtID;
		public string empName;


		public frmReadActivityLogs()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
		}

		private void frmModifyDiagnosis_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
