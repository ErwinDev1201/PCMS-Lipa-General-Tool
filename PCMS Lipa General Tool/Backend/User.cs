using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;


namespace PCMS_Lipa_General_Tool.Class
{
	public class User
	{

		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Database db = new();

		readonly SecurityEncryption sec = new();
		readonly emailSender mailSender = new();
		readonly ActivtiyLogs log = new();
		readonly Notification notif = new();
		
		//private string email;
		public string EmpName;



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
				notif.LogError("GetDBID", empName, "User", "N/A", ex);
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
				notif.LogError("FillAdminUserProfile", empName, "User", txtIntID, ex);
			}
		}

		public DataTable GetSearch(
	string itemToSearch,
	string statusColumn,
	out string searchCount,
	string empName,
	string window)
		{
			DataTable resultTable = new();
			searchCount = "No records found";

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();

				// Base Query
				string query = (window == "EmpInfo")
					? @"SELECT [EMPLOYEE ID], [EMPLOYEE NAME], [EMAIL ADDRESS], 
                      [BROADVOICE NO.], [DEPARTMENT], POSITION, OFFICE, STATUS, REMARKS 
               FROM [User Information] WHERE 1=1"
					: @"SELECT * FROM [User Information] WHERE 1=1";

				// Search by name or username if `itemToSearch` is not null or empty
				if (!string.IsNullOrEmpty(itemToSearch))
				{
					query += " AND (USERNAME LIKE @itemToSearch OR [EMPLOYEE NAME] LIKE @itemToSearch)";
				}

				// Apply status filter only if it's not "All"
				if (!string.IsNullOrEmpty(statusColumn) && statusColumn != "All")
				{
					query += " AND STATUS = @statusSearch";
				}

				using SqlCommand cmd = new(query, conn);

				// Add parameters only when necessary
				if (!string.IsNullOrEmpty(itemToSearch))
				{
					cmd.Parameters.AddWithValue("@itemToSearch", $"%{itemToSearch}%");
				}

				if (!string.IsNullOrEmpty(statusColumn) && statusColumn != "All")
				{
					cmd.Parameters.AddWithValue("@statusSearch", statusColumn);
				}

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Set search count message
				searchCount = resultTable.Rows.Count > 0
					? $"Total records: {resultTable.Rows.Count}"
					: "No records found";
			}
			catch (SqlException sqlEx)
			{
				notif.LogError("GetSearch", empName, "Database", "SQL Error", sqlEx);
				searchCount = "Database error occurred while fetching records.";
			}
			catch (Exception ex)
			{
				notif.LogError("GetSearch", empName, "Application", "General Error", ex);
				searchCount = "Unexpected error occurred.";
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
						return "Username exists.";
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
				notif.LogError("CheckIfExistinDB", "N/A", "CommonTask", "SQL Exception", sqlEx);
				return "A database error occurred. Please try again later.";
			}
			catch (Exception ex)
			{
				notif.LogError("CheckIfExistinDB", "N/A", "CommonTask", "General Exception", ex);
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

		public string GetUsersEmail(string name, string empName)
		{
			string email = null;
			using var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				using SqlCommand cmd = new("SELECT [Email Address] FROM [User Information] WHERE [Employee Name] = @name", con);
				cmd.Parameters.AddWithValue("@name", name);

				using var reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					email = reader.IsDBNull(0) ? null : reader.GetString(0);
				}
			}
			catch (Exception ex)
			{
				notif.LogError("GetUsersEmail", empName, "CommonTask", "N/A", ex);
			}
			return email;
		}

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

				string query = "SELECT USERNAME, PASSWORD, [EMAIL ADDRESS], [Employee Name] FROM [User Information] WHERE USERNAME = @UserName";

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

				// Correctly fetching values from the reader
				string userNameDB = reader.IsDBNull(reader.GetOrdinal("USERNAME")) ? string.Empty : reader.GetString(reader.GetOrdinal("USERNAME"));
				string emailDB = reader.IsDBNull(reader.GetOrdinal("EMAIL ADDRESS")) ? string.Empty : reader.GetString(reader.GetOrdinal("EMAIL ADDRESS"));
				string name = reader.IsDBNull(reader.GetOrdinal("Employee Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Employee Name"));


				if (!emailDB.Equals(email, StringComparison.OrdinalIgnoreCase))
				{
					message = "The email provided didn't match the username. Ensure you have the correct email or ask your administrator to reset it.";
					status = "Warning";
					return true;
				}

				UpdatePassword(password, userNameDB, reason, empName);
				SendCredentialstoEmail(email, userName, empName, password, name, "Reset", null, null, null, null, null, null);

				//SendCredentialstoEmail(email, userName, empName, password, "Reset");

				//NotifyUser(userName, email, password, reason);

				message = "Password updated successfully.";
				status = "Success";
				return true;
			}
			catch (Exception ex)
			{
				notif.LogError("UpdateUserPassword", empName, "User", "N/A", ex);
				message = "An error occurred while updating the user password.";
				status = "Error";
				return false;
			}
		}

		private void UpdatePassword(string password, string userName, string reason, string empName)
		{
			string updateQuery;
			if (reason == "adminreset")
			{
				updateQuery = "UPDATE [User Information] SET PASSWORD = @Password, [First Time Login] = 'Yes' WHERE USERNAME = @UserName";
			}
			else
			{
				updateQuery = "UPDATE [User Information] SET PASSWORD = @Password, [First Time Login] = 'NO' WHERE USERNAME = @UserName";
			}
			

			string logMessage;

			using (var con = new SqlConnection(_dbConnection))
			using (var cmd = new SqlCommand(updateQuery, con))
			{
				cmd.Parameters.AddWithValue("@UserName", userName);
				cmd.Parameters.AddWithValue("@Password", sec.PassHash(password));

				con.Open();
				cmd.ExecuteNonQuery();

				
				switch (reason.ToLower())
				{
					case "forgot":
						logMessage = $"{userName} updated their password due to a forgotten password.";
						log.AddActivityLog(logMessage, userName, logMessage, "FORGOT PASSWORD UPDATE");
						break;

				    case "change":
					logMessage = $"{userName} updated their password due to a system requirement.";
					log.AddActivityLog(logMessage, userName, "System required password change", "CHANGE PASSWORD UPDATE");
					//UpdateFirstLoginInfo("UPDATE [User Information] SET [FIRST TIME LOGIN] = 'NO' WHERE USERNAME = @UserName", userName, empName, out string message);
					break;

					case "adminreset":
						logMessage = $"{empName} (Admin) updated the password for {userName}.";
						log.AddActivityLog(logMessage, userName, logMessage, "ADMIN PASSWORD RESET");
						break;

				}
			}
			//log.AddActivityLog($"{empName} Password Update", empName, "${empName} Password Update", "UPDATE PASSWORD");
		}

		private void NotifyUser(string userName, string email, string password, string reason)
		{
			string emailSubject = reason == "forgot" ? "Password Reset Request" : "Password Change Confirmation";
			string emailContent = $"Your new password for PCMS Lipa General Tool is: {password}\n\nPlease do not share this email or your password.";
			mailSender.SendEmail(email, emailSubject, emailContent, null, "PCMS Lipa General Tool - User", null);
			//mailSender.SendEmail(email, emailSubject, email, "CMS Lipa General Tool - noreply", null, null, null);

			//mailSender.SendEmail("noAttach", emailContent, null, emailSubject, email, "PCMS Lipa General Tool - noreply", null, null);

			string discordMessage = $"{userName} updated their password.";
			log.AddActivityLog(discordMessage, userName, discordMessage, "USER PASSWORD UPDATED");
			
		}


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
				notif.LogError("GetEmployeeList", empName, "Pantry", "N/A", ex);
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
				notif.LogError("GetEmployeeList", empName, "Pantry", "N/A", ex);
			}
			return items;
		}

		public List<string> GetUserITList(string empName)
		{
			var query = "SELECT [EMPLOYEE NAME] FROM [User Information] WHERE STATUS = 'ACTIVE' AND DEPARTMENT = 'IT'";
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
				notif.LogError("GetEmployeeList", empName, "Pantry", "N/A", ex);
			}
			return items;
		}

	

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

		public bool EmployeeDatabaseAllInfo(
			string request,
			string empID,
			string empName,
			string userName,
			//string passWord,
			string userAccess,
			string position,
			string userDept,
			string userStatus,
			string workEmail,
			string broadvoiceNo,
			string office,
			string firstLogin,
			string theme,
			string rdWebUsername,
			string rdWebPassword,
			string lytecUsername,
			string lytecPassword,
			string emailPassword,
			DateTime dateOfBirth,
			string broadvoiceUsername,
			string broadvoicePassword,
			string pcName,
			string pcUsername,
			string pcPassword,
			string team,
			string remarks,
			string discordUsername,
			string discordPassword,
			string employmentStatus,
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
                              [Email Address] = @WORKEMAIL,
                              [OFFICE] = @OFFICE,
                              [RDWeb Username] = @RDUN, 
							  [RDWeb Password] = @RDPW,
							  [Lytec Username] = @LYTECUN, 
							  [Lytec Password] = @LYTECPW, 
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
                          WHERE
                              [Employee ID] = @EMPID",

					"Create" => @"INSERT INTO [User Information]
                          ([EMPLOYEE NAME], USERNAME, PASSWORD, [USER ACCESS],
                          POSITION, DEPARTMENT, [STATUS], [EMAIL ADDRESS], 
                          OFFICE, [FIRST TIME LOGIN], THEME, [RDWeb Username], 
						  [RDWeb Password], [Lytec Username], [Lytec Password], 
						  [Email Password], [Broadvoice No.], [Broadvoice Username], [Broadvoice Password],
                          [PC Assigned], [PC Username], [PC Password], Remarks, [Date of Birth], [Team], 
						  [Discord Username], [Discord Password], [Employment Status]) 
                     VALUES
                          (@EMPNAME, @USERNAME, @PASSWORD, @USERACCESS,
                          @POSITION, @USERDEPT, @USERSTATUS, @WORKEMAIL, 
                          @OFFICE, @FIRSTLOGIN, @THEME, @RDUN,
						  @RDPW, @LYTECUN, @LYTECPW,
                          @EMAILPW, @BVNO, @BVUN, @BVPW,
						  @PCASS, @PCUN, @PCPW, @REMARKS, @DOB, @Team,
                          @DCUsername, @DCPassword, @EmpStatus)",

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
					cmd.Parameters.AddWithValue("@OFFICE", office ?? string.Empty);
					cmd.Parameters.AddWithValue("@BVStatus", userStatus ?? string.Empty);
					//cmd.Parameters.AddWithValue("@EMPID", empID ?? string.Empty);
					cmd.Parameters.AddWithValue("@RDUN", rdWebUsername ?? string.Empty);
					cmd.Parameters.AddWithValue("@RDPW", rdWebPassword ?? string.Empty);
					cmd.Parameters.AddWithValue("@LYTECUN", lytecUsername ?? string.Empty);
					cmd.Parameters.AddWithValue("@LYTECPW", lytecPassword ?? string.Empty);
					cmd.Parameters.AddWithValue("@EMAILPW", emailPassword ?? string.Empty);
					cmd.Parameters.AddWithValue("@DOB", dateOfBirth);
					cmd.Parameters.AddWithValue("@BVNO", broadvoiceNo ?? string.Empty);
					cmd.Parameters.AddWithValue("@BVUN", broadvoiceUsername ?? string.Empty);
					cmd.Parameters.AddWithValue("@BVPW", broadvoicePassword ?? string.Empty);
					cmd.Parameters.AddWithValue("@PCASS", pcName ?? string.Empty);
					cmd.Parameters.AddWithValue("@PCUN", pcUsername ?? string.Empty);
					cmd.Parameters.AddWithValue("@PCPW", pcPassword ?? string.Empty);
					cmd.Parameters.AddWithValue("@Team", team ?? string.Empty);
					cmd.Parameters.AddWithValue("@REMARKS", remarks ?? string.Empty);
					cmd.Parameters.AddWithValue("@DCUsername", discordUsername ?? string.Empty);
					cmd.Parameters.AddWithValue("@DCPassword", discordPassword ?? string.Empty);
					cmd.Parameters.AddWithValue("@FIRSTLOGIN", firstLogin ?? string.Empty);
					cmd.Parameters.AddWithValue("@EmpStatus", employmentStatus ?? string.Empty);
				}

				// Add specific parameters for "Create"
				

				if (request == "Create")
				{
					var password = GenerateRandomPassword();
					//cmd.Parameters.AddWithValue("@NEWEMP", newEmp ?? "N/A");
					cmd.Parameters.AddWithValue("@PASSWORD", sec.PassHash(password ?? string.Empty));
					cmd.Parameters.AddWithValue("@THEME", theme ?? "Default");
					SendCredentialstoEmail(workEmail.Trim(), userName.Trim(), authorName.Trim(), password.Trim(), empName, "Create", rdWebUsername, rdWebPassword, lytecUsername, lytecPassword, workEmail, broadvoiceNo);
				}

				// Add parameter for all requests
				cmd.Parameters.AddWithValue("@EMPID", empID ?? string.Empty);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{authorName} {request.ToLower()}d Employee ID: {empID}";
				message = $"Done! {empID} has been successfully {request.ToLower()}d.";
				

				log.AddActivityLog(message, authorName, logs, $"{request.ToUpper()} USER INFORMATION");
				return true;
				//fe.SendToastNotifDesktop(message, "Success");
			}
			catch (Exception ex)
			{
				notif.LogError($"EmployeeDatabase {request}", authorName, "User", empID, ex);
				message = $"Failed to {request.ToLower()} {empID}, Please try again later";
				return false;
		
			}
			finally
			{
				conn.Close();
			}
		}

		public void SendCredentialstoEmail(string recipientEmail, string userName, string empName, string password, string Name, string action, string rdwebName, string rdwebPassword, string lytecUsername, string lytecPassword, string email, string bvNO)
		{
			string machineName = Environment.MachineName;
			string emailAddressCC = machineName == "ERWIN-PC" ? "Edimson@yopmail.com" : "Edimson@pcmsbilling.net";

			try
			{
				string subject;
				StringBuilder bodyBuilder = new();

				// Common greeting and introduction
				bodyBuilder.AppendFormat("<h3>Hello {0},</h3>", Name);

				if (action == "Reset")
				{
					subject = "Password Reset";
					bodyBuilder.Append("<p>Your account credentials for PCMS Lipa General Tools have been updated. Please check the details below:</p>");
				}
				else // "Create" action includes additional credentials
				{
					subject = "Your Account Credentials";
					bodyBuilder.Append("<p>Welcome to Primary Care Management Service. Please log in and change your password immediately. Your account credentials for PCMS Lipa General Tools are as follows:</p>");
				}

				// Common user credentials
				bodyBuilder.AppendFormat(@"
        <ul>
            <li><strong>Username:</strong> {0}</li>
            <li><strong>Password:</strong> {1}</li>
        </ul>", userName, password);

				// Additional RDWeb and Lytec credentials when creating a new account
				if (action == "Create")
				{
					bodyBuilder.AppendFormat(@"
            <p>Your Logins to RDWeb and Lytec are as follows:</p>
            <ul>
                <li><strong>RDWeb Username:</strong> {0}</li>
                <li><strong>RDWeb Password:</strong> {1}</li>
                <li><strong>Lytec Username:</strong> {2}</li>
                <li><strong>Lytec Password:</strong> {3}</li>
			</ul>
			<p>For your reference, here are the other details:</p>
			<ul>
			<li><strong>Work Email:</strong> {4}</li>
				<li><strong>Broadvoice Number:</strong> {5}</li>	
            </ul>", rdwebName, rdwebPassword, lytecUsername, lytecPassword, email, bvNO);
				}

				// Closing message
				bodyBuilder.Append("<p>Best regards,<br>System Administrator</p>");

				// Send email
				mailSender.SendEmail(recipientEmail, subject, bodyBuilder.ToString(), emailAddressCC, "PCMS Lipa General Tool - User", null);
			}
			catch (Exception ex)
			{
				notif.LogError(nameof(SendCredentialstoEmail), empName, "User", recipientEmail, ex);
			}
		}


		public string GenerateRandomPassword()
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
			//string query =  window == "EmpInfo"
			//	? "SELECT [EMPLOYEE ID], [EMPLOYEE NAME], [EMAIL ADDRESS], [BROADVOICE NO.], [DEPARTMENT], POSITION, OFFICE, STATUS, REMARKS FROM [User Information] WHERE STATUS = 'ACTIVE'"
			//	: "SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS], [Broadvoice No.] FROM [User Information] WHERE STATUS = 'ACTIVE'";
			string query = window == "EmpInfo"
				? "SELECT [EMPLOYEE ID], [EMPLOYEE NAME], [EMAIL ADDRESS], [BROADVOICE NO.], [DEPARTMENT], POSITION, OFFICE, STATUS, REMARKS FROM [User Information] WHERE STATUS = 'ACTIVE'"
				: "SELECT * FROM [User Information] WHERE STATUS = 'ACTIVE'";

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
				notif.LogError("ViewEmployeeInformationUser", empName, "User", "N/A", ex);
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
				notif.LogError("UpdateUserTheme", empName, "User", "N/A", ex);
				message = $"Failed to update theme for {empName}, Please try again later";
				//return false;
			}
		}

