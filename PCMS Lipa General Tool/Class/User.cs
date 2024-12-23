using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace PCMS_Lipa_General_Tool.Class
{
	public class User
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private static readonly string EmailHost = ConfigurationManager.AppSettings["smtpserver"];

		readonly SecurityEncryption sec = new();
		readonly emailSender mailSender = new();
		readonly ActivtiyLogs log = new();
		readonly Error error = new();
		readonly Database db = new();
		
		private string email;

		public void GetDBID(out string ID, string empName)
		{

			ID = string.Empty;

			string nextSequence = db.GetSequenceNo("UserInfoSeq", "PCMS-0");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					ID = nextSequence;
				}
			}
			catch (Exception ex)
			{
				error.LogError("GetDBID", empName, "User", "N/A", ex);
			}
			////db.GetSequenceNo("textbox", "UserInfoSeq", txtIntID.Text, null, "PCMS-0");
		}

		public void FillUserProfile(
				string txtIntID,
				out string txtName,
				out string txtUsername,
				out string cmbLevel,
				out string cmbRole,
				out string txtRDWebUsername,
				out string txtRDWebPassword,
				out string txtLytecUsername,
				out string txtLytecPassword,
				out string txtEmail,
				out string txtBroadvoice,
				out string txtDateOfBirth,
				out string dcUsername,
				out string dcPassword,
				string empName)
		{
			// Initialize all out parameters with default values
			txtIntID = txtName = txtUsername = cmbLevel = cmbRole = txtRDWebUsername = txtRDWebPassword = string.Empty;
			txtLytecUsername = txtLytecPassword = txtEmail = txtBroadvoice = txtDateOfBirth = dcUsername = dcPassword = string.Empty;

			try
			{
				using var con = new SqlConnection(_dbConnection);
				using var cmd = new SqlCommand(@"
        SELECT
            [Employee ID], [EMPLOYEE NAME], USERNAME,
            [USER ACCESS], [POSITION], [RDWEB USERNAME],
            [RDWEB PASSWORD], [LYTEC USERNAME], [LYTEC PASSWORD],
            [EMAIL ADDRESS], [Broadvoice No.], [DATE OF BIRTH],
            [Discord Username], [Discord Password]
        FROM
            [User Information]
        WHERE 
            [EMPLOYEE NAME] = @EmployeeName", con);

				cmd.Parameters.AddWithValue("@EmployeeName", empName);

				con.Open();
				using var reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					// Safely assign values to out parameters
					txtIntID = reader["Employee ID"]?.ToString() ?? string.Empty;
					txtName = reader["EMPLOYEE NAME"]?.ToString() ?? string.Empty;
					txtUsername = reader["USERNAME"]?.ToString() ?? string.Empty;
					cmbLevel = reader["USER ACCESS"]?.ToString() ?? string.Empty;
					cmbRole = reader["POSITION"]?.ToString() ?? string.Empty;
					txtRDWebUsername = reader["RDWEB USERNAME"]?.ToString() ?? string.Empty;
					txtRDWebPassword = reader["RDWEB PASSWORD"]?.ToString() ?? string.Empty;
					txtLytecUsername = reader["LYTEC USERNAME"]?.ToString() ?? string.Empty;
					txtLytecPassword = reader["LYTEC PASSWORD"]?.ToString() ?? string.Empty;
					txtEmail = reader["EMAIL ADDRESS"]?.ToString() ?? string.Empty;
					txtBroadvoice = reader["Broadvoice No."]?.ToString() ?? string.Empty;
					txtDateOfBirth = reader["DATE OF BIRTH"]?.ToString() ?? string.Empty;
					dcUsername = reader["Discord Username"]?.ToString() ?? string.Empty;
					dcPassword = reader["Discord Password"]?.ToString() ?? string.Empty;
				}
			}
			catch (Exception ex)
			{
				// Handle exceptions appropriately
				error.LogError("FillAdminUserProfile", empName, "User", txtIntID, ex);
			}
		}

		//public void FillUserProfile(
		//	string txtIntID,
		//	string txtName,
		//	string txtUsername,
		//	string cmbLevel,
		//	string cmbRole,
		//	string txtRDWebUsername,
		//	string txtRDWebPassword,
		//	string txtLytecUsername,
		//	string txtLytecPassword,
		//	string txtEmail,
		//	string txtBroadvoice,
		//	string txtDateOfBirth,
		//	string dcUsername,
		//	string dcPassword, 
		//	string empName)
		//{
		//	var query = "SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [USER ACCESS], [POSITION], [RDWEB USERNAME], [RDWEB PASSWORD], [LYTEC USERNAME], [LYTEC PASSWORD], [EMAIL ADDRESS], [Broadvoice No.], [DATE OF BIRTH], [Discord Username], [Discord Password] FROM [User Information] WHERE USERNAME='" + txtUsername + "'";
		//	try
		//	{
		//		using var connection = new SqlConnection(_dbConnection);
		//		using var command = new SqlCommand(query, connection);
		//
		//		connection.Open();
		//		using var reader = command.ExecuteReader();
		//
		//		if (reader.Read())
		//		{
		//			// Create a helper method to reduce repetitive code
		//			void SetTextBoxValue(string textBox, int columnIndex)
		//			{
		//				if (textBox != null && !reader.IsDBNull(columnIndex))
		//				{
		//					textBox = reader.GetString(columnIndex);
		//				}
		//			}
		//
		//			// Populate fields
		//			SetTextBoxValue(txtIntID, 0);
		//			SetTextBoxValue(txtName, 1);
		//			SetTextBoxValue(txtUsername, 2);
		//			SetTextBoxValue(cmbLevel, 3);
		//			SetTextBoxValue(cmbRole, 4);
		//			SetTextBoxValue(txtRDWebUsername, 5);
		//			SetTextBoxValue(txtRDWebPassword, 6);
		//			SetTextBoxValue(txtLytecUsername, 7);
		//			SetTextBoxValue(txtLytecPassword, 8);
		//			SetTextBoxValue(txtEmail, 9);
		//			SetTextBoxValue(txtBroadvoice, 10);
		//			SetTextBoxValue(txtDateOfBirth, 11);
		//			SetTextBoxValue(dcUsername, 12);
		//			SetTextBoxValue(dcPassword, 13);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		error.LogError("FillUserProfile", empName, "User", txtIntID, ex);           //task.ExecutedbCollBackupCsv(empName);
		//	}
		//}
		//


		public void FillAdminUserProfile(
			string EmpID,
			out string RDWebUsername,
			out string RDWebPassword,
			out string LytecUsername,
			out string LytecPassword,
			out string EmailPassword,
			out string BVUsername,
			out string BVPassword,
			out string PCName,
			out string PCUsername,
			out string PCPassword,
			out string Remarks,
			out string DateOfBirth,
			out string cmbDirectReport,
			out string firstTimeLogin,
			out string DCUsername,
			out string DCPassword,
			out string cmbEmploymentStatus,
			string empName,
			string popupfrom)
		{
			// Initialize out parameters with default values
			RDWebUsername = RDWebPassword = LytecUsername = LytecPassword = EmailPassword = string.Empty;
			BVUsername = BVPassword = PCName = PCUsername = PCPassword = Remarks = string.Empty;
			DateOfBirth = cmbDirectReport = firstTimeLogin = DCUsername = DCPassword = cmbEmploymentStatus = string.Empty;

			try
			{
				using var con = new SqlConnection(_dbConnection);
				using var cmd = new SqlCommand(@"
            SELECT 
                [Employee ID], [EMPLOYEE NAME], [RDWEB USERNAME], [RDWEB PASSWORD],
                [LYTEC USERNAME], [LYTEC PASSWORD], [EMAIL ADDRESS], [EMAIL PASSWORD],
                [BROADVOICE NO.], [Broadvoice Username], [Broadvoice Password],
                [PC Assigned], [PC USERNAME], [PC PASSWORD], TEAM, REMARKS,
                [DATE OF BIRTH], [Employment Status], [FIRST TIME LOGIN],
                [Discord Username], [Discord Password]
            FROM 
                [User Information]
            WHERE 
                [Employee ID] = @EmployeeID", con);

				cmd.Parameters.AddWithValue("@EmployeeID", EmpID);

				con.Open();
				using var reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					// Use GetOrdinal to map column names to indices once
					RDWebUsername = reader["RDWEB USERNAME"]?.ToString() ?? string.Empty;
					RDWebPassword = reader["RDWEB PASSWORD"]?.ToString() ?? string.Empty;
					LytecUsername = reader["LYTEC USERNAME"]?.ToString() ?? string.Empty;
					LytecPassword = reader["LYTEC PASSWORD"]?.ToString() ?? string.Empty;
					EmailPassword = reader["EMAIL PASSWORD"]?.ToString() ?? string.Empty;
					BVUsername = reader["Broadvoice Username"]?.ToString() ?? string.Empty;
					BVPassword = reader["Broadvoice Password"]?.ToString() ?? string.Empty;
					PCName = reader["PC Assigned"]?.ToString() ?? string.Empty;
					PCUsername = reader["PC USERNAME"]?.ToString() ?? string.Empty;
					PCPassword = reader["PC PASSWORD"]?.ToString() ?? string.Empty;
					Remarks = reader["REMARKS"]?.ToString() ?? string.Empty;
					DateOfBirth = reader["DATE OF BIRTH"]?.ToString() ?? string.Empty;
					cmbEmploymentStatus = reader["Employment Status"]?.ToString() ?? string.Empty;
					firstTimeLogin = reader["FIRST TIME LOGIN"]?.ToString() ?? string.Empty;
					DCUsername = reader["Discord Username"]?.ToString() ?? string.Empty;
					DCPassword = reader["Discord Password"]?.ToString() ?? string.Empty;
				}
			}
			catch (Exception ex)
			{
				error.LogError("FillAdminUserProfile", empName, "User", EmpID, ex);
			}
		}


		//try
		//{
		//	using var con = new SqlConnection(_dbConnection);
		//	using var cmd = new SqlCommand(@"
		//SELECT 
		//    [Employee ID], [EMPLOYEE NAME], [RDWEB USERNAME], [RDWEB PASSWORD],
		//    [LYTEC USERNAME], [LYTEC PASSWORD], [EMAIL ADDRESS], [EMAIL PASSWORD],
		//    [BROADVOICE NO.], [Broadvoice Username], [Broadvoice Password],
		//    [PC Assigned], [PC USERNAME], [PC PASSWORD], TEAM, REMARKS,
		//    [DATE OF BIRTH], [Employment Status], [FIRST TIME LOGIN],
		//    [Discord Username], [Discord Password]
		//FROM 
		//    [User Information]
		//WHERE 
		//    [Employee ID] = @EmployeeID", con);
		//
		//	cmd.Parameters.AddWithValue("@EmployeeID", empNoSelected.Text);
		//
		//	con.Open();
		//	using var reader = cmd.ExecuteReader();
		//
		//	if (reader.Read())
		//	{
		//		// Use a helper method to populate the UI elements
		//		void SetTextBoxValue(Control control, int columnIndex)
		//		{
		//			if (control != null && !reader.IsDBNull(columnIndex))
		//			{
		//				switch (control)
		//				{
		//					case RadTextBox textBox:
		//						textBox.Text = reader.GetString(columnIndex);
		//						break;
		//					case RadDropDownList dropDownList:
		//						dropDownList.Text = reader.GetString(columnIndex);
		//						break;
		//					case RadTextBoxControl textBoxControl:
		//						textBoxControl.Text = reader.GetString(columnIndex);
		//						break;
		//				}
		//			}
		//		}
		//
		//		// Assign values to controls
		//		SetTextBoxValue(txtIDNumber, 0);
		//		SetTextBoxValue(EmpName, 1);
		//		SetTextBoxValue(RDWEbUsername, 2);
		//		SetTextBoxValue(RDWebPassword, 3);
		//		SetTextBoxValue(LytecUsername, 4);
		//		SetTextBoxValue(LytecPassword, 5);
		//		SetTextBoxValue(EmailAddress, 6);
		//		SetTextBoxValue(EmailPassword, 7);
		//		SetTextBoxValue(BVnumber, 8);
		//		SetTextBoxValue(BVUsername, 9);
		//		SetTextBoxValue(BvPassword, 10);
		//		SetTextBoxValue(PCName, 11);
		//		SetTextBoxValue(PCUsername, 12);
		//		SetTextBoxValue(PCPassword, 13);
		//		SetTextBoxValue(cmbDirectReport, 14);
		//		SetTextBoxValue(Remarks, 15);
		//		SetTextBoxValue(DateOfBirth, 16);
		//		SetTextBoxValue(cmbEmploymentStatus, 17);
		//		SetTextBoxValue(firstTimeLogin, 18);
		//		SetTextBoxValue(DCUsername, 19);
		//		SetTextBoxValue(DCPassword, 20);
		//	}
		//}
		//catch (Exception ex)
		//{
		//	error.LogError("FillAdminUserProfile", empName, "User", txtIDNumber.Text, ex);
		//}
		//catch (Exception ex)
		//{
		//	error.LogError("FillAdminUserProfile", empName, "User", txtIDNumber.Text, ex);
		//}



		//public void FillAdminUserProfile(RadTextBox txtIDNumber, RadTextBox EmpName, RadTextBox RDWEbUsername, RadTextBox RDWebPassword, RadTextBox LytecUsername, RadTextBox LytecPassword, RadTextBox EmailAddress, RadTextBox EmailPassword, RadTextBox BVnumber, RadTextBox BVUsername, RadTextBox BvPassword, RadTextBox PCName, RadTextBox PCUsername, RadTextBox PCPassword, RadTextBoxControl Remarks, RadTextBox DateOfBirth, RadDropDownList cmbDirectReport, RadDropDownList firstTimeLogin, RadTextBox DCUsername, RadTextBox DCPassword, RadDropDownList cmbEmploymentStatus, string empName, RadTextBox empNoSelected)
		//{
		//	try
		//	{
		//		using (SqlConnection con = new SqlConnection(_dbConnection))
		//		{
		//			con.Open();
		//			using (SqlCommand cmd = new SqlCommand("SELECT [Employee ID], [EMPLOYEE NAME], [RDWEB USERNAME], [RDWEB PASSWORD], [LYTEC USERNAME], [LYTEC PASSWORD], [EMAIL ADDRESS]," +
		//				"[EMAIL PASSWORD], [BROADVOICE NO.], [Broadvoice Username], [Broadvoice Password], [PC Assigned], [PC USERNAME], [PC PASSWORD], TEAM, REMARKS, [DATE OF BIRTH], [Employment Status], [FIRST TIME LOGIN], [Discord Username], [Discord Password] FROM [User Information] WHERE [Employee ID] = '" + empNoSelected.Text + "'", con))
		//			{
		//				using (SqlDataReader reader = cmd.ExecuteReader())
		//				{
		//					if (reader.Read())
		//					{
		//						txtIDNumber.Text = (reader.IsDBNull(0) ? null : reader.GetString(0));
		//						EmpName.Text = (reader.IsDBNull(1) ? null : reader.GetString(1));
		//						RDWEbUsername.Text = (reader.IsDBNull(2) ? null : reader.GetString(2));
		//						RDWebPassword.Text = (reader.IsDBNull(3) ? null : reader.GetString(3));
		//						LytecUsername.Text = (reader.IsDBNull(4) ? null : reader.GetString(4));
		//						LytecPassword.Text = (reader.IsDBNull(5) ? null : reader.GetString(5));
		//						EmailAddress.Text = (reader.IsDBNull(6) ? null : reader.GetString(6));
		//						EmailPassword.Text = (reader.IsDBNull(7) ? null : reader.GetString(7));
		//						BVnumber.Text = (reader.IsDBNull(8) ? null : reader.GetString(8));
		//						BVUsername.Text = (reader.IsDBNull(9) ? null : reader.GetString(9));
		//						BvPassword.Text = (reader.IsDBNull(10) ? null : reader.GetString(10));
		//						PCName.Text = (reader.IsDBNull(11) ? null : reader.GetString(11));
		//						PCUsername.Text = (reader.IsDBNull(12) ? null : reader.GetString(12));
		//						PCPassword.Text = (reader.IsDBNull(13) ? null : reader.GetString(13));
		//						cmbDirectReport.Text = (reader.IsDBNull(14) ? null : reader.GetString(14));
		//						Remarks.Text = (reader.IsDBNull(15) ? null : reader.GetString(15));
		//						DateOfBirth.Text = (reader.IsDBNull(16) ? null : reader.GetString(16));
		//						firstTimeLogin.Text = (reader.IsDBNull(18) ? null : reader.GetString(18));
		//						cmbEmploymentStatus.Text = (reader.IsDBNull(17) ? null : reader.GetString(17));
		//						DCUsername.Text = (reader.IsDBNull(19) ? null : reader.GetString(19));
		//						DCPassword.Text = (reader.IsDBNull(20) ? null : reader.GetString(20));
		//					}
		//				}
		//			}
		//		}
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: DBQuery \n Process: ViewPasswordDGTextbox \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, NameofUser, Global.DCErrorWebHook, Global.DCErrorInvite);
		//		task.ExecutedbCollBackupCsv(empName);
		//	}
		//}



		//public void FillUpEmpTxtBox(RadGridView dgproviderInfo, RadTextBox txtEmpID, RadTextBox txtEmpName, RadTextBox txtEmail, RadTextBox txtBroadvoice, RadDropDownList cmbDept, RadDropDownList cmbPosition, RadDropDownList txtOffice, RadDropDownList txtStatus, RadTextBoxControl txtRemarks, string empName)
		//{
		//	using var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		con.Open();
		//		using SqlCommand cmd = new("SELECT [EMPLOYEE ID], [EMPLOYEE NAME], [EMAIL ADDRESS], [BROADVOICE NO.], [DEPARTMENT], POSITION, OFFICE, STATUS, REMARKS FROM [User Information] WHERE STATUS = 'ACTIVE'", con);
		//		cmd.ExecuteNonQuery();
		//		if (dgproviderInfo.SelectedRows.Count > 0)
		//		{
		//			var row = dgproviderInfo.SelectedRows[0];
		//			{
		//				txtEmpID.Text = row.Cells[0].Value + string.Empty;
		//				txtEmpName.Text = row.Cells[1].Value + string.Empty;
		//				txtEmail.Text = row.Cells[2].Value + string.Empty;
		//				txtBroadvoice.Text = row.Cells[3].Value + string.Empty;
		//				cmbDept.Text = row.Cells[4].Value + string.Empty;
		//				cmbPosition.Text = row.Cells[5].Value + string.Empty;
		//				txtOffice.Text = row.Cells[6].Value + string.Empty;
		//				txtStatus.Text = row.Cells[7].Value + string.Empty;
		//				txtRemarks.Text = row.Cells[8].Value + string.Empty;
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		error.LogError("FillAdminUserProfile", empName, "User", null, ex);
		//		//task.ExecutedbCollBackupCsv(empName);
		//	}
		//	finally
		//	{
		//		con.Close();
		//	}
		//
		//}
		//
		//public void FillUpUserTxtBox(
		//	RadGridView dgUser,
		//	RadTextBox txtIntID,
		//	RadTextBox txtName,
		//	RadTextBox txtUsername,
		//	RadDropDownList cmbUserAccess,
		//	RadDropDownList cmbPosition,
		//	RadDropDownList cmbUserDept,
		//	RadDropDownList cmbUserStatus,
		//	RadDropDownList cmbOffice,
		//	RadTextBox txtemail,
		//	RadTextBox bvNo,
		//	string empName)
		//{
		//	// SQL query to fetch user details
		//	var query = "SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS], [Broadvoice No.] FROM [User Information]";
		//
		//	// Open a connection and execute the query using SqlDataReader
		//	using (var con = new SqlConnection(_dbConnection))
		//	{
		//		try
		//		{
		//			con.Open();
		//			using (var cmd = new SqlCommand(query, con))
		//			{
		//				// Execute the query and retrieve the results
		//				using (var reader = cmd.ExecuteReader())
		//				{
		//					// If data is available, process the first row
		//					if (reader.HasRows)
		//					{
		//						reader.Read();
		//
		//						// Assign values from the query result to the respective controls
		//						txtIntID.Text = reader["EMPLOYEE ID"]?.ToString() ?? string.Empty;
		//						txtName.Text = reader["EMPLOYEE NAME"]?.ToString() ?? string.Empty;
		//						txtUsername.Text = reader["USERNAME"]?.ToString() ?? string.Empty;
		//						cmbUserDept.Text = reader["DEPARTMENT"]?.ToString() ?? string.Empty;
		//						cmbUserAccess.Text = reader["USER ACCESS"]?.ToString() ?? string.Empty;
		//						cmbPosition.Text = reader["POSITION"]?.ToString() ?? string.Empty;
		//						cmbUserStatus.Text = reader["STATUS"]?.ToString() ?? string.Empty;
		//						cmbOffice.Text = reader["OFFICE"]?.ToString() ?? string.Empty;
		//						txtemail.Text = reader["EMAIL ADDRESS"]?.ToString() ?? string.Empty;
		//						bvNo.Text = reader["Broadvoice No."]?.ToString() ?? string.Empty;
		//					}
		//					else
		//					{
		//						// If no rows are found, clear the fields
		//						ClearFields(txtIntID, txtName, txtUsername, cmbUserDept, cmbUserAccess, cmbPosition, cmbUserStatus, cmbOffice, txtemail, bvNo);
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			// Improved error logging with a dedicated method
		//			var errorMessage = $"{ex.Message}\n\n" +
		//					   $"Name: {empName}\nModule: DBQuery\nProcess: FillUpUserTxtBox\n\n" +
		//					   $"Detailed Error: {ex}";
		//
		//			// Publish to Discord and create backup CSV if needed
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", errorMessage, NameofUser, Global.DCErrorWebHook, Global.DCErrorInvite);
		//			task.ExecutedbCollBackupCsv(empName);
		//		}
		//		finally
		//		{
		//			// Ensure the connection is closed even if an exception occurs
		//			con.Close();
		//		}
		//	}
		//}
		//
		//// Method to clear text boxes and combo boxes
		//private void ClearFields(RadTextBox txtIntID, RadTextBox txtName, RadTextBox txtUsername, RadDropDownList cmbUserDept,
		//						  RadDropDownList cmbUserAccess, RadDropDownList cmbPosition, RadDropDownList cmbUserStatus,
		//						  RadDropDownList cmbOffice, RadTextBox txtemail, RadTextBox bvNo)
		//{
		//	txtIntID.Clear();
		//	txtName.Clear();
		//	txtUsername.Clear();
		//	cmbUserDept.SelectedIndex = -1;
		//	cmbUserAccess.SelectedIndex = -1;
		//	cmbPosition.SelectedIndex = -1;
		//	cmbUserStatus.SelectedIndex = -1;
		//	cmbOffice.SelectedIndex = -1;
		//	txtemail.Clear();
		//	bvNo.Clear();
		//}
		//
		//// Method to handle error logging
		//private void LogError(Exception ex, string empName)
		//{
		//	
		//}
		//
		//public void FillUpUserTxtBox(RadGridView dgUser, RadTextBox txtIntID, RadTextBox txtName, RadTextBox txtUsername, RadDropDownList cmbUserAccess, RadDropDownList cmbPosition, RadDropDownList cmbUserDept, RadDropDownList cmbUserStatus, RadDropDownList cmbOffice, RadTextBox txtemail, RadTextBox bvNo, string empName)
		//{
		//	//[Employee ID], [EMPLOYEE NAME], USERNAME, [GROUP], USERTYPE, ROLE, STATUS, OFFICE, [EMAIL ADDRESS]
		//	var query = "SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS], [Broadvoice No.] FROM [User Information]";
		//	using var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		con.Open();
		//		using SqlCommand cmd = new(query, con);
		//		cmd.ExecuteNonQuery();
		//		var row = dgUser.SelectedRows[0];
		//		if (dgUser.SelectedRows.Count > 0)
		//		{
		//			{
		//				txtIntID.Text = row.Cells[0].Value + string.Empty;
		//				txtName.Text = row.Cells[1].Value + string.Empty;
		//				txtUsername.Text = row.Cells[2].Value + string.Empty;
		//				cmbUserDept.Text = row.Cells[3].Value + string.Empty;
		//				cmbUserAccess.Text = row.Cells[4].Value + string.Empty;
		//				cmbPosition.Text = row.Cells[5].Value + string.Empty;
		//				cmbUserStatus.Text = row.Cells[6].Value + string.Empty;
		//				cmbOffice.Text = row.Cells[7].Value + string.Empty;
		//				bvNo.Text = row.Cells[9].Value + string.Empty;
		//				txtemail.Text = row.Cells[8].Value + string.Empty;
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		//MessageBox.Show("Error connecting Database \n \n" + ex + "\n \n" + "Inform the Programmer/Author/Developer \n Erwin Alcantara (Skype: aerwin0629; Email: Erwin@pcmsbilling.net", Global.ProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		//		error.LogError("FillUpUserTxtBox", empName, "User", null, ex);
		//	}
		//	finally
		//	{
		//		con.Close();
		//	}
		//
		//}
		//
		//// Method to clear text boxes and combo boxes



		//public void FillUpUserTxtBox(RadGridView dgUser, RadTextBox txtIntID, RadTextBox txtName, RadTextBox txtUsername, RadDropDownList cmbUserAccess, RadDropDownList cmbPosition, RadDropDownList cmbUserDept, RadDropDownList cmbUserStatus, RadDropDownList cmbOffice, RadTextBox txtemail, RadTextBox bvNo, string empName)
		//{
		//	//[Employee ID], [EMPLOYEE NAME], USERNAME, [GROUP], USERTYPE, ROLE, STATUS, OFFICE, [EMAIL ADDRESS]
		//	var query = "SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS], [Broadvoice No.] FROM [User Information]";
		//	using (var con = new SqlConnection(_dbConnection))
		//	{
		//		try
		//		{
		//			con.Open();
		//			using (SqlCommand cmd = new SqlCommand(query, con))
		//			{
		//				cmd.ExecuteNonQuery();
		//				var row = dgUser.SelectedRows[0];
		//				if (dgUser.SelectedRows.Count > 0)
		//				{
		//					{
		//						txtIntID.Text = row.Cells[0].Value + string.Empty;
		//						txtName.Text = row.Cells[1].Value + string.Empty;
		//						txtUsername.Text = row.Cells[2].Value + string.Empty;
		//						cmbUserDept.Text = row.Cells[3].Value + string.Empty;
		//						cmbUserAccess.Text = row.Cells[4].Value + string.Empty;
		//						cmbPosition.Text = row.Cells[5].Value + string.Empty;
		//						cmbUserStatus.Text = row.Cells[6].Value + string.Empty;
		//						cmbOffice.Text = row.Cells[7].Value + string.Empty;
		//						txtemail.Text = row.Cells[8].Value + string.Empty;
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			//RadMessageBox.Show("Error connecting Database \n \n" + ex + "\n \n" + "Inform the Programmer/Author/Developer \n Erwin Alcantara (Skype: aerwin0629; Email: Erwin@pcmsbilling.net", Global.ProgName, MessageBoxButtons.OK, RadMessageIcon.Error);
		//			var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: DBQuery \n Process: FillUpUserTxtBox \n\n Detailed Error: " + ex.ToString();
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, NameofUser, Global.DCErrorWebHook, Global.DCErrorInvite);
		//			////ExecutedbDemoBackupCsv();
		//			task.ExecutedbCollBackupCsv(empName);
		//		}
		//		finally
		//		{
		//			con.Close();
		//		}
		//	}
		//
		//}
		//

		public DataTable GetSearch(
			string itemToSearch,
			string statusColumn,
			out string searchCount, string empName)
		{
			DataTable resultTable = new();

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();

				// Define the base query
				string query = $@"
SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT],
[USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS]
FROM [User Information]
WHERE USERNAME LIKE @itemToSearch";

				// Add the STATUS filter only if statusColumn is not "All"
				if (statusColumn != "All")
				{
					query += " AND STATUS LIKE @statusSearch";
				}

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@itemToSearch", $"%{itemToSearch}%");

				// Add the @statusSearch parameter only if statusColumn is not "All"
				if (statusColumn != "All")
				{
					cmd.Parameters.AddWithValue("@statusSearch", $"%{statusColumn}%");
				}

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				// Log the error and provide feedback
				error.LogError("SearchEmpTwoColumnOneFieldText", empName, "User", "N/A", ex);
				searchCount = "Error occurred while fetching records.";
			}

			return resultTable;
		}

		public string CheckIfExistinDB(string username, string modLoc, string request)
		{
			using SqlConnection conn = new(_dbConnection);
			conn.Open();
			try
			{
				using SqlCommand command = new(
					@"SELECT COUNT(*) FROM [User Information] WHERE USERNAME = @username", conn);
				command.Parameters.AddWithValue("@username", username);

				int userCount = (int)command.ExecuteScalar();

				if (userCount > 0)
				{
					if (modLoc == "UserMgmt" && request == "Create")
					{
						return "Username already exists.";
					}
				}
				else
				{
					if (request == "Login" && modLoc == "Login")
					{
						return "Username not found.";
					}
				}

				return string.Empty; // No issues found
			}
			catch (SqlException sqlEx)
			{
				error.LogError("CheckIfExistinDB", "N/A", "CommonTask", "SQL Exception", sqlEx);
				return "A database error occurred. Please try again later.";
			}
			catch (Exception ex)
			{
				error.LogError("CheckIfExistinDB", "N/A", "CommonTask", "General Exception", ex);
				return "An unexpected error occurred. Please contact support.";
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}
		}

		public void GetUsersEmail(string name, string empName)
		{
			using var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				using SqlCommand cmd = new("SELECT [Employee Name], [Email Address] FROM [User Information] WHERE [Employee Name] = '" + name + "'", con);
				using var reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					email = reader.IsDBNull(1) ? null : reader.GetString(1);
				}
			}
			catch (Exception ex)
			{
				error.LogError("GetUsersEmail", empName, "CommonTask", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}

		public bool UpdateFirstLoginInfo(string query, string userName, string empName, out string message)
		{
			message = string.Empty;

			try
			{
				using var con = new SqlConnection(_dbConnection);
				con.Open();

				using var command = new SqlCommand(query, con);
				// Add the parameter for @UserName with explicit type and size (adjust SqlDbType and size as needed)
				var parameter = new SqlParameter("@UserName", SqlDbType.NVarChar, 50)
				{
					Value = userName
				};
				command.Parameters.Add(parameter);

				int rowsAffected = command.ExecuteNonQuery();
				if (rowsAffected > 0)
				{
					message = "First login info updated successfully.";
					return true;
				}
				else
				{
					message = "No records were updated.";
					return false;
				}
			}
			catch (SqlException sqlEx)
			{
				error.LogError("UpdateFirstLoginInfo", empName, "CommonTask", "N/A", sqlEx);
				message = $"SQL Error: {sqlEx.Message}";
				return false;
			}
			catch (Exception ex)
			{
				error.LogError("UpdateFirstLoginInfo", empName, "CommonTask", "N/A", ex);
				message = $"An error occurred: {ex.Message}";
				return false;
			}
		}


		//public bool UpdateFirstLoginInfo(string query, string userName, string empName, out string message)
		//{
		//	try
		//	{
		//		using (var con = new SqlConnection(_dbConnection))
		//		{
		//			con.Open();
		//			using var command = new SqlCommand(query, con);
		//			// Add the parameter for @UserName
		//			command.Parameters.AddWithValue("@UserName", userName);
		//
		//			command.ExecuteNonQuery();
		//		}
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		error.LogError("UpdateFirstLoginInfo", empName, "CommonTask", "N/A", ex);
		//		message = $"Failed to Update FirstLoginInfo";
		//		return false;
		//	}
		//}

		public bool UpdateUserPassword
			(string userName,
			string email,
			string password,
			string reason,
			string empName,
			out string message,
			out string status)
		{
			try
			{
				// Early validation for critical inputs
				if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
				{
					message = "Username and Password should not be empty";
					status = "Warning";
					return true;
				}

				string query = "SELECT USERNAME, PASSWORD, [EMAIL ADDRESS] FROM [User Information] WHERE USERNAME = @UserName";

				using var con = new SqlConnection(_dbConnection);
				using var cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@UserName", userName);
				con.Open();

				using var reader = cmd.ExecuteReader();
				if (!reader.Read())
				{
					message = "Your username does not match our records";
					status = "Warning";
					return true;
				}

				string userNameDB = reader.GetString(0);
				string emailDB = reader.GetString(2);

				if (!emailDB.Equals(email, StringComparison.OrdinalIgnoreCase))
				{
					message = "The email provided didn't match the username. Ensure you have the correct email or ask your administrator to reset it.";
					status = "Warning";
					return true;
				}

				UpdatePassword(password, userNameDB, empName);
				LogPasswordChange(userName, reason, empName);

				NotifyUser(userName, email, password, reason);

				message = "Password updated successfully.";
				status = "Success";
				return true;
			}
			catch (Exception ex)
			{
				error.LogError("UpdateUserPassword", empName, "User", "N/A", ex);
				message = "An error occurred while updating the user password.";
				status = "Error";
				return false;
			}
		}

		private void UpdatePassword(string password, string userName, string empName)
		{
			string updateQuery = "UPDATE [User Information] SET PASSWORD = @Password WHERE USERNAME = @UserName";

			using (var con = new SqlConnection(_dbConnection))
			using (var cmd = new SqlCommand(updateQuery, con))
			{
				cmd.Parameters.AddWithValue("@UserName", userName);
				cmd.Parameters.AddWithValue("@Password", sec.PassHash(password));

				con.Open();
				cmd.ExecuteNonQuery();
			}
			log.AddActivityLog($"{empName} Password Update", empName, "${empName} Password Update", "UPDATE PASSWORD");
		}

		private void LogPasswordChange(string userName, string reason, string empName)
		{
			string logMessage;

			switch (reason.ToLower())
			{
				case "forgot":
					logMessage = $"{userName} updated their password due to a forgotten password.";
					log.AddActivityLog(logMessage, userName, logMessage, "FORGOT PASSWORD UPDATE");
					break;

				case "change":
					logMessage = $"{userName} updated their password due to a system requirement.";
					log.AddActivityLog(logMessage, userName, "System required password change", "CHANGE PASSWORD UPDATE");
					UpdateFirstLoginInfo("UPDATE [User Information] SET [FIRST TIME LOGIN] = 'NO' WHERE USERNAME = @UserName", userName, empName, out string message);
					break;

				default:
					logMessage = $"{userName} (Admin) updated the password for another user.";
					log.AddActivityLog(logMessage, userName, userName, "ADMIN PASSWORD RESET");
					break;
			}
		}

		private void NotifyUser(string userName, string email, string password, string reason)
		{
			string emailSubject = reason == "forgot" ? "Password Reset Request" : "Password Change Confirmation";
			string emailContent = $"Your new password for PCMS Lipa General Tool is: {password}\n\nPlease do not share this email or your password.";

			mailSender.SendEmail("noAttach", emailContent, null, emailSubject, email, "PCMS Lipa General Tool - noreply", null, null);

			string discordMessage = $"{userName} updated their password.";
			log.AddActivityLog(discordMessage, userName, discordMessage, "USER PASSWORD UPDATED");
			
		}

		///private void ShowMessage(string message, string caption, RadMessageIcon icon)
		///{
		///	RadMessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
		

		//public void UpdatePassword(string password, string username, string empName)
		//{
		//	string hashedPassword = secEnc.PassHash(password);
		//
		//	try
		//	{
		//		using (var conn = new SqlConnection(_dbConnection))
		//		{
		//			conn.Open();
		//			string query = "UPDATE [User Information] SET [PASSWORD] = @Password WHERE USERNAME = @Username";
		//			using (var cmd = new SqlCommand(query, conn))
		//			{
		//				cmd.Parameters.AddWithValue("@Password", hashedPassword);
		//				cmd.Parameters.AddWithValue("@Username", username);
		//				cmd.ExecuteNonQuery();
		//			}
		//		}
		//		// Optionally, log or notify user of successful update here
		//	}
		//	catch (Exception ex)
		//	{
		//		string errorMessage = $"Error: {ex.Message}\n\nName: {empName}\nModule: UpdatePassword\nProcess: Update\n\nDetailed Error: {ex}";
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", errorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//}
		//

		//public void UpdatePassword(string password, string username, string empName)
		//{
		//	using (SqlConnection conn = new SqlConnection(_dbConnection))
		//	{
		//		try
		//		{
		//			conn.Open();
		//			using (SqlCommand cmd = new SqlCommand("UPDATE [User Information] SET [PASSWORD] = @PASSWORD WHERE USERNAME = @USERNAME", conn))
		//			{
		//				cmd.Parameters.AddWithValue("@PASSWORD", secEnc.PassHash(password));
		//				cmd.Parameters.AddWithValue("@USERNAME", username);
		//				cmd.ExecuteNonQuery();
		//			}
		//			//RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//		}
		//		catch (Exception ex)
		//		{
		//			var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: UpdatePassword \n Process: Update \n\n Detailed Error: " + ex.ToString();
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//		}
		//		finally
		//		{
		//			conn.Close();
		//		}
		//	}
		//}

		//public void UpdateUserPassword(string userName, string email, string adminMessage, string password, string reason, string empName)
		//{
		//	try
		//	{
		//		string query = "SELECT USERNAME, PASSWORD, [EMAIL ADDRESS] FROM [User Information] WHERE USERNAME = @UserName";
		//
		//		using (var con = new SqlConnection(_dbConnection))
		//		using (var cmd = new SqlCommand(query, con))
		//		{
		//			cmd.Parameters.AddWithValue("@UserName", userName);
		//			con.Open();
		//
		//			using (var reader = cmd.ExecuteReader())
		//			{
		//				if (!reader.Read())
		//				{
		//					RadMessageBox.Show("Your username does not match our records", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					return;
		//				}
		//
		//				string userNameDB = reader.GetString(0);
		//				string emailDB = reader.GetString(2);
		//
		//				if (!emailDB.Equals(email, StringComparison.OrdinalIgnoreCase))
		//				{
		//					RadMessageBox.Show("The email provided didn't match the username. Ensure you have the correct email or ask your administrator to reset it.", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
		//					return;
		//				}
		//
		//				if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(userName))
		//				{
		//					RadMessageBox.Show("Username and Password should not be empty", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
		//					return;
		//				}
		//
		//				UpdatePassword(password, userNameDB, empName);
		//
		//				if (reason == "forgot")
		//				{
		//					log.AddActivityLog($"{userName} updated their password", userName, $"{userName} did a password reset", "FORGOT PASSWORD UPDATE");
		//				}
		//				else if (reason == "change")
		//				{
		//					log.AddActivityLog($"{userName} updated their password", userName, "System required password change", "CHANGE PASSWORD UPDATE");
		//					task.UpdateFirstLoginInfo("UPDATE [User Information] SET [FIRST TIME LOGIN] = 'NO' WHERE USERNAME = @UserName", userName, empName, "Password successfully updated");
		//					//task.UpdateValues("UPDATE [User Information] SET [FIRST TIME LOGIN] = 'NO' WHERE USERNAME = " + userName + ,  empName, "Password successfully updated");
		//				}
		//				else
		//				{
		//					log.AddActivityLog($"{userName} (Admin) updated the password for {userName}", userName, userName, "ADMIN PASSWORD RESET");
		//				}
		//
		//				winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", $"{userName} updated their password", "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//
		//				string emailContent = $"Your new password for PCMS Lipa General Tool is: {password}\n\nPlease do not share this email or your password.";
		//				emailSender.SendPasswordEmail(emailContent, email, reason == "forgot" ? "Password Reset Request" : "Password Change Confirmation");
		//
		//				fe.SendToastNotifDesktop(adminMessage);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		string errorMessage = $"Error: {ex.Message}\n\nName: {userName}\nModule: DBQuery\nProcess: UpdatePassword\n\nDetailed Error: {ex}";
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", errorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//}
		//

		//public void UpdateUserPassword(string userName, string email, string adminMessage, string password, string reason, string empName)
		//{
		//	try
		//	{
		//		var query = $"SELECT DISTINCT USERNAME, PASSWORD, [EMAIL ADDRESS] FROM [User Information] WHERE USERNAME = '{userName}'";
		//		using (SqlConnection con = new SqlConnection(_dbConnection))
		//		{
		//			using (SqlCommand cmd = new SqlCommand(query, con))
		//			{
		//				con.Open();
		//				using (SqlDataReader reader = cmd.ExecuteReader())
		//				{
		//					if (reader.Read())
		//					{
		//						string userNameDB = reader.GetString(0);
		//						string emailDB = reader.GetString(2);
		//						var firstTimeLogin = "UPDATE [User Information] SET [FIRST TIME LOGIN] = 'NO' WHERE USERNAME='" + userName + "'";
		//						if (password != null || password.Length != 0 || userName != null || userName.Length != 0)
		//						{
		//							if (userNameDB == userName)
		//							{
		//								if (emailDB.ToLower() == email.ToLower())
		//								{
		//									if (reason == "forgot" || reason == "change")
		//									{
		//										//string updatequery = "UPDATE [User Information] SET PASSWORD='" + secEnc.PassHash(password) + "' WHERE [USERNAME]='" + userName + "'";
		//										UpdatePassword(password, userNameDB, empName);
		//										if (reason == "forgot")
		//										{
		//											log.AddActivityLog(userName + " updated the password for " + userName, userName, userName + " did some password update on his/her account", "FORGOT PASSWORD UPDATE");
		//										}
		//										else if (reason == "change")
		//										{
		//											log.AddActivityLog(userName + " updated the password for " + userName, userName, userName + " System required to change password", "CHANGE PASSWORD UPDATE");
		//											task.UpdateValues(firstTimeLogin, empName, "Password successfully updated");
		//										}
		//										winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", userName + " did some password update on his/her account", "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//										var content = "Here is your new password for your PCMS Lipa General Tool Account.<br/> Password is: " + password + "\n\n <br/><br/><b>Do not share this email or your password to anyone.";
		//										emailSender.SendPasswordEmail(content, email, "Request for New Password User Login");
		//										
		//									}
		//									else
		//									{
		//										//string updatequery = "UPDATE [User Information] SET PASSWORD='" + secEnc.PassHash(password) + "' WHERE [USERNAME]='" + userName + "'";
		//										UpdatePassword(password, userNameDB, empName);
		//										log.AddActivityLog(userName + "(Admin) updated the password for " + userName, userName, userName, "ADMIN PASSWORD RESET");
		//										winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", userName + " did some password update on his/her account", "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//										var content = "We have received a request to reset your password for your PCMS Lipa General Tool Account.<br/> Your new Password is: " + password + "\n\n <br/><br/><b>Do not share this email or your password to anyone.";
		//										emailSender.SendPasswordEmail(content, email, "Password Reset Request");
		//									}
		//									fe.SendToastNotifDesktop(adminMessage);
		//								}
		//								else
		//								{
		//									RadMessageBox.Show("The email provided didn't matched with the username provided, Ensure that you have the correct email \n or \n Ask your Administrator or Developer to reset it for you.", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
		//								}
		//							}
		//							else
		//							{
		//								RadMessageBox.Show("Cannot find the username, Ensure that you have the correct username \n or \n Ask your Administrator or Developer to reset it for you.", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
		//							}
		//						}
		//						else
		//						{
		//							RadMessageBox.Show("Username and Password should not leave empty", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
		//						}
		//					}
		//					else
		//					{
		//						RadMessageBox.Show("Your username does not match our records", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//				}
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		var ErrorMessage = ex.Message + "\n\n Name: " + userName + "\nModule: DBQuery \n Process: UpdatePassword \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, NameofUser, Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//}

		public List<string> GetEmployeeList(string empName)
		{
			var query = "SELECT [EMPLOYEE NAME] FROM [User Information] WHERE STATUS = 'ACTIVE'";
			var items = new List<string>();
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlCommand cmd = new(query, con);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					items.Add(reader.GetString(0));
				}
				con.Close();
			}
			catch (Exception ex)
			{
				error.LogError("GetEmployeeList", empName, "Pantry", "N/A", ex);
			}
			return items;
		}


		public List<string> GetManagementList(string empName)
		{
			var query = "SELECT [EMPLOYEE NAME] FROM [User Information] WHERE STATUS = 'ACTIVE' AND STATUS = 'MANAGEMENT'";
			var items = new List<string>();
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlCommand cmd = new(query, con);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					items.Add(reader.GetString(0));
				}
				con.Close();
			}
			catch (Exception ex)
			{
				error.LogError("GetEmployeeList", empName, "Pantry", "N/A", ex);
			}
			return items;
		}


		//public void FillComboEmp(RadDropDownList cmbEmpList, string query, string empName)
		//{
		//	var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		con.Open();
		//		SqlCommand cmd = new SqlCommand(query, con);
		//		SqlDataReader reader = cmd.ExecuteReader();
		//		while (reader.Read())
		//		{
		//			string name = reader.GetString(0);
		//			cmbEmpList.Items.Add(name);
		//		}
		//		con.Close();
		//	}
		//	catch (Exception ex)
		//	{
		//		var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: DBQuery \n Process: FillComboEmp \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, NameofUser, Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//}
		//


		public bool IsValidEmail(string email)
		{
			try
			{
				var addr = new MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}

		public bool EmployeeDatabase(
			string request,
			string empID,
			string empName,
			string userName,
			string passWord,
			string userAccess,
			string position,
			string userDept,
			string userStatus,
			string workEmail,
			string bvNo,
			string office,
			string newEmp,
			string theme,
			string authorName,
			out string message)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				ValidateInputs(workEmail);
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs;

				// Determine the SQL command based on the request type
				cmd.CommandText = request switch
				{
					"Update" => @"UPDATE [User Information]
                          SET 
                              [EMPLOYEE NAME] = @EMPNAME,
                              USERNAME = @USERNAME,
                              [USER ACCESS] = @USERACCESS,
                              POSITION = @POSITION,
                              DEPARTMENT = @USERDEPT,
                              [STATUS] = @USERSTATUS,
                              [EMAIL ADDRESS] = @WORKEMAIL,
                              [OFFICE] = @OFFICE,
                              [Broadvoice No.] = @BVNo,
                              [Broadvoice Status] = @BVStatus
                          WHERE
                              [Employee ID] = @EMPID",
					"Create" => @"INSERT INTO [User Information]
                          ([EMPLOYEE NAME], USERNAME, PASSWORD, [USER ACCESS],
                          POSITION, DEPARTMENT, [STATUS], [EMAIL ADDRESS], 
                          [Broadvoice No.], OFFICE, [FIRST TIME LOGIN], THEME) 
                          VALUES
                          (@EMPNAME, @USERNAME, @PASSWORD, @USERACCESS,
                          @POSITION, @USERDEPT, @USERSTATUS, @WORKEMAIL, 
                          @BVNO, @OFFICE, @NEWEMP, @THEME)",
					"Delete" => @"DELETE FROM [User Information]
                          WHERE
                              [Employee ID] = @EMPID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add common parameters for "Update" and "Create"
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@EMPNAME", empName ?? string.Empty);
					cmd.Parameters.AddWithValue("@USERNAME", userName ?? string.Empty);
					cmd.Parameters.AddWithValue("@USERACCESS", userAccess ?? string.Empty);
					cmd.Parameters.AddWithValue("@POSITION", position ?? string.Empty);
					cmd.Parameters.AddWithValue("@USERDEPT", userDept ?? string.Empty);
					cmd.Parameters.AddWithValue("@USERSTATUS", userStatus ?? string.Empty);
					cmd.Parameters.AddWithValue("@WORKEMAIL", workEmail ?? string.Empty);
					cmd.Parameters.AddWithValue("@BVNO", bvNo ?? string.Empty);
					cmd.Parameters.AddWithValue("@OFFICE", office ?? string.Empty);
					cmd.Parameters.AddWithValue("@BVStatus", userStatus ?? string.Empty);
				}

				// Add specific parameters for "Create"
				if (request == "Create")
				{
					cmd.Parameters.AddWithValue("@NEWEMP", newEmp ?? "N/A");
					cmd.Parameters.AddWithValue("@PASSWORD", sec.PassHash(passWord ?? string.Empty));
					cmd.Parameters.AddWithValue("@THEME", theme ?? "Default");
				}

				// Add parameter for all requests
				cmd.Parameters.AddWithValue("@EMPID", empID ?? string.Empty);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{authorName} {request.ToLower()}d Employee ID: {empID}";
				message = $"Done! {empID} has been successfully {request.ToLower()}d.";
				SendCredentialstoEmail(workEmail.Trim(), userName.Trim(), empName.Trim());

				log.AddActivityLog(message, authorName, logs, $"{request.ToUpper()} USER INFORMATION");
				return true;
				//fe.SendToastNotifDesktop(message, "Success");
			}
			catch (Exception ex)
			{
				error.LogError($"EmployeeDatabase {request}", authorName, "User", newEmp, ex);
				message = $"Failed to {request.ToLower()} {empID}, Please try again later";
				return false;
				//MessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}


		//public void EmployeeDatabase(
		//	string request,
		//	RadTextBox txtempID,
		//	RadTextBox txtempName,
		//	RadTextBox txtuserName,
		//	RadTextBox txtpassWord,
		//	RadDropDownList cmbUserAccess,
		//	RadDropDownList cmbPosition,
		//	RadDropDownList cmbUserDept,
		//	RadDropDownList cmbUserStatus,
		//	RadTextBox txtWorkEmail,
		//	RadTextBox txtBVNo,
		//	RadDropDownList cmbOffice,
		//	string newEmp,
		//	string theme,
		//	string empName)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		ValidateInputs(txtWorkEmail.Text);
		//		conn.Open();
		//		SqlCommand cmd = new()
		//		{
		//			Connection = conn
		//		};
		//
		//		string logs, message;
		//
		//		cmd.CommandText = request switch
		//		{
		//			"Update" => @"UPDATE [User Information]
		//                  SET 
		//                      [EMPLOYEE NAME] = @EMPNAME,
		//                      USERNAME = @USERNAME,
		//                      [USER ACCESS] = @USERACCESS,
		//                      POSITION = @POSITION,
		//                      DEPARTMENT = @USERDEPT,
		//                      [STATUS] = @USERSTATUS,
		//                      [EMAIL ADDRESS] = @WORKEMAIL,
		//                      [OFFICE] = @OFFICE,
		//                      [Broadvoice No.] = @BVNo,
		//                      [Broadvoice Status] = @BVStatus
		//                  WHERE
		//                      [Employee ID] = @EMPID",
		//			"Create" => @"INSERT INTO [User Information]
		//                  ([EMPLOYEE NAME], USERNAME, PASSWORD, [USER ACCESS],
		//                  POSITION, DEPARTMENT, [STATUS], [EMAIL ADDRESS], 
		//                  [Broadvoice No.], OFFICE, [FIRST TIME LOGIN], THEME) 
		//                  VALUES
		//                  (@EMPNAME, @USERNAME, @PASSWORD, @USERACCESS,
		//                  @POSITION, @USERDEPT, @USERSTATUS, @WORKEMAIL, 
		//                  @BVNO, @OFFICE, @NEWEMP, @THEME)",
		//			"Delete" => @"DELETE FROM [User Information]
		//                  WHERE
		//                      [Employee ID] = @EMPID",
		//			_ => throw new ArgumentException("Invalid request type."),
		//		};
		//
		//		// Add common parameters for "Update" and "Create"
		//		if (request != "Delete")
		//		{
		//			cmd.Parameters.AddWithValue("@EMPNAME", txtempName.Text);
		//			cmd.Parameters.AddWithValue("@USERNAME", txtuserName.Text);
		//			cmd.Parameters.AddWithValue("@USERACCESS", cmbUserAccess.Text);
		//			cmd.Parameters.AddWithValue("@POSITION", cmbPosition.Text);
		//			cmd.Parameters.AddWithValue("@USERDEPT", cmbUserDept.Text);
		//			cmd.Parameters.AddWithValue("@USERSTATUS", cmbUserStatus.Text);
		//			cmd.Parameters.AddWithValue("@WORKEMAIL", txtWorkEmail.Text);
		//			cmd.Parameters.AddWithValue("@BVNO", txtBVNo.Text);
		//			cmd.Parameters.AddWithValue("@OFFICE", cmbOffice.Text);
		//			cmd.Parameters.AddWithValue("@BVStatus", cmbUserStatus.Text);
		//		}
		//
		//		// Add specific parameters for "Create"
		//		if (request == "Create")
		//		{
		//			cmd.Parameters.AddWithValue("@NEWEMP", newEmp ?? "N/A");
		//			cmd.Parameters.AddWithValue("@PASSWORD", sec.PassHash(txtpassWord.Text));
		//			cmd.Parameters.AddWithValue("@THEME", theme ?? "Default");
		//		}
		//
		//		// Add parameter for all requests
		//		cmd.Parameters.AddWithValue("@EMPID", txtempID.Text);
		//
		//		// Execute query
		//		cmd.ExecuteNonQuery();
		//
		//		// Log activity
		//		logs = $"{empName} {request.ToLower()}d Employee ID: {txtempID.Text}";
		//		message = $"Done! {txtempID.Text} has been successfully {request.ToLower()}d.";
		//		SendCredentialstoEmail(txtWorkEmail.Text.Trim(), txtuserName.Text.Trim(), txtempName.Text.Trim());
		//
		//		log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} USER INFORMATION");
		//		fe.SendToastNotifDesktop(message, "Success");
		//	}
		//	catch (Exception ex)
		//	{
		//		error.LogError($"EmployeeDatabase {request}", empName, "Adjuster", newEmp, ex);
		//		RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
		//	}
		//	finally
		//	{
		//		conn.Close();
		//	}
		//}


		private void SendCredentialstoEmail(string recipientEmail, string userName, string empName)
		{
			try
			{
		
				var password = GenerateRandomPassword();

				string subject = "Your Account Credentials";
				string body = $@"
                    <h3>Hello {empName},</h3>
                    <p>Welcome to Primary Care Management Service. Your account credentials for PCMS Lipa General Tools are as follows:</p>
                    <ul>
                        <li><strong>Username:</strong> {userName}</li>
                        <li><strong>Password:</strong> {password}</li>
                    </ul>
                    <p>Please log in and change your password immediately.</p>
                    <p>Best regards,<br>SystemAdministrator</br> </p>";

				SendEmail(recipientEmail, subject, body);
				//ShowNotification("Email sent successfully!", NotificationType.Success);
			}
			catch (Exception ex)
			{
				error.LogError($"SendCredentialstoEmail", empName,"User", recipientEmail, ex);
				//RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);/ ShowNotification($"Error sending email: {ex.Message}", NotificationType.Error);
			}
		}

		private void SendEmail(string recipient, string subject, string body)
		{

			string smtpHost = "smtp." + EmailHost;
			int smtpPort = 587;
			string senderEmail = "erwin@pcmsbilling.net";
			string senderPassword = "W3yHzy-j-A";
			int retryCount = 3; // Number of retries
			int retryDelay = 2000; // Delay in milliseconds between retries

			for (int attempt = 1; attempt <= retryCount; attempt++)
			{
				try
				{
					using var smtpClient = new SmtpClient(smtpHost)
					{
						Port = smtpPort,
						Credentials = new NetworkCredential(senderEmail, senderPassword),
						EnableSsl = true
					};

					using MailMessage mail = new()
					{
						From = new MailAddress(senderEmail, "PCMS Lipa General Tool"),
						Subject = subject,
						Body = body

					};
					mail.IsBodyHtml = true;	
					mail.To.Add(recipient);
					mail.CC.Add("edimson@pcmsbilling.net");
					mail.Bcc.Add("mr.erwinalcantara@gmail.com");
					smtpClient.Send(mail);
				}
				catch (Exception ex)
				{
					if (attempt == retryCount)
					{
						error.LogError("SendEmail", null, "User", recipient, ex);
						return;
					}

					// Delay before retrying
					Task.Delay(retryDelay).Wait();
				}
			}
		}

		private string GenerateRandomPassword()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			return new string(Enumerable.Range(1, 12).Select(_ => chars[random.Next(chars.Length)]).ToArray());
		}


		private void ValidateInputs(string email)
		{
			MailAddress emailToCheck = new(email); //// Throws FormatException if invalid
		}

		public DataTable ViewEmployeeInformationUser(string empName, out string lblCount, string window)
		{
			string query =  window == "EmpInfo"
				? "SELECT [EMPLOYEE ID], [EMPLOYEE NAME], [EMAIL ADDRESS], [BROADVOICE NO.], [DEPARTMENT], POSITION, OFFICE, STATUS, REMARKS FROM [User Information] WHERE STATUS = 'ACTIVE'"
				: "SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS], [Broadvoice No.] FROM [User Information] WHERE STATUS = 'ACTIVE'";

			var data = new DataTable();
			lblCount = string.Empty;

			try
			{
				using var con = new SqlConnection(_dbConnection);
				using var adp = new SqlDataAdapter(query, con);

				// Fill the DataTable with data from the query
				adp.Fill(data);

				// Calculate the record count
				lblCount = $"Total records: {data.Rows.Count}";
			}
			catch (Exception ex)
			{
				error.LogError("ViewEmployeeInformationUser", empName, "User", "N/A", ex);
			}

			return data;
		}

	

		public void UpdateUserTheme(string empName, string menuItem, out string message)
		{
			var query = "UPDATE [User Information] SET [THEME] = '" + menuItem + "' WHERE [EMPLOYEE NAME] ='" + empName + "'";
			try
			{
				using (var con = new SqlConnection(_dbConnection))
				{
					con.Open();
					using var command = new SqlCommand(query, con);
					command.ExecuteNonQuery();
				}
				//RadMessageBox.Show("Theme successfully updated", "Information", MessageBoxButtons.OK, RadMessageIcon.Info);
				message = "Theme sucessfully updated and applied";
			}
			catch (Exception ex)
			{
				error.LogError("UpdateUserTheme", empName, "User", "N/A", ex);
				message = $"Failed to update theme for {empName}, Please try again later";
				//return false;
			}
		}

		//public void EmployeeDatabase(string request, RadTextBox txtempID, RadTextBox txtempName, RadTextBox txtuserName, RadTextBox txtpassWord, RadDropDownList cmbUserAccess, RadDropDownList cmbPosition, RadDropDownList cmbUserDept, RadDropDownList cmbUserStatus, RadTextBox txtWorkEmail, RadTextBox txtBVNo, RadDropDownList cmbOffice, string newEmp, string theme, string empName)
		//{
		//	try
		//	{
		//		var emailtoCheck = new MailAddress(txtWorkEmail.Text);
		//		using (SqlConnection conn = new SqlConnection(_dbConnection))
		//		{
		//			switch (request)
		//			{
		//				case "Patch":
		//					{
		//						string message = String.Format("I made an update for {1}. Check the details below." +
		//							"\n\n\nEmployee ID: {0}\nEmployee Name: {1}\nUsername: {2}\nWork Email: {3}\nBroadvoice No: {4}\nUser Access: {5}\nPosition: {6}\nOffice: {7}\nDepartment: {8}\nUser Status: {9}",
		//							txtempID.Text,
		//							txtempName.Text,
		//							txtuserName.Text,
		//							txtWorkEmail.Text,
		//							txtBVNo.Text,
		//							cmbUserAccess.Text,
		//							cmbPosition.Text,
		//							cmbOffice.Text,
		//							cmbUserDept.Text,
		//							cmbUserStatus.Text);
		//						try
		//						{
		//							conn.Open();
		//							using (SqlCommand cmd = new SqlCommand("UPDATE [User Information]" +
		//								"SET [EMPLOYEE NAME] = @EMPNAME, USERNAME = @USERNAME, [USER ACCESS] = @USERACCESS, " +
		//								"POSITION = @POSITION, DEPARTMENT = @USERDEPT, [STATUS] = @USERSTATUS, [EMAIL ADDRESS] = @WORKEMAIL, " +
		//								"[OFFICE] = @OFFICE, [Broadvoice No.] = @BVNo, [Broadvoice Status] = @BVStatus WHERE [Employee ID] = @EMPID", conn))
		//							{
		//								cmd.Parameters.AddWithValue("@EMPID", txtempID.Text);
		//								cmd.Parameters.AddWithValue("@EMPNAME", txtempName.Text);
		//								cmd.Parameters.AddWithValue("@USERNAME", txtuserName.Text);
		//								//cmd.Parameters.AddWithValue("@PASSWORD", txtpassWord.Text);
		//								cmd.Parameters.AddWithValue("@USERACCESS", cmbUserAccess.Text);
		//								cmd.Parameters.AddWithValue("@POSITION", cmbPosition.Text);
		//								cmd.Parameters.AddWithValue("@USERDEPT", cmbUserDept.Text);
		//								cmd.Parameters.AddWithValue("@USERSTATUS", cmbUserStatus.Text);
		//								cmd.Parameters.AddWithValue("@WORKEMAIL", txtWorkEmail.Text);
		//								cmd.Parameters.AddWithValue("@BVNo", txtBVNo.Text);
		//								cmd.Parameters.AddWithValue("@OFFICE", cmbOffice.Text);
		//								if (cmbUserStatus.Text == "InActive")
		//								{
		//									cmd.Parameters.AddWithValue("@BVStatus", "Available");
		//								}
		//								else
		//								{
		//									cmd.Parameters.AddWithValue("@BVStatus", "In-Use");
		//								}
		//								//cmd.Parameters.AddWithValue("@NEWEMP", newEmp);
		//								//cmd.Parameters.AddWithValue("@THEME", theme);
		//								cmd.ExecuteNonQuery();
		//							}
		//							string logs = empName + " updated information Employee ID: " + txtempID.Text;
		//							log.AddActivityLog(message, empName, logs, "UPDATED EMPLOYEE INFORMATION");
		//							winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//							fe.SendToastNotifDesktop(logs);
		//							//RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//						}
		//						catch (Exception ex)
		//						{
		//							var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: EmployeeDatabase \n Process: Patch \n Entry ID: " + txtempID.Text + " \n\n Detailed Error: " + ex.ToString();
		//							winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//							RadMessageBox.Show(ErrorMessageUpdate + txtempID.Text, "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
		//						}
		//						finally
		//						{
		//							conn.Close();
		//						}
		//
		//						break;
		//					}
		//
		//				case "Create":
		//					{
		//						string message = String.Format("I update the records for {1}. Check the details below." +
		//							"\n\n\nEmployee ID: {0}\nEmployee Name: {1}\nUsername: {2}\nWork Email: {3}\nUser Access: {4}\nPosition: {5}\nOffice: {6}\nDepartment: {7}\nUser Status: {8}",
		//							txtempID.Text,
		//							txtempName.Text,
		//							txtuserName.Text,
		//							txtWorkEmail.Text,
		//							txtBVNo.Text,
		//							cmbUserAccess.Text,
		//							cmbPosition.Text,
		//							cmbOffice.Text,
		//							cmbUserDept.Text,
		//							cmbUserStatus.Text);
		//						try
		//						{
		//							conn.Open();
		//							//using (SqlCommand cmd = new SqlCommand("INSERT INTO [User Information] ([Employee ID], [EMPLOYEE NAME], USERNAME, PASSWORD, [USER ACCESS], POSITION, DEPARTMENT, [STATUS], [EMAIL ADDRESS], OFFICE, [FIRST TIME LOGIN], THEME)" +
		//							//	"VALUES (@EMPID, @EMPNAME, @USERNAME, @PASSWORD, @USERACCESS, @POSITION, @USERDEPT, @USERSTATUS, @WORKEMAIL, @OFFICE, @NEWEMP, @THEME)", conn))
		//							using (SqlCommand cmd = new SqlCommand("INSERT INTO [User Information] ([EMPLOYEE NAME], USERNAME, PASSWORD, [USER ACCESS], POSITION, DEPARTMENT, [STATUS], [EMAIL ADDRESS], [Broadvoice No.], OFFICE, [FIRST TIME LOGIN], THEME)" +
		//									"VALUES (@EMPNAME, @USERNAME, @PASSWORD, @USERACCESS, @POSITION, @USERDEPT, @USERSTATUS, @WORKEMAIL, @BVNO, @OFFICE, @NEWEMP, @THEME)", conn))
		//							{
		//								//cmd.Parameters.AddWithValue("@EMPID", txtempID.Text);
		//								cmd.Parameters.AddWithValue("@EMPNAME", txtempName.Text);
		//								cmd.Parameters.AddWithValue("@USERNAME", txtuserName.Text);
		//								cmd.Parameters.AddWithValue("@PASSWORD", secEnc.PassHash(txtpassWord.Text));
		//								cmd.Parameters.AddWithValue("@USERACCESS", cmbUserAccess.Text);
		//								cmd.Parameters.AddWithValue("@POSITION", cmbPosition.Text);
		//								cmd.Parameters.AddWithValue("@USERDEPT", cmbUserDept.Text);
		//								cmd.Parameters.AddWithValue("@USERSTATUS", cmbUserStatus.Text);
		//								cmd.Parameters.AddWithValue("@WORKEMAIL", txtWorkEmail.Text);
		//								cmd.Parameters.AddWithValue("@BVNo", txtBVNo.Text);
		//								cmd.Parameters.AddWithValue("@OFFICE", cmbOffice.Text);
		//								cmd.Parameters.AddWithValue("@NEWEMP", newEmp);
		//								cmd.Parameters.AddWithValue("@THEME", theme);
		//								cmd.ExecuteNonQuery();
		//							}
		//							string logs = empName + " added Employee ID: " + txtempID.Text;
		//							SendCredentialstoEmail(txtempName.Text, txtWorkEmail.Text, empName, txtuserName.Text);
		//							log.AddActivityLog(message, empName, logs, "ADDED EMPLOYEE INFORMATION");
		//							winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//							fe.SendToastNotifDesktop(logs);
		//							//RadMessageBox.Show("Record successfully Added", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//						}
		//
		//						catch (Exception ex)
		//						{
		//							var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: EmployeeDatabase \n Process: Create \n Entry ID: " + txtempID.Text + " \n\n Detailed Error: " + ex.ToString();
		//							winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//							RadMessageBox.Show(ErrorMessageCreate + txtempID.Text, "Failed to Create", MessageBoxButtons.OK, RadMessageIcon.Error);
		//						}
		//						finally
		//						{
		//							conn.Close();
		//						}
		//						break;
		//					}
		//				case "Delete":
		//					{
		//						string message = String.Format("I removed {1} in the records. Check the details below." +
		//							"\n\n\nEmployee ID: {0}\nEmployee Name: {1}\nUsername: {2}\nWork Email: {3}\nUser Access: {4}\nPosition: {5}\nOffice: {6}\nDepartment: {7}\nUser Status: {8}",
		//							txtempID.Text,
		//							txtempName.Text,
		//							txtuserName.Text,
		//							txtWorkEmail.Text,
		//							txtBVNo.Text,
		//
		//							cmbUserAccess.Text,
		//							cmbPosition.Text,
		//							cmbOffice.Text,
		//							cmbUserDept.Text,
		//							cmbUserStatus.Text);
		//						try
		//						{
		//							conn.Open();
		//							using (SqlCommand cmd = new SqlCommand("DELETE FROM [User Information] WHERE [Employee ID] = @EMPID", conn))
		//							{
		//								cmd.Parameters.AddWithValue("@EMPID", txtempID.Text);
		//								cmd.ExecuteNonQuery();
		//							}
		//							string logs = empName + " deleted Employee ID: " + txtempID.Text;
		//							log.AddActivityLog(message, empName, logs, "DELETED EMPLOYEE INFORMATION");
		//							winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//							fe.SendToastNotifDesktop(logs);
		//							//RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//						}
		//						catch (Exception ex)
		//						{
		//							var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: EmployeeDatabase \n Process: Delete \n Entry ID: " + txtempID.Text + " \n\n Detailed Error: " + ex.ToString();
		//							winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//							RadMessageBox.Show(ErrorMessageDelete + txtempID.Text, "Failed to Delete", MessageBoxButtons.OK, RadMessageIcon.Error);
		//						}
		//						finally
		//						{
		//							conn.Close();
		//						}
		//
		//						break;
		//					}
		//			}
		//		}
		//	}
		//	catch (FormatException ex)
		//	{
		//		var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: EmployeeDatabase \n Process: Create \n Entry ID: " + txtempID.Text + " \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//		RadMessageBox.Show("Invalid email", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
		//
		//	}
		//	//user.EmployeeDatabase("Patch", txtIntID, txtEmployeeName, txtUsername, txtPassword, cmbUserAccess, cmbPosition, cmbUserDept, cmbUserStatus, txtWorkEmail, cmbOffice, null, null, empName);
		//
		//}
		//

		public bool MoreEmployeeDatabase(
	string empID,
	string empName,
	string rdWebUsername,
	string rdWebPassword,
	string lytecUsername,
	string lytecPassword,
	string workEmail,
	string emailPassword,
	DateTime dateOfBirth,
	string broadvoiceNo,
	string broadvoiceUsername,
	string broadvoicePassword,
	string pcName,
	string pcUsername,
	string pcPassword,
	string team,
	string remarks,
	string firstLogin,
	string discordUsername,
	string discordPassword,
	string employmentStatus,
	string authorName,
	out string message)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();

				// Generate activity message
				string logs = $@"I added an update for {empName}. Check the details below.

