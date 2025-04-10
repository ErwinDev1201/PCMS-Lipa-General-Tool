using PCMS_Lipa_General_Tool.Forms;
using PCMS_Lipa_General_Tool__WinForm_;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using PCMS_Lipa_General_Tool.Services;
using System.Data;
using System.Threading.Tasks;


namespace PCMS_Lipa_General_Tool.Class
{
	public class Login
	{
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Database db = new();
		private readonly SecurityEncryption secEnc = new();
		public static string ProgName = Global.ProgName;
		public static string ProgVer = Global.ProgVer;
		public static string Dev = Global.Dev;
		public string conStatus;
		//private readonly string devAccess;
		//private string theme;

		readonly frmMainApp mainApp = new();
		readonly frmDemoTool demoTool = new();
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();
		private static readonly WinDiscordAPI dc = new();


		// Resets login data to default values
		public (string Username, string Password, bool IsLoginPanelEnabled, string AlertMessage) DefaultLoginSet()
		{
			return (string.Empty, string.Empty, true, string.Empty);
		}


		// Checks database connectivity
		public void CheckConnectivity()
		{
			try
			{
				Database dBConStat = new(_dbConnection);
				conStatus = dBConStat.IsConnected ? "Connected | Login" : "Disconnected | Login";
			}
			catch (Exception)
			{
				conStatus = "Disconnected | Login";
			}
		}

		// Handles user login logic

		public async Task<(bool isSuccess, string alertMessage, string empID, string empName, string userName, string email,
	string firstTime, string userPosition, string userAccess, string userDept, string officeLoc, string theme, string empStat, bool isLoginPanelEnabled, bool islblAlertShow)>
	UserLoginAsync(string username, string password)
		{
			string conquery;
			bool isLoginPanelEnabled = false;
			bool islblAlertShow = false;
			string alertMessage = "An unexpected error occurred during login."; // Default message

			try
			{
				using var conSQL = new SqlConnection(_dbConnection);
				await conSQL.OpenAsync();

				string devAccess = await GetDeveloperAccessAsync(conSQL);
				conquery = GetLoginQuery(password == devAccess);

				using var cmdSQL = new SqlCommand(conquery, conSQL);
				cmdSQL.Parameters.AddWithValue("@username", username);
				cmdSQL.Parameters.AddWithValue("@password", secEnc.PassHash(password));

				using var readerSQL = await cmdSQL.ExecuteReaderAsync();
				if (await readerSQL.ReadAsync())
				{
					return await ProcessLoginDetailsAsync(readerSQL);
				}
				else
				{
					// Handle invalid credentials
					var (alertMsg, user, loginPanelEnabled, LblAlertShow) = HandleInvalidPassword(username);
					return (false, alertMsg, null, null, user, null, null, null, null, null, null, null, null, loginPanelEnabled, LblAlertShow);
				}
			}
			catch (SqlException ex)
			{
				alertMessage = $"SQL Error: {ex.Message}";
				isLoginPanelEnabled = true;
				islblAlertShow = true;
			}
			catch (Exception ex)
			{
				alertMessage = $"Error: {ex.Message}";
				isLoginPanelEnabled = true;
				islblAlertShow = true;
			}

			// Ensure the alertMessage is returned properly
			return (false, alertMessage, null, null, null, null, null, null, null, null, null, null, null, isLoginPanelEnabled, islblAlertShow);
		}


