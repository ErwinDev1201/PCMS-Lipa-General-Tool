
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmOnlineLogins : Telerik.WinControls.UI.RadForm
	{
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		private readonly OnlineLogins onlineLogin = new();

		//private readonly MailSender mailSender = new MailSender();		
		public string txtLogNo;
		public string txtID;
		public string empName;
		public string accessLevel;
		public string officeLoc;
		public bool TextWasChanged = false;

		public frmOnlineLogins()
		{
			InitializeComponent();
			ShowPasswordEye();
			UserAccessDefault();
			///CountLogins();			
			//chkShowPassword.Checked = true;
			dgOnlineLogins.ReadOnly = true;
			//txtPassword.Visibility = Visibility.Hidden;
			//mainProcess.CreateDbId(txtlogID, auditID, @"AL-");
		}

		private void ShowPasswordEye()
		{
			this.txtPassword.UseSystemPasswordChar = true;
			RadButtonElement btn = new();
			btn.Click += btn_Click;
			this.txtPassword.RightButtonItems.Add(btn);
			btn.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
			btn.BorderElement.Visibility = ElementVisibility.Collapsed;
			var font1 = ThemeResolutionService.GetCustomFont("Font Awesome 5 Free Regular");
			btn.CustomFont = font1.Name;
			btn.CustomFontSize = 5;
			//btn.Text = "\ue13D";
			btn.Text = "\ue052";
		}

		private void btn_Click(object sender, EventArgs e)
		{
			this.txtPassword.UseSystemPasswordChar = !this.txtPassword.UseSystemPasswordChar;
		}

		private void UserAccessDefault()
		{
			chkUpdateDiscord.Visible = false;
			if (accessLevel == "Programmer")
			{
				chkUpdateDiscord.Enabled = true;
				btnNew.Enabled = true;
			}
			else if (accessLevel == "Management" || accessLevel == "Administrator")
			{
				btnNew.Enabled = true;
				chkUpdateDiscord.Visible = false;
			}
			else if (accessLevel == "User" || accessLevel == "Power User")
			{
				btnNew.Enabled = false;
				chkUpdateDiscord.Visible = false;
			}
			btnSaveUpdate.Enabled = false;
			btnDelete.Enabled = false;
			btnCancel.Enabled = false;
			btnOpenLink.Enabled = false;
			txtLoginID.Enabled = false;
			txtInsuranceName.Enabled = false;
			txtWebLink.Enabled = false;
			txtaccntOwner.Enabled = false;
			txtPassword.Enabled = false;
			txtUsername.Enabled = false;
			dgOnlineLogins.Enabled = true;
			//txtSearchLogins.Enabled = true;
			txtRemarks.Enabled = false;
			//txtSearchLogins.Clear();
			chkUpdateDiscord.IsChecked = true;
			//chkUpdateDiscord.Enabled = false;
			//dgOnlineLogins.Enabled = false;
			cmbBrowser.Enabled = false;
			if (officeLoc == "SAN DIMAS")
			{
				cmbBrowser.Visible = false;
				txtRemarks.Height = 56;
				lblbrowsertouse.Visible = false;
			}
			ShowDataUserAccess();

		}


		private void ShowDataUserAccess()
		{
			dgOnlineLogins.BestFitColumns(BestFitColumnMode.DisplayedDataCells);
			var dataTable = onlineLogin.ViewOnlineLogins(empName, out string lblCount);

			dgOnlineLogins.DataSource = dataTable;
			dgOnlineLogins.Columns["Password"].IsVisible = false;
			lblSearchCount.Text = lblCount;
		}
		

		private void DoubleClickEnable()
		{
			if (accessLevel == "User")
			{
				txtLoginID.Enabled = false;
				txtInsuranceName.Enabled = true;
				txtWebLink.Enabled = true;
				txtaccntOwner.Enabled = true;
				txtUsername.Enabled = true;
				txtPassword.Enabled = true;
				txtRemarks.Enabled = true;
				btnNew.Enabled = false;
				//chkShowPassword.Enabled = true;
				//chkShowPassword.IsChecked = false;
				btnSaveUpdate.Enabled = false;
				btnSaveUpdate.Text = "Update";
				btnDelete.Enabled = false;
				btnCancel.Enabled = true;
				cmbBrowser.Enabled = false;
				btnOpenLink.Enabled = true;
				btnDelete.Enabled = false;
			}
			else if (accessLevel == "Power User")
			{
				txtLoginID.Enabled = false;
				txtInsuranceName.Enabled = true;
				txtaccntOwner.Enabled = true;
				txtWebLink.Enabled = true;
				txtUsername.Enabled = true;
				txtPassword.Enabled = true;
				txtRemarks.Enabled = true;
				btnNew.Enabled = false;
				//chkShowPassword.IsChecked = false;
				//chkShowPassword.Enabled = true;
				btnSaveUpdate.Enabled = true;
				cmbBrowser.Enabled = true;
				btnSaveUpdate.Text = "Update";
				btnCancel.Enabled = true;
				btnOpenLink.Enabled = true;
				btnDelete.Enabled = false;
			}
			else
			{
				txtLoginID.Enabled = false;
				txtInsuranceName.Enabled = true;
				txtaccntOwner.Enabled = true;
				txtWebLink.Enabled = true;
				txtUsername.Enabled = true;
				txtPassword.Enabled = true;
				txtRemarks.Enabled = true;
				btnNew.Enabled = false;
				//chkShowPassword.IsChecked = false;
				//chkShowPassword.Enabled = true;
				cmbBrowser.Enabled = true;
				btnSaveUpdate.Enabled = true;
				btnSaveUpdate.Text = "Update";
				btnCancel.Enabled = true;
				btnOpenLink.Enabled = true;
				btnDelete.Enabled = true;
			}
		}


		//private void CountLogins()
		//{
		//	try
		//	{
		//		string _concount = "SELECT COUNT(*) FROM [ONLINE LOGINS]";
		//		int countLogin = mainProcess.CountRows(_concount);
		//		lblCount.Text = countLogin.ToString();
		//	}
		//	catch (Exception ex)
		//	{
		//		//mailSender.SendEmail(ex.Message +"\n\n Name: " + lblName.Content + "\n Module: DemoOnlineLogins \n Process: CountLogins \n\n Detailed Error: " + ex.ToString());
		//		var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\n Module: DemoOnlineLogins \n Process: CountLogins \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//
		//}

		private void ClearforSearchFunc()
		{
			txtLoginID.Clear();
			txtInsuranceName.Clear();
			txtWebLink.Clear();
			txtUsername.Clear();
			txtPassword.Clear();
			txtRemarks.Clear();
			txtaccntOwner.Clear();
			//chkShowPassword.IsChecked = false;
		}

		private void Clear()
		{
			txtLoginID.Clear();
			txtInsuranceName.Clear();
			txtWebLink.Clear();
			txtUsername.Clear();
			txtPassword.Clear();
			txtRemarks.Clear();
			//txtSearchLogins.Clear();
			//txtSearchLogins.Focus();
			txtaccntOwner.Clear();
			//chkShowPassword.IsChecked = false;
			//mainProcess.CreateDbId(txtLoginID, onlineID, @"OLID-");
			//mainProcess.CreateDbId(txtlogID, auditID, @"AL-");
		}

		private void AutoFill()
		{
			try
			{
				if (dgOnlineLogins.SelectedRows.Count > 0)
				{
					//txtEmpID = { Text = selectedRow.Cells["Employee ID"].Value?.ToString() ?? string.Empty },
					txtLoginID.Text = dgOnlineLogins.SelectedRows[0].Cells["Login ID"].Value + string.Empty;
					txtInsuranceName.Text = dgOnlineLogins.SelectedRows[0].Cells["Insurance Name"].Value + string.Empty;
					txtWebLink.Text = dgOnlineLogins.SelectedRows[0].Cells["URL Link"].Value + string.Empty;
					txtUsername.Text = dgOnlineLogins.SelectedRows[0].Cells["Username"].Value + string.Empty;
					txtPassword.Text = dgOnlineLogins.SelectedRows[0].Cells["Password"].Value + string.Empty;
					txtaccntOwner.Text = dgOnlineLogins.SelectedRows[0].Cells["Account Owner"].Value + string.Empty;
					txtRemarks.Text = dgOnlineLogins.SelectedRows[0].Cells["Remarks"].Value + string.Empty;
					cmbBrowser.Text = dgOnlineLogins.SelectedRows[0].Cells["Browser"].Value + string.Empty;
				}
			}
			catch (Exception ex)
			{

				error.LogError($"AutoFill", empName, "Online Logins", txtLoginID.Text, ex);
				RadMessageBox.Show($"Error during updating text field information. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			DoubleClickEnable();
		}

			



		private void btnNew_Click(object sender, EventArgs e)
		{
			Clear();
			txtLoginID.Enabled = false;
			txtInsuranceName.Enabled = true;
			txtWebLink.Enabled = true;
			txtaccntOwner.Enabled = true;
			txtUsername.Enabled = true;
			txtPassword.Enabled = true;
			//chkShowPassword.IsChecked = true;
			//chkShowPassword.Enabled = true;
			txtRemarks.Enabled = true;
			btnNew.Enabled = false;
			btnSaveUpdate.Enabled = true;
			btnOpenLink.Enabled = false;
			btnSaveUpdate.Text = "Save";
			btnDelete.Enabled = false;
			btnCancel.Enabled = true;
			cmbBrowser.Enabled = true;
			btnOpenLink.Enabled = false;
			//txtSearchLogins.Enabled = false;
			dgOnlineLogins.Enabled = false;
			txtInsuranceName.Focus();
			onlineLogin.GetDBLoginID(out string ID, empName);
			txtLoginID.Text = ID;
			//mainProcess.CreateDbId(txtLoginID, onlineID, @"OLID-");
		}

		

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (btnSaveUpdate.Text == "Update")
			{
				if (RadMessageBox.Show(
					"Would you like to go ahead and update this record?",
					"Confirmation",
					MessageBoxButtons.YesNo,
					RadMessageIcon.Question
				) == DialogResult.Yes)
				{
					bool isSuccess = onlineLogin.OnlineLoginDB(
						"Update",
						txtLoginID.Text,
						txtInsuranceName.Text,
						txtWebLink.Text,
						txtUsername.Text,
						txtPassword.Text,
						txtRemarks.Text,
						txtaccntOwner.Text,
						cmbBrowser.Text,
						chkUpdateDiscord.Checked,
						empName,
						out string message);
					if (isSuccess)
					{
						fe.SendToastNotifDesktop(message, "Success");
					}
					else
					{
						fe.SendToastNotifDesktop(message, "Failed");
					};
				}
			}
			else
			{
				// Validate input fields
				if (ValidateInputs())
				{
					bool isSuccess = onlineLogin.OnlineLoginDB(
						"Create",
						txtLoginID.Text,
						txtInsuranceName.Text,
						txtWebLink.Text,
						txtUsername.Text,
						txtPassword.Text,
						txtRemarks.Text,
						txtaccntOwner.Text,
						cmbBrowser.Text,
						chkUpdateDiscord.Checked,
						empName,
						out string message);
					if (isSuccess)
					{
						fe.SendToastNotifDesktop(message, "Success");
					}
					else
					{
						fe.SendToastNotifDesktop(message, "Failed");
					};

				}
			}
			Clear();
			ShowDataUserAccess();
			UserAccessDefault();
			txtInsuranceName.Focus();
		}

		/// <summary>
		/// Validates user inputs and shows appropriate error messages.
		/// </summary>
		/// <returns>True if all inputs are valid; otherwise, false.</returns>
		/// 
		private bool ValidateInputs()
		{
			if (string.IsNullOrEmpty(txtInsuranceName.Text))
			{
				RadMessageBox.Show("Missing Insurance Name", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
				return false;
			}

			if (string.IsNullOrEmpty(txtWebLink.Text))
			{
				RadMessageBox.Show("Missing Website Link", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
				return false;
			}

			if (officeLoc == "Lipa" && string.IsNullOrEmpty(cmbBrowser.Text))
			{
				RadMessageBox.Show("Please Select Browser to use", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
				return false;
			}

			return true;
		}

		//private void btnSave_Click(object sender, EventArgs e)
		//{
		//	if (btnSaveUpdate.Text == "Update")
		//	{
		//		if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
		//		{
		//			onlineLogin.OnlineLoginDB("Update", txtLoginID, txtInsuranceName, txtWebLink, txtUsername, txtPassword, txtRemarks, txtaccntOwner, cmbBrowser, chkUpdateDiscord, empName);
		//			ShowDataUserAccess();
		//			UserAccessDefault();
		//			//RadMessageBox.Show("Online Login Access updated successfully", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//			Clear();
		//		}
		//	}
		//	else
		//	{
		//		if (string.IsNullOrEmpty(txtInsuranceName.Text))
		//		{
		//			RadMessageBox.Show("Missing Insurance Name", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//			return;
		//		}
		//		if (string.IsNullOrEmpty(txtWebLink.Text))
		//		{
		//			RadMessageBox.Show("Missing Website Link", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//			return;
		//		}
		//		if (officeLoc == "Lipa" && string.IsNullOrEmpty(cmbBrowser.Text))
		//		{
		//			RadMessageBox.Show("Please Select Browser to use", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//			return;
		//		}
		//		try
		//		{
		//			onlineLogin.OnlineLoginDB("Create", txtLoginID, txtInsuranceName, txtWebLink, txtUsername, txtPassword, txtRemarks, txtaccntOwner, cmbBrowser, chkUpdateDiscord, empName);
		//			Clear();
		//			ShowDataUserAccess();
		//			UserAccessDefault();
		//			txtInsuranceName.Focus();
		//		}
		//		catch (Exception ex)
		//		{
		//			//mailSender.
		//			//(ex.Message +"\n\n Name: " + lblName.Content + "\n Module: DemoOnlineLogins \n Process: btnUpdate_Click \n\n Detailed Error: " + ex.ToString());
		//			var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\n Module: DemoOnlineLogins \n Process: btnUpdate_Click \n\n Detailed Error: " + ex.ToString();
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//		}
		//	}
		//}
		//


		private void btnOpenLink_Click(object sender, EventArgs e)
		{
			if (txtWebLink.Text == "")
			{
				RadMessageBox.Show("URL Link is Empty", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
			}
			else
			{
				if (officeLoc == "LIPA" && cmbBrowser.Text != "Local Browser")
				{
					RadMessageBox.Show("Please use Browser from RD Web (pcmsbilling) to open this sites");
					//var remotePCMessage = "I'm using a PCMS Lipa remote PC to access websites that are only available in the US.";
					////winDiscordAPI.PublishtoDiscord("PCMS Lipa General Tool - Remote PC", "", remotePCMessage, empName, "https://discord.com/api/webhooks/1091534659579551744/3Nb2cboGirv3OCAQ-xhImXI9ck3uJ35JbTHLU34iJ9-EK7MgY3cUbT_uXBtNM28Pwhvx", "https://discord.gg/4Ns5NJaW");
					//Process.Start(_remotePc);
				}
				else
				{
					if (accessLevel == "Programmer")
					{
						Process.Start(txtWebLink.Text);
					}
					else
					{
						try
						{
							Process.Start("chrome.exe", txtWebLink.Text);
						}
						catch (Exception)
						{
							Process.Start(txtWebLink.Text);
						}
					}
				}
				var AuditTextContent = empName + " open the link for " + txtInsuranceName.Text;
				log.AddActivityLog(AuditTextContent, empName, AuditTextContent, "OPEN ONLINE LOGIN");

			}
			UserAccessDefault();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				bool isSuccess = onlineLogin.OnlineLoginDB(
						"Delete",
						txtLoginID.Text,
						txtInsuranceName.Text,
						txtWebLink.Text,
						txtUsername.Text,
						txtPassword.Text,
						txtRemarks.Text,
						txtaccntOwner.Text,
						cmbBrowser.Text,
						chkUpdateDiscord.Checked,
						empName,
						out string message);
				if (isSuccess)
				{
					fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					fe.SendToastNotifDesktop(message, "Failed");
				};
				ShowDataUserAccess();
				UserAccessDefault();
				Clear();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			UserAccessDefault();
			Clear();
		}

		//private void txtSearchLogins_TextChanged(object sender, EventArgs e)
		//{
		//	ClearforSearchFunc();
		//	try
		//	{
		//		//chkShowPassword.IsChecked = false;
		//		//var query = "SELECT DAID, INSURANCE, [WEB LINK], BROWSER, USERNAME, [ACCOUNT OWNER], REMARKS FROM [ONLINE LOGINS] WHERE INSURANCE LIKE '%" + txtSearchLogins.Text + "%' OR [WEB LINK] LIKE '%" + txtSearchLogins.Text + "%' OR USERNAME LIKE '%" +
		//		//	txtSearchLogins.Text + "%' OR REMARKS LIKE '%" +
		//		//txtSearchLogins.Text + "%'";
		//		//mainProcess.SearchDatagrid(dgOnlineLogins, query);
		//		//mainProcess.SearchTwoColumnOneFieldText(dgOnlineLogins, "[ONLINE LOGINS]", "[Insurance Name]", "[Remarks]", txtSearchLogins, lblCount, empName);
		//	}
		//	catch (Exception ex)
		//	{
		//		//mailSender.SendEmail(ex.Message +"\n\n Name: " + lblName.Content + "\n Module: DemoOnlineLogins \n Process: txtSearchOL_TextChanged \n\n Detailed Error: " + ex.ToString());
		//		var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\n Module: DemoOnlineLogins \n Process: txtSearchOL_TextChanged \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//}

		private void dgOnlineLogins_MouseDoubleClick(object sender, MouseEventArgs e)
		{

			AutoFill();
		
		}   







		private void dgOnlineLogins_KeyUp(object sender, KeyEventArgs e)
		{
			//DisplayOnlineLoginsInfoInControls();
		}

		//private void chkShowPassword_CheckStateChanged(object sender, EventArgs e)
		//{
		//	if (chkShowPassword.Checked)
		//	{
		//		txtPassword.UseSystemPasswordChar = false;
		//	}
		//	else
		//	{
		//		txtPassword.UseSystemPasswordChar = true;
		//	}
		//}

		private void frmOnlineLogins_Load(object sender, EventArgs e)
		{
			//this.dgOnlineLogins.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			this.dgOnlineLogins.BestFitColumns(BestFitColumnMode.DisplayedCells);
			//AdjustLastColumnSize();
			//this.dgOnlineLogins.SizeChanged += dgOnlineLogins_SizeChanged;

		}

		private void dgOnlineLogins_SizeChanged(object sender, EventArgs e)
		{
			AdjustLastColumnSize();
		}

		private void AdjustLastColumnSize()
		{
			var totalColumnsWidth = 0;
			for (var index = 0; index <= this.dgOnlineLogins.Columns.Count - 2; index++)
				totalColumnsWidth += this.dgOnlineLogins.Columns[index].Width;

			var calculatedLastColWidth = this.dgOnlineLogins.Width - totalColumnsWidth -
				this.dgOnlineLogins.TableElement.RowHeaderColumnWidth - this.dgOnlineLogins.TableElement.VScrollBar.Size.Width;
			//if (calculatedLastColWidth > 0)
			//{
			//	this.dgOnlineLogins.Columns.Last().Width = calculatedLastColWidth - 5;
			//}
		}  //

		private void lblCount_KeyDown(object sender, KeyEventArgs e)
		{

		}

		private void frmOnlineLogins_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void lblCount_Click(object sender, EventArgs e)
		{

		}

		private void txtSearchOnlineLogins_TextChanged(object sender, EventArgs e)
		{
			ClearforSearchFunc();
			try
			{
				DataTable resultTable = onlineLogin.SearchData(
				txtSearchOnlineLogins.Text,
				out string searchcount, empName);

				dgOnlineLogins.DataSource = resultTable;
				lblSearchCount.Text = searchcount;
				dgOnlineLogins.Enabled = true;

			}
			catch (Exception ex)
			{
				error.LogError("txtSearch_TextChanged", empName, "frmAdjusterInfo", null, ex);
			}
			//if (txtSearchOnlineLogins.TextLength > 0)
			//{
			//	task.SearchTwoColumnOneFieldText(dgOnlineLogins, "[ONLINE LOGINS]", "[Insurance Name]", "[Remarks]", txtSearchOnlineLogins, lblSearchCount, empName);
			//	dgOnlineLogins.Enabled = true;
			//}
		}

		//private void dgOnlineLogins_CellDoubleClick(object sender, GridViewCellEventArgs e)
		//{
		//	try
		//	{
		//		// Ensure the clicked cell is valid
		//		if (e.Row == null || e.Column == null)
		//			return;
		//
		//		// Get the clicked row
		//		var selectedRow = e.Row;
		//
		//		// Populate the fields with data from the selected row
		//		txtLoginID.Text = selectedRow.Cells["LoginID"].Value?.ToString() ?? string.Empty;
		//		txtInsuranceName.Text = selectedRow.Cells["InsuranceName"].Value?.ToString() ?? string.Empty;
		//		txtWebLink.Text = selectedRow.Cells["WebLink"].Value?.ToString() ?? string.Empty;
		//		txtUsername.Text = selectedRow.Cells["Username"].Value?.ToString() ?? string.Empty;
		//		txtPassword.Text = selectedRow.Cells["Password"].Value?.ToString() ?? string.Empty;
		//		txtaccntOwner.Text = selectedRow.Cells["AccountOwner"].Value?.ToString() ?? string.Empty;
		//		txtRemarks.Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty;
		//		cmbBrowser.Text = selectedRow.Cells["Browser"].Value?.ToString() ?? string.Empty;
		//
		//		// Trigger any additional logic you need
		//		DoubleClickEnable();
		//	}
		//	catch (Exception ex)
		//	{
		//		// Display an error message if something goes wrong
		//		MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//}
	}
}