Employee ID: {empID}
Employee Name: {empName}
RDWeb Username: {rdWebUsername}
RD Password: {rdWebPassword}
Lytec Username: {lytecUsername}
Lytec Password: {lytecPassword}
Email: {workEmail}
Email Password: {emailPassword}
Date of Birth: {dateOfBirth:yyyy-MM-dd}
Broadvoice No: {broadvoiceNo}
Broadvoice Username: {broadvoiceUsername}
Broadvoice Password: {broadvoicePassword}
PC Name: {pcName}
PC Username: {pcUsername}
PC Password: {pcPassword}
Team: {team}
Discord Username: {discordUsername}
Discord Password: {discordPassword}
First Login: {firstLogin}
Remarks: {remarks}
Employment Status: {employmentStatus}";

				// SQL command to update employee information
				string sql = @"UPDATE [User Information] 
                       SET [EMPLOYEE NAME] = @EMPNAME, 
                           [RDWeb Username] = @RDUN, 
                           [RDWeb Password] = @RDPW,
                           [Lytec Username] = @LYTECUN, 
                           [Lytec Password] = @LYTECPW, 
                           [Email Address] = @WORKEMAIL, 
                           [Email Password] = @EMAILPW, 
                           [Broadvoice No.] = @BVNO,
                           [Broadvoice Username] = @BVUN,  
                           [Broadvoice Password] = @BVPW, 
                           [PC Assigned] = @PCASS, 
                           [PC Username] = @PCUN,
                           [PC Password] = @PCPW, 
                           Remarks = @REMARKS, 
                           [Date of Birth] = @DOB, 
                           [Team] = @Team, 
                           [First Time Login] = @FIRSTLOGIN, 
                           [Discord Username] = @DCUsername, 
                           [Discord Password] = @DCPassword, 
                           [Employment Status] = @EmpStatus 
                       WHERE [Employee ID] = @EMPID";

				using SqlCommand cmd = new(sql, conn);
				cmd.Parameters.AddWithValue("@EMPID", empID);
				cmd.Parameters.AddWithValue("@EMPNAME", empName);
				cmd.Parameters.AddWithValue("@RDUN", rdWebUsername);
				cmd.Parameters.AddWithValue("@RDPW", rdWebPassword);
				cmd.Parameters.AddWithValue("@LYTECUN", lytecUsername);
				cmd.Parameters.AddWithValue("@LYTECPW", lytecPassword);
				cmd.Parameters.AddWithValue("@WORKEMAIL", workEmail);
				cmd.Parameters.AddWithValue("@EMAILPW", emailPassword);
				cmd.Parameters.AddWithValue("@DOB", dateOfBirth);
				cmd.Parameters.AddWithValue("@BVNO", broadvoiceNo);
				cmd.Parameters.AddWithValue("@BVUN", broadvoiceUsername);
				cmd.Parameters.AddWithValue("@BVPW", broadvoicePassword);
				cmd.Parameters.AddWithValue("@PCASS", pcName);
				cmd.Parameters.AddWithValue("@PCUN", pcUsername);
				cmd.Parameters.AddWithValue("@PCPW", pcPassword);
				cmd.Parameters.AddWithValue("@Team", team);
				cmd.Parameters.AddWithValue("@REMARKS", remarks);
				cmd.Parameters.AddWithValue("@DCUsername", discordUsername);
				cmd.Parameters.AddWithValue("@DCPassword", discordPassword);
				cmd.Parameters.AddWithValue("@FIRSTLOGIN", firstLogin);
				cmd.Parameters.AddWithValue("@EmpStatus", employmentStatus);

				// Execute the query
				cmd.ExecuteNonQuery();

				// Log activity
				message = $"{authorName} updated Employee ID: {empID}";
				log.AddActivityLog(message, authorName, logs, "UPDATED MORE EMPLOYEE INFORMATION");
				return true;
				///fe.SendToastNotifDesktop(message, "success");
			}
			catch (Exception ex)
			{
				error.LogError("MoreEmployeeDatabase", authorName, "User", empID, ex);
				message = $"Failed to update {empID}, Please try again later";
				return false;
				///MessageBox.Show($"{ex.Message} {empID}", "Failed to Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}

		public DataTable GetAdminSearch(
			string itemToSearch,
			string statusColumn,
			out string searchCount, string empName)
		{
			DataTable resultTable = new();

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();

				// Define the base query
				string query = $@"
SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT],
[USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS]
FROM [User Information]
WHERE USERNAME LIKE @itemToSearch";

				// Add the STATUS filter only if statusColumn is not "All"
				if (statusColumn != "All")
				{
					query += " AND STATUS LIKE @statusSearch";
				}

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@itemToSearch", $"%{itemToSearch}%");

				// Add the @statusSearch parameter only if statusColumn is not "All"
				if (statusColumn != "All")
				{
					cmd.Parameters.AddWithValue("@statusSearch", $"%{statusColumn}%");
				}

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				// Log the error and provide feedback
				error.LogError("GetAdminSearch", empName, "CommonTask", "N/A", ex);
				searchCount = "Error occurred while fetching records.";
			}

			return resultTable;
		}

		public DataTable SearchData(
	string searchTerm,
	out string searchCount,
	string empName)
		{
			DataTable resultTable = new();

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				string query = $@"
SELECT *
FROM [User Information]
WHERE [Employee Name] LIKE @searchTerm
OR [Broadvoice No.] LIKE @searchTerm
OR [Remarks] LIKE @searchTerm";

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				error.LogError("SearchData", empName, "Adjuster", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}

		//public void MoreEmployeeDatabase(RadTextBox txtempID, RadTextBox txtempName, RadTextBox txtRDWebName, RadTextBox txtRDWebPassword, RadTextBox txtLytecUsername, RadTextBox txtLytecPassword, RadTextBox txtWorkEmail, RadTextBox txtEmailPassword, RadTextBox DateOfBirth, RadTextBox broadvoiceNo, RadTextBox broadvoiceUsername, RadTextBox broadvoicePassword, RadTextBox pcName, RadTextBox pcUN, RadTextBox pcPW, RadDropDownList team, RadTextBoxControl Remarks, RadDropDownList cmbFirstLogin, RadTextBox dcUsernaame, RadTextBox dcPassword, string empName, RadDropDownList cmbEmploymentStat)
		//{
		//
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		conn.Open();
		//		string message = String.Format("I added an update for {1}. Check the details below." +
		//					"\n\n\nEmployee ID: {0}\nEmployee Name: {1}\nRDWeb Username: {2}\nRD Password: {3}\nLytect Username: {4}\nLytec Password: {5}\nEmail: {6}\nEmail Password: {7}\nDate of Birth: {8}" +
		//					"\nBroadvoice No: {9}\nBroadvoice Username: {10}\nBroadvoice Password: {11}\nPC Name: {12}\nPC Username: {13}\nPC Password: {14}" +
		//					"\nPC Team: {15}\nDiscord Username: {16}\nDiscord Password: {17}\nFirst Login: {18}\nRemarks: {19}\nEmployment Status: {20}",
		//					txtempID.Text,
		//					txtempName.Text,
		//					txtRDWebName.Text,
		//					txtRDWebPassword.Text,
		//					txtLytecUsername.Text,
		//					txtLytecPassword.Text,
		//					txtWorkEmail.Text,
		//					txtEmailPassword.Text,
		//					DateOfBirth.Text,
		//					broadvoiceNo.Text,
		//					broadvoiceUsername.Text,
		//					broadvoicePassword.Text,
		//					pcName.Text,
		//					pcUN.Text,
		//					pcPW.Text,
		//					team.Text,
		//					dcUsernaame.Text,
		//					dcPassword.Text,
		//					cmbFirstLogin.Text,
		//					Remarks.Text,
		//					cmbEmploymentStat.Text);
		//		using (SqlCommand cmd = new("UPDATE [User Information] SET [EMPLOYEE NAME] = @EMPNAME, [RDWeb Username] = @RDUN, [RDWeb Password] = @RDPW," +
		//			"[Lytec Username] = @LYTECUN, [Lytec Password] = @LYTECPW, [Email Address] = @WORKEMAIL, [Email Password] = @EMAILPW, [Broadvoice No.] = @BVNO," +
		//			"[Broadvoice Username] = @BVUN,  [Broadvoice Password] = @BVPW, [PC Assigned] = @PCASS, [PC Username] = @PCUN," +
		//			"[PC Password] = @PCPW, Remarks = @REMARKS, [Date of Birth] = @DOB, [Team] = @Team, [First Time Login] = @FIRSTLOGIN, [Discord Username] = @DCUsername, [Discord Password] = @DCPassword, [Employment Status] = @EmpStatus WHERE [Employee ID] = @EMPID", conn))
		//		{
		//			cmd.Parameters.AddWithValue("@EMPID", txtempID.Text);
		//			cmd.Parameters.AddWithValue("@EMPNAME", txtempName.Text);
		//			cmd.Parameters.AddWithValue("@RDUN", txtRDWebName.Text);
		//			cmd.Parameters.AddWithValue("@RDPW", txtRDWebPassword.Text);
		//			cmd.Parameters.AddWithValue("@LYTECUN", txtLytecUsername.Text);
		//			cmd.Parameters.AddWithValue("@LYTECPW", txtLytecPassword.Text);
		//			cmd.Parameters.AddWithValue("@WORKEMAIL", txtWorkEmail.Text);
		//			cmd.Parameters.AddWithValue("@EMAILPW", txtEmailPassword.Text);
		//			cmd.Parameters.AddWithValue("@DOB", DateOfBirth.Text);
		//			cmd.Parameters.AddWithValue("@BVNO", broadvoiceNo.Text);
		//			cmd.Parameters.AddWithValue("@BVUN", broadvoiceUsername.Text);
		//			cmd.Parameters.AddWithValue("@BVPW", broadvoicePassword.Text);
		//			cmd.Parameters.AddWithValue("@PCASS", pcName.Text);
		//			cmd.Parameters.AddWithValue("@PCUN", pcUN.Text);
		//			cmd.Parameters.AddWithValue("@PCPW", pcPW.Text);
		//			cmd.Parameters.AddWithValue("@Team", team.Text);
		//			cmd.Parameters.AddWithValue("@REMARKS", Remarks.Text);
		//			cmd.Parameters.AddWithValue("@DCUsername", dcUsernaame.Text);
		//			cmd.Parameters.AddWithValue("@DCPassword", dcPassword.Text);
		//			cmd.Parameters.AddWithValue("@FIRSTLOGIN", cmbFirstLogin.Text);
		//			cmd.Parameters.AddWithValue("@EmpStatus", cmbEmploymentStat.Text);
		//			cmd.ExecuteNonQuery();
		//		}
		//		string logs = empName + " updated Employee ID: " + txtempID.Text;
		//		log.AddActivityLog(message, empName, logs, "UPDATED MORE EMPLOYEE INFORMATION");
		//		fe.SendToastNotifDesktop(logs);
		//		//RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//	}
		//	catch (Exception ex)
		//	{
		//		error.LogError("MoreEmployeeDatabase", empName, "User", "N/A", ex);
		//		RadMessageBox.Show($@"{ex.Message} {txtempID.Text}", "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
		//	}
		//	finally
		//	{
		//		conn.Close();
		//	}
		//}
	}
}