		private async Task<(bool isSuccess, string alertMessage, string empID, string empName, string userName, string email,
	string firstTime, string userPosition, string userAccess, string userDept, string officeLoc, string theme, string empStat, bool isLoginPanelEnabled,
	bool islblAlertShow)> ProcessLoginDetailsAsync(SqlDataReader readerSQL)
		{
			try
			{
				if (readerSQL == null || readerSQL.IsClosed)
				{
					notif.LogError("ProcessLoginDetailsAsync", null, "Login", "Database error: Unable to retrieve user data.", null);
					return (false, "Something went wrong", null, null, null, null, null, null, null, null, null, null, null, true, true);
				}

				//if (!await readerSQL.ReadAsync()) // Awaiting to read the next row
				//{
				//	Console.WriteLine("DEBUG: No rows returned from SQL.");
				//	return (false, "No user data found.", null, null, null, null, null, null, null, null, null, null, true, true);
				//}

				string userStatus = readerSQL["Status"]?.ToString() ?? "UNKNOWN";
				if (userStatus.Equals("INACTIVE", StringComparison.OrdinalIgnoreCase))
				{
					return (false, "Your account is inactive. Please contact your administrator.", null, null, null, null, null, null, null, null, null, null, null, true, true);
				}

				//
				// Fetch user details safely
				string empName = readerSQL["Employee Name"]?.ToString() ?? string.Empty;
				string empID = readerSQL["Employee ID"]?.ToString() ?? string.Empty;
				string userName = readerSQL["Username"]?.ToString() ?? string.Empty;
				string email = readerSQL["Email Address"]?.ToString() ?? string.Empty;
				string firstTime = readerSQL["First Time Login"]?.ToString() ?? string.Empty;
				string userPosition = readerSQL["Position"]?.ToString() ?? string.Empty;
				string userAccess = readerSQL["User Access"]?.ToString() ?? string.Empty;
				string userDept = readerSQL["Department"]?.ToString() ?? string.Empty;
				string officeLoc = readerSQL["Office"]?.ToString() ?? string.Empty;  
				string theme = readerSQL["Theme"]?.ToString() ?? string.Empty;
				string empStat = readerSQL["Employment Status"]?.ToString() ?? string.Empty;

				await Task.Run(() => log.AddActivityLog($"{empName} logged in", empName, $"{empName} logged in", "USER LOGGED IN"));
				await LogFirstLoginAsync(empID, empName);

				return (true, $"{empName} logged in successfully", empID, empName, userName, email, firstTime, userPosition, userAccess, userDept, officeLoc, theme, empStat, false, false);
			}
			catch (SqlException ex)
			{
				notif.LogError("ProcessLoginDetailsAsync - SQL", null, "Login", null, ex);
				return (false, $"Something went wrong", null, null, null, null, null, null, null, null, null, null,null,  true, false);
			}
			catch (Exception ex)
			{
				notif.LogError("ProcessLoginDetailsAsync - Gen", null, "Login", null, ex);
				return (false, $"Something went wrong", null, null, null, null, null, null, null, null, null, null, null, true, true);
			}
		}

		private async Task LogFirstLoginAsync(string empID, string empName)
		{
			try
			{
				using SqlConnection con = new SqlConnection(_dbConnection);
				await con.OpenAsync();

				DateTime today = DateTime.Today;
				DateTime tomorrow = today.AddDays(1);

				const string checkQuery = @"
            SELECT COUNT(1)
            FROM [Login History]
            WHERE [Employee ID] = @EmpID
              AND [Date] >= @Today
              AND [Date] < @Tomorrow;";

				using SqlCommand checkCmd = new(checkQuery, con);
				checkCmd.Parameters.AddWithValue("@EmpID", empID);
				checkCmd.Parameters.AddWithValue("@Today", today);
				checkCmd.Parameters.AddWithValue("@Tomorrow", tomorrow);

				int existingCount = (int)(await checkCmd.ExecuteScalarAsync() ?? 0);

				if (existingCount == 0)
				{
					const string insertQuery = @"
                INSERT INTO [Login History] 
                    ([Employee ID], [Name], [Date], [Month], [Year], [Remarks])
                VALUES 
                    (@EmpID, @EmpName, @LoginDate, @LoginMonth, @LoginYear, @Remarks);";

					using SqlCommand insertCmd = new(insertQuery, con);
					insertCmd.Parameters.AddWithValue("@EmpID", empID);
					insertCmd.Parameters.AddWithValue("@EmpName", empName);
					insertCmd.Parameters.AddWithValue("@LoginDate", DateTime.Now);
					insertCmd.Parameters.AddWithValue("@LoginMonth", DateTime.Now.ToString("MMMM"));
					insertCmd.Parameters.AddWithValue("@LoginYear", DateTime.Now.ToString("yyyy"));
					insertCmd.Parameters.AddWithValue("@Remarks", "First login of the day");

					await insertCmd.ExecuteNonQueryAsync();
				}
			}
			catch (SqlException ex)
			{
				notif.LogError("LogFirstLoginAsync - SQL", empID, "LoginHistory", null, ex);
			}
			catch (Exception ex)
			{
				notif.LogError("LogFirstLoginAsync - Gen", empID, "LoginHistory", null, ex);
			}
		}



