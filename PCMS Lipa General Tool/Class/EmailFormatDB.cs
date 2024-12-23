using Newtonsoft.Json.Converters;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace PCMS_Lipa_General_Tool.Class
{
	public class EmailFormatDB
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();

		public void GetDBID(out string ID, string empName)
		{
			ID = string.Empty;
			
			string nextSequence = db.GetSequenceNo("EmailFormatSeq", "EF-");

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
				error.LogError("GetDBID", empName, "EmailFormatDB", "N/A", ex);
			}
			//db.GetSequenceNo("textbox", "EmailFormatSeq", txtIntID.Text, null, "EF-");
		}

		public DataTable ViewEmailFormatList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Insurance Email Format]";
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
				error.LogError("ViewEmailFormatList", empName, "EmailFormatDB", "N/A", ex);
			}

			return data;
		}



		public bool EmailFormatDBRequest(
			string request,
			string formatID,
			string insuranceName, 
			string emailFormat,
			string remarks,
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

				string logs;

				cmd.CommandText = request switch
				{
					"Update" => @"
								UPDATE [Insurance Email Format] SET [Insurance] = @INSURANCE
									[Email Format] = @EMAILFORMAT, REMARKS = @REMARKS 
								WHERE
									[Format ID] = @FORMATID",
					"Create" => @"
								INSERT INTO [Insurance Email Format] ([Format ID],
								[Insurance], [Email Format], [Remarks])
								VALUES
								(@FORMATID, @INSURANCE, @EMAILFORMAT, @REMARKS)",
					"Delete" => @"DELETE FROM [Insurance Email Format] WHERE [Format ID] = @FORMATID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					//cmd.Parameters.AddWithValue("@FORMATID", formatID);
					cmd.Parameters.AddWithValue("@INSURANCE", insuranceName);
					cmd.Parameters.AddWithValue("@EMAILFORMAT", emailFormat);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@FORMATID", formatID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Email Format ID: {formatID}";
				message = $"Done! {formatID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} EMAIL FORMAT INFORMATION");
				return true;
				//fe.SendToastNotifDesktop(logs, "success");
			}
			catch (Exception ex)
			{
				error.LogError($"EmailFormatDBRequest - {request}", empName, "EmailFormatDB", formatID, ex);
				message = $"Failed to {request.ToLower()} {formatID}, Please try again later";
				return false;
				//throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
			}
			finally
			{
				conn.Close();
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
FROM [Insurance Email Format]
WHERE [Insurance] LIKE @searchTerm
OR [Email Format] LIKE @searchTerm";

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				error.LogError("SearchData", empName, "BillReviews", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}

		///public void EmailFormatDBRequest(string request, RadTextBox formatID, RadTextBox insuranceName, RadTextBox emailFormat, RadTextBoxControl remarks, string empName)
		///{
		///
		///	//var addtoCart = "INSERT INTO [Pantry Listahan] ([INT ID], DATE, [TIME STAMP], NAME, PRODUCT, QUANTITY, PRICE, [TOTAL PRICE], SUMMARY, REMARKS) VALUES
		///	//('" + txtIntID.Text + "','" + dateInserted + "','" + sqlFormattedDate + "','" + cmbItemEmpList.Text + "','" + cmbProductList.Text + "','" + txtQuantity.Text + "','" + txtPrice.Text + "','" + txtTotalPrice.Text + "','" + txtSummary.Text + "','" + txtRemarks.Text + "')";
		///	using (SqlConnection conn = new SqlConnection(_dbConnection))
		///	{
		///		switch (request)
		///		{
		///			case "Patch":
		///				{
		///					string message = String.Format("I made an update for {1}. Check the details below." +
		///						"\n\n\nFormat ID: {0}\nInsurance Name: {1}\nEmail Format: {2}\nRemarks: {3}",
		///						formatID.Text,
		///						insuranceName.Text,
		///						emailFormat.Text,
		///						remarks.Text);
		///					try
		///					{
		///						conn.Open();
		///						using (SqlCommand cmd = new SqlCommand(", conn))
		///						{
		///							
		///							cmd.ExecuteNonQuery();
		///
		///						}
		///						string logs = empName + " updated Format ID: " + formatID.Text;
		///						log.AddActivityLog(message, empName, logs, "UPDATED EMAIL FORMAT INFORMATION");
		///						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		///						fe.SendToastNotifDesktop(logs);
		///						//RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		///					}
		///					catch (Exception ex)
		///					{
		///						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: EmailFormatDBRequest \n Process: Patch \n Entry ID: " + formatID.Text + " \n\n Detailed Error: " + ex.ToString();
		///						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		///						RadMessageBox.Show(ErrorMessageUpdate + formatID.Text, "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
		///					}
		///					finally
		///					{
		///						conn.Close();
		///					}
		///
		///					break;
		///				}
		///
		///			case "Create":
		///				{
		///					//var addtoCart = "INSERT INTO [Pantry Listahan] ([INT ID], DATE, [TIME STAMP], NAME, PRODUCT, QUANTITY, PRICE, [TOTAL PRICE], SUMMARY, REMARKS) VALUES
		///					//('" + txtIntID.Text + "','" + dateInserted + "','" + sqlFormattedDate + "','" + cmbItemEmpList.Text + "','" + cmbProductList.Text + "','" + txtQuantity.Text + "','" + txtPrice.Text + "','" + txtTotalPrice.Text + "','" + txtSummary.Text + "','" + txtRemarks.Text + "')";
		///					string message = String.Format("I added {2} in the records. Check the details below." +
		///						"\n\n\nFormat ID: {0}\nInsurance Name: {1}\nEmail Format: {2}\nRemarks: {3}",
		///						formatID.Text,
		///						insuranceName.Text,
		///						emailFormat.Text,
		///						remarks.Text);
		///					try
		///					{
		///						conn.Open();
		///						using (SqlCommand cmd = new SqlCommand(""))
		///						{
		///							//cmd.Parameters.AddWithValue("@FORMATID", formatID.Text);
		///							cmd.Parameters.AddWithValue("@INSURANCE", insuranceName.Text);
		///							cmd.Parameters.AddWithValue("@EMAILFORMAT", emailFormat.Text);
		///							cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		///							cmd.ExecuteNonQuery();
		///						}
		///						string logs = empName + " added Format ID: " + formatID.Text;
		///						log.AddActivityLog(message, empName, logs, "ADDED EMAIL FORMAT INFORMATION");
		///						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		///						fe.SendToastNotifDesktop(logs);
		///						//RadMessageBox.Show("Record successfully Added", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		///					}
		///
		///					catch (Exception ex)
		///					{
		///						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: EmailFormatDBRequest \n Process: Create \n Entry ID: " + formatID.Text + " \n\n Detailed Error: " + ex.ToString();
		///						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		///						RadMessageBox.Show(ErrorMessageCreate + formatID.Text, "Failed to Create", MessageBoxButtons.OK, RadMessageIcon.Error);
		///					}
		///					finally
		///					{
		///						conn.Close();
		///					}
		///
		///					break;
		///				}
		///
		///			case "Delete":
		///				{
		///					string message = String.Format("I removed {2} in the records. Check the details below." +
		///						"\n\n\nFormat ID: {0}\nInsurance Name: {1}\nEmail Format: {2}\nRemarks: {3}",
		///						formatID.Text,
		///						insuranceName.Text,
		///						emailFormat.Text,
		///						remarks.Text);
		///					try
		///					{
		///						conn.Open();
		///						using (SqlCommand cmd = new SqlCommand(", conn))
		///						{
		///							
		///							cmd.ExecuteNonQuery();
		///						}
		///						string logs = empName + " deleted Format ID: " + formatID.Text;
		///						log.AddActivityLog(message, empName, logs, "DELETED EMAIL FORMAT INFORMATION");
		///						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		///						fe.SendToastNotifDesktop(logs);
		///						//RadMessageBox.Show("Record successfully Deleted", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		///					}
		///					catch (Exception ex)
		///					{
		///						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: EmailFormatDBRequest \n Process: Delete \n Entry ID: " + formatID.Text + " \n\n Detailed Error: " + ex.ToString();
		///						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		///						RadMessageBox.Show(ErrorMessageDelete + formatID.Text, "Failed to Delete", MessageBoxButtons.OK, RadMessageIcon.Error);
		///					}
		///					finally
		///					{
		///						conn.Close();
		///					}
		///
		///					break;
		///				}
		///		}
		///	}
		///}
	}
}
