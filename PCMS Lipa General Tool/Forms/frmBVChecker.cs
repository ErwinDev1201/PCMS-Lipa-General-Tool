using PCMS_Lipa_General_Tool.Class;
using System;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmBVChecker : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();

		public string empName;
		public string accessLevel;
		readonly Broadvoice bv = new();

		public frmBVChecker()
		{
			InitializeComponent();
			//LoadEmployeeInformation();
		}

		private void btnCheckBV_Click(object sender, EventArgs e)
		{
			CheckAvailable();
		}

		private void CheckAvailable()
		{
			this.Height = 753;
			this.Width = 511;

			var dataTable = bv.ViewBroadvoiceList(empName, out string lblCount);

			dgAvailability.DataSource = dataTable;

			lblSearchCount.Text = lblCount;
		}

		///private void LoadEmployeeInformation()
		///{
		///	var query = "SELECT DISTINCT [BROADVOICE NO.], [Broadvoice Username], [Broadvoice Password] FROM [User Information] ORDER BY [BROADVOICE NO.]";
		///	task.ViewDatagrid(dgAvailability, query, lblSearchCount, empName);
		///}
	}
}
