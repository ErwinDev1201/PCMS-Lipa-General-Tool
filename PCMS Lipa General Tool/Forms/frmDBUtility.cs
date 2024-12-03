using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmDBUtility : Telerik.WinControls.UI.RadForm
	{

		private readonly CommonTask task = new();

		public string empName;
		public string accessLevel;


		public frmDBUtility()
		{

			var backend = new DeveloperAccess(); // Assume BackendService contains FillComboDropdown
			List<string> items = backend.FillComboDropdown(empName);
			InitializeComponent();
			PopulateDatabaseTable(items);
		}



		public void PopulateDatabaseTable(List<string> items)
		{
			try
			{
				cmbTable.Items.Clear(); // Clear existing items, if any
				foreach (var item in items)
				{
					cmbTable.Items.Add(item);
				}
			}
			catch (Exception ex)
			{

				task.LogError("PopulateDatabaseTable", empName, "frmDBUtility", null, ex);
			}
			
		}

		private void btnChangeSeq_Click(object sender, EventArgs e)
		{
			task.AlterDBSequece(txtSequence, cmbTable, empName);
			task.AddActivityLog($"Sequence of {cmbTable.Text} is changed by {empName} to {txtSequence.Text}", empName, $"{empName} change the sequence of {cmbTable.Text}", "DB CHANGED SEQUENCE");
			RadMessageBox.Show("Sequence successfully updated to " + txtSequence.Text);
			ClearData();
		}

		private void GetcurrentSequence()
		{
			var query = $"SELECT current_value FROM sys.sequences WHERE name = '{cmbTable.Text}'";
			task.GetSequenceNoPre(query, lblcurrentVal);
		}

		private void cmbTable_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			GetcurrentSequence();
		}

		private void ClearData()
		{
			cmbTable.Text = null;
			txtSequence.Text = null;
			lblcurrentVal.Text = null;
		}
	}
}