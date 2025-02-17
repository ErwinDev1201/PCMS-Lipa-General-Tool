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
        public string empName;
        public string accessLevel;
		private readonly ITTaskList task = new();
		private static readonly Notification notif = new();	


		public frmITTask()
        {
            InitializeComponent();
			//ShowTaskListinUI();
        }

		private void btnNewTicket_Click(object sender, EventArgs e)
		{
			var frmTask = new frmModTask();
			task.GetDBID(out string ID, empName);
			frmTask.txtTaskID.Text = ID;
			frmTask.empName = empName;
			frmTask.accessLevel = accessLevel;
			frmTask.btnUpdateSave.Text = "Save";
			frmTask.btnDelete.Visible = false;
			frmTask.btnCancel.Location = new Point(378, 148);
			frmTask.cmbPriority.Text = "Normal";
			frmTask.cmbReporter.Text = empName;
			frmTask.cmbStatus.Text = "To Do";
			frmTask.txtTaskID.ReadOnly = true;
			frmTask.ShowDialog();
		}

        private void ShowTaskListinUI()
		{
			
			var data = task.ViewTaskList(empName, out string lblCount);
			dgTask.DataSource = data;
			lblSearchCount.Text = lblCount;
		}

		private void frmITTask_Load(object sender, EventArgs e)
		{
			dgTask.BestFitColumns(BestFitColumnMode.AllCells);
			dgTask.ReadOnly = true;
			ShowTaskListinUI();
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
					cmbStatus = { Text = selectedRow.Cells["Status"].Value.ToString() },
					cmbAssigne = { Text = selectedRow.Cells["Assigned To"].Value.ToString() },
					cmbReporter = { Text = selectedRow.Cells["Reporter"].Value.ToString() }
				};
				if (empName != "Erwin Alcantara" && empName != "Dimz Escalona")
				{
					modTask.Readonly();
				}
				modTask.ShowDialog();
				ShowTaskListinUI();

			}
			catch (Exception ex)
			{
				notif.LogError("dgTask_MouseDoubleClick", empName, "frmITTask", null, ex);
			}
		}
	}
}
