using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class FrmAdjusterinformation : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly Adjuster adj = new();
		//private readonly MailSender mailSender = new MailSender();						
		public string accessLevel;
		public string EmpName;


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


		public void FillModifyAdjusterWindow()
		{
			frmModifyAdjusterInfo modAdj = new();
			try
			{
				if (dgAdjusterInfo.SelectedRows.Count > 0)
				{
					string insID = dgAdjusterInfo.SelectedRows[0].Cells[0].Value + string.Empty;
					string insName = dgAdjusterInfo.SelectedRows[0].Cells[1].Value + string.Empty;
					string adjName = dgAdjusterInfo.SelectedRows[0].Cells[2].Value + string.Empty;
					string phoneNo = dgAdjusterInfo.SelectedRows[0].Cells[3].Value + string.Empty;
					string ext = dgAdjusterInfo.SelectedRows[0].Cells[4].Value + string.Empty;
					string faxNo = dgAdjusterInfo.SelectedRows[0].Cells[5].Value + string.Empty;
					string email = dgAdjusterInfo.SelectedRows[0].Cells[6].Value + string.Empty;
					string supervisor = dgAdjusterInfo.SelectedRows[0].Cells[7].Value + string.Empty;
					string remarks = dgAdjusterInfo.SelectedRows[0].Cells[8].Value + string.Empty;

					modAdj.txtIntID.Text = insID;
					modAdj.txtInsuranceName.Text = insName;
					modAdj.txtAdjusterName.Text = adjName;
					modAdj.txtphoneno.Text = phoneNo;
					modAdj.txtExtension.Text = ext;
					modAdj.txtFax.Text = faxNo;
					modAdj.txtEmailAdd.Text = email;
					modAdj.txtSupervisor.Text = supervisor;
					modAdj.txtRemarks.Text = remarks;

				}
			}
			catch (Exception ex)
			{
				task.LogError("FillAdjusterInfo", EmpName, "Adjuster", null, ex);
			}
		}


		private void dgAdjusterInfo_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (dgAdjusterInfo.SelectedRows.Count == 0)
					return;

				var selectedRow = dgAdjusterInfo.SelectedRows[0];
				var modAdj = new frmModifyAdjusterInfo
				{
					txtIntID = { Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty },
					txtInsuranceName = { Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty },
					txtAdjusterName = { Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty },
					txtphoneno = { Text = selectedRow.Cells[3].Value?.ToString() ?? string.Empty },
					txtExtension = { Text = selectedRow.Cells[4].Value?.ToString() ?? string.Empty },
					txtFax = { Text = selectedRow.Cells[5].Value?.ToString() ?? string.Empty },
					txtEmailAdd = { Text = selectedRow.Cells[6].Value?.ToString() ?? string.Empty },
					txtSupervisor = { Text = selectedRow.Cells[7].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells[8].Value?.ToString() ?? string.Empty }
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
				task.LogError("dgAdjusterInfo_DoubleClick", EmpName, "frmAdjusterInfo", null, ex);
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
			var modifyAdjInfo = new frmModifyAdjusterInfo
			{
				txtIntID = { ReadOnly = true },
				btnDelete = { Visible = false },
				btnUpdateSave = { Text = "Save" },
				Text = "New Adjuster Information",
				empName = EmpName
			};
			modifyAdjInfo.txtInsuranceName.Focus();
			modifyAdjInfo.GetDBID();
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
			task.SearchTwoColumnOneFieldText(dgAdjusterInfo, "[Adjuster Information]", "[Insurance Name]", "[Remarks]", txtSearch, lblCountSearch, EmpName);
		}
	}
}
