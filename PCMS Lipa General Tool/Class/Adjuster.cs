using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Class
{
	public class Adjuster
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		private static readonly CommonTask task = new();


		public DataTable ViewAdjusterList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Adjuster Information]";
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
				task.LogError("ViewAdjusterList", empName, "Adjuster", "N/A", ex);
			}

			return data;
		}


		public void AdjusterDBRequest(
			string request,
			string adjID,
			string insuranceName,
			string adjusterName,
			string phoneNo,
			string ext,
			string faxNo,
			string email,
			string supervisorName,
			string remarks,
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

				string logs, message;

				// Determine SQL command based on the request
				cmd.CommandText = request switch
				{
					"Update" => @"UPDATE [Adjuster Information]
                            SET [Insurance Name] = @INSURANCE, [Adjuster Name] = @ADJNAME,
                            [Phone No.] = @PHONENO, [Extension] = @EXT, [Fax No.] = @FAX,
                            [Email] = @EMAIL, [Supervisor] = @SUPERVISOR, REMARKS = @REMARKS
                          WHERE
                            [Adjuster ID] = @ADJID",
					"Create" => @"INSERT INTO [Adjuster Information] ([Adjuster ID], [Insurance Name],
                            [Adjuster Name], [Phone No.], [Extension], [Fax No.],
                            [Email], [Supervisor], REMARKS)
                          VALUES 
                            (@ADJID, @INSURANCE, @ADJNAME, @PHONENO, @EXT, @FAX, @EMAIL, @SUPERVISOR, @REMARKS)",
					"Delete" => @"DELETE FROM [Adjuster Information]
                          WHERE [Adjuster ID] = @ADJID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Update and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@INSURANCE", insuranceName ?? string.Empty);
					cmd.Parameters.AddWithValue("@ADJNAME", adjusterName ?? string.Empty);
					cmd.Parameters.AddWithValue("@PHONENO", phoneNo ?? string.Empty);
					cmd.Parameters.AddWithValue("@EXT", ext ?? string.Empty);
					cmd.Parameters.AddWithValue("@FAX", faxNo ?? string.Empty);
					cmd.Parameters.AddWithValue("@EMAIL", email ?? string.Empty);
					cmd.Parameters.AddWithValue("@SUPERVISOR", supervisorName ?? string.Empty);
					cmd.Parameters.AddWithValue("@REMARKS", remarks ?? string.Empty);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@ADJID", adjID ?? string.Empty);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Adjuster ID: {adjID}";
				message = $"Done! {adjID} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} ADJUSTER INFORMATION");
				task.SendToastNotifDesktop(message);
			}
			catch (Exception ex)
			{
				task.LogError($"adjusterDBRequest {request}", empName, "Adjuster", adjID, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}



		//public void AdjusterInfo(string request, RadTextBox adjID, RadTextBox insuranceName, RadTextBox adjusterName, RadTextBox phoneNo, RadTextBox ext, RadTextBox faxNo, RadTextBox email, RadTextBox supervisorName, RadTextBoxControl remarks, string empName)
		//{
		//	using (SqlConnection conn = new SqlConnection(_dbConnection))
		//	{
		//
		//		switch (request)
		//		{
		//			case "Patch":
		//				{
		//					string message = String.Format("I made an update for {0}. Check the details below.\n\n" +
		//						"Adjuster ID: {0}\nInsurance Name: {1}\nAdjuster Name: {2}\nPhone No:{3}-{4}\nFax No:{5}\nEmail:{6}\nSupervisor Name:{7}\nRemarks:{8}",
		//						adjID.Text,
		//						insuranceName.Text,
		//						adjusterName.Text,
		//						phoneNo.Text,
		//						ext.Text,
		//						faxNo.Text,
		//						email.Text,
		//						supervisorName.Text,
		//						remarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("UPDATE [Adjuster Information] SET [Insurance Name] = @INSURANCE, [Adjuster Name] = @ADJNAME," +
		//							"[Phone No.] = @PHONENO, [Extension] = @EXT, [Fax No.] = @FAX, [Email] = @EMAIL, [Supervisor] = @SUPERVISOR, REMARKS = @REMARKS" +
		//							" WHERE [Adjuster ID] = @ADJID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@ADJID", adjID.Text);
		//							cmd.Parameters.AddWithValue("@INSURANCE", insuranceName.Text);
		//							cmd.Parameters.AddWithValue("@ADJNAME", adjusterName.Text);
		//							cmd.Parameters.AddWithValue("@PHONENO", phoneNo.Text);
		//							cmd.Parameters.AddWithValue("@EXT", ext.Text);
		//							cmd.Parameters.AddWithValue("@FAX", faxNo.Text);
		//							cmd.Parameters.AddWithValue("@EMAIL", email.Text);
		//							cmd.Parameters.AddWithValue("@SUPERVISOR", supervisorName.Text);
		//							cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						string logs = empName + " updated information for Adjuster ID: " + adjID.Text;
		//						task.AddActivityLog(message, empName, logs, "UPDATED ADJUSTER INFORMATION");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						task.SendToastNotifDesktop(logs);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: AdjusterInfo \n Process: Patch \n Entry ID: " + adjID.Text + " \n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageUpdate + insuranceName.Text + ", Please try again later", "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
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
		//					string message = String.Format("I added {0} in the records. Check the details below.\n\n" +
		//						"Adjuster ID: {0}\nInsurance Name: {1}\nAdjuster Name: {2}\nPhone No:{3}-{4}\nFax No:{5}\nEmail:{6}\nSupervisor Name:{7}\nRemarks:{8}",
		//						adjID.Text,
		//						insuranceName.Text,
		//						adjusterName.Text,
		//						phoneNo.Text,
		//						ext.Text,
		//						faxNo.Text,
		//						email.Text,
		//						supervisorName.Text,
		//						remarks.Text);
		//
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("INSERT INTO [Adjuster Information] ([Insurance Name], [Adjuster Name], [Phone No.], @INSURANCE, [Extension]," +
		//							"[Fax No.], [Email], [Supervisor], REMARKS) VALUES (@ADJNAME, @PHONENO, @EXT, @FAX, EMAIL, @SUPERVISOR,  @REMARKS", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@ADJID", adjID.Text);
		//							cmd.Parameters.AddWithValue("@INSURANCE", insuranceName.Text);
		//							cmd.Parameters.AddWithValue("@ADJNAME", adjusterName.Text);
		//							cmd.Parameters.AddWithValue("@PHONENO", phoneNo.Text);
		//							cmd.Parameters.AddWithValue("@EXT", ext.Text);
		//							cmd.Parameters.AddWithValue("@FAX", faxNo.Text);
		//							cmd.Parameters.AddWithValue("@EMAIL", email.Text);
		//							cmd.Parameters.AddWithValue("@SUPERVISOR", supervisorName.Text);
		//							cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		//							cmd.ExecuteNonQuery();
		//							string logs = empName + " added Adjuster ID: " + adjID.Text;
		//							task.AddActivityLog(message, empName, logs, "ADDED ADJUSTER INFORMATION");
		//							winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//							//RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//							task.SendToastNotifDesktop(logs);
		//						}
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: AdjusterInfo \n Process: Create \n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageCreate + insuranceName.Text + ", Please try again later", "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
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
		//					string message = String.Format("I removed {0} in the records. Check the details below.\n\n" +
		//						"Adjuster ID: {0}\nInsurance Name: {1}\nAdjuster Name: {2}\nPhone No:{3}-{4}\nFax No:{5}\nEmail:{6}\nSupervisor Name:{7}\nRemarks:{8}",
		//						adjID.Text,
		//						insuranceName.Text,
		//						adjusterName.Text,
		//						phoneNo.Text,
		//						ext.Text,
		//						faxNo.Text,
		//						email.Text,
		//						supervisorName.Text,
		//						remarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("DELETE FROM [Adjuster Information] WHERE [Adjuster ID] = @ADJID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@ADJID", adjID.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						string logs = empName + " deleted Adjuster ID: " + adjID.Text;
		//						task.AddActivityLog(message, empName, logs, "DELETED ADJUSTER INFORMATION");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						task.SendToastNotifDesktop(logs);
		//						///RadMessageBox.Show("Record successfully Deleted", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: AdjusterInfo \n Process: Delete \n Entry ID: " + adjID.Text + " \n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageDelete + insuranceName.Text + ", Please try again later", "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
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
	}
}
