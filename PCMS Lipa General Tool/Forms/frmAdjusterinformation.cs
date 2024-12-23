using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class FrmAdjusterinformation : Telerik.WinControls.UI.RadForm
	{
		private readonly Adjuster adj = new();
		//private readonly MailSender mailSender = new MailSender();						
		public string accessLevel;
		public string EmpName;
		private static readonly Error error = new();


		public FrmAdjusterinformation()
		{
			InitializeComponent();
			ShowAdjInfo();
		}

		private void ShowAdjInfo()
		{
			var dataTable = adj.ViewAdjusterList(EmpName, out string lblCount);

			dgAdjusterInfo.DataSource = dataTable;

			lblCountSearch.Text = lblCount;
		}



		private void dgAdjusterInfo_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (dgAdjusterInfo.SelectedRows.Count == 0)
					return;

				var selectedRow = dgAdjusterInfo.SelectedRows[0];
				var modAdj = new frmModAdjusterInfo
				{
					txtIntID = { Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty },
					txtInsuranceName = { Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty },
					txtAdjusterName = { Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty },
					txtphoneno = { Text = selectedRow.Cells[3].Value?.ToString() ?? string.Empty },
					txtExtension = { Text = selectedRow.Cells[4].Value?.ToString() ?? string.Empty },
					txtFax = { Text = selectedRow.Cells[5].Value?.ToString() ?? string.Empty },
					txtEmailAdd = { Text = selectedRow.Cells[6].Value?.ToString() ?? string.Empty },
					txtSupervisor = { Text = selectedRow.Cells[7].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells[8].Value?.ToString() ?? string.Empty },
					empName = EmpName
				};

				if (accessLevel != "User")
				{
					modAdj.Text = "View/Update Adjuster Information";
					modAdj.btnUpdateSave.Text = "Update";
				}
				else
				{
					modAdj.Text = "View Adjuster Information";
					modAdj.btnDelete.Visible = false;
					modAdj.btnUpdateSave.Visible = false;
				}

				modAdj.ShowDialog();
				ShowAdjInfo();
			}
			catch (Exception ex)
			{
				error.LogError("dgAdjusterInfo_DoubleClick", EmpName, "frmAdjusterInfo", null, ex);
			}
		}

		//No bugs found.

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//
		//}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var modifyAdjInfo = new frmModAdjusterInfo
			{
				txtIntID = { ReadOnly = true },
				btnDelete = { Visible = false },
				btnUpdateSave = { Text = "Save" },
				Text = "New Adjuster Information",
				empName = EmpName
			};
			modifyAdjInfo.txtInsuranceName.Focus();
			adj.GetDBID(out string ID, EmpName);
			modifyAdjInfo.txtIntID.Text = ID;
			modifyAdjInfo.ShowDialog();
			ShowAdjInfo();

		}

		private void frmAdjusterinformation_Load(object sender, EventArgs e)
		{
			dgAdjusterInfo.BestFitColumns(BestFitColumnMode.AllCells);
			//this.dgAdjusterInfo.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgAdjusterInfo.ReadOnly = true;
		}

		private void frmAdjusterinformation_KeyDown(object sender, KeyEventArgs e)
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
				DataTable resultTable = adj.SearchData(
				txtSearch.Text,
				out string searchcount, EmpName);

				dgAdjusterInfo.DataSource = resultTable;
				lblCountSearch.Text = searchcount;

			}
			catch (Exception ex)
			{
				error.LogError("txtSearch_TextChanged", EmpName, "frmAdjusterInfo", null, ex);
			}
			
			//task.SearchTwoColumnOneFieldText(dgAdjusterInfo, "[Adjuster Information]", "[Insurance Name]", "[Remarks]", txtSearch, lblCountSearch, EmpName);
		}
	}
}
