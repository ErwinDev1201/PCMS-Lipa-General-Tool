using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using PCMS_Lipa_General_Tool.Class;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmITTask : Telerik.WinControls.UI.RadForm
	{
		public string _empName;
		public string _accessLevel;

		private readonly ITTaskList task = new();
		private static readonly Notification notif = new();

		public frmITTask()
		{
			InitializeComponent();
			cmbEmployeeStat.Text = "To Do";
			txtSearch.NullText = "Search Task";
		}

		private void frmITTask_Load(object sender, EventArgs e)
		{
			dgTask.BestFitColumns(BestFitColumnMode.DisplayedCells);
			dgTask.ReadOnly = true;
			ShowTaskListinUI();
		}

		private void ShowTaskListinUI()
		{
			if (string.IsNullOrEmpty(_empName) || string.IsNullOrEmpty(_accessLevel))
				return;

			dgTask.BestFitColumns(BestFitColumnMode.DisplayedCells);
			var data = task.ViewTaskList(_empName, out string lblCount, _accessLevel, cmbEmployeeStat.Text);
			dgTask.DataSource = data;
			lblSearchCount.Text = lblCount;
		}

		private void btnNewTicket_Click(object sender, EventArgs e)
		{
			task.GetDBID(out string ID, _empName);
			var frmTask = new frmModTask()
			{
				empName = _empName,
				accessLevel = _accessLevel,
				txtComment = { Enabled = false },
				btnCancel = { Location = new Point(378, 148) },
				cmbPriority = { Text = "Normal" },
				btnUpdateSave = { Text = "Save" },
				cmbStatus = { Text = "To Do", Enabled = false },
				btnDelete = { Visible = false },
				txtTaskID = { ReadOnly = true, Text = ID },
				cmbReporter = { Enabled = (_accessLevel != "Administrator" && _accessLevel != "Programmer") || cmbEmployeeStat.Enabled }

			};
			frmTask.ShowDialog();
		}


		private void dgTask_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgTask.SelectedRows.Count == 0)
					return;

				var selectedRow = dgTask.SelectedRows[0];
				var modTask = new frmModTask
				{
					txtTaskID = { Text = selectedRow.Cells["Task ID"].Value.ToString() },
					cmbCategory = { Text = selectedRow.Cells["Category"].Value.ToString() },
					cmbPriority = { Text = selectedRow.Cells["Priority"].Value.ToString() },
					txtSummary = { Text = selectedRow.Cells["Summary"].Value.ToString() },
					txtDescription = { Text = selectedRow.Cells["Description"].Value.ToString() },
					txtComment = { Text = selectedRow.Cells["Comment"].Value.ToString() },
					cmbStatus = { Text = selectedRow.Cells["Status"].Value.ToString() },
					cmbAssigne = { Text = selectedRow.Cells["Assigned To"].Value.ToString() },
					cmbReporter = { Text = selectedRow.Cells["Reporter"].Value.ToString() }
				};
				if (_empName != "Erwin Alcantara" && _empName != "Dimz Escalona")
				{
					modTask.Readonly();
				}
				else
				{
					modTask.btnUpdateSave.Text = "Update";
					modTask.btnDelete.Visible = true;
					modTask.btnCancel.Location = new Point(378, 148);
				}
				modTask.ShowDialog();
				ShowTaskListinUI();

			}
			catch (Exception ex)
			{
				notif.LogError("dgTask_MouseDoubleClick", _empName, "frmITTask", null, ex);
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			LoadSearchandFilter();
		}

		private void LoadSearchandFilter()
		{
			dgTask.BestFitColumns(BestFitColumnMode.DisplayedCells);
			var data = task.GetSearch(txtSearch.Text, cmbEmployeeStat.Text, out string lblCount, _empName, _accessLevel);
			dgTask.DataSource = data;
			lblSearchCount.Text = lblCount;
		}

		private void cmbEmployeeStat_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			LoadSearchandFilter();
		}
	}
}
