﻿using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmAssignProvider : Telerik.WinControls.UI.RadForm
	{
		private readonly Provider provider = new();
		private readonly User user = new();
		private static readonly Notification notif = new();
		private static readonly Database db = new();
		private static readonly FEWinForm fe = new();

		public string txtID;
		public string empName;


		public frmAssignProvider()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
			dgAssignedProvider.ReadOnly = true;
			LoadDefaults();
		}

		public void GetDBListID()
		{
			string nextSequence = db.GetSequenceNo("AssignProvider", "AP-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					txtIntID.Text = nextSequence;
				}
			}
			catch (Exception ex)
			{
				notif.LogError("GetDBListID", empName, "frmAssignProvider", "N/A", ex);
			}

			//db.GetSequenceNo("textbox", "PantryListSeq", txtIntID.Text, null, "PL-");
		}
		//public void GetDBID()
		//{
		//
		//	db.GetSequenceNo("textbox", "AssignProvider", txtIntID.Text, null, "AP-");
		//}

		public void LoadDefaults()
		{
			ClearData();
			PullValueforDropdown();
			LoadDatabasetoTable();
			GetDBListID();

		}

		public void LoadDatabasetoTable()
		{
			var dataTable = provider.ViewProviderAssignee(empName, out string lblCount);

			dgAssignedProvider.DataSource = dataTable;

			lblCountResult.Text = lblCount;
		}

		private void ClearData()
		{
			txtIntID.Clear();
			cmbEmployeeName.Text = "Select Employee";
			cmbProviderName.Text = "Select Provider";
			txtRemarks.Clear();
			GetDBListID();
		}


		private void btnSave_Click(object sender, EventArgs e)
		{

			if (string.IsNullOrWhiteSpace(cmbEmployeeName.Text) || string.IsNullOrWhiteSpace(cmbProviderName.Text))
			{
				RadMessageBox.Show("Please select Employee or Provider in the Dropdown", "Warning", MessageBoxButtons.OK, RadMessageIcon.Error);
				return;
			}

			string operationType = btnSave.Text == "Save" ? "Create" : "Patch";
			bool isSuccess = provider.AssignProvider(
				operationType,
				txtIntID.Text,
				cmbProviderName.Text,
				cmbEmployeeName.Text,
				txtRemarks.Text,
				empName,
				out string message
			);

			fe.SendToastNotifDesktop(message, isSuccess ? "Success" : "Failed");

			ClearData();
			LoadDefaults();

		}


		private void frmModifyEmailformat_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			bool isSuccess = provider.AssignProvider("Delete", txtIntID.Text, cmbProviderName.Text, cmbEmployeeName.Text, txtRemarks.Text, empName, out string message);
			fe.SendToastNotifDesktop(message, isSuccess ? "Success" : "Failed");
		}

		private void dgAssignedProvider_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			PullDataFromTabletoTextBox();
		}

		private void DoubleClickEnable()
		{
			cmbEmployeeName.Enabled = true;
			cmbProviderName.Enabled = true;
			txtRemarks.Enabled = true;
			txtIntID.Enabled = false;
		}

		private void PullDataFromTabletoTextBox()
		{
			DoubleClickEnable();
			btnSave.Text = "Update";
			try
			{
				if (dgAssignedProvider.SelectedRows.Count == 0)
					return;

				var selectedRow = dgAssignedProvider.SelectedRows[0];

				txtIntID.Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty;
				cmbEmployeeName.Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty;
				cmbProviderName.Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty;
				txtRemarks.Text = selectedRow.Cells[6].Value?.ToString() ?? string.Empty;
				
			}
			catch (Exception ex)
			{
				notif.LogError("PullDataFromTabletoTextBox", empName, "frmAssignProvider", "N/A", ex);
			}
		}

		private void PullValueforDropdown()
		{
			cmbEmployeeName.Items.Clear();
			FillEmployeeDropdown();
			cmbProviderName.Items.Clear();
			FillProviderDropdown();
			
		}


		private void FillProviderDropdown()
		{
			List<string> items = provider.GetProviderList(empName);
			cmbProviderName.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbProviderName.Items.Add(item);
			}
		}

		private void FillEmployeeDropdown()
		{
			List<string> items = user.GetEmployeeList(empName);
			cmbEmployeeName.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbEmployeeName.Items.Add(item);
			}
		}
	}
}
