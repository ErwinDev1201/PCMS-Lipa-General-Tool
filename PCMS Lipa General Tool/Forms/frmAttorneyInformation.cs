using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmAttorneyInformation : Telerik.WinControls.UI.RadForm
	{
		private readonly Attorney atty = new();
		private readonly CommonTask task = new CommonTask();
		public string accessLevel;
		public string EmpName;

		public frmAttorneyInformation()
		{
			InitializeComponent();
			ListAtty();
		}



		private void ListAtty()
		{
			string lblCount;
			var dataTable = atty.ViewAttorneyList(EmpName, out lblCount);

			dgDefAtty.DataSource = dataTable;

			lblDefSearchCount.Text = lblCount;
		}

		private void dgDefAtty_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dgDefAtty.SelectedRows.Count > 0)
			{
				var modAtty = new frmModifyAtty();
				atty.FillAttyEmpInfo(dgDefAtty, modAtty.txtIntID, modAtty.cmbAttyType, modAtty.txtAttyName, modAtty.txtPhoneNo, modAtty.txtFaxNo, modAtty.txtEmailAdd, modAtty.txtRemarks, EmpName);
				if (accessLevel == "User")
				{
					modAtty.Text = "View Attorney Information";
					modAtty.btnDelete.Visible = false;
					modAtty.btnUpdateSave.Visible = false;

				}
				else
				{
					modAtty.Text = "View/Update Attorney Information";
					//modAdj.btnDelete.Visible = false;
					modAtty.btnUpdateSave.Text = "Update";
				}
				modAtty.ShowDialog();
			}
			ListAtty();
		}

		private void frmAttorneyInformation_Load(object sender, EventArgs e)
		{
			dgDefAtty.BestFitColumns(BestFitColumnMode.AllCells);
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
			var modAtty = new frmModifyAtty();
			modAtty.btnDelete.Visible = false;
			modAtty.btnUpdateSave.Text = "Save";
			modAtty.Text = "New Attorney";
			modAtty.GetDBID();
			modAtty.cmbAttyType.Focus();
			modAtty.ShowDialog();
			modAtty.empName = EmpName;
			ListAtty();
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{

			task.SearchTwoColumnOneFieldText(dgDefAtty, "[Attorney Information]", "[Attorney Name]", "[Attorney Name]", txtSearch, lblDefSearchCount, EmpName);
		}

		private void cmbAttorneyOption_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			if (cmbAttorneyOption.Text == "All" || txtSearch.Text == null)
			{
				ListAtty();
			}
			else if (cmbAttorneyOption.Text == "All" && txtSearch.Text != "")
			{
				task.SearchTwoColumnOneFieldText(dgDefAtty, "[Attorney Information]", "[Attorney Name]", "[Attorney Name]", txtSearch, lblDefSearchCount, EmpName);
			}
			if (txtSearch.Text != null && cmbAttorneyOption.Text != null)
			{
				task.SearchTwoColumnTwoFieldCombo(dgDefAtty, "[Attorney Information]", "[Attorney Name]", "[Attorney Type]", txtSearch, cmbAttorneyOption, lblDefSearchCount, EmpName);
			}
			else if (txtSearch.Text == null && cmbAttorneyOption.Text != null)
			{
				task.SearchTwoColumnOneFieldCombo(dgDefAtty, "[Attorney Information]", "[Attorney Type]", "[Attorney Type]", cmbAttorneyOption, lblDefSearchCount, EmpName);
			}
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
