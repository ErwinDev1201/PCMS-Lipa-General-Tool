using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmManageproduct : Telerik.WinControls.UI.RadForm
	{
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		private readonly Pantry pantry = new();

		public string empName;
		public string accessLevel;

		public frmManageproduct()
		{
			InitializeComponent();
			DefaultItem();
		}

		private void ShowAllUserAccess()
		{
			dgPantryProduct.BestFitColumns(BestFitColumnMode.AllCells);
			var dataTable = pantry.ViewProductList(empName, out string lblCount);
			dgPantryProduct.DataSource = dataTable;
			lblSearchCount.Text = lblCount;
		}

		private void DefaultItem()
		{
			txtIntID.Visible = true;
			txtIntID.Enabled = false;
			txtIntID.ReadOnly = true;
			txtPrice.Enabled = false;
			txtRemarks.Enabled = false;
			txtProductName.Enabled = false;
			btnDelete.Enabled = false;
			btnSave.Enabled = false;
			btnCancel.Enabled = false;
			dgPantryProduct.ReadOnly = true;
			lblalert.Visible = false;
			//txtSearch.Enabled = true;
			btnNew.Enabled = true;
			dgPantryProduct.BestFitColumns(BestFitColumnMode.AllCells);
			ShowAllUserAccess();
			paneltable.Enabled = true;
		}

		private void DisableInput()
		{ 
			txtIntID.Enabled = false;
			txtIntID.ReadOnly = true;
			txtPrice.Enabled = false;
			txtRemarks.Enabled = false;
			txtProductName.Enabled = false;
			btnDelete.Enabled = false;
			btnSave.Enabled = false;
			btnCancel.Enabled = false;
			dgPantryProduct.Enabled = false;
			//txtSearch.Enabled = true;
			btnNew.Enabled = false;
			dgPantryProduct.BestFitColumns(BestFitColumnMode.AllCells);
			ShowAllUserAccess();
			paneltable.Enabled = false;
		}


		private void Clear()
		{
			txtIntID.Clear();
			txtIntID.Clear();
			txtPrice.Clear();
			txtRemarks.Clear();
			txtProductName.Clear();
			//mainProcess.CreateDbId(txtIntID, Sql, @"PD-");
		}


		private void DoubleClickEnable()
		{
			txtPrice.Enabled = true;
			txtRemarks.Enabled = true;
			txtProductName.Enabled = true;
			btnCancel.Enabled = true;
			btnSave.Enabled = true;
			btnSave.Text = "Update";
			btnDelete.Enabled = true;
			btnNew.Enabled = false;
			paneltable.Enabled = true;
			txtIntID.Enabled = true;

		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			txtIntID.Enabled = true;
			txtPrice.Enabled = true;
			txtRemarks.Enabled = true;
			txtProductName.Enabled = true;
			txtProductName.Focus();
			btnDelete.Enabled = false;
			btnCancel.Enabled = true;
			btnNew.Enabled = false;
			btnSave.Enabled = true;
			btnSave.Text = "Save";
			dgPantryProduct.ReadOnly = true;
			//mainProcess.CreateDbId(txtIntID, Sql, @"PD-");
			paneltable.Enabled = false;
			GetDBListID();

		}


		private void GetDBListID()
		{

			string nextSequence = db.GetSequenceNo("PantryProdSeq", "PD-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					txtIntID.Text = nextSequence;
				}
			}
			catch (Exception ex)
			{
				notif.LogError("GetDBListID", empName, "frmManageProduct", "N/A", ex);
			}
			///db.GetSequenceNo("textbox", "PantryProdSeq", txtIntID.Text, null, "PD -");
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (btnSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					bool isSuccess = pantry.PantryProductDBRequest("Update", txtIntID.Text, txtProductName.Text, txtPrice.Text, txtRemarks.Text, empName, out string message);
					if (isSuccess)
					{
						fe.SendToastNotifDesktop(message, "Success");
					}
					else
					{
						fe.SendToastNotifDesktop(message, "Failed");
					}

				}
			}
			else
			{
				if (txtProductName.Text == "")
				{
					RadMessageBox.Show("Missing Product Name", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
				}
				else if (txtPrice.Text == "")
				{
					RadMessageBox.Show("Price is Missing", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
				}
				else
				{
					pantry.CheckProductExist(txtProductName.Text, empName, out string checkmessage);
					if (checkmessage != null)
					{
						fe.SendToastNotifDesktop(checkmessage, "Warning");
					}
					else
					{
						bool isSuccess = pantry.PantryProductDBRequest("Create", txtIntID.Text, txtProductName.Text, txtPrice.Text, txtRemarks.Text, empName, out string message);
						if (isSuccess)
						{
							fe.SendToastNotifDesktop(message, "Success");
						}
						else
						{
							fe.SendToastNotifDesktop(message, "Error");
						}

					}
					

					//pantry.CheckProductExist(txtIntID.Text, txtProductName.Text, txtPrice.Text, txtRemarks.Text, empName);
				}
			}
			ShowAllUserAccess();
			Clear();
			DefaultItem();
			txtProductName.Focus();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				bool isSuccess = pantry.PantryProductDBRequest("Delete", txtIntID.Text, txtProductName.Text, txtPrice.Text, txtRemarks.Text, empName, out string message);
				if (isSuccess)
				{
					fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					fe.SendToastNotifDesktop(message, "Failed");
				}
			}
			Clear();
			ShowAllUserAccess();
			DefaultItem();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Clear();
			DefaultItem();
			btnNew.Enabled = true;
		}

		private void dgPantryProduct_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			AutoFill();
		}

		private void AutoFill()
		{
			
			if (dgPantryProduct.SelectedRows.Count == 0)
				return;

			var selectedRow = dgPantryProduct.SelectedRows[0];
			txtIntID.Text = selectedRow.Cells["Product ID"].Value?.ToString() ?? string.Empty;
			txtProductName.Text = selectedRow.Cells["Product Name"].Value?.ToString() ?? string.Empty;
			txtPrice.Text = selectedRow.Cells["Price"].Value?.ToString() ?? string.Empty;
			txtRemarks.Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty;
			DoubleClickEnable();
		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}

		private void frmManageproduct_Load(object sender, EventArgs e)
		{
			DefaultItem();
		}

		private void frmManageproduct_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			{
				e.Handled = true;
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{

			try
			{
				DataTable resultTable = pantry.SearchData(
				txtSearch.Text,
				out string searchcount, empName);

				dgPantryProduct.DataSource = resultTable;
				lblSearchCount.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("txtSearch_TextChanged", empName, "frmAdjusterInfo", null, ex);
			}
			//task.SearchTwoColumnOneFieldText(dgPantryProduct, "[Pantry Product]", "[Product Name]", "[Remarks]", txtSearch, lblSearchCount, empName);

		}

		private void txtProductName_TextChanged(object sender, EventArgs e)
		{
			if (txtProductName.TextLength > 5)
			{
				pantry.CheckProductExist(txtProductName.Text, empName, out string message);
				lblalert.Visible = true;
				lblalert.Text = message;
				//fe.SendToastNotifDesktop(message, "Warning");
			}
		}
	}
}
