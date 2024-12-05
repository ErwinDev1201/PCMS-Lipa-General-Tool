using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
namespace PCMS_Lipa_General_Tool.Class
{
	public class OnlineLogins
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private readonly CommonTask task = new();
		readonly WinDiscordAPI dc = new();


		public void FillPasswordDGTextBox(string query, RadTextBox txtPassword, string empName)
		{
			try
			{
				using SqlConnection con = new(_dbConnection);
				con.Open();
				using SqlCommand cmd = new(query, con);
				using SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					txtPassword.Text = (reader.GetString(0));
				}

			}
			catch (Exception ex)
			{
				task.LogError("OnlineLoginDB", empName, "OnlineLogins", "", ex);
			}
		}

		public DataTable FetchOnlineLoginsData(string empName)
		{
			string query = "SELECT * FROM [Online Logins]";
			var dataTable = new DataTable();

			try
			{
				using var con = new SqlConnection(_dbConnection);
				using var cmd = new SqlCommand(query, con);
				using var adapter = new SqlDataAdapter(cmd);

				// Fill the DataTable with data from the query
				adapter.Fill(dataTable);
			}
			catch (Exception ex)
			{
				task.LogError("FetchOnlineLoginsData", empName, "OnlineLogins", string.Empty, ex);
				throw;
			}

			return dataTable;
		}


		public void FillOnlineLoginsInfo(
			DataRow selectedRow,
			out string intID,
			out string name,
			out string link,
			out string username,
			out string password,
			out string accountOwner,
			out string remarks,
			out string browser)
		{
			// Initialize outputs
			intID = selectedRow[0]?.ToString() ?? string.Empty;
			name = selectedRow[1]?.ToString() ?? string.Empty;
			link = selectedRow[2]?.ToString() ?? string.Empty;
			username = selectedRow[3]?.ToString() ?? string.Empty;
			password = selectedRow[4]?.ToString() ?? string.Empty;
			accountOwner = selectedRow[5]?.ToString() ?? string.Empty;
			remarks = selectedRow[6]?.ToString() ?? string.Empty;
			browser = selectedRow[7]?.ToString() ?? string.Empty;
		}


		//public void NewFillUpTextBoxwcmb( RadGridView dataGrid, RadTextBox IntID, RadTextBox Name, RadTextBox Link, RadTextBox Username, RadTextBox accountOwner, RadTextBoxControl Remarks, RadDropDownList Browser, string empName)
		//{
		//	using var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		string query = "SELECT * FROM [Online Logins]";
		//		con.Open();
		//		using SqlCommand cmd = new(query, con);
		//		cmd.ExecuteNonQuery();
		//		if (dataGrid.SelectedRows.Count > 0)
		//		{
		//			var row = dataGrid.SelectedRows[0];
		//			{
		//				IntID.Text = row.Cells[0].Value + string.Empty;
		//				Name.Text = row.Cells[1].Value + string.Empty;
		//				Link.Text = row.Cells[2].Value + string.Empty;
		//				Browser.Text = row.Cells[3].Value + string.Empty;
		//				Username.Text = row.Cells[4].Value + string.Empty;
		//				accountOwner.Text = row.Cells[5].Value + string.Empty;
		//				Remarks.Text = row.Cells[6].Value + string.Empty;
		//
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		task.LogError("NewFillUpTextBoxwcmb", empName, "OnlineLogins", "", ex);
		//	}
		//	finally
		//	{
		//		con.Close();
		//	}
		//

		public void OnlineLoginDB(
	string request,
	string insID,
	string insName,
	string webLink,
	string userName,
	string passWord,
	string remarks,
	string owner,
	string browser,
	bool updateDC,
	string empName)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs, message, dcmessage;

				// Determine the SQL command based on the request type
				cmd.CommandText = request switch
				{
					"Update" => @"UPDATE [ONLINE LOGINS]
                          SET [Insurance Name] = @INSURANCE, [URL Link] = @LINK, USERNAME = @USERNAME,
                              PASSWORD = @PASSWORD, REMARKS = @REMARKS, [ACCOUNT OWNER] = @OWNER,
                              BROWSER = @BROWSER
                          WHERE
                              [LOGIN ID] = @INSID",
					"Create" => @"INSERT INTO [ONLINE LOGINS]
                          ([LOGIN ID], [Insurance Name], [URL Link], Username, Password, Remarks, [Account Owner], Browser)
                          VALUES 
                              (@INSID, @INSURANCE, @LINK, @USERNAME, @PASSWORD, @REMARKS, @OWNER, @BROWSER)",
					"Delete" => @"DELETE FROM [ONLINE LOGINS]
                          WHERE
                              [LOGIN ID] = @INSID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters for Update and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@INSURANCE", insName ?? string.Empty);
					cmd.Parameters.AddWithValue("@LINK", webLink ?? string.Empty);
					cmd.Parameters.AddWithValue("@USERNAME", userName ?? string.Empty);
					cmd.Parameters.AddWithValue("@PASSWORD", passWord ?? string.Empty);
					cmd.Parameters.AddWithValue("@REMARKS", remarks ?? string.Empty);
					cmd.Parameters.AddWithValue("@OWNER", owner ?? string.Empty);
					cmd.Parameters.AddWithValue("@BROWSER", browser ?? string.Empty);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@INSID", insID ?? string.Empty);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				dcmessage = $@"Hi, I just {request}d the a online access for {insName}. Here are the details you might want to check:

                Insurance Name: {insName}
                Website Link: {webLink}
                Username: {userName}
                Password: {passWord}
                Account Owner: {owner}
                Remarks: {remarks}";

				logs = $"{empName} {request.ToLower()}d OnlineLogin ID: {insID}";
				message = $"Done! {insID} has been successfully {request.ToLower()}d.";

				// Publish to Discord if updateDC is true
				if (updateDC)
				{
					dc.PublishtoDiscord(Global.ProgName, $"Online Logins Access {request}d - {insName}", dcmessage, empName, Global.Onloginwebhook, Global.onlogininvite);
				}

				// Add activity log and send a toast notification
				task.AddActivityLog(dcmessage, empName, logs, $"{request.ToUpper()} ONLINE LOGIN INFORMATION");
				task.SendToastNotifDesktop(message);
			}
			catch (Exception ex)
			{
				task.LogError("OnlineLoginDB", empName, "OnlineLogins", insID, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}


		//public void OnlineLoginDB(string request, RadTextBox insID, RadTextBox insName, RadTextBox webLink, RadTextBox userName, RadTextBox passWord, RadTextBoxControl remarks, RadTextBox owner, RadDropDownList browser, RadCheckBox updateDC, string empName)
		//{
		//
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		conn.Open();
		//		SqlCommand cmd = new()
		//		{
		//			Connection = conn
		//		};
		//
		//		string logs, message, dcmessage;
		//
		//		cmd.CommandText = request switch
		//		{
		//			"Update" => @"UPDATE [ONLINE LOGINS]
		//						SET [Insurance Name] = @INSURANCE, [URL Link] = @LINK, USERNAME = @USERNAME,
		//							PASSWORD = @PASSWORD, REMARKS = @REMARKS, [ACCOUNT OWNER] = @OWNER,
		//							BROWSER = @BROWSER
		//						WHERE
		//							[LOGIN ID] = @INSID",
		//			"Create" => @"INSERT INTO [ONLINE LOGINS]
		//							([LOGIN ID], [Insurance Name], [URL Link], Username, Password, Remarks, [Account Owner], Browser)
		//						VALUES 
		//							(@INSID, @INSURANCE, @LINK, @USERNAME, @PASSWORD, @REMARKS, @OWNER, @BROWSER)",
		//			"Delete" => @"DELETE FROM [ONLINE LOGINS]
		//						WHERE
		//							[LOGIN ID] = @INSID",
		//			_ => throw new ArgumentException("Invalid request type."),
		//		};
		//
		//		// Add parameters common to Patch and Create
		//		if (request != "Delete")
		//		{
		//
		//			cmd.Parameters.AddWithValue("@INSURANCE", insName.Text);
		//			cmd.Parameters.AddWithValue("@LINK", webLink.Text);
		//			cmd.Parameters.AddWithValue("@USERNAME", userName.Text);
		//			cmd.Parameters.AddWithValue("@PASSWORD", passWord.Text);
		//			cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		//			cmd.Parameters.AddWithValue("@OWNER", owner.Text);
		//			cmd.Parameters.AddWithValue("@BROWSER", browser.Text);
		//		}
		//
		//		// Common parameter for all requests
		//		cmd.Parameters.AddWithValue("@INSID", insID.Text);
		//
		//		// Execute query
		//		cmd.ExecuteNonQuery();
		//
		//		// Log activity
		//		// Use string interpolation for clarity and efficiency
		//		dcmessage = $@"Hi, I just {request}d the login for {insName.Text}. Here are the details you might want to check:
		//
		//		Insurance Name: {insName.Text}
		//		Website Link: {webLink.Text}
		//		Username: {userName.Text}
		//		Password: {passWord.Text}
		//		Account Owner: {owner.Text}
		//		Remarks: {remarks.Text}";
		//
		//		logs = $"{empName} {request.ToLower()}d OnlineLogin ID: {insID.Text}";
		//		message = $"Done! {insID.Text} has been successfully {request.ToLower()}d.";
		//		if (updateDC.Checked == true)
		//		{
		//			dc.PublishtoDiscord(Global.ProgName, $"Online Logins Access {request}d - {insName.Text}", dcmessage, empName, Global.Onloginwebhook, Global.onlogininvite);
		//		}
		//		
		//		task.AddActivityLog(dcmessage, empName, logs, $"{request.ToUpper()} ONLINE LOGIN INFORMATION");
		//		task.SendToastNotifDesktop(message);
		//	}
		//	catch (Exception ex)
		//	{
		//		task.LogError("OnlineLoginDB", empName, "OnlineLogins", insID.Text, ex);
		//		RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
		//	}
		//	finally
		//	{
		//		conn.Close();
		//	}
		//}
		//
		public DataTable ViewOnlineLogins(string empName, out string lblCount)
		{
			const string query = "SELECT [LOGIN ID], [INSURANCE NAME], [URL LINK], BROWSER, USERNAME, [ACCOUNT OWNER], REMARKS FROM [ONLINE LOGINS] ORDER BY [LOGIN ID] ASC";
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
				task.LogError("ViewOnlineLogins", empName, "OnlineLogins", "N/A", ex);
			}

			return data;
		}

		//public void OnlineLoginDB(string request, RadTextBox insID, RadTextBox insName, RadTextBox webLink, RadTextBox userName, RadTextBox passWord, RadTextBoxControl remarks, RadTextBox owner, RadDropDownList browser, RadCheckBox updateDC, string empName)
		//{
		//	using (SqlConnection conn = new SqlConnection(_dbConnection))
		//	{
		//		switch (request)
		//		{
		//			case "Patch":
		//				{
		//
		//					string logs = empName + " Updated the login information for " + insName.Text + " with Login ID: " + insID.Text;
		//					string message = String.Format("I made an update for {0}. Check the details below. \n\n{0}\n{1}\nUsername: {2}\nPassword: {3}\nAccount Owner: {4}\nRemarks:{5}",
		//						insName.Text,
		//						webLink.Text,
		//						userName.Text,
		//						passWord.Text,
		//						owner.Text,
		//						remarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("UPDATE [ONLINE LOGINS]" +
		//							"SET [Insurance Name] = @INSURANCE, [URL Link] = @LINK, USERNAME = @USERNAME, PASSWORD = @PASSWORD, REMARKS = @REMARKS, [ACCOUNT OWNER] = @OWNER, BROWSER = @BROWSER WHERE [LOGIN ID] = @INSID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@INSID", insID.Text);
		//							cmd.Parameters.AddWithValue("@INSURANCE", insName.Text);
		//							cmd.Parameters.AddWithValue("@LINK", webLink.Text);
		//							cmd.Parameters.AddWithValue("@USERNAME", userName.Text);
		//							cmd.Parameters.AddWithValue("@PASSWORD", passWord.Text);
		//							cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		//							cmd.Parameters.AddWithValue("@OWNER", owner.Text);
		//							cmd.Parameters.AddWithValue("@BROWSER", browser.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						task.AddActivityLog(message, empName, logs, "UPDATED ONLINE LOGIN");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						if (updateDC.IsChecked == true)
		//						{
		//							winDiscordAPI.PublishtoDiscord("PCMS Lipa General Tool", "Online Access Updated - " + insName.Text, message, empName, "https://discord.com/api/webhooks/1069052004271407235/X1u8oGuPi9RJoMK2TP3B2sUW2ABTZfOYt0AqHGA4K04e4PSKwL21mtJdi3MRelH2Kbq_", "https://discord.gg/cx3zY9XF");
		//						}
		//						task.SendToastNotifDesktop(logs);
		//						///RadMessageBox.Show("Online Login Access successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: OnlineLoginDB \n Process: Patch \n Entry ID: " + insID.Text + " \n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageUpdate + insName.Text, "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
		//					}
		//					finally
		//					{
		//						conn.Close();
		//					}
		//
		//					break;
		//				}
		//
		//			case "Create":
		//				{
		//					string logs = empName + " Added new login information for " + insName.Text + " with Login ID: " + insID.Text;
		//					string message = String.Format("I created online login for {0}. Check the details below. \n\n{0}\n{1}\nUsername: {2}\nPassword: {3}\nAccount Owner: {4}\nRemarks:{5}",
		//						insName.Text,
		//						webLink.Text,
		//						userName.Text,
		//						passWord.Text,
		//						owner.Text,
		//						remarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("INSERT INTO [ONLINE LOGINS] ([Insurance Name], [URL Link], Username, Password, Remarks, [Account Owner], Browser) " +
		//							"VALUES (@INSURANCE, @LINK, @USERNAME, @PASSWORD, @REMARKS, @OWNER, @BROWSER)", conn))
		//						{
		//							//cmd.Parameters.AddWithValue("@INSID", insID.Text);
		//							cmd.Parameters.AddWithValue("@INSURANCE", insName.Text);
		//							cmd.Parameters.AddWithValue("@LINK", webLink.Text);
		//							cmd.Parameters.AddWithValue("@USERNAME", userName.Text);
		//							cmd.Parameters.AddWithValue("@PASSWORD", passWord.Text);
		//							cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		//							cmd.Parameters.AddWithValue("@OWNER", owner.Text);
		//							cmd.Parameters.AddWithValue("@BROWSER", browser.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						task.AddActivityLog(message, empName, logs, "ADDED ONLINE LOGIN");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						if (updateDC.IsChecked == true)
		//						{
		//							winDiscordAPI.PublishtoDiscord("PCMS Lipa General Tool", "Online Access Updated - " + insName.Text, message, empName, "https://discord.com/api/webhooks/1069052004271407235/X1u8oGuPi9RJoMK2TP3B2sUW2ABTZfOYt0AqHGA4K04e4PSKwL21mtJdi3MRelH2Kbq_", "https://discord.gg/cx3zY9XF");
		//						}
		//						task.SendToastNotifDesktop(logs);
		//						//RadMessageBox.Show("Online Login Access successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: OnlineLoginDB \n Process: Create \n Entry ID: " + insID.Text + "\n\nDetailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageCreate + insID.Text, "Failed to Create", MessageBoxButtons.OK, RadMessageIcon.Error);
		//					}
		//					finally
		//					{
		//						conn.Close();
		//					}
		//
		//					break;
		//				}
		//
		//			case "Delete":
		//				{
		//					string logs = empName + " deleted login information for " + insName.Text + " with Login ID: " + insID.Text;
		//					string message = String.Format("I deleted the online login for {0}. Check the details below. \n\n{0}\n{1}\nUsername:{2}\nPassword: {3}\nAccount Owner: {4}\nRemarks:{5}",
		//						insName.Text,
		//						webLink.Text,
		//						userName.Text,
		//						passWord.Text,
		//						owner.Text,
		//						remarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("DELETE FROM [ONLINE LOGINS] WHERE [LOGIN ID] = @INSID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@INSID", insID.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						task.AddActivityLog(message, empName, logs, "DELETED ONLINE LOGIN");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						if (updateDC.IsChecked == true)
		//						{
		//							winDiscordAPI.PublishtoDiscord("PCMS Lipa General Tool", "Online Access Updated - " + insName.Text, message, empName, "https://discord.com/api/webhooks/1069052004271407235/X1u8oGuPi9RJoMK2TP3B2sUW2ABTZfOYt0AqHGA4K04e4PSKwL21mtJdi3MRelH2Kbq_", "https://discord.gg/cx3zY9XF");
		//						}
		//						task.SendToastNotifDesktop(logs);
		//						RadMessageBox.Show("Online Login Access successfully Deleted", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: OnlineLoginDB \n Process: Delete \n Entry ID: " + insID.Text + " \n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageDelete + insID.Text, "Failed to Delete", MessageBoxButtons.OK, RadMessageIcon.Error);
		//					}
		//					finally
		//					{
		//						conn.Close();
		//					}
		//
		//					break;
		//				}
		//		}
		//	}
		//}
		//
	}
}
