using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmViewActivityLogs : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		public string empName;
		public string accessLevel;

		public frmViewActivityLogs()
		{
			InitializeComponent();
			//FillActionCmb();
			ViewLog();
		}

		private void ViewLog()
		{
			dgActivityLogs.BestFitColumns(BestFitColumnMode.AllCells);
			const string query = "SELECT * FROM [Activity Logs] ORDER BY [Activity ID]";
			task.ViewDatagrid(dgActivityLogs, query, lblSearchCount, empName);

		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//
		//
		private void frmViewActivityLogs_Load(object sender, EventArgs e)
		{
			dgActivityLogs.BestFitColumns(BestFitColumnMode.AllCells);
			dgActivityLogs.ReadOnly = true;
		}


		private void frmViewActivityLogs_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}


		public void FillActionCmb()
		{
			cmbAction.Items.Clear();
			var query = "SELECT DISTINCT [Action] from [Activity Logs] ORDER BY [Action]";
			task.FillComboDropdown(cmbAction, query, empName);
		}

		//private void cmbAction_PopupOpening(object sender, System.ComponentModel.CancelEventArgs e)
		//{
		//	
		//}

		//private void cmbAction_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		//{
		//	
		//
		//
		//}

		private void dgActivityLogs_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dgActivityLogs.SelectedRows.Count > 0)
			{
				var readActivityLogs = new frmReadActivityLogs
				{
					txtIntID = { Text = dgActivityLogs.SelectedRows[0].Cells[0].Value + string.Empty, ReadOnly = true },
					txtTimeStamp = { Text = dgActivityLogs.SelectedRows[0].Cells[1].Value + string.Empty, ReadOnly = true },
					txtActionMake = { Text = dgActivityLogs.SelectedRows[0].Cells[2].Value + string.Empty, ReadOnly = true },
					txtAction = { Text = dgActivityLogs.SelectedRows[0].Cells[3].Value + string.Empty, ReadOnly = true },
					txtMessage = { Text = dgActivityLogs.SelectedRows[0].Cells[4].Value + string.Empty, IsReadOnly = true },
					txtDCLogs = { Text = dgActivityLogs.SelectedRows[0].Cells[5].Value + string.Empty, ReadOnly = true },
					Text = "Activity Logs"
				};
				readActivityLogs.ShowDialog();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			task.SearchTwoColumnOneFieldText(dgActivityLogs, "[Activity Logs]", "NAME", "Message", txtSearch, lblSearchCount, empName);
		}

		private void cmbAction_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			if (txtSearch.Text == "")
			{
				task.SearchTwoColumnOneFieldCombo(dgActivityLogs, "[Activity Logs]", "Action", "Action", cmbAction, lblSearchCount, empName);
			}
			else
			{
				task.SearchTwoColumnTwoFieldCombo(dgActivityLogs, "[Activity Logs]", "Name", "Action", txtSearch, cmbAction, lblSearchCount, empName);
			}
		}

		private void cmbAction_PopupOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			FillActionCmb();
		}
	}
}