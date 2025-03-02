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
	string firstTime, string userPosition, string userAccess, string userDept, string officeLoc, string theme, bool isLoginPanelEnabled, bool islblAlertShow)>
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
					return (false, alertMsg, null, null, user, null, null, null, null, null, null, null, loginPanelEnabled, LblAlertShow);
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
			return (false, alertMessage, null, null, null, null, null, null, null, null, null, null, isLoginPanelEnabled, islblAlertShow);
		}


		private async Task<(bool isSuccess, string alertMessage, string empID, string empName, string userName, string email,
	string firstTime, string userPosition, string userAccess, string userDept, string officeLoc, string theme, bool isLoginPanelEnabled,
	bool islblAlertShow)> ProcessLoginDetailsAsync(SqlDataReader readerSQL)
		{
			try
			{
				if (readerSQL == null || readerSQL.IsClosed)
				{
					notif.LogError("ProcessLoginDetailsAsync", null, "Login", "Database error: Unable to retrieve user data.", null);
					return (false, "Something went wrong", null, null, null, null, null, null, null, null, null, null, true, true);
				}

				//if (!await readerSQL.ReadAsync()) // Awaiting to read the next row
				//{
				//	Console.WriteLine("DEBUG: No rows returned from SQL.");
				//	return (false, "No user data found.", null, null, null, null, null, null, null, null, null, null, true, true);
				//}

				string userStatus = readerSQL["Status"]?.ToString() ?? "UNKNOWN";
				if (userStatus.Equals("INACTIVE", StringComparison.OrdinalIgnoreCase))
				{
					return (false, "Your account is inactive. Please contact your administrator.", null, null, null, null, null, null, null, null, null, null, true, true);
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

				await Task.Run(() => log.AddActivityLog($"{empName} logged in", empName, $"{empName} logged in", "USER LOGGED IN"));

				return (true, $"{empName} logged in successfully", empID, empName, userName, email, firstTime, userPosition, userAccess, userDept, officeLoc, theme, false, false);
			}
			catch (SqlException ex)
			{
				notif.LogError("ProcessLoginDetailsAsync", null, "Login", null, ex);
				return (false, $"Something went wrong", null, null, null, null, null, null, null, null, null, null, true, false);
			}
			catch (Exception ex)
			{
				notif.LogError("ProcessLoginDetailsAsync", null, "Login", null, ex);
				return (false, $"Something went wrong", null, null, null, null, null, null, null, null, null, null, true, true);
			}
		}

		private async Task<string> GetDeveloperAccessAsync(SqlConnection connection)
		{
			string query = "SELECT DeveloperAccess FROM [User Information] WHERE [Username] ='Erwin'";
			using var cmd = new SqlCommand(query, connection); // ❌ SqlCommand should be `using`
			object result = await cmd.ExecuteScalarAsync();
			return result?.ToString() ?? string.Empty;
		}


		private string GetLoginQuery(bool isDevAccess)
		{
			return isDevAccess
				? "SELECT DISTINCT [Employee ID], [Employee Name], [Username], [Email Address], [First Time Login], [Position], [User Access], [Department], [Office], [Theme], [Status]  FROM [User Information] WHERE USERNAME = @username"
				: "SELECT DISTINCT [Employee ID], [Employee Name], [Username], [Email Address], [First Time Login], [Position], [User Access], [Department], [Office], [Theme], [Status]  FROM [User Information] WHERE USERNAME = @username AND PASSWORD = @password";
		}

		public async Task<(bool isSuccess, string alertMessage, string EmpName, string userName, string email,
		string firstTime, string userPosition, string userAccess, string userDept, string officeLoc, string theme)>
		ProcessLoginAsync(SqlDataReader readerSQL)
		{
			try
			{
				if (readerSQL == null || readerSQL.IsClosed || !await readerSQL.ReadAsync())
				{
					return (false, "Database error: Unable to retrieve user data.", null, null, null, null, null, null, null, null, null);
				}

				using (readerSQL) // Ensuring proper disposal of the SqlDataReader
				{
					string userStatus = readerSQL["Status"]?.ToString() ?? "UNKNOWN";
					if (userStatus.Equals("INACTIVE", StringComparison.OrdinalIgnoreCase))
					{
						return (false, "Your account is inactive. Please contact your administrator.", null, null, null, null, null, null, null, null, null);
					}

					// Fetch user details safely
					string empName = readerSQL["Employee Name"]?.ToString() ?? string.Empty;
					string userName = readerSQL["Username"]?.ToString() ?? string.Empty;
					string email = readerSQL["Email Address"]?.ToString() ?? string.Empty;
					string firstTime = readerSQL["First Time Login"]?.ToString() ?? string.Empty;
					string userPosition = readerSQL["Position"]?.ToString() ?? string.Empty;
					string userAccess = readerSQL["User Access"]?.ToString() ?? string.Empty;
					string userDept = readerSQL["Department"]?.ToString() ?? string.Empty;
					string officeLoc = readerSQL["Office"]?.ToString() ?? string.Empty;
					string theme = readerSQL["Theme"]?.ToString() ?? string.Empty;
					theme = theme ?? ConfigureUserThemeIfMissing(readerSQL, userName);
					return (true, "Login successful.", empName, userName, email, firstTime, userPosition, userAccess, userDept, officeLoc, theme);
				}
			}
			catch (SqlException ex)
			{
				return (false, $"SQL Error: {ex.Message}", null, null, null, null, null, null, null, null, null);
			}
			catch (Exception ex)
			{
				return (false, $"Error: {ex.Message}", null, null, null, null, null, null, null, null, null);
			}
		}


				//	if (string.IsNullOrWhiteSpace(EmpName) || string.IsNullOrWhiteSpace(userName))
				//	{
				//		alertMessage = "User details are incomplete. Contact administrator.";
				//		return;
				//	}
				//
				//	// Handle first-time login
				//	if (firstTime.Equals("YES", StringComparison.OrdinalIgnoreCase))
				//	{
				//		(username, password, EmpName, isLoginPanelEnabled, alertMessage) =
				//			PromptFirstTimePasswordChange(username, password, EmpName, isLoginPanelEnabled, email);
				//		Console.WriteLine(alertMessage);
				//	}
				//
				//	// Configure user theme if missing
				//	if (string.IsNullOrWhiteSpace(theme))
				//	{
				//		alertMessage = await Task.Run(() => ConfigureUserThemeIfMissing(readerSQL, username));
				//	}
				//
				//	// Log user information
				//	string logMessage = $"{EmpName} logged in to PCMS General Tool\nTime Logged In: {DateTime.Now:G}\nUser Access: {UserAccess}\nPosition: {Position}\nLocation: {officeLoc}";
				//	await Task.Run(() => SetMainAppProperties(mainApp, readerSQL, logMessage, EmpName, userName, UserAccess, Position, officeLoc, theme));
				//
				//	// Determine which form to show
				//	bool showDemoTool = await Task.Run(() => ConfigureVisibility(mainApp, demoTool, UserAccess, Department, Position, readerSQL));
				//
				//	// Ensure UI updates are performed on the main thread
				//	if (mainApp.InvokeRequired)
				//	{
				//		mainApp.Invoke(new Action(() => ConfigureAndShowForm(showDemoTool ? demoTool : mainApp, EmpName)));
				//	}
				//	else
				//	{
				//		ConfigureAndShowForm(showDemoTool ? demoTool : mainApp, EmpName);
				//	}
				//}

	

		//		private void ProcessLogin(
		//	SqlDataReader readerSQL,
		//	string username,
		//	string password,
		//	bool isLoginPanelEnabled,
		//	string alertMessage)
		//		{
		//			try
		//			{
		//				// Ensure readerSQL is valid
		//				if (readerSQL == null || readerSQL.IsClosed)
		//				{
		//					alertMessage = "Database error: Unable to retrieve user data.";
		//					return;
		//				}
		//
		//				// Read and validate user status
		//				string userStatus = readerSQL.GetString(readerSQL.GetOrdinal("Status"));
		//				if (userStatus.Equals("INACTIVE", StringComparison.OrdinalIgnoreCase))
		//				{
		//					var (AlertMessage, IsLoginPanelEnabled) = HandleInactiveUser(username, password);
		//					alertMessage = AlertMessage;
		//					isLoginPanelEnabled = IsLoginPanelEnabled;
		//					return;
		//				}
		//
		//				// Extract user details
		//				EmpName = readerSQL.GetString(readerSQL.GetOrdinal("Employee Name"));
		//				userName = readerSQL.GetString(readerSQL.GetOrdinal("Username"));
		//				email = readerSQL.GetString(readerSQL.GetOrdinal("Email Address"));
		//				firstTime = readerSQL.GetString(readerSQL.GetOrdinal("First Time Login"));
		//
		//				// First-time login check
		//				if (firstTime.Equals("YES", StringComparison.OrdinalIgnoreCase))
		//				{
		//					// Capture the updated values with unique names
		//					var (updatedUsername, updatedPassword, updatedEmpName, updatedIsLoginPanelEnabled, updatedAlertMessage) =
		//						PromptFirstTimePasswordChange(username, password, EmpName, isLoginPanelEnabled, email);
		//
		//					// Assign back to original variables if needed
		//					username = updatedUsername;
		//					password = updatedPassword;
		//					EmpName = updatedEmpName;
		//					isLoginPanelEnabled = updatedIsLoginPanelEnabled;
		//					alertMessage = updatedAlertMessage;
		//
		//					Console.WriteLine(alertMessage);
		//
		//				}
		//
		//				// Load additional user details
		//				Position = readerSQL.GetString(readerSQL.GetOrdinal("Position"));
		//				UserAccess = readerSQL.GetString(readerSQL.GetOrdinal("User Access"));
		//				Department = readerSQL.GetString(readerSQL.GetOrdinal("Department"));
		//				officeLoc = readerSQL.GetString(readerSQL.GetOrdinal("Office"));
		//				theme = readerSQL.GetString(readerSQL.GetOrdinal("Theme"));
		//
		//				// Configure user theme if missing
		//				if (string.IsNullOrWhiteSpace(theme))
		//				{
		//					alertMessage = ConfigureUserThemeIfMissing(readerSQL, username);
		//					//ConfigureUserThemeIfMissing(readerSQL, username, alertMessage);
		//				}
		//
		//				// Log user information
		//				string logMessage = $"{EmpName} logged in PCMS General Tool\nTime Logged In: {DateTime.Now:G}\nUser Access: {UserAccess}\nPosition: {Position}\nLocation: {officeLoc}";
		//				SetMainAppProperties(mainApp, readerSQL, logMessage);
		//
		//				// Determine visibility of mainApp or demoTool
		//				bool showDemoTool = ConfigureVisibility(mainApp, demoTool, UserAccess, Department, Position, readerSQL);
		//
		//				// Display the correct form
		//				if (showDemoTool)
		//				{
		//					ConfigureAndShowForm(demoTool, EmpName);
		//				}
		//				else
		//				{
		//					ConfigureAndShowForm(mainApp, EmpName);
		//				}
		//			}
		//			catch (SqlException ex)
		//			{
		//				notif.LogError("ProcessLogin", EmpName, "Login", ex.Message, ex);
		//				alertMessage = "Something went wrong";
		//				fe.SendToastNotifDesktop(alertMessage, "Something went wrong");
		//			}
		//			catch (Exception ex)
		//			{
		//				notif.LogError("ProcessLogin", EmpName, "Login", ex.Message, ex);
		//				alertMessage = "Something went wrong";
		//				fe.SendToastNotifDesktop(alertMessage, "Something went wrong");
		//			}
		//		}
		//
		// Helper method to configure and show form
		private void ConfigureAndShowForm(Form form, string employeeName)
		{
			form.Text = $"{ProgName} ver. {ProgVer} ({employeeName})";
			form.Show();
		}

		private string ConfigureUserThemeIfMissing(SqlDataReader readerSQL, string username)
		{
			string theme = string.Empty;

			try
			{
				// Get the ordinal position of the "Theme" column
				int themeOrdinal = readerSQL.GetOrdinal("Theme");

				// If theme is NOT null, return the existing theme
				if (!readerSQL.IsDBNull(themeOrdinal))
				{
					return readerSQL["Theme"]?.ToString() ?? "Crystal"; // Return existing or default theme
				}

				// If theme is null, update it to the default value
				const string query = "UPDATE [User Information] SET THEME = @theme WHERE USERNAME = @username";

				using (var conSQL = new SqlConnection(_dbConnection))
				using (var cmdTheme = new SqlCommand(query, conSQL))
				{
					theme = "Crystal"; // Default theme
					cmdTheme.Parameters.Add("@theme", SqlDbType.VarChar, 50).Value = theme;
					cmdTheme.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;

					conSQL.Open();
					int rowsAffected = cmdTheme.ExecuteNonQuery();

					if (rowsAffected == 0)
					{
						notif.LogError("ConfigureUserThemeIfMissing", username, "Login", "Theme update failed. No rows affected.", null);
						return "Theme update failed. No rows affected."; // Error message
					}
				}
			}
			catch (SqlException ex)
			{
				notif.LogError("ConfigureUserThemeIfMissing", username, "Login", ex.Message, ex);
				return "Database error occurred while updating theme.";
			}
			catch (Exception ex)
			{
				notif.LogError("ConfigureUserThemeIfMissing", username, "Login", ex.Message, ex);
				return "An unexpected error occurred.";
			}

			return theme; // Return the theme (updated or existing)
		}


		//private void SetMainAppProperties(frmMainApp mainApp, SqlDataReader readerSQL, string logMessage,
		//							  string EmpName, string userName, string UserAccess, string Position,
		//							  string officeLoc, string theme)
		//{
		//	try
		//	{
		//		mainApp.employeeID = readerSQL.GetString(readerSQL.GetOrdinal("Employee ID"));
		//		mainApp.EmpName = EmpName;
		//		mainApp.userName = userName;
		//		mainApp.accessLevel = UserAccess;
		//		mainApp.statlblUsername.Text = EmpName;
		//		mainApp.statlblAccess.Text = UserAccess;
		//		mainApp.statlblPosition.Text = Position;
		//		mainApp.officeLoc = officeLoc;
		//		mainApp.themeName = theme;
		//		mainApp.position = Position;
		//		mainApp._dbConnection = _dbConnection;
		//
		//		log.AddActivityLog(logMessage, EmpName, $"{EmpName} logged in", "USER LOGGED IN");
		//	}
		//	catch (Exception ex)
		//	{
		//		notif.LogError("SetMainAppProperties", EmpName, "Login", $"Error setting main app properties: {ex.Message}", ex);
		//	}
		//}
		//
		//private bool ConfigureVisibility(frmMainApp mainApp, frmDemoTool demoTool, string role, string department, string position, SqlDataReader readerSQL)
		//{
		//	try
		//	{
		//		string combineRole = $"{role}_{department}_{position}";
		//		frmMainApp appInstance = mainApp; // Capture local variable
		//
		//		var rolesMapping = new Dictionary<HashSet<string>, Action>
		//{
		//	{ new HashSet<string> { "Programmer_All Department_Programmer", "Programmer_IT_Programmer" },
		//	  () => { appInstance.pictureBox1.Visible = true; } },
		//
		//	{ new HashSet<string> { "Administrator_All Department_Supervisor", "Administrator_All Department_Operations Manager", "Administrator_IT_Operations Manager" },
		//	  () => HideAdminControls(appInstance) },
		//
		//	{ new HashSet<string> { "Management_All Department_Supervisor", "Management_All Department_Operations Manager" },
		//	  () => { HideManagementControls(appInstance); appInstance.mnuManageProduct.Visibility = ElementVisibility.Collapsed; } },
		//
		//	{ new HashSet<string> { "Power User_Private_Collector", "User_Private_Collector" },
		//	  () => HidePrivateCollectorControls(appInstance) },
		//
		//	{ new HashSet<string> { "Power User_Workers Comp_Private_Collector", "User_Workers Comp_Collector" },
		//	  () => HideWorkersCollectorControls(appInstance) },
		//
		//	{ new HashSet<string> { "Power User_Private_Back Office", "Power User_Worker Comp_Back Office", "User_Private_Back Office", "User_Workers Comp_Back Office" },
		//	  () =>
		//	  {
		//		  if (readerSQL != null && !readerSQL.IsClosed)
		//		  {
		//			  SetupBackOfficeUser(demoTool, readerSQL);
		//		  }
		//	  }
		//	}
		//};
		//
		//		foreach (var entry in rolesMapping)
		//		{
		//			if (entry.Key.Contains(combineRole))
		//			{
		//				entry.Value.Invoke();
		//				return entry.Key == rolesMapping.Keys.Last();
		//			}
		//		}
		//
		//		notif.LogError("ConfigureVisibility", EmpName, "Login", @$"Role not configured properly
        //Name: {EmpName}
        //Access: {role}
        //Position: {position}
        //Department: {department}", null);
		//
		//		Application.Exit();
		//		return false;
		//	}
		//	catch (Exception ex)
		//	{
		//		notif.LogError("ConfigureVisibility", EmpName, "Login", $"Error configuring visibility: {ex.Message}", ex);
		//		return false;
		//	}
		//}
		//

		

		//private void ApplyVisibility(frmMainApp mainApp, params RadMenuItem[] menuItems)
		//{
		//	foreach (var item in menuItems) item.Visibility = ElementVisibility.Collapsed;
		//	mainApp.pictureBox1.BringToFront();
		//}

		//private void SetupBackOfficeUser(frmDemoTool demoTool, SqlDataReader readerSQL)
		//{
		//	try
		//	{
		//		demoTool.employeeID = readerSQL.GetString(readerSQL.GetOrdinal("Employee ID"));
		//		demoTool.EmpName = EmpName;
		//		demoTool.userName = userName;
		//		demoTool.accessLevel = UserAccess;
		//		demoTool.statlblUsername.Text = EmpName;
		//		demoTool.statlblAccess.Text = UserAccess;
		//		demoTool.statlblPosition.Text = Position;
		//		demoTool.officeLoc = officeLoc;
		//		demoTool._dbConnection = _dbConnection;
		//		demoTool.Text = $"{ProgName} | Demo Tool | ({EmpName})";
		//	}
		//	catch (Exception ex)
		//	{
		//		notif.LogError("SetupBackOfficeUser", EmpName, "Login", $"Error setting up back office user: {ex.Message}", ex);
		//	}
		//}




//		private void SetMainAppProperties(frmMainApp mainApp, SqlDataReader readerSQL, string logMessage)
//		{
//			mainApp.employeeID = readerSQL.GetString(readerSQL.GetOrdinal("Employee ID"));
//			mainApp.EmpName = EmpName;
//			mainApp.userName = userName;
//			mainApp.accessLevel = UserAccess;
//			mainApp.statlblUsername.Text = EmpName;
//			mainApp.statlblAccess.Text = UserAccess;
//			mainApp.statlblPosition.Text = Position;
//			mainApp.officeLoc = officeLoc;
//			mainApp.themeName = theme;
//			mainApp.position = Position;
//			mainApp._dbConnection = _dbConnection;
//
//			log.AddActivityLog(logMessage, EmpName, $"{EmpName} logged in", "USER LOGGED IN");
//		}
//
//		private bool ConfigureVisibility(frmMainApp mainApp, frmDemoTool demoTool, string role, string department, string position, SqlDataReader readerSQL)
//		{
//			HashSet<string> programmerRoles =
//			[
//				"Programmer_All Department_Programmer",
//				"Programmer_IT_Programmer"];
//			HashSet<string> adminRoles =
//			[
//			"Administrator_All Department_Supervisor",
//			"Administrator_All Department_Operations Manager",
//			"Administrator_IT_Operations Manager"
//		];
//			HashSet<string> managementRoles =
//			[
//			"Management_All Department_Supervisor",
//			"Management_All Department_Operations Manager"
//		];
//			HashSet<string> privateCollectorRoles =
//			[
//			"Power User_Private_Collector",
//			"User_Private_Collector"
//		];
//			HashSet<string> workersCollectorRoles =
//			[
//			"Power User_Workers Comp_Private_Collector",
//			"User_Workers Comp_Collector"
//		];
//			HashSet<string> backOfficeRoles =
//			[
//			"Power User_Private_Back Office",
//			"Power User_Worker Comp_Back Office",
//			"User_Private_Back Office",
//			"User_Workers Comp_Back Office"
//		];
//
//			string combineRole = $"{role}_{department}_{position}";
//
//			if (programmerRoles.Contains(combineRole))
//			{
//				mainApp.pictureBox1.Visible = true;
//				return false;
//			}
//			else if (adminRoles.Contains(combineRole))
//			{
//				HideAdminControls(mainApp);
//				return false;
//			}
//			else if (managementRoles.Contains(combineRole))
//			{
//				HideManagementControls(mainApp);
//				mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
//				return false;
//			}
//			else if (privateCollectorRoles.Contains(combineRole))
//			{
//				HidePrivateCollectorControls(mainApp);
//				return false;
//			}
//			else if (workersCollectorRoles.Contains(combineRole))
//			{
//				HideWorkersCollectorControls(mainApp);
//				return false;
//			}
//			else if (backOfficeRoles.Contains(combineRole))
//			{
//				if (readerSQL != null && !readerSQL.IsClosed)
//				{
//					SetupBackOfficeUser(demoTool, readerSQL);
//					return true;
//				}
//			}
//			else
//			{
//				notif.LogError("ConfigureVisibility", EmpName, "Login", @$"Role not configured properly
//Name: {EmpName}
//Access: {role}
//Position: {position}
//Departmen: {department}", null);
//				//fe.SendToastNotifDesktop("Your role or access is not configured properly. Please check with Erwin.", "Error");
//				Application.Exit();
//			}
//			return false;
//		}	//
//
//		private void HideAdminControls(frmMainApp mainApp)
//		{
//			Console.WriteLine("Hiding Admin controls");
//			mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuActivityLog.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuDBSequence.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
//			mainApp.pictureBox1.BringToFront();
//		}
//
//		private void HideManagementControls(frmMainApp mainApp)
//		{
//			Console.WriteLine("Hiding management controls");
//			mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuActivityLog.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuDBSequence.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
//			mainApp.pictureBox1.BringToFront();
//		}
//
//		private void HidePrivateCollectorControls(frmMainApp mainApp)
//		{
//			Console.WriteLine("Hiding Collector controls");
//			mainApp.mnuWorkcomp.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuBackOffice.Visibility = ElementVisibility.Collapsed;
//			mainApp.separator1.Visibility = ElementVisibility.Collapsed;
//			mainApp.seperator2.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuDBSequence.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuActivityLog.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuAssignProvider.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
//			mainApp.pictureBox1.BringToFront();
//		}
//
//		private void HideWorkersCollectorControls(frmMainApp mainApp)
//		{
//			Console.WriteLine("Hiding Collector controls");
//			mainApp.mnuPrivateCollectors.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuBackOffice.Visibility = ElementVisibility.Collapsed;
//			mainApp.separator1.Visibility = ElementVisibility.Collapsed;
//			mainApp.seperator2.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
//			mainApp.mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
//			mainApp.pictureBox1.BringToFront();
//		}
//
//		private void SetupBackOfficeUser(frmDemoTool demoTool, SqlDataReader readerSQL)
//		{
//			Console.WriteLine("Setting up BackOffice user controls");
//
//			demoTool.employeeID = readerSQL.GetString(readerSQL.GetOrdinal("Employee ID"));// Ensure index 10 is correct
//			demoTool.EmpName = EmpName; // Ensure EmpName is correctly initialized
//			demoTool.userName = userName;
//			demoTool.accessLevel = UserAccess;
//			demoTool.statlblUsername.Text = EmpName;
//			demoTool.statlblAccess.Text = UserAccess;
//			demoTool.statlblPosition.Text = Position;
//			demoTool.officeLoc = officeLoc;
//			demoTool._dbConnection = _dbConnection;
//			demoTool.Text = $"{ProgName} | Demo Tool | ({EmpName})";
//
//		}
//
		private (string AlertMessage, bool IsLoginPanelEnabled) HandleInactiveUser(string username, string password)
		{
			string alertMessage = $"YOUR ACCOUNT HAS BEEN DISABLED (Username: {username})";
			bool isLoginPanelEnabled = true;

			var resetValues = DefaultLoginSet();
			username = resetValues.Username;
			password = resetValues.Password;
			isLoginPanelEnabled = resetValues.IsLoginPanelEnabled;
			alertMessage = resetValues.AlertMessage;

			return (alertMessage, isLoginPanelEnabled);
		}


		private (string UpdatedUsername, string UpdatedPassword, string UpdatedEmpName, bool UpdatedIsLoginPanelEnabled, string UpdatedAlertMessage)
PromptFirstTimePasswordChange(string username, string password, string empName, bool isLoginPanelEnabled, string email)
		{
			// Simulate password change prompt logic
			var mandatoryChangePassword = new frmResetPassword
			{
				txtEmpID = { Text = username },
				txtWorkEmail = { Text = email },
				empName = empName,
				changePassword = "Yes"
			};

			mandatoryChangePassword.ShowDialog();

			// Assign the returned values from DefaultLoginSet
			var resetValues = DefaultLoginSet();
			username = resetValues.Username;
			password = resetValues.Password;
			isLoginPanelEnabled = resetValues.IsLoginPanelEnabled;

			string alertMessage = $"First-time login detected. Password reset email sent to {email}.";

			return (username, password, empName, isLoginPanelEnabled, alertMessage);
		}




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


	//public class Login
	//{
	//	private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
	//	private readonly CommonTask task = new();
	//	private readonly SecurityEncryption secEnc = new();
	//	public static string ProgName = Global.ProgName;
	//	public static string ProgVer = Global.ProgVer;
	//	public static string Dev = Global.Dev;
	//	public string conStatus;
	//	private string EmpName;
	//	//private string userName;
	//	private string Position;
	//	private string Department;
	//	private string UserStatus;
	//	private string UserAccess;
	//	private string officeLoc;
	//	private string firstTime;
	//	private string email;
	//	private string devAccess;
	//	private string theme;
	//
	//	public void DefaultLoginSet(ref string username, ref string password, ref bool isLoginPanelEnabled, out string alertMessage)
	//	{
	//		username = string.Empty;
	//		password = string.Empty;
	//		isLoginPanelEnabled = true;
	//		alertMessage = string.Empty;
	//	}
	//
	//	public void CheckConnectivity()
	//	{
	//		try
	//		{
	//			DBConStatus dBConStat = new(_dbConnection);
	//			conStatus = dBConStat.IsConnected ? "Connected | Login" : "Disconnected | Login";
	//		}
	//		catch (Exception)
	//		{
	//			conStatus = "Disconnected | Login";
	//		}
	//	}
	//
	//	public void UserLogin(ref string username, ref string password, ref bool isLoginPanelEnabled, ref string alertMessage, string empName)
	//	{
	//		string conquery;
	//		isLoginPanelEnabled = false;
	//
	//		try
	//		{
	//			using var conSQL = new SqlConnection(_dbConnection);
	//			conSQL.Open();
	//			devAccess = GetDeveloperAccess(conSQL);
	//
	//			// Determine query based on developer access
	//			conquery = GetLoginQuery(password == devAccess);
	//
	//			using var cmdSQL = new SqlCommand(conquery, conSQL);
	//			cmdSQL.Parameters.AddWithValue("@username", username);
	//			cmdSQL.Parameters.AddWithValue("@password", secEnc.PassHash(password));
	//
	//			using var readerSQL = cmdSQL.ExecuteReader();
	//			if (readerSQL.Read())
	//			{
	//				ProcessLogin(readerSQL, ref username, ref password, ref isLoginPanelEnabled, ref alertMessage, empName);
	//			}
	//			else
	//			{
	//				HandleInvalidPassword(ref alertMessage, ref password, ref username, ref isLoginPanelEnabled);
	//			}
	//		}
	//		catch (Exception ex)
	//		{
	//			LogAndNotifyError(ex, username, ref alertMessage);
	//			isLoginPanelEnabled = true;
	//		}
	//	}
	//
	//	// Helper Methods
	//
	//	private string GetDeveloperAccess(SqlConnection conSQL)
	//	{
	//		string developerAccess = string.Empty;
	//		var checkDevPassword = "SELECT DeveloperAccess FROM [User Information] WHERE Username = 'Erwin'";
	//
	//		using var cmdSQLDevPass = new SqlCommand(checkDevPassword, conSQL);
	//		using var readerDevPassword = cmdSQLDevPass.ExecuteReader();
	//		if (readerDevPassword.Read())
	//		{
	//			developerAccess = readerDevPassword.GetString(0);
	//		}
	//		return developerAccess;
	//	}
	//
	//	private string GetLoginQuery(bool isDevAccess)
	//	{
	//		return isDevAccess
	//			? "SELECT DISTINCT USERNAME, PASSWORD, [EMPLOYEE NAME], POSITION, [USER ACCESS], [DEPARTMENT], [STATUS], [OFFICE], [FIRST TIME LOGIN], [EMAIL ADDRESS], [Employee ID], THEME, [Employment Status] FROM [User Information] WHERE USERNAME = @username"
	//			: "SELECT DISTINCT USERNAME, PASSWORD, [EMPLOYEE NAME], POSITION, [USER ACCESS], [DEPARTMENT], [STATUS], [OFFICE], [FIRST TIME LOGIN], [EMAIL ADDRESS], [Employee ID], THEME, [Employment Status] FROM [User Information] WHERE USERNAME = @username AND PASSWORD = @password";
	//	}
	//
	//	private void ProcessLogin(SqlDataReader readerSQL, ref string username, ref string password, ref bool isLoginPanelEnabled, ref string alertMessage, string empName)
	//	{
	//		UserStatus = readerSQL.GetString(6);
	//
	//		if (string.Equals(UserStatus, "INACTIVE", StringComparison.OrdinalIgnoreCase))
	//		{
	//			HandleInactiveUser(ref alertMessage, ref password, ref username, ref isLoginPanelEnabled);
	//			return;
	//		}
	//
	//		username = readerSQL.GetString(0);
	//		EmpName = readerSQL.GetString(2);
	//		email = readerSQL.GetString(9);
	//		firstTime = readerSQL.GetString(8);
	//
	//		if (string.Equals(firstTime, "YES", StringComparison.OrdinalIgnoreCase))
	//		{
	//			PromptFirstTimePasswordChange(ref username, ref password, ref isLoginPanelEnabled, ref alertMessage, email);
	//		}
	//		else
	//		{
	//			Position = readerSQL.GetString(3);
	//			UserAccess = readerSQL.GetString(4);
	//			Department = readerSQL.GetString(5);
	//			officeLoc = readerSQL.GetString(7);
	//			theme = readerSQL.GetString(11);
	//
	//			// Further user role-specific configuration can be done here
	//		}
	//	}
	//
	//	private void HandleInactiveUser(ref string alertMessage, ref string password, ref string username, ref bool isLoginPanelEnabled)
	//	{
	//		alertMessage = $"YOUR ACCOUNT HAS BEEN DISABLED (Username: {username})";
	//		isLoginPanelEnabled = true;
	//		DefaultLoginSet(ref username, ref password, ref isLoginPanelEnabled, out _);
	//	}
	//
	//	private void PromptFirstTimePasswordChange(ref string username, ref string password, ref bool isLoginPanelEnabled, ref string alertMessage, string email)
	//	{
	//		// Implementation for first-time password change
	//		DefaultLoginSet(ref username, ref password, ref isLoginPanelEnabled, out alertMessage);
	//	}
	//
	//	private void HandleInvalidPassword(ref string alertMessage, ref string password, ref string username, ref bool isLoginPanelEnabled)
	//	{
	//		alertMessage = "PASSWORD IS INCORRECT";
	//		string logs = $"Incorrect password used for {username}";
	//		log.AddActivityLog(logs, "", logs, "INCORRECT PASSWORD");
	//		isLoginPanelEnabled = true;
	//		password = string.Empty;
	//	}
	//
	//	private void LogAndNotifyError(Exception ex, string username, ref string alertMessage)
	//	{
	//		string errorMessage = $"Error processing login for {username}:\n{ex}";
	//		notif.LogError("LogAndNotifyError", EmpName, "Login", errorMessage, ex);
	//		alertMessage = "An error occurred, please check with Developer.";
	//	}
	//}

	//public class Login
	//{
	//	private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
	//	private readonly CommonTask task = new();
	//	private readonly SecurityEncryption secEnc = new();
	//	public static string ProgName = Global.ProgName;
	//	public static string ProgVer = Global.ProgVer;
	//	public static string Dev = Global.Dev;
	//	public string conStatus;
	//	private string EmpName;
	//	private string userName;
	//	private string Position;
	//	private string Department;
	//	private string UserStatus;
	//	private string UserAccess;
	//	private string officeLoc;
	//	private string firsTime;
	//	private string email;
	//	private string devAccess;
	//	private string theme;
	//
	//
	//	public void DefaultLoginSet(RadTextBox txtUsername, RadTextBox txtPassword, RadPanel loginPanel, RadLabel lblAlert)
	//	{
	//		txtUsername.Focus();
	//		txtUsername.Clear();
	//		txtPassword.Clear();
	//		loginPanel.Enabled = true;
	//		lblAlert.Text = string.Empty;
	//	}
	//
	//	public void CheckConnectivity()
	//	{
	//		try
	//		{
	//			DBConStatus dBConStat = new(_dbConnection);
	//			if (dBConStat.IsConnected)
	//			{
	//				conStatus = "Connected | Login";
	//			}
	//			else
	//			{
	//				conStatus = "Disconnected | Login";
	//			}
	//		}
	//		catch (Exception)
	//		{
	//
	//			conStatus = "Disconnected | Login";
	//		}
	//	}
	//
	//	public void UserLogin(RadTextBox txtUsername, RadTextBox txtPassword, RadPanel loginPanel, RadLabel lblalert, RadForm frmLogin)
	//	{
	//		string conquery;
	//		loginPanel.Enabled = false;
	//
	//		try
	//		{
	//			using var conSQL = new SqlConnection(_dbConnection);
	//			conSQL.Open();
	//			devAccess = GetDeveloperAccess(conSQL);
	//
	//			// Determine query based on developer access
	//			conquery = GetLoginQuery(txtPassword.Text == devAccess);
	//
	//			using var cmdSQL = new SqlCommand(conquery, conSQL);
	//			cmdSQL.Parameters.AddWithValue("@username", txtUsername.Text);
	//			cmdSQL.Parameters.AddWithValue("@password", secEnc.PassHash(txtPassword.Text));
	//
	//			using var readerSQL = cmdSQL.ExecuteReader();
	//			if (readerSQL.Read())
	//			{
	//				ProcessLogin(readerSQL, txtUsername, txtPassword, loginPanel, lblalert, frmLogin);
	//			}
	//			else
	//			{
	//				HandleInvalidPassword(lblalert, txtPassword, txtUsername, loginPanel);
	//			}
	//		}
	//		catch (Exception ex)
	//		{
	//			LogAndNotifyError(ex, txtUsername.Text, lblalert);
	//			loginPanel.Enabled = true;
	//		}
	//	}
	//
	//	// Helper Methods
	//
	//	private string GetDeveloperAccess(SqlConnection conSQL)
	//	{
	//		string developerAccess = string.Empty;
	//		var checkDevPassword = "SELECT DeveloperAccess FROM [User Information] WHERE Username = 'Erwin'";
	//
	//		using (var cmdSQLDevPass = new SqlCommand(checkDevPassword, conSQL))
	//		{
	//			using var readerDevPassword = cmdSQLDevPass.ExecuteReader();
	//			if (readerDevPassword.Read())
	//			{
	//				developerAccess = readerDevPassword.GetString(0);
	//			}
	//		}
	//		return developerAccess;
	//	}
	//
	//	private string GetLoginQuery(bool isDevAccess)
	//	{
	//		return isDevAccess
	//			? "SELECT DISTINCT USERNAME, PASSWORD, [EMPLOYEE NAME], POSITION, [USER ACCESS], [DEPARTMENT], [STATUS], [OFFICE], [FIRST TIME LOGIN], [EMAIL ADDRESS], [Employee ID], THEME, [Employment Status] FROM [User Information] WHERE USERNAME = @username"
	//			: "SELECT DISTINCT USERNAME, PASSWORD, [EMPLOYEE NAME], POSITION, [USER ACCESS], [DEPARTMENT], [STATUS], [OFFICE], [FIRST TIME LOGIN], [EMAIL ADDRESS], [Employee ID], THEME, [Employment Status] FROM [User Information] WHERE USERNAME = @username AND PASSWORD = @password";
	//	}
	//
	//	private void ProcessLogin(SqlDataReader readerSQL, RadTextBox txtUsername, RadTextBox txtPassword, RadPanel loginPanel, RadLabel lblalert, RadForm frmLogin)
	//	{
	//		var mainApp = new frmMainApp();
	//		var demoTool = new frmDemoTool();
	//
	//		var conSQL = new SqlConnection(_dbConnection);
	//
	//		UserStatus = readerSQL.GetString(6);
	//
	//		if (string.Equals(UserStatus, "INACTIVE", StringComparison.OrdinalIgnoreCase))
	//		{
	//			HandleInactiveUser(lblalert, txtPassword, txtUsername, loginPanel);
	//			return;
	//		}
	//
	//		userName = readerSQL.GetString(0);
	//		EmpName = readerSQL.GetString(2);
	//		email = readerSQL.GetString(9);
	//		firsTime = readerSQL.GetString(8);
	//
	//		if (string.Equals(firsTime, "YES", StringComparison.OrdinalIgnoreCase))
	//		{
	//			PromptFirstTimePasswordChange(txtUsername, txtPassword, loginPanel, lblalert, email);
	//		}
	//		else
	//		{
	//			Position = readerSQL.GetString(3);
	//			UserAccess = readerSQL.GetString(4);
	//			Department = readerSQL.GetString(5);
	//			officeLoc = readerSQL.GetString(7);
	//
	//			ConfigureUserThemeIfMissing(readerSQL, conSQL, txtUsername);
	//			// Further user role-specific configuration can be done here
	//			theme = readerSQL.GetString(11);
	//			var logMessage = $"{EmpName} logged in PCMS General Tool\nTime Logged In: {DateTime.Now:G}\nUser Access: {UserAccess}\nPosition: {Position}\nLocation: {officeLoc}";
	//			try
	//			{
	//				switch (UserAccess)
	//				{
	//					case "Programmer":
	//					case "Administrator":
	//					case "Management":
	//					case "Power User":
	//					case "User":
	//						frmLogin.Hide();
	//						SetMainAppProperties(mainApp, readerSQL, logMessage);
	//						//Console.WriteLine($"Calling ConfigureVisibility with role: {UserAccess}");
	//						bool showDemoTool = ConfigureVisibility(mainApp, demoTool, UserAccess, Department, Position, readerSQL);
	//
	//						if (showDemoTool)
	//						{
	//							demoTool.Text = $"{ProgName} ver. {ProgVer} ({EmpName})";
	//							demoTool.Show();
	//						}
	//						else
	//						{
	//							mainApp.Text = $"{ProgName} ver. {ProgVer} ({EmpName})";
	//							mainApp.Show();
	//						}
	//						break;
	//					default:
	//						throw new InvalidOperationException($"Unhandled user role: {UserAccess}");
	//				}
	//			}
	//			catch (Exception ex)
	//			{
	//				notif.LogError("ProcessLogin", EmpName, "Login", txtUsername.Text, ex);
	//				lblalert.Text = "Something went wrong, please contact the developer.";
	//				loginPanel.Enabled = true;
	//			}
	//		}
	//	}
	//
	//	private void HandleInactiveUser(RadLabel lblalert, RadTextBox txtPassword, RadTextBox txtUsername, RadPanel loginPanel)
	//	{
	//		lblalert.Text = $"YOUR ACCOUNT HAS BEEN DISABLED (Username: {txtUsername.Text})";
	//		lblalert.Visible = true;
	//		loginPanel.Enabled = true;
	//		DefaultLoginSet(txtUsername, txtPassword, loginPanel, lblalert);
	//	}
	//
	//	private void PromptFirstTimePasswordChange(RadTextBox txtUsername, RadTextBox txtPassword, RadPanel loginPanel, RadLabel lblalert, string email)
	//	{
	//		var changePassword = new frmResetPassword
	//		{
	//			Text = "Change Password",
	//			changePassword = "Yes",
	//			btnOK = { Text = "Change Password" },
	//			txtWorkEmail = { Enabled = false, Text = email },
	//			txtEmpID = { Enabled = false, Text = userName },
	//			cmbLevel = UserAccess
	//		};
	//		changePassword.ShowDialog();
	//		DefaultLoginSet(txtUsername, txtPassword, loginPanel, lblalert);
	//	}
	//
	//	private void ConfigureUserThemeIfMissing(SqlDataReader readerSQL, SqlConnection conSQL, RadTextBox txtUsername)
	//	{
	//		if (readerSQL.IsDBNull(11))
	//		{
	//			string themeQuery = "UPDATE [User Information] SET THEME = @theme WHERE USERNAME = @username";
	//			using var cmdTheme = new SqlCommand(themeQuery, conSQL); // Use conSQL directly here
	//			cmdTheme.Parameters.AddWithValue("@theme", "Crystal");
	//			cmdTheme.Parameters.AddWithValue("@username", txtUsername.Text);
	//			cmdTheme.ExecuteNonQuery();
	//		}
	//	}
	//
	//
	//	private void HandleInvalidPassword(RadLabel lblalert, RadTextBox txtPassword, RadTextBox txtUsername, RadPanel loginPanel)
	//	{
	//		lblalert.Text = "PASSWORD IS INCORRECT";
	//		string logs = $"Incorrect password used for {txtUsername.Text}";
	//		log.AddActivityLog(logs, "", logs, "INCORRECT PASSWORD");
	//		lblalert.Visible = true;
	//		loginPanel.Enabled = true;
	//		txtPassword.Clear();
	//		txtUsername.Focus();
	//	}
	//
	//	private void LogAndNotifyError(Exception ex, string username, RadLabel lblalert)
	//	{
	//		string errorMessage = $"Error processing login for {username}:\n{ex}";
	//		notif.LogError("LogAndNotifyError", EmpName, "Login", errorMessage, ex);
	//		lblalert.Text = "An error occurred, please check with Developer.";
	//	}
	//
	//
	//	private void SetMainAppProperties(frmMainApp mainApp, SqlDataReader readerSQL, string logMessage)
	//	{
	//		mainApp.employeeID = readerSQL.GetString(10);
	//		mainApp.EmpName = EmpName;
	//		mainApp.userName = userName;
	//		mainApp.accessLevel = UserAccess;
	//		mainApp.statlblUsername.Text = EmpName;
	//		mainApp.statlblAccess.Text = UserAccess;
	//		mainApp.statlblPosition.Text = Position;
	//		mainApp.officeLoc = officeLoc;
	//		mainApp.themeName = theme;
	//		mainApp.position = Position;
	//
	//		log.AddActivityLog(logMessage, EmpName, $"{EmpName} logged in", "USER LOGGED IN");
	//	}
	//
	//
	//	private bool ConfigureVisibility(frmMainApp mainApp, frmDemoTool demoTool, string role, string department, string position, SqlDataReader readerSQL)
	//	{
	//		//mainApp.pgViewCollectorNotes.Visible = false;
	//		string combineRole = $"{role}_{department}_{position}";
	//
	//		if (combineRole.Contains("Programmer_All Department_Programmer"))
	//		{
	//			// Specific visibility settings for Programmer role can be added here
	//			mainApp.pictureBox1.BringToFront();
	//			return false; // Show MainApp by default for this role
	//		}
	//		else if (combineRole.Contains("Administrator_All Department_Supervisor") || combineRole.Contains("Administrator_All Department_Operations Manager"))
	//		{
	//			HideAdminControls(mainApp);
	//			return false; // Show MainApp for Administrator roles
	//		}
	//		else if (combineRole.Contains("Management_All Department_Supervisor") || combineRole.Contains("Management_All Department_Operations Manager"))
	//		{
	//			HideAdminControls(mainApp);
	//			mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//			return false; // Show MainApp for Management roles
	//		}
	//		else if (combineRole == "Power User_Private_Collector" || combineRole == "User_Private_Collector")
	//		{
	//			HidePrivateCollectorControls(mainApp);
	//			return false;
	//		}
	//		else if (combineRole == "Power User_Workers Comp_Private_Collector" || combineRole == "User_Workers Comp_Collector")
	//		{
	//			HideWorkersCollectorControls(mainApp);
	//			return false;
	//		}
	//		else if (combineRole == "Power User_Private_Back Office" || combineRole == "Power User_Worker Comp_Back Office" ||
	//				 combineRole == "User_Private_Back Office" || combineRole == "User_Workers Comp_Back Office")
	//		{
	//			SetupBackOfficeUser(demoTool, readerSQL);
	//			return true;
	//		}
	//		else
	//		{
	//			RadMessageBox.Show("Your role or access is not configured Properly, Please check with Erwin", "Role Exception", MessageBoxButtons.OK, RadMessageIcon.Error);
	//			Application.Exit();
	//		}
	//
	//		return false;
	//	}
	//
	//	private void HideAdminControls(frmMainApp mainApp)
	//	{
	//		Console.WriteLine("Hiding Admin controls");
	//		mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuActivityLog.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuDBSequence.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
	//		mainApp.pictureBox1.BringToFront();
	//	}
	//
	//	private void HidePrivateCollectorControls(frmMainApp mainApp)
	//	{
	//		Console.WriteLine("Hiding Collector controls");
	//		mainApp.mnuWorkcomp.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuBackOffice.Visibility = ElementVisibility.Collapsed;
	//		mainApp.separator1.Visibility = ElementVisibility.Collapsed;
	//		mainApp.seperator2.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuDBSequence.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuActivityLog.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuAssignProvider.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
	//		mainApp.pictureBox1.BringToFront();
	//	}
	//
	//	private void HideWorkersCollectorControls(frmMainApp mainApp)
	//	{
	//		Console.WriteLine("Hiding Collector controls");
	//		mainApp.mnuPrivateCollectors.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuBackOffice.Visibility = ElementVisibility.Collapsed;
	//		mainApp.separator1.Visibility = ElementVisibility.Collapsed;
	//		mainApp.seperator2.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//		mainApp.mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
	//		mainApp.pictureBox1.BringToFront();
	//	}
	//
	//	private void SetupBackOfficeUser(frmDemoTool demoTool, SqlDataReader readerSQL)
	//	{
	//		Console.WriteLine("Setting up BackOffice user controls");
	//
	//		demoTool.employeeID = readerSQL.GetString(10); // Ensure index 10 is correct
	//		demoTool.EmpName = EmpName; // Ensure EmpName is correctly initialized
	//		demoTool.userName = userName;
	//		demoTool.accessLevel = UserAccess;
	//		demoTool.statlblUsername.Text = EmpName;
	//		demoTool.statlblAccess.Text = UserAccess;
	//		demoTool.statlblPosition.Text = Position;
	//		demoTool.officeLoc = officeLoc;
	//		demoTool.Text = $"{ProgName} | Demo Tool | ({EmpName})";
	//
	//	}
	//

	//public void userLogin(RadTextBox txtUsername, RadTextBox txtPassword, RadPanel loginPanel, RadLabel lblalert, RadForm frmLogin)
	//{
	//	string conquery;
	//	loginPanel.Enabled = false;
	//	using (var conSQL = new SqlConnection(_dbConnection))
	//	{
	//		conSQL.Open();
	//		var checkDevPassword = "SELECT DeveloperAccess FROM [User Information] WHERE Username LIKE 'Erwin'";
	//		using (var cmdSQLDevPass = new SqlCommand(checkDevPassword, conSQL))
	//		{
	//			using (var readerDevPassword = cmdSQLDevPass.ExecuteReader())
	//			{
	//				if (readerDevPassword.Read())
	//				{
	//					devAccess = readerDevPassword.GetString(0);
	//				}
	//			}
	//		}
	//		if (txtPassword.Text == devAccess)
	//		{
	//			conquery = "SELECT DISTINCT USERNAME, PASSWORD, [EMPLOYEE NAME], POSITION, [USER ACCESS], [DEPARTMENT], [STATUS], [OFFICE], [FIRST TIME LOGIN], [EMAIL ADDRESS], [Employee ID],  THEME, [Employment Status] FROM [User Information] WHERE USERNAME = @username";
	//		}
	//		else
	//		{
	//			conquery = "SELECT DISTINCT USERNAME, PASSWORD, [EMPLOYEE NAME], POSITION, [USER ACCESS], [DEPARTMENT], [STATUS], [OFFICE], [FIRST TIME LOGIN], [EMAIL ADDRESS], [Employee ID], THEME, [Employment Status] FROM [User Information] WHERE USERNAME = @username AND PASSWORD = @password";
	//		}
	//		using (var cmdSQL = new SqlCommand(conquery, conSQL))
	//		{
	//			cmdSQL.Parameters.AddWithValue("@username", txtUsername.Text);
	//			cmdSQL.Parameters.AddWithValue("@password", secEnc.PassHash(txtPassword.Text));
	//			using (var readerSQL = cmdSQL.ExecuteReader())
	//			{
	//				if (readerSQL.Read())
	//				{
	//
	//					UserStatus = readerSQL.GetString(6);
	//					if (UserStatus == "INACTIVE" || UserStatus == "InActive" || UserStatus == "inactive")
	//					{
	//						//RadMessageBox.Show("User Account has been disable, please check with with the admin");
	//						lblalert.Text = "YOUR ACCOUNT HAS BEEN DISABLED (Username: " + txtUsername.Text + ")";
	//						lblalert.Visible = true;
	//						loginPanel.Enabled = true;
	//						DefaultLoginSet(txtUsername, txtPassword, loginPanel, lblalert);
	//					}
	//					else
	//					{
	//
	//						userName = readerSQL.GetString(0);
	//						EmpName = readerSQL.GetString(2);
	//						email = readerSQL.GetString(9);
	//						//idNumber = readerSQL.GetString(10);
	//						firsTime = readerSQL.GetString(8);
	//						if (firsTime == "YES" || firsTime == "Yes" || firsTime == "yes")
	//						{
	//							var changePassword = new frmResetPassword
	//							{
	//								Text = "Change Password",
	//								changePassword = "Yes"
	//							};
	//							changePassword.btnOK.Text = "Change Password";
	//							changePassword.txtWorkEmail.Enabled = false;
	//							changePassword.txtEmpID.Enabled = false;
	//							changePassword.txtEmpID.Text = userName;
	//							changePassword.txtWorkEmail.Text = email;
	//							changePassword.cmbLevel = UserAccess;
	//							changePassword.ShowDialog();
	//							DefaultLoginSet(txtUsername, txtPassword, loginPanel, lblalert);
	//						}
	//						else
	//						{
	//							Position = readerSQL.GetString(3);
	//							UserAccess = readerSQL.GetString(4);
	//							Department = readerSQL.GetString(5);
	//							officeLoc = readerSQL.GetString(7);
	//							if (readerSQL.IsDBNull(11))
	//							{
	//								var themeName = "UPDATE [User Information] SET THEME = 'Crystal' WHERE USERNAME='" + txtUsername.Text + "'";
	//								task.UpdateValues(themeName, EmpName, "Theme successfully updated");
	//								//RadMessageBox.Show("Done! setting environment", "Information", MessageBoxButtons.OK, RadMessageIcon.Info);
	//								DefaultLoginSet(txtUsername, txtPassword, loginPanel, lblalert);
	//							}
	//							else
	//							{
	//								var demoTool = new frmDemoTool();
	//								var mainApp = new frmMainApp();
	//								theme = readerSQL.GetString(11);
	//								try
	//								{
	//									string LogMessage = EmpName + " logged in PCMS General Tool \n" +
	//											"Time Logged In: " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString()
	//											+ "\nUser Access: " + UserAccess + "\nPosition: " + Position + "\nLocation: " + officeLoc;
	//									//string message = EmpName + " Succesfully Login";										
	//									switch (UserAccess)
	//									{
	//										case "Programmer":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10);
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.userName = userName;
	//											mainApp.EmpName = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.officeLoc = officeLoc;
	//											mainApp.themeName = theme;
	//											//log.AddActivityLog(txtAudtiID, message, EmpName, message, "LOG IN");
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										case "Administrator":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10);
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											mainApp.EmpName = EmpName;
	//											mainApp.userName = userName;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//											mainApp.officeLoc = officeLoc;
	//											mainApp.themeName = theme;
	//											//mainApp.mnuAdmin.Visibility = ElementVisibility.Visible;
	//											mainApp.mnuActivityLog.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDBSequence.Visibility = ElementVisibility.Collapsed;
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										case "Management":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10);
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											mainApp.EmpName = EmpName;
	//											mainApp.userName = userName;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.officeLoc = officeLoc;
	//											mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuActivityLog.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDBSequence.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//											mainApp.themeName = theme;
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										case "Power User" when Department == "All Department":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10);
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											mainApp.EmpName = EmpName;
	//											mainApp.userName = userName;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.officeLoc = officeLoc;
	//											mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//											mainApp.themeName = theme;
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										case "Power User" when Position == "Collector":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10);
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											mainApp.EmpName = EmpName;
	//											mainApp.userName = userName;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.officeLoc = officeLoc;
	//											mainApp.separator1.Visibility = ElementVisibility.Collapsed;
	//											mainApp.seperator2.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuBackOffice.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//											mainApp.themeName = theme;
	//											mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										case "Power User" when Department == "Private" && Position == "Collector":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10);
	//											mainApp.mnuWorkcomp.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuBackOffice.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											mainApp.separator1.Visibility = ElementVisibility.Collapsed;
	//											mainApp.seperator2.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//											mainApp.EmpName = EmpName;
	//											mainApp.userName = userName;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.officeLoc = officeLoc;
	//											mainApp.pictureBox1.Visible = false;
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										case "Power User" when Department == "Private" && Position == "Back Office":
	//											frmLogin.Hide();
	//											demoTool.employeeID = readerSQL.GetString(10);
	//											demoTool.EmpName = EmpName;
	//											demoTool.userName = userName;
	//											demoTool.accessLevel = UserAccess;
	//											demoTool.statlblUsername.Text = EmpName;
	//											demoTool.statlblAccess.Text = UserAccess;
	//											demoTool.statlblPosition.Text = Position;
	//											demoTool.officeLoc = officeLoc;
	//											demoTool.userName = userName;
	//											mainApp.themeName = theme;
	//											//demoTool.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											////demoTool.mnuEmpInfo.Visibility = ElementVisibility.Visible;
	//											//demoTool.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											demoTool.Text = ProgName + " | Demo Tool | (" + EmpName + ")";
	//											demoTool.accessLevel = "Power User";
	//											demoTool.EmpName = EmpName;
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											demoTool.Show();
	//											break;
	//
	//										case "Power User" when Department == "Workers Comp" && Position == "Collector":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10);
	//											mainApp.separator1.Visibility = ElementVisibility.Collapsed;
	//											mainApp.seperator2.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuBackOffice.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//											mainApp.userName = userName;
	//											mainApp.mnuPrivateCollectors.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuOnlineLogins.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//											mainApp.EmpName = EmpName;
	//											mainApp.userName = userName;
	//											mainApp.themeName = theme;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.officeLoc = officeLoc;
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										case "User" when Department == "Private" && Position == "Collector":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10);
	//											mainApp.mnuWorkcomp.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuBackOffice.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											mainApp.separator1.Visibility = ElementVisibility.Collapsed;
	//											mainApp.seperator2.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//											mainApp.EmpName = EmpName;
	//											mainApp.userName = userName;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.themeName = theme;
	//											mainApp.officeLoc = officeLoc;
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										case "User" when Department == "Workers Comp" && Position == "Collector":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10); ;
	//											mainApp.mnuBackOffice.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuPrivateCollectors.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuOnlineLogins.Visibility = ElementVisibility.Collapsed;
	//											mainApp.separator1.Visibility = ElementVisibility.Collapsed;
	//											mainApp.seperator2.Visibility = ElementVisibility.Collapsed;
	//											mainApp.EmpName = EmpName;
	//											mainApp.userName = userName;
	//											mainApp.themeName = theme;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.officeLoc = officeLoc;
	//											//mainApp.lblLevel.Content = "User";
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//											//mainApp.mnuThemes.Visibility = ElementVisibility.Collapsed;
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										case "User" when Department == "Private" && Position == "Back Office":
	//											frmLogin.Hide();
	//											demoTool.employeeID = readerSQL.GetString(10);
	//											demoTool.EmpName = EmpName;
	//											demoTool.userName = userName;
	//											demoTool.accessLevel = UserAccess;
	//											demoTool.statlblUsername.Text = EmpName;
	//											demoTool.statlblAccess.Text = UserAccess;
	//											demoTool.statlblPosition.Text = Position;
	//											demoTool.officeLoc = officeLoc;
	//											demoTool.userName = userName;
	//											mainApp.themeName = theme;
	//											demoTool.Text = ProgName + " | Demo Tool | (" + EmpName + ")";
	//											demoTool.accessLevel = "User";
	//											demoTool.EmpName = EmpName;
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											demoTool.Show();
	//											break;
	//
	//										case "User" when Department == "All Department":
	//											frmLogin.Hide();
	//											mainApp.employeeID = readerSQL.GetString(10);
	//											mainApp.mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
	//											mainApp.EmpName = EmpName;
	//											mainApp.userName = userName;
	//											mainApp.themeName = theme;
	//											mainApp.accessLevel = UserAccess;
	//											mainApp.statlblUsername.Text = EmpName;
	//											mainApp.statlblAccess.Text = UserAccess;
	//											mainApp.statlblPosition.Text = Position;
	//											mainApp.officeLoc = officeLoc;
	//											mainApp.mnuDevAccountAccess.Visibility = ElementVisibility.Collapsed;
	//											//mainApp.lblLevel.Content = "User";
	//											mainApp.mnuAdmin.Visibility = ElementVisibility.Collapsed;
	//											mainApp.mnuManageProduct.Visibility = ElementVisibility.Collapsed;
	//											//mainApp.mnuThemes.Visibility = ElementVisibility.Collapsed;
	//											mainApp.Text = ProgName + " ver. " + ProgVer + " (" + EmpName + ")";
	//											log.AddActivityLog(LogMessage, EmpName, EmpName + " logged in", "USER LOGGED IN");
	//											mainApp.Show();
	//											break;
	//
	//										default:
	//											{
	//												var ErrorMessage = "An error occured while trying to process the request \n Module: Login \n Username entered: " + txtUsername.Text + "\n Password entered: " + txtPassword.Text + "\n Department: " + Department + "\n UserAccess: " + UserAccess + "\n Position: " + Position + "\n Process: btnLogin_Click";
	//												winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
	//												RadMessageBox.Show("Something went wrong", "Error Message", MessageBoxButtons.OK, RadMessageIcon.Error);
	//												break;
	//											}
	//									}
	//								}
	//								catch (Exception ex)
	//								{
	//									var ErrorMessage = "An error occured while trying to process the request \n Module: frmLogin \n Username entered: " + txtUsername.Text + "\n Password entered: " + txtPassword.Text + "\n Department: " + Department + "\n UserAccess: " + UserAccess + "\n Position: " + Position + "\n Process: btnLogin_Click \n\n Detailed Error: \n" + ex.ToString();
	//									winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
	//									lblalert.Text = "Something went Wrong, Please check with Developer";
	//									//RadMessageBox.Show("Something went wrong", "Error Message");
	//									loginPanel.Enabled = true;
	//								}
	//							}
	//						}
	//					}
	//				}
	//				else
	//				{
	//					lblalert.Text = "PASSWORD IS INCORRECT";
	//					string logs = "Incorrect password used for " + txtUsername.Text;
	//					log.AddActivityLog(logs, "", logs, "INCORRECT PASSWORD");
	//					winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
	//					loginPanel.Enabled = true;
	//					lblalert.Visible = true;
	//					txtPassword.Clear();
	//					txtUsername.Focus();
	//
	//				}
	//			}
	//		}
	//	}
	//}
}
