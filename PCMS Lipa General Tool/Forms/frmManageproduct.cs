
using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmManageproduct : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
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
			task.GetSequenceNo("textbox", "PantryProdSeq", txtIntID, null, "PD -");
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (btnSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					pantry.PantryProductDBRequest("Update", txtIntID, txtProductName, txtPrice, txtRemarks, empName);
				
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
					var querycheck = $"SELECT [Product Name] FROM [Pantry Product] WHERE [Product Name] = '{txtProductName.Text}'";
					pantry.CheckProductExist(querycheck, txtIntID, txtProductName, txtPrice, txtRemarks, empName);
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
				pantry.PantryProductDBRequest("Delete", txtIntID, txtProductName, txtPrice, txtRemarks, empName);
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
			DoubleClickEnable();
			if (dgPantryProduct.SelectedRows.Count > 0)
			{
				pantry.FillProductInfo(dgPantryProduct, txtIntID, txtProductName, txtPrice, txtRemarks, empName);
			}
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
			task.SearchTwoColumnOneFieldText(dgPantryProduct, "[Pantry Product]", "[Product Name]", "[Remarks]", txtSearch, lblSearchCount, empName);
		
		}
	}
}
