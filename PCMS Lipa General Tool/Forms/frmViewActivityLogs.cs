using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using DiscordMessenger;
using DocumentFormat.OpenXml.Vml.Office;
using System.Collections.Generic;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmViewActivityLogs : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		readonly ActivtiyLogs activity = new();

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
			var dataTable = activity.ViewActivityLogs(empName, out string lblCount);
			dgActivityLogs.DataSource = dataTable;
			lblSearchCount.Text = lblCount;
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


		private void FillActionDropdown()
		{
			List<string> items = activity.GetListofAction(empName);
			cmbAction.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbAction.Items.Add(item);
			}
		}


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
			FillActionDropdown();
		}
	}
}