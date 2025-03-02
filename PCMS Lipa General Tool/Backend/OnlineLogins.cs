using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Data.SqlClient;
namespace PCMS_Lipa_General_Tool.Class
{
	public class OnlineLogins
	{

		private readonly string _dbConnection = db.GetDbConnection();
		readonly WinDiscordAPI dc = new();
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();


		public void GetDBLoginID(out string ID, string empName)
		{
			ID = string.Empty;
			string nextSequence = db.GetSequenceNo("OnlineLoginSeq", "OL-");
			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					ID = nextSequence;
					return;
				}
			}
			catch (Exception ex)
			{
				notif.LogError("GetDBID", empName, "OnlineLogins", ID, ex);
			}
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
					FROM [ONLINE LOGINS]
					WHERE [Insurance Name] LIKE @searchTerm
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
				notif.LogError("SearchData", empName, "Adjuster", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}

		public bool OnlineLoginDB(
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
			string empName,
			out string message)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs, dcmessage;

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
				dcmessage = $@"Hi, I just {request.ToLower()}d the a online access for {insName}. Here are the details you might want to check:

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
				log.AddActivityLog(dcmessage, empName, logs, $"{request.ToUpper()} ONLINE LOGIN INFORMATION");
				return true;
				///fe.SendToastNotifDesktop(message, "Success");
			}
			catch (SqlException sqlex)
			{
				notif.LogError("OnlineLoginDB - SQlEx", empName, "OnlineLogins", insID, sqlex);
				message = $"Failed to {request.ToLower()} {insID}, Please try again later";
				return false;
				///throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
			}
			catch (Exception ex)
			{
				notif.LogError("OnlineLoginDB - General Error", empName, "OnlineLogins", insID, ex);
				message = $"Failed to {request.ToLower()} {insID}, Please try again later";
				return false;
				///throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
			}
			finally
			{
				conn.Close();
			}
		}


		public DataTable ViewOnlineLogins(string empName, out string lblCount)
		{
			const string query = "SELECT * FROM [ONLINE LOGINS] ORDER BY [LOGIN ID] ASC";
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
				notif.LogError("ViewOnlineLogins", empName, "OnlineLogins", "N/A", ex);
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
		//						log.AddActivityLog(message, empName, logs, "UPDATED ONLINE LOGIN");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						if (updateDC.IsChecked == true)
		//						{
		//							winDiscordAPI.PublishtoDiscord("PCMS Lipa General Tool", "Online Access Updated - " + insName.Text, message, empName, "https://discord.com/api/webhooks/1069052004271407235/X1u8oGuPi9RJoMK2TP3B2sUW2ABTZfOYt0AqHGA4K04e4PSKwL21mtJdi3MRelH2Kbq_", "https://discord.gg/cx3zY9XF");
		//						}
		//						fe.SendToastNotifDesktop(logs);
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
		//						log.AddActivityLog(message, empName, logs, "ADDED ONLINE LOGIN");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						if (updateDC.IsChecked == true)
		//						{
		//							winDiscordAPI.PublishtoDiscord("PCMS Lipa General Tool", "Online Access Updated - " + insName.Text, message, empName, "https://discord.com/api/webhooks/1069052004271407235/X1u8oGuPi9RJoMK2TP3B2sUW2ABTZfOYt0AqHGA4K04e4PSKwL21mtJdi3MRelH2Kbq_", "https://discord.gg/cx3zY9XF");
		//						}
		//						fe.SendToastNotifDesktop(logs);
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
		//						log.AddActivityLog(message, empName, logs, "DELETED ONLINE LOGIN");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						if (updateDC.IsChecked == true)
		//						{
		//							winDiscordAPI.PublishtoDiscord("PCMS Lipa General Tool", "Online Access Updated - " + insName.Text, message, empName, "https://discord.com/api/webhooks/1069052004271407235/X1u8oGuPi9RJoMK2TP3B2sUW2ABTZfOYt0AqHGA4K04e4PSKwL21mtJdi3MRelH2Kbq_", "https://discord.gg/cx3zY9XF");
		//						}
		//						fe.SendToastNotifDesktop(logs);
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