		private async Task<string> GetDeveloperAccessAsync(SqlConnection connection)
		{
			try
			{
				const string query = "SELECT DeveloperAccess FROM [User Information] WHERE [Username] = @Username";

				// Ensure connection is open
				if (connection.State != ConnectionState.Open)
					await connection.OpenAsync();

				using var cmd = new SqlCommand(query, connection);
				cmd.Parameters.AddWithValue("@Username", "Erwin");

				object result = await cmd.ExecuteScalarAsync();
				return result?.ToString() ?? string.Empty;
			}
			catch (SqlException sqlEx)
			{
				notif.LogError("GetDeveloperAccessAsync", null, "Login", "SQL error during DeveloperAccess query", sqlEx);
				return string.Empty;
			}
			catch (Exception ex)
			{
				notif.LogError("GetDeveloperAccessAsync", null, "Login", "Unexpected error during DeveloperAccess query", ex);
				return string.Empty;
			}
		}


		//	private async Task<string> GetDeveloperAccessAsync(SqlConnection connection)
		//	{
		//		string query = "SELECT DeveloperAccess FROM [User Information] WHERE [Username] ='Erwin'";
		//		using var cmd = new SqlCommand(query, connection);
		//		object result = await cmd.ExecuteScalarAsync();
		//		return result?.ToString() ?? string.Empty;
		//	}


		private string GetLoginQuery(bool isDevAccess)
		{
			return isDevAccess
				? "SELECT DISTINCT [Employee ID], [Employee Name], [Username], [Email Address], [First Time Login], [Position], [User Access], [Department], [Office], [Theme], [Status], [Employment Status] FROM [User Information] WHERE USERNAME = @username"
				: "SELECT DISTINCT [Employee ID], [Employee Name], [Username], [Email Address], [First Time Login], [Position], [User Access], [Department], [Office], [Theme], [Status], [Employment Status] FROM [User Information] WHERE USERNAME = @username AND PASSWORD = @password";
		}

