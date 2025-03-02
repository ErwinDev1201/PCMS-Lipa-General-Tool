using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModifyNotes : Telerik.WinControls.UI.RadForm
	{
		private readonly CollectorNotes cx = new();
		private readonly Provider provider = new();
		private readonly Notification notif = new();  
		//private readonly MailSender mailSender = new MailSender();
		public string txtID;
		public string empName;
		public string position;


		public frmModifyNotes(string empName, string position)
		{
			InitializeComponent();
			this.empName = empName;
			this.position = position;
			txtIntID.ReadOnly = true;
			PullValueforDropdown(); // provider list
		}


		
		private void ClearData()
		{
			txtIntID.Clear();
			txtRemarks.Clear();
			cmbProviderList.Items.Clear();
			txtNotes.Clear();
			txtPatientName.Clear();
			txtChartNo.Clear();
			PullValueforDropdown();


			txtIntID.Enabled = true;
			txtRemarks.Enabled = true;
			cmbProviderList.Enabled = true;
			txtNotes.Enabled = true;
			txtPatientName.Enabled = true;
			txtChartNo.Enabled = true;
			btnDelete.Enabled = true;
			btnUpdateSave.Enabled = true;
			cx.GetDBID(out string intId, empName);
			txtIntID.Text = intId;
		}

		private void DisableInput()
		{
			txtIntID.Enabled = false;
			txtRemarks.Enabled = false;
			cmbProviderList.Enabled = false;
			txtNotes.Enabled = false;
			txtPatientName.Enabled = false;
			txtChartNo.Enabled = false;
			btnDelete.Enabled = false;
			btnUpdateSave.Enabled = false;
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					cx.NoteDBRequest(
						"Update",
						txtIntID.Text,
						cmbProviderList.Text,
						txtChartNo.Text,
						txtPatientName.Text,
						txtNotes.Text,
						txtRemarks.Text,
						empName);
					
				}
			}
			else
			{
				if (string.IsNullOrEmpty(cmbProviderList.Text) || string.IsNullOrEmpty(txtChartNo.Text) || string.IsNullOrEmpty(txtPatientName.Text) || string.IsNullOrEmpty(txtNotes.Text))
				{
					RadMessageBox.Show("Oops! It looks like some important information is missing. \n Please fill in the Notes, Provider, Chart No, and Patient Name fields to proceed.", "Information", MessageBoxButtons.OK, RadMessageIcon.Info);
				}
				else
				{
					cx.NoteDBRequest(
						"Create",
						txtIntID.Text,
						cmbProviderList.Text,
						txtChartNo.Text,
						txtPatientName.Text,
						txtNotes.Text,
						txtRemarks.Text,
						empName);
				}
			}
			ClearData();
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				cx.NoteDBRequest(
					"Delete",
					txtIntID.Text,
					cmbProviderList.Text,
					txtChartNo.Text,
					txtPatientName.Text,
					txtNotes.Text,
					txtRemarks.Text,
					empName);
				ClearData();
				Close();
			}
		}

		private void frmModifyDiagnosis_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		public void PullValueforDropdown()
		{
			cmbProviderList.Items.Clear();
			if (position == "Collector")
			{
				FillProviderperCollectorDropdown();
			}
			else
			{
				FillProviderDropdown();
			}

		}

		private void txtChartNo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true; // Disallow the key if it's not a digit or control
			}
		}

		private void FillProviderDropdown()
		{
			List<string> items = provider.GetProviderList(empName);
			cmbProviderList.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbProviderList.Items.Add(item);
			}
		}

		private void FillProviderperCollectorDropdown()
		{
			List<string> items = provider.GetProviderListperCollector(empName);
			cmbProviderList.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbProviderList.Items.Add(item);
			}
		}
	}
}