//		
//		public DataTable GetAdminSearch(
//			string itemToSearch,
//			string statusColumn,
//			out string searchCount, string empName)
//		{
//			DataTable resultTable = new();
//
//			using SqlConnection conn = new(_dbConnection);
//			try
//			{
//				conn.Open();
//
//				// Define the base query
//				string query = $@"
//	  [Employee ID]
//      ,[Employee Name]
//      ,[Username]
//      ,[Department]
//      ,[Position]
//      ,[RDWeb Username]
//      ,[Lytec Username]
//      ,[Email Address]
//      ,[Broadvoice No.]
//      ,[PC Assigned]
//      ,[PC Username]
//      ,[Team]
//      ,[Status]
//      ,[Employment Status]
//      ,[Date of Birth]
//      ,[Office]
//      ,[First Time Login]
//      ,[Discord Username]
//      ,[Remarks]
//WHERE USERNAME LIKE @itemToSearch";
//
//				// Add the STATUS filter only if statusColumn is not "All"
//				if (statusColumn != "All")
//				{
//					query += " AND STATUS LIKE @statusSearch";
//				}
//
//				using SqlCommand cmd = new(query, conn);
//				cmd.Parameters.AddWithValue("@itemToSearch", $"%{itemToSearch}%");
//
//				// Add the @statusSearch parameter only if statusColumn is not "All"
//				if (statusColumn != "All")
//				{
//					cmd.Parameters.AddWithValue("@statusSearch", $"%{statusColumn}%");
//				}
//
//				using SqlDataAdapter adapter = new(cmd);
//				adapter.Fill(resultTable);
//
//				// Calculate the search count
//				searchCount = $"Total records: {resultTable.Rows.Count}";
//			}
//			catch (Exception ex)
//			{
//				// Log the error and provide feedback
//				notif.LogError("GetAdminSearch", empName, "CommonTask", "N/A", ex);
//				searchCount = "Error occurred while fetching records.";
//			}
//
//			return resultTable;
//		}
	}
}