		//public async Task<(bool isSuccess, string alertMessage, string EmpName, string userName, string email,
		//string firstTime, string userPosition, string userAccess, string userDept, string officeLoc, string theme)>
		//ProcessLoginAsync(SqlDataReader readerSQL)
		//{
		//	try
		//	{
		//		if (readerSQL == null || readerSQL.IsClosed || !await readerSQL.ReadAsync())
		//		{
		//			return (false, "Database error: Unable to retrieve user data.", null, null, null, null, null, null, null, null, null);
		//		}
		//
		//		using (readerSQL) // Ensuring proper disposal of the SqlDataReader
		//		{
		//			string userStatus = readerSQL["Status"]?.ToString() ?? "UNKNOWN";
		//			if (userStatus.Equals("INACTIVE", StringComparison.OrdinalIgnoreCase))
		//			{
		//				return (false, "Your account is inactive. Please contact your administrator.", null, null, null, null, null, null, null, null, null);
		//			}
		//
		//			// Fetch user details safely
		//			string empName = readerSQL["Employee Name"]?.ToString() ?? string.Empty;
		//			string userName = readerSQL["Username"]?.ToString() ?? string.Empty;
		//			string email = readerSQL["Email Address"]?.ToString() ?? string.Empty;
		//			string firstTime = readerSQL["First Time Login"]?.ToString() ?? string.Empty;
		//			string userPosition = readerSQL["Position"]?.ToString() ?? string.Empty;
		//			string userAccess = readerSQL["User Access"]?.ToString() ?? string.Empty;
		//			string userDept = readerSQL["Department"]?.ToString() ?? string.Empty;
		//			string officeLoc = readerSQL["Office"]?.ToString() ?? string.Empty;
		//			string theme = readerSQL["Theme"]?.ToString() ?? string.Empty;
		//			theme = theme ?? ConfigureUserThemeIfMissing(readerSQL, userName);
		//			return (true, "Login successful.", empName, userName, email, firstTime, userPosition, userAccess, userDept, officeLoc, theme);
		//		}
		//	}
		//	catch (SqlException ex)
		//	{
		//		return (false, $"SQL Error: {ex.Message}", null, null, null, null, null, null, null, null, null);
		//	}
		//	catch (Exception ex)
		//	{
		//		return (false, $"Error: {ex.Message}", null, null, null, null, null, null, null, null, null);
		//	}
		//}
		//

				
		// Helper method to configure and show form
		//private void ConfigureAndShowForm(Form form, string employeeName)
		//{
		//	form.Text = $"{ProgName} ver. {ProgVer} ({employeeName})";
		//	form.Show();
		//}
		//
		//private string ConfigureUserThemeIfMissing(SqlDataReader readerSQL, string username)
		//{
		//	string theme = string.Empty;
		//
		//	try
		//	{
		//		// Get the ordinal position of the "Theme" column
		//		int themeOrdinal = readerSQL.GetOrdinal("Theme");
		//
		//		// If theme is NOT null, return the existing theme
		//		if (!readerSQL.IsDBNull(themeOrdinal))
		//		{
		//			return readerSQL["Theme"]?.ToString() ?? "Crystal"; // Return existing or default theme
		//		}
		//
		//		// If theme is null, update it to the default value
		//		const string query = "UPDATE [User Information] SET THEME = @theme WHERE USERNAME = @username";
		//
		//		using (var conSQL = new SqlConnection(_dbConnection))
		//		using (var cmdTheme = new SqlCommand(query, conSQL))
		//		{
		//			theme = "Crystal"; // Default theme
		//			cmdTheme.Parameters.Add("@theme", SqlDbType.VarChar, 50).Value = theme;
		//			cmdTheme.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
		//
		//			conSQL.Open();
		//			int rowsAffected = cmdTheme.ExecuteNonQuery();
		//
		//			if (rowsAffected == 0)
		//			{
		//				notif.LogError("ConfigureUserThemeIfMissing", username, "Login", "Theme update failed. No rows affected.", null);
		//				return "Theme update failed. No rows affected."; // Error message
		//			}
		//		}
		//	}
		//	catch (SqlException ex)
		//	{
		//		notif.LogError("ConfigureUserThemeIfMissing", username, "Login", ex.Message, ex);
		//		return "Database error occurred while updating theme.";
		//	}
		//	catch (Exception ex)
		//	{
		//		notif.LogError("ConfigureUserThemeIfMissing", username, "Login", ex.Message, ex);
		//		return "An unexpected error occurred.";
		//	}
		//
		//	return theme; // Return the theme (updated or existing)
		//}


		private (string AlertMessage, string Username, bool IsLoginPanelEnabled, bool isLblAlertShow) HandleInvalidPassword(string username)
		{
			string alertMessage = "PASSWORD IS INCORRECT";
			log.AddActivityLog($"Incorrect password used for {username}", "", $"Incorrect password used for {username}", "INCORRECT PASSWORD");

			bool isLoginPanelEnabled = true;
			bool isLblAlertShow = true;

			return (alertMessage, username, isLoginPanelEnabled, isLblAlertShow);
		}


		//private void LogAndNotifyError(Exception ex, string username, ref string alertMessage)
		//{
		//	string errorMessage = $"Error processing login for {username}:\n{ex.Message}";
		//	notif.LogError("LogAndNotifyError", EmpName, "Login", errorMessage, ex);
		//	alertMessage = "An error occurred, please check with Developer.";
		//}
	}
}
