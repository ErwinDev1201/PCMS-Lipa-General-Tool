using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls;
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System.Data.SqlClient;

namespace PCMS_Lipa_General_Tool
{
    public partial class frmModTask : Telerik.WinControls.UI.RadForm
    {
		public string empName;
		public string accessLevel;
		private static readonly Notification notif = new();
		private readonly ActivtiyLogs log = new();
		private readonly ITTaskList task = new();
		private readonly FEWinForm fe = new();
		private readonly User user = new();


		public frmModTask()
        {
            InitializeComponent();
		}

		private void radPanel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void frmModTask_Load(object sender, EventArgs e)
		{
			txtTaskID.ReadOnly = true;
			txtSummary.NullText = "Enter a brief Summary of Task";
			txtDescription.NullText = "Enter a detailed Description of Task";
		}

		public void FillEmployeeDropdown()
		{
			List<string> items = user.GetEmployeeList(empName);
			cmbReporter.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbReporter.Items.Add(item);
			}
		}

		public void Readonly()
		{
			txtTaskID.ReadOnly = true;
			txtSummary.ReadOnly = true;
			txtDescription.ReadOnly = true;
			cmbCategory.Enabled = false;
			cmbPriority.Enabled = false;
			cmbAssigne.Enabled = false;
			cmbStatus.Enabled = false;
			cmbReporter.Enabled = false;
			btnUpdateSave.Enabled = false;
			btnDelete.Enabled = false;
			btnCancel.Enabled = false;
		}

		public void DisableInput()
		{
			txtSummary.Enabled = false;
			txtDescription.Enabled = false;
			cmbCategory.Enabled = false;
			cmbPriority.Enabled = false;
			cmbAssigne.Enabled = false;
			cmbStatus.Enabled = false;
			btnUpdateSave.Enabled = false;
			btnCancel.Enabled = false;
			btnDelete.Enabled = false;
		}

		public void ClearData()
		{
			//txtTaskID.Clear();
			task.GetDBID(out string ID, empName);
			txtTaskID.Text = ID;
			txtSummary.Clear();
			txtDescription.Clear();
			cmbCategory.SelectedIndex = -1;
			cmbPriority.SelectedIndex = -1;
			cmbAssigne.SelectedIndex = -1;
			cmbStatus.SelectedIndex = -1;
			cmbReporter.SelectedIndex = -1;
		}

		


		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			try
			{
				// Validate input fields first
				if(txtDescription.Text == "" || txtSummary.Text == "" || cmbCategory.Text == "" || cmbPriority.Text == "" || cmbAssigne.Text == "" || cmbStatus.Text == "" || cmbReporter.Text == "")
				{
					fe.SendToastNotifDesktop("Please fill out all required fields.", "Warning");
					return;
				}

				// Disable input fields during operation
				DisableInput();

				// Determine the operation type (Create or Update)
				string operationType = btnUpdateSave.Text.Equals("Update", StringComparison.OrdinalIgnoreCase) ? "Update" : "Create";

				if (operationType == "Update")
				{
					// Confirm update operation
					if (RadMessageBox.Show("Would you like to proceed with updating this task?",
										   "Confirmation",
										   MessageBoxButtons.YesNo,
										   RadMessageIcon.Question) != DialogResult.Yes)
					{
						return; // Exit if user selects "No"
					}
				}

				// Process the task request
				bool isSuccess = ProcessTaskRequest(operationType, out string message);

				// Show toast notification based on result
				fe.SendToastNotifDesktop(message, isSuccess ? "Success" : "Failed");

				// Final cleanup
				ClearData();
				Close();
			}
			catch (Exception ex)
			{
				fe.SendToastNotifDesktop($"An error occurred: {ex.Message}", "Error");
			}

		}

		private void GetITList()
		{
			cmbAssigne.Items.Clear();
			List<string> items = user.GetUserITList(empName);
			cmbAssigne.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbAssigne.Items.Add(item);
			}
		}

		private void GetEmployee()
		{
			List<string> items = user.GetEmployeeList(empName);
			cmbReporter.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbReporter.Items.Add(item);
			}
		}

		private bool ProcessTaskRequest(string operationType, out string message)
		{
			try
			{
				// Determine Created and Updated Dates based on operationType
				string createdDate = operationType.Equals("Create", StringComparison.OrdinalIgnoreCase)
					? DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")
					: string.Empty;

				string updatedDate = operationType.Equals("Update", StringComparison.OrdinalIgnoreCase)
					? DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")
					: string.Empty;

				// Ensure txtComment is only updated for 'Update' operation
				string comment = operationType.Equals("Update", StringComparison.OrdinalIgnoreCase)
					? txtComment.Text
					: string.Empty;

				// Execute Database Request
				bool result = task.TaskDBRequest(
					operationType,
					txtTaskID.Text,
					cmbCategory.Text,
					cmbPriority.Text,
					txtSummary.Text,
					txtDescription.Text,
					comment,  // Conditioned update for txtComment
					cmbAssigne.Text,
					cmbStatus.Text,
					cmbReporter.Text,
					createdDate,
					updatedDate,
					empName,
					out message
				);

				if (result)
				{
					// Logging activity after successful execution
					log.AddActivityLog(message, empName, $"Task {txtTaskID.Text} has been {operationType}ed.", operationType);

					// Sending notification
					notif.NotifyTask(empName, txtSummary.Text, txtTaskID.Text, cmbStatus.Text, txtDescription.Text, cmbReporter.Text, cmbAssigne.Text);
				}

				return result;
			}
			catch (SqlException sqlEx)
			{
				notif.LogError("ProcessTaskRequest", empName, "frmModTask", "SQL_ERROR", sqlEx);
				message = "A database error occurred while processing the task.";
				return false;
			}
			catch (Exception ex)
			{
				notif.LogError("ProcessTaskRequest", empName, "frmModTask", "GENERAL_ERROR", ex);
				message = "Something went wrong while processing the task.";
				return false;
			}
		}



		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				// Confirmation dialog before deleting
				DialogResult result = RadMessageBox.Show("Are you sure you want to delete this task?",
														 "Delete Confirmation",
														 MessageBoxButtons.YesNo,
														 RadMessageIcon.Info);

				if (result != DialogResult.Yes)
				{
					return; // Exit if user selects "No"
				}

				// Process the delete request
				bool isSuccess = ProcessTaskRequest("Delete", out string message);

				// Show result message
				fe.SendToastNotifDesktop(message, isSuccess ? "Success" : "Failed");

				if (isSuccess)
				{
					ClearData();  // Clear input fields after successful deletion
					Close();      // Close the form if needed
				}
			}
			catch (Exception ex)
			{
				notif.LogError("btnDelete_Click", empName, "frmModTask", "N/A", ex);
				//fe.SendToastNotifDesktop($"An error occurred: {ex.Message}", "Error");
			}
		}

		private void cmbReporter_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			FillEmployeeDropdown();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult result = RadMessageBox.Show(@"Are you sure you want to cancel this task?

This will only close this window pop-up.", "Cancel Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Info);
			if (result != DialogResult.Yes)
			{
				return;
			}
			Close();
		}

		private void cmbAssigne_PopupOpening(object sender, CancelEventArgs e)
		{
			GetITList();
		}

		private void cmbReporter_PopupOpening(object sender, CancelEventArgs e)
		{
			GetEmployee();
		}
	}

}
