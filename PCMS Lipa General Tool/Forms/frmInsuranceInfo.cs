using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmInsuranceInfo : Telerik.WinControls.UI.RadForm
	{
		private static readonly Error error = new();
		private readonly Insurance insurance = new();
		//private readonly MailSender mailSender = new MailSender();				
		public string txtID;
		public string accessLevel;
		public string EmpName;


		public frmInsuranceInfo()
		{
			InitializeComponent();
			ShowInsInfo();

		}

		private void frmAdjusterinformation_Load(object sender, EventArgs e)
		{
			dgInsuranceInfo.BestFitColumns(BestFitColumnMode.AllCells);
			dgInsuranceInfo.ReadOnly = true;
		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}

		private void ShowInsInfo()
		{

			var dataTable = insurance.ViewInsuraceList(EmpName, out string lblCount);

			dgInsuranceInfo.DataSource = dataTable;

			lblcountSearchResult.Text = lblCount;
		}

		private void dgInsuranceInfo_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgInsuranceInfo.SelectedRows.Count == 0)
					return;

				var selectedRow = dgInsuranceInfo.SelectedRows[0];
				var modPrivIns = new frmModPrivateInfoIns
				{
					txtIntID = { Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty },
					txtInsuranceName = { Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty },
					txtInsCode = { Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty },
					txtInsuranceAddress = { Text = selectedRow.Cells[3].Value?.ToString() ?? string.Empty },
					txtPayerID = { Text = selectedRow.Cells[4].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells[8].Value?.ToString() ?? string.Empty }
				};

				if (accessLevel != "User")
				{
					modPrivIns.Text = "View/Update Adjuster Information";
					modPrivIns.btnUpdateSave.Text = "Update";
				}
				else
				{
					modPrivIns.Text = "View Adjuster Information";
					modPrivIns.btnDelete.Visible = false;
					modPrivIns.btnUpdateSave.Visible = false;
				}

				modPrivIns.ShowDialog();
				ShowInsInfo();
			}
			catch (Exception ex)
			{
				error.LogError("dgInsuranceInfo_MouseDoubleClick", EmpName, "frmInsuranceInfo", null, ex);
			}
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var dlgInsInfor = new frmModPrivateInfoIns
			{
				btnUpdateSave = { Text = "Save" },
				btnDelete = { Visible = false },
				Text = "New Insurance Info",
				empName = EmpName
			};
			dlgInsInfor.GetDBID();
			dlgInsInfor.txtInsCode.Focus();
			dlgInsInfor.ShowDialog();
			ShowInsInfo();
		}

		private void frmInsuranceInfo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			try
			{
				DataTable resultTable = insurance.SearchData(
				txtSearch.Text,
				out string searchcount, EmpName);

				dgInsuranceInfo.DataSource = resultTable;
				lblcountSearchResult.Text = searchcount;

			}
			catch (Exception ex)
			{
				error.LogError("txtSearch_TextChanged", EmpName, "frmHearingRep", null, ex);
			}
			//task.SearchTwoColumnOneFieldText(dgInsuranceInfo, "[Insurance Info]", "[Insurance Name]", "[Address]", txtSearch, lblcountSearchResult, EmpName);
		}
	}
}
