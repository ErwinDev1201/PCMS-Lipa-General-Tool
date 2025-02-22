using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmAttorneyInformation : Telerik.WinControls.UI.RadForm
	{
		private readonly Attorney atty = new();
		public string accessLevel;
		public string EmpName;
		private static readonly Notification notif = new();

		public frmAttorneyInformation()
		{
			InitializeComponent();
			ListAtty();
		}



		private void ListAtty()
		{
			var dataTable = atty.ViewAttorneyList(EmpName, out string lblCount);
			dgDefAtty.DataSource = dataTable;
			dgDefAtty.BestFitColumns(BestFitColumnMode.DisplayedCells);
			lblDefSearchCount.Text = lblCount;
		}

		private void dgDefAtty_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgDefAtty.SelectedRows.Count == 0)
					return;

				var selectedRow = dgDefAtty.SelectedRows[0];
				var modAtty = new frmModAtty
				{
					txtIntID = { Text = selectedRow.Cells["Attorney ID"].Value?.ToString() ?? string.Empty },
					cmbAttyType = { Text = selectedRow.Cells["Attorney Type"].Value?.ToString() ?? string.Empty },
					txtAttyName = { Text = selectedRow.Cells["Attorney Name"].Value?.ToString() ?? string.Empty },
					txtPhoneNo = { Text = selectedRow.Cells["Phone No."].Value?.ToString() ?? string.Empty },
					txtFaxNo = { Text = selectedRow.Cells["Fax No."].Value?.ToString() ?? string.Empty },
					txtEmailAdd = { Text = selectedRow.Cells["Email"].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty }
				};

				if (accessLevel != "User")
				{
					modAtty.Text = "View/Update Attorney Information";
					modAtty.btnUpdateSave.Text = "Update";
				}
				else
				{
					modAtty.Text = "View Adjuster Information";
					modAtty.btnDelete.Visible = false;
					modAtty.btnUpdateSave.Visible = false;
				}

				modAtty.ShowDialog();
				ListAtty();
			}
			catch (Exception ex)
			{
				notif.LogError("dgAdjusterInfo_DoubleClick", EmpName, "frmAdjusterInfo", null, ex);
			}
		}

		private void frmAttorneyInformation_Load(object sender, EventArgs e)
		{
			dgDefAtty.BestFitColumns(BestFitColumnMode.DisplayedCells);
			dgDefAtty.ReadOnly = true;
		}




		//private void txtSearchDef_TextChanged(object sender, EventArgs e)
		//{
		//	
		//
		//}



		private void frmAttorneyInformation_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var modAtty = new frmModAtty();
			modAtty.btnDelete.Visible = false;
			modAtty.btnUpdateSave.Text = "Save";
			modAtty.Text = "New Attorney";
			atty.GetDBID(out string ID, EmpName);
			modAtty.txtIntID.Text = ID;
			modAtty.cmbAttyType.Focus();
			modAtty.ShowDialog();
			modAtty.empName = EmpName;
			ListAtty();
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			LoadSearchFilter();
			///task.SearchTwoColumnOneFieldText(dgDefAtty, "[Attorney Information]", "[Attorney Name]", "[Attorney Name]", txtSearch, lblDefSearchCount, EmpName);
		}

		private void LoadSearchFilter()
		{
			try
			{
				dgDefAtty.BestFitColumns(BestFitColumnMode.DisplayedCells);
				DataTable resultTable = atty.SearchData(
					txtSearch.Text,
					cmbAttorneyOption.Text,
				out string searchcount, EmpName);
				dgDefAtty.DataSource = resultTable;
				lblDefSearchCount.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("LoadSearchFilter", EmpName, "frmAttorneyInformation", null, ex);
			}
		}

		private void cmbAttorneyOption_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			LoadSearchFilter();
			//if (cmbAttorneyOption.Text == "All" || txtSearch.Text == null)
			//{
			//	ListAtty();
			//}
			//else if (cmbAttorneyOption.Text == "All" && txtSearch.Text != "")
			//{
			//	task.(dgDefAtty, "[Attorney Information]", "[Attorney Name]", "[Attorney Name]", txtSearch, lblDefSearchCount, EmpName);
			//}
			//if (txtSearch.Text != null && cmbAttorneyOption.Text != null)
			//{
			//	task.SearchTwoColumnTwoFieldCombo(dgDefAtty, "[Attorney Information]", "[Attorney Name]", "[Attorney Type]", txtSearch, cmbAttorneyOption, lblDefSearchCount, EmpName);
			//}
			//else if (txtSearch.Text == null && cmbAttorneyOption.Text != null)
			//{
			//	task.SearchTwoColumnOneFieldCombo(dgDefAtty, "[Attorney Information]", "[Attorney Type]", "[Attorney Type]", cmbAttorneyOption, lblDefSearchCount, EmpName);
			//}
		}

		//private void cmbAttorneyOption_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		//{
		//	try
		//	{
		//		if (cmbAttorneyOption.Text == "All" || txtSearchDef.Text == null)
		//		{
		//			ListAtty();
		//		}
		//		else if (cmbAttorneyOption.Text == "All" && txtSearchDef.Text != "")
		//		{
		//			mainProcess.SearchTwoColumnOneFieldText(dgDefAtty, "[Attorney Information]", "[Attorney Name]", "[Attorney Name]", txtSearchDef, lblDefSearchCount, EmpName);
		//		}
		//		if (txtSearchDef.Text != null && cmbAttorneyOption.Text != null)
		//		{
		//			mainProcess.SearchTwoColumnTwoFieldCombo(dgDefAtty, "[Attorney Information]", "[Attorney Name]", "[Attorney Type]", txtSearchDef, cmbAttorneyOption, lblDefSearchCount, EmpName);
		//		}
		//		else if (txtSearchDef.Text == null && cmbAttorneyOption.Text != null)
		//		{
		//			mainProcess.SearchTwoColumnOneFieldCombo(dgDefAtty, "[Attorney Information]", "[Attorney Type]", "[Attorney Type]", cmbAttorneyOption, lblDefSearchCount, EmpName);
		//		}
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		var ErrorMessage = ex.Message + "\n\n Name: " + EmpName + "\n Module: frmAttorneyInformation \n Process: txtDefSearch_TextChanged \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//
		//}
	}
}
