using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;



namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmPantry : RadForm
	{
		public string _empName;
		public string _accessLevel;
		private readonly Pantry pantry = new();
		private static readonly Notification notif = new();
		private static readonly FEWinForm fe = new();
		private readonly User user = new();
		private readonly ActivtiyLogs log = new();



		public frmPantry()
		{
			InitializeComponent();
			AutoCompleteCombo();
			LoadPantryListwithFilter();
		}



		private void DefaultFields()
		{
			// Set default configurations for all users
			
			btnNew.Enabled = true;
			cmbProductList.Enabled = false;
			txtPrice.Enabled = false;
			txtIntID.Enabled = false;
			btnCancelFilter.Enabled = true;
			txtSummary.Enabled = true;
			txtTotalPrice.Enabled = true;
			txtQuantity.Enabled = false;
			txtRemarks.Enabled = false;
			cmbProductList.Text = "";
			dgPantryList.Enabled = true;
			btnAdditem.Visible = false;
			btnAdditem.Text = "Add Item";
			btnCancel.Visible = false;
			lblstatus.Text = "Ready";
			btnRemove.Visible = false;
			dtpFrom.Enabled = true;
			dtpTo.Enabled = true;
			btnNew.Visible = true;
			//FillProductDropdown();
			//FillEmployeeDropdown(cmbEmployee);
			LoadPantryListwithFilter();
			txtPrice.Text = "";
			txtIntID.Text = "";
			txtQuantity.Text = "";
			txtRemarks.Text = "";
			cmbProductList.Text = "";
			cmbEmployee.Text = _empName;
			cmbItemEmpList.Text = _empName;
			lblstatus.Text = "Ready";
			dgPantryList.ReadOnly = true;

			// Apply specific settings for certain employees
			if (_empName == "Edimson Escalona" || _empName == "Erwin Alcantara")
			{
				cmbProductList.Enabled = false;
				txtPrice.ReadOnly = true;
				txtSummary.IsReadOnly = true;
				txtTotalPrice.IsReadOnly = true;
				txtRemarks.IsReadOnly = true;
				txtTotalPrice.Enabled = true;
				btnExport.Enabled = true;
				btnExport.Visible = true;
				cmbEmployee.Enabled = true;
				cmbItemEmpList.Enabled = true;
				lblEmployeetoUpdate.Visible = true;

			}
			else
			{
				//cmbItemEmpList.Visible = false;
				btnExport.Enabled = false;
				btnExport.Visible = false;
				cmbEmployee.Visible = true;
				cmbEmployee.Enabled = false;
				cmbItemEmpList.Visible = false;
				btnCancelFilter.Height = 79;
				lblEmployeetoUpdate.Visible = false;
				btnCancelFilter.Location = new System.Drawing.Point(385, 35);
			}
		}

		private void FillProductDropdown()
		{
			List<string> items = pantry.GetProductList(_empName);
			cmbProductList.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbProductList.Items.Add(item);
			}
		}


		private void AutoCompleteCombo()
		{
			cmbProductList.AutoCompleteMode = AutoCompleteMode.Suggest;
			cmbProductList.DropDownListElement.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;
			cmbEmployee.AutoCompleteMode = AutoCompleteMode.Suggest;
			cmbEmployee.DropDownListElement.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;
			cmbItemEmpList.AutoCompleteMode = AutoCompleteMode.Suggest;
			cmbItemEmpList.DropDownListElement.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;
			//cmbProductList.DropDownListElement.AutoCompleteAppend.LimitToList = true;
		}



		private void btnNew_Click(object sender, EventArgs e)
		{
			cmbProductList.Enabled = true;
			txtIntID.Enabled = true;
			txtPrice.Enabled = true;
			txtQuantity.Enabled = true;
			txtRemarks.Enabled = true;
			txtRemarks.IsReadOnly = false;
			cmbProductList.Text = "";
			txtPrice.ReadOnly = false;
			btnAdditem.Visible = true;
			btnRemove.Enabled = false;
			btnCancel.Visible = true;
			btnCancelFilter.Enabled = false;
			btnAdditem.Text = "Add Item";
			btnAdditem.Enabled = true;
			btnCancel.Enabled = false;
			dtpFrom.Enabled = false;
			dtpTo.Enabled = false;
			btnCancel.Enabled = true;
			txtQuantity.Text = "1";
			btnNew.Enabled = false;
			btnExport.Enabled = false;
			cmbEmployee.Enabled = false;
			cmbProductList.ReadOnly = false;
			txtQuantity.ReadOnly = false;
			Clear();
			if (_empName == "Edimson Escalona" || _empName == "Erwin Alcantara")
			{
				cmbItemEmpList.Text = _empName;
				cmbEmployee.Text = _empName;
			}
			else
			{
				cmbEmployee.Text = _empName;
				cmbItemEmpList.Text = _empName;
				cmbItemEmpList.Visible = false;
				cmbEmployee.Enabled = false;

			}
			dgPantryList.Enabled = false;
			pantry.GetDBListID(out string ID, _empName);
			txtIntID.Text = ID;

		}

		

		private void Clear()
		{
			txtSummary.Text = ""; ;
			txtTotalPrice.Text = "";
			cmbProductList.Text = "";
			txtPrice.Text = "";
			txtRemarks.Text = "";
			//cmbItemEmpList.Text = "";
		}



		public void FillEmployeeDropdown(RadDropDownList cmbList)
		{
			cmbList.Items.Clear();
			List<string> items = user.GetEmployeeList(_empName);
			 // Clear existing items, if any
			foreach (var item in items)
			{
				cmbList.Items.Add(item);
			}
		}

		private void Multiply()
		{

			bool isAValid = decimal.TryParse(txtPrice.Text, out decimal a);
			bool isBValid = int.TryParse(txtQuantity.Text, out int b);
			if (isAValid && isBValid)
				txtTotalPrice.Text = (a * b).ToString();
			else
				txtTotalPrice.Text = "";
		}

		private void ProcessPantryItem()
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtQuantity.Text) ||
					string.IsNullOrWhiteSpace(txtPrice.Text) ||
					string.IsNullOrWhiteSpace(txtTotalPrice.Text))
				{
					RadMessageBox.Show("Cannot save your item, please select the product and enter the quantity.",
						"Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
					return;
				}

				string action = btnAdditem.Text == "Update" ? "Update" : "Create";
				bool isUpdate = action == "Update";

				if (isUpdate &&
					DialogResult.Yes != RadMessageBox.Show("Are you sure you want to update this record?",
					"Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					return;
				}

				bool isSuccess = pantry.PantryListDBRequest(
					action,
					txtIntID.Text,
					cmbItemEmpList.Text,
					cmbProductList.Text,
					int.TryParse(txtQuantity.Text, out int quantity) ? quantity : 0,
					decimal.TryParse(txtPrice.Text, out decimal price) ? price : 0,
					decimal.TryParse(txtTotalPrice.Text, out decimal totalPrice) ? totalPrice : 0,
					txtSummary.Text,
					txtRemarks.Text,
					_empName,
					out string message);

				string status = isSuccess ? "Success" : "Failed";
				fe.SendToastNotifDesktop(message, status);

				// Resetting the UI after processing
				dtpFrom.Value = DateTime.Now;
				LoadPantryListwithFilter();
				ListPantryNoChangeinName();
				DefaultFields();
			}
			catch (FormatException ex)
			{
				notif.LogError("ProcessPantryItem", _empName, "frmPantry", null, ex);
				fe.SendToastNotifDesktop($"Invalid input format", "Error");
				//RadMessageBox.Show($"Invalid input format: {ex.Message}", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			catch (Exception ex)
			{
				notif.LogError("ProcessPantryItem", _empName, "frmPantry", null, ex);
				fe.SendToastNotifDesktop($"An unexpected error occurred", "Error");
			}
		}


		private void btnAdditem_Click(object sender, EventArgs e)
		{
			ProcessPantryItem();
		}



		private void txtQuantity_TextChanged(object sender, EventArgs e)
		{
			Multiply();
			RefreshSummary();
		}

		private void RefreshSummary()
		{
			if (txtQuantity.Text == "")
			{
				txtSummary.Text = "";
			}
			else
			{
				txtSummary.Text = $@"{txtQuantity.Text} Piece(s)
of
{cmbProductList.Text}";
			}

		}

		private void cmbProductList_PopupOpening(object sender, CancelEventArgs e)
		{
			FillProductDropdown();
		}

		public void SumPantryExpense()
		{
			try
			{
				pantry.CalculateTotalPantryExpense(cmbEmployee.Text, dtpFrom.Value, dtpTo.Value, out decimal totalPrice);
				txtTotalPrice.Text = totalPrice.ToString();
				txtSummary.Enabled = true;
				txtSummary.Text = $@"Total List is
₱ {txtTotalPrice.Text}";
			}
			catch (Exception ex)
			{
				notif.LogError("SumPantryExpense", _empName, "frmPantry", null, ex); 
			}
		}

		private void LoadPantryListwithFilter()
		{
			try
			{
				if (string.IsNullOrWhiteSpace(cmbEmployee.Text))
				{
					cmbEmployee.Text = _empName;
				}
				dgPantryList.BestFitColumns(BestFitColumnMode.DisplayedCells);
				pantry.ViewPantryList(dgPantryList, "withFilter", lblsearchCount, _empName, cmbEmployee.Text, dtpFrom.Value, dtpTo.Value);
				SumPantryExpense();
			}
			catch (Exception ex)
			{
				notif.LogError("LoadPantryListwithFilter", _empName, "frmPantry", null, ex);
			}
		}


		private void cmbProductList_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{

			try
			{

				//process.ProductListIndex(txtPrice, query);
				//pantry.FillItemPrice(out string txtPrice, cmbProductList.Text, _empName);
				string price = pantry.FillItemPrice(cmbProductList.Text, _empName);
				txtPrice.Text = price;
				if (txtQuantity.Text != "")
				{
					txtSummary.Text = $@"{txtQuantity.Text} Piece(s)
of
{cmbProductList.Text}";
					Multiply();
				}
				else
				{
					txtSummary.Text = cmbProductList.Text;
					txtQuantity.Focus();
					Multiply();
				}
			}
			catch (Exception ex)
			{
				notif.LogError("cmbProductList_SelectedIndexChanged", _empName, "frmPantry", null, ex);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DefaultFields();
		}

		private void cmbEmployee_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			LoadPantryListwithFilter();
		}

		private void btnMgeproduct_Click(object sender, EventArgs e)
		{
			var dlgManageProduct = new frmManageproduct
			{
				Text = "Manage Product"
			};
			dlgManageProduct.ShowDialog();
		}

		 


		public DataTable GetDataTableFromRadGridView(RadGridView gridView)
		{
			DataTable dataTable = new();

			// Add columns
			foreach (GridViewDataColumn column in gridView.Columns)
			{
				if (!string.IsNullOrEmpty(column.HeaderText))
				{
					dataTable.Columns.Add(column.HeaderText, column.DataType);
				}
			}

			// Add rows
			foreach (GridViewRowInfo row in gridView.Rows)
			{
				if (!row.IsVisible) continue; // Skip hidden rows

				DataRow dataRow = dataTable.NewRow();
				foreach (GridViewDataColumn column in gridView.Columns)
				{
					if (!string.IsNullOrEmpty(column.HeaderText) && dataTable.Columns.Contains(column.HeaderText))
					{
						dataRow[column.HeaderText] = row.Cells[column.Name]?.Value ?? DBNull.Value;
					}
				}
				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
		}

		private async void btnExport_Click(object sender, EventArgs e)
		{
			try
			{
				DisableAll();
				lblstatus.Text = "Creating Spreadsheets and Collecting Data...";

				await Task.Delay(1000);

				ExportService exportService = new();
				string filePath = exportService.LoadExportXcel(dgPantryList, dtpFrom.Value, dtpTo.Value, _empName);

				if (string.IsNullOrEmpty(filePath))
					throw new Exception("Failed to generate export file.");

				lblstatus.Text = "Preparing Email Content and checking attachment...";
				await Task.Delay(1000);

				string dtpFromValue = dtpFrom.Value.ToShortDateString();
				string dtpToValue = dtpTo.Value.ToShortDateString();

				string mailSubject = $"Pantry List from {dtpFromValue} - {dtpToValue}";
				string recipientEmail = pantry.GetRecipientEmail(_empName);

				string mailContent = @$"Hi,
Attached is the Extracted Pantry List from {dtpFromValue} - {dtpToValue}

Regards,
Tm Pantry Store
via PCMS Lipa General Tool";

				lblstatus.Text = $"Sending email to {recipientEmail}...";
				await Task.Delay(1000);

				Console.WriteLine($"Email: {recipientEmail}, Mail Subjet: {mailSubject}, File Path: {filePath}, Content: {mailContent}");

				bool isSent = pantry.SendEmailWithAttachment(mailContent, filePath, mailSubject, recipientEmail);

				if (isSent)
				{
					log.AddActivityLog($"{_empName} exported {mailSubject}  to {recipientEmail}", _empName, "Pantry", "Export");
					fe.SendToastNotifDesktop($"Email sent to {recipientEmail}", "Success");
					await Task.Delay(1000);
					lblstatus.Text = $"Spreadsheet attached and sent to {recipientEmail}.";
				}
				else
				{
					lblstatus.Text = "Failed to send email.";
				}
				LoadPantryListwithFilter();
				EnableAll();
				DefaultFields();
			}
			catch (Exception ex)
			{
				notif.LogError("btnExport_Click", _empName, "frmPantry", null, ex);
			}
		}

		private void DisableAll()
		{
			btnNew.Enabled = false;
			dgPantryList.ReadOnly = false;
			grpItems.Enabled = false;
			dgPantryList.Enabled = false;
			btnAdditem.Enabled = false;
			btnCancel.Enabled = false;
			cmbProductList.Enabled = false;
			txtPrice.Enabled = false;
			txtRemarks.Enabled = false;
			txtSummary.Enabled = false;
			txtQuantity.Enabled = false;
			txtTotalPrice.Enabled = false;
			btnRemove.Enabled = false;
			btnAdditem.Enabled = false;
			btnRemove.Enabled = false;
			btnExport.Enabled = false;
			dtpFrom.Enabled = false;
			dtpTo.Enabled = false;
			btnCancel.Enabled = false;
			txtSummary.Text = "";
			txtQuantity.Text = "";
			txtRemarks.Text = "";
			cmbProductList.Text = "";
			cmbEmployee.Enabled = false;
			btnNew.Visible = true;
		}

		private void EnableAll()
		{
			btnNew.Enabled = true;
			dgPantryList.ReadOnly = false;
			grpItems.Enabled = true;
			dgPantryList.Enabled = true;
			btnAdditem.Enabled = true;
			btnCancel.Enabled = true;
			cmbProductList.Enabled = true;
			txtPrice.Enabled = true;
			txtRemarks.Enabled = true;
			txtSummary.Enabled = true;
			txtQuantity.Enabled = true;
			txtTotalPrice.Enabled = true;
			btnRemove.Enabled = true;
			btnAdditem.Enabled = true;
			btnRemove.Enabled = true;
			btnExport.Enabled = true;
			dtpFrom.Enabled = true;
			dtpTo.Enabled = true;
			btnCancel.Enabled = true;
			txtSummary.Text = "";
			txtQuantity.Text = "";
			txtRemarks.Text = "";
			cmbProductList.Text = "";
			cmbEmployee.Enabled = true;
			btnNew.Visible = true;
		}

		//
		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				bool isSuccess = pantry.PantryListDBRequest(
					"Delete",
					txtIntID.Text,
					cmbItemEmpList.Text,
					cmbProductList.Text,
					int.Parse(txtQuantity.Text),
					decimal.Parse(txtPrice.Text),
					decimal.Parse(txtTotalPrice.Text),
					txtSummary.Text,
					txtRemarks.Text,
					_empName,
					out string message);
				if (isSuccess)
				{
					fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					fe.SendToastNotifDesktop(message, "Failed");
				}
				DefaultFields();
				LoadPantryListwithFilter();
			}

		}

		private void AutoFillUp()
		{
			try
			{
				bool isAdmin = _empName is "Edimson Escalona" or "Erwin Alcantara" or "Dimz Escalona";

				// Common UI updates
				grpItems.Enabled = true;
				cmbEmployee.Enabled = false;
				cmbProductList.Enabled = true;
				txtPrice.Enabled = true;
				txtQuantity.Enabled = true;
				txtTotalPrice.Enabled = true;
				txtSummary.Enabled = true;
				txtRemarks.Enabled = true;
				btnCancel.Visible = true;
				btnCancel.Enabled = true;
				dtpFrom.Visible = true;
				dtpTo.Visible = true;

				if (isAdmin)
				{
					// Admin-specific UI updates
					txtPrice.ReadOnly = false;
					cmbItemEmpList.Visible = true;
					cmbItemEmpList.ReadOnly = false;
					btnRemove.Visible = true;
					btnRemove.Enabled = true;
					btnAdditem.Enabled = true;
					btnAdditem.Text = "Update";
					btnAdditem.Visible = true;
					btnNew.Enabled = false;
				}
				else
				{
					// Non-admin-specific UI updates
					SetReadOnly(true, txtPrice, txtQuantity, txtTotalPrice, txtSummary, txtRemarks);
					cmbProductList.ReadOnly = true;
					btnAdditem.Visible = false;
					btnRemove.Visible = false;
					btnCancel.Width = 366;
					btnCancel.Location = new System.Drawing.Point(22, 467);
					txtRemarks.Height = 94;
					btnAdditem.Text = "Update";
				}

				// Populate fields if a row is selected
				if (dgPantryList.SelectedRows.Count > 0)
				{
					var selectedRow = dgPantryList.SelectedRows[0];
					txtIntID.Text = selectedRow.Cells["List ID"].Value?.ToString();
					cmbItemEmpList.Text = selectedRow.Cells["Employee Name"].Value?.ToString();
					cmbProductList.Text = selectedRow.Cells["Product"].Value?.ToString();
					txtPrice.Text = selectedRow.Cells["Price"].Value?.ToString();
					txtQuantity.Text = selectedRow.Cells["Quantity"].Value?.ToString();
					txtTotalPrice.Text = selectedRow.Cells["Total Price"].Value?.ToString();
					txtSummary.Text = selectedRow.Cells["Summary"].Value?.ToString();
					txtRemarks.Text = selectedRow.Cells["Remarks"].Value?.ToString();
				}
			}
			catch (Exception ex)
			{
				notif.LogError("AutoFillUp", _empName, "frmPantry", null, ex);
			}
		}

		/// <summary>
		/// Sets the ReadOnly property for multiple controls.
		/// </summary>
		/// <param name="isReadOnly">Whether the controls should be read-only.</param>
		/// <param name="controls">The controls to update.</param>
		private void SetReadOnly(bool isReadOnly, params Control[] controls)
		{
			foreach (var control in controls)
			{
				switch (control)
				{
					case RadTextBox txtBox:
						txtBox.ReadOnly = isReadOnly;
						break;
					case RadTextBoxControl txtBoxControl:
						txtBoxControl.IsReadOnly = isReadOnly;
						break;
					default:
						control.Enabled = !isReadOnly;
						break;
				}
			}
		}
		private void dtpFrom_ValueChanged(object sender, EventArgs e)
		{
			if (dtpFrom.Value.ToString("MMMM") == "February")

			{
				dtpTo.Value = dtpFrom.Value.AddDays(12);
			}
			else if (dtpFrom.Value.ToString("MMMM") == "April" || dtpFrom.Value.ToString("MMMM") == "June" || dtpFrom.Value.ToString("MMMM") == "September" || dtpFrom.Value.ToString("MMMM") == "November")
			{
				dtpTo.Value = dtpFrom.Value.AddDays(14);
			}
			else
			{
				dtpTo.Value = dtpFrom.Value.AddDays(15);
			}
			LoadPantryListwithFilter();


		}

		private void DgFormatting()
		{

			//GridViewDateTimeColumn date = (GridViewDateTimeColumn)this.dgPantryList.Columns["Date"];
			//date.FormatString = "{0:yyyy-MM-dd}";  //day-month-yea
			//GridViewDateTimeColumn time = (GridViewDateTimeColumn)this.dgPantryList.Columns["Time Stamp"];
			//time.FormatString = "{0:hh:mm:ss tt}";  //day-month-yea
		}

		private void frmPantry_Load(object sender, EventArgs e)
		{
			dtpFrom.Value = DateTime.Now;
			cmbEmployee.Text = _empName;
			DefaultFields();
			dgPantryList.BestFitColumns(BestFitColumnMode.DisplayedCells);
		}

		private void dgPantryList_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			AutoFillUp();
		}

		private void btnCancelFilter_Click(object sender, EventArgs e)
		{
			dtpFrom.Value = DateTime.Now;
			cmbEmployee.Text = _empName;
			//cmbItemEmpList.Visible = false;
			if (_empName == "Edimson Escalona" || _empName == "Erwin Alcantara")
			{
				cmbEmployee.Enabled = true;
				cmbItemEmpList.Visible = true;
			}
		}


		private void ListPantryNoChangeinName()
		{
			pantry.ViewPantryList(dgPantryList, "nofilter", lblsearchCount, _empName, cmbEmployee.Text, dtpFrom.Value, dtpTo.Value);
			SumPantryExpense();
		
		}

		private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			{
				e.Handled = true;
			}
		}

		private void cmbItemEmpList_PopupOpening(object sender, CancelEventArgs e)
		{
			FillEmployeeDropdown(cmbItemEmpList);
		}

		private void cmbEmployee_PopupOpening(object sender, CancelEventArgs e)
		{
			FillEmployeeDropdown(cmbEmployee);
		}
	}

}
