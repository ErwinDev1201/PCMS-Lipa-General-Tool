using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Class
{
	public class BillReview
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		private static readonly CommonTask task = new();

		public DataTable ViewBillReviewList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Bill Review Directory]";
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
				task.LogError("ViewBillReviewList", empName, "BillReview", "N/A", ex);
			}

			return data;
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
FROM [Bill Review Directory]
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
				task.LogError("SearchData", empName, "BillReviews", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}


		//public void FillBillReview(RadGridView dgBillReview, RadTextBox txtIntID, RadTextBox txtInsuranceName, RadTextBox txtPhoneNo, RadTextBox txtFaxNo, RadTextBox txturPhoneNo, RadTextBox txturFaxNo, RadTextBox txtbrPhoneNo, RadTextBox txtbrFaxNo, RadTextBox txtOnlineEmail, RadTextBoxControl txtRemarks, string empName)
		//{
		//	using (SqlConnection con = new SqlConnection(_dbConnection))
		//	{
		//		try
		//		{
		//			con.Open();
		//			{
		//				using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Bill Review Directory]", con))
		//				{
		//					cmd.ExecuteNonQuery();
		//					var dgRow = dgBillReview.SelectedRows[0];
		//					{
		//						txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
		//						txtInsuranceName.Text = dgRow.Cells[1].Value + string.Empty;
		//						txtPhoneNo.Text = dgRow.Cells[2].Value + string.Empty;
		//						txtFaxNo.Text = dgRow.Cells[3].Value + string.Empty;
		//						txturPhoneNo.Text = dgRow.Cells[4].Value + string.Empty;
		//						txturFaxNo.Text = dgRow.Cells[5].Value + string.Empty;
		//						txtbrPhoneNo.Text = dgRow.Cells[6].Value + string.Empty;
		//						txtbrFaxNo.Text = dgRow.Cells[7].Value + string.Empty;
		//						txtOnlineEmail.Text = dgRow.Cells[8].Value + string.Empty;
		//						txtRemarks.Text = dgRow.Cells[9].Value + string.Empty;
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: DBQuery \n Process: FillHearingRep \n\n Detailed Error: " + ex.ToString();
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, NameofUser, Global.DCErrorWebHook, Global.DCErrorInvite);
		//		}
		//		finally
		//		{
		//			con.Close();
		//		}
		//	}
		//}

		//public void FillEmailInfo(RadGridView dgEmailInfo, RadTextBox txtIntID, RadTextBox txtInsuranceName, RadTextBox txtEmailFormat, RadTextBoxControl txtRemarks, string empName)
		//{
		//	if (dgEmailInfo.SelectedRows.Count == 0)
		//	{
		//		RadMessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, RadMessageIcon.Info);
		//		return;
		//	}
		//
		//	var dgRow = dgEmailInfo.SelectedRows[0];
		//
		//	try
		//	{
		//		using SqlConnection con = new(_dbConnection);
		//		using SqlCommand cmd = new("SELECT * FROM [Insurance Email Format]", con);
		//		con.Open();
		//
		//		// Populate TextBoxes with selected row data
		//		txtIntID.Text = dgRow.Cells[0].Value?.ToString() ?? string.Empty;
		//		txtInsuranceName.Text = dgRow.Cells[1].Value?.ToString() ?? string.Empty;
		//		txtEmailFormat.Text = dgRow.Cells[2].Value?.ToString() ?? string.Empty;
		//		txtRemarks.Text = dgRow.Cells[3].Value?.ToString() ?? string.Empty;
		//	}
		//	catch (Exception ex)
		//	{
		//		task.LogError($"FillEmailInfo", empName, "BillReview", "N/A", ex);
		//
		//	}
		//}
		//
		//public void FillEmailInfo(RadGridView dgEmailInfo, RadTextBox txtIntID, RadTextBox txtInsuranceName, RadTextBox txtEmailFormat, RadTextBoxControl txtRemarks, string empName)
		//{
		//	using (SqlConnection con = new SqlConnection(_dbConnection))
		//	{
		//		try
		//		{
		//			con.Open();
		//			{
		//				using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Insurance Email Format]", con))
		//				{
		//					cmd.ExecuteNonQuery();
		//					var dgRow = dgEmailInfo.SelectedRows[0];
		//					{
		//						txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
		//						txtInsuranceName.Text = dgRow.Cells[1].Value + string.Empty;
		//						txtEmailFormat.Text = dgRow.Cells[2].Value + string.Empty;
		//						txtRemarks.Text = dgRow.Cells[3].Value + string.Empty;
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: DBQuery \n Process: FillEasyPrint \n\n Detailed Error: " + ex.ToString();
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, NameofUser, Global.DCErrorWebHook, Global.DCErrorInvite);
		//		}
		//		finally
		//		{
		//			con.Close();
		//		}
		//	}
		//}

		public void BillReviewDBRequest(
			string request, string reviewerID, string insuranceName, string phoneNo, string faxNo,
			string urphoneNO, string urfaxNo, string brphoneNo, string brfaxNo,
			string email, string remarks, string empName)
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

				cmd.CommandText = request switch
				{
					"Update" => @"
                            UPDATE [Bill Review Directory] 
                            SET [Insurance Name] = @INSURANCENAME, [Phone No.] = @PHONENO, [Fax No.] = @FAXNO, 
                                [UR Phone No.] = @URPHONENO, [UR Fax No.] = @URFAXNO, [BR Phone No.] = @BRPHONENO, 
                                [BR Fax No.] = @BRFAXNO, Email = @EMAIL, Remarks = @REMARKS  
                            WHERE [Reviewer ID] = @REVIEWERID",
					"Create" => @"
                            INSERT INTO [Bill Review Directory] 
                            ([Reviewer ID], [Insurance Name], [Phone No.], [Fax No.], [UR Phone No.], [UR Fax No.], 
                             [BR Phone No.], [BR Fax No.], Email, Remarks) 
                            VALUES (@REVIEWERID, @INSURANCENAME, @PHONENO, @FAXNO, @URPHONENO, @URFAXNO, @BRPHONENO, 
                                    @BRFAXNO, @EMAIL, @REMARKS)",
					"Delete" => "DELETE FROM [Bill Review Directory] WHERE [Reviewer ID] = @REVIEWERID",
					_ => throw new ArgumentException("Invalid request type"),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{

					cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName);
					cmd.Parameters.AddWithValue("@PHONENO", phoneNo);
					cmd.Parameters.AddWithValue("@FAXNO", faxNo);
					cmd.Parameters.AddWithValue("@URPHONENO", urphoneNO);
					cmd.Parameters.AddWithValue("@URFAXNO", urfaxNo);
					cmd.Parameters.AddWithValue("@BRPHONENO", brphoneNo);
					cmd.Parameters.AddWithValue("@BRFAXNO", brfaxNo);
					cmd.Parameters.AddWithValue("@EMAIL", email);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@REVIEWERID", reviewerID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Bill Reviewer ID: {reviewerID}";
				message = $"Done! {reviewerID} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} BILL REVIEW INFORMATION");

			}
			catch (Exception ex)
			{
				task.LogError("BillReviewDBRequest", empName, "BillReview", reviewerID, ex);
				throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
				//RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}


		//public void BillReviewDBRequest(string request, RadTextBox reviewerID, RadTextBox insuranceName, RadTextBox phoneNo, RadTextBox faxNo, RadTextBox urphoneNO, RadTextBox urfaxNo, RadTextBox brphoneNo, RadTextBox brfaxNo, RadTextBox email, RadTextBoxControl remarks, string empName)
		//{
		//
		//	//var addtoCart = "INSERT INTO [Pantry Listahan] ([INT ID], DATE, [TIME STAMP], NAME, PRODUCT, QUANTITY, PRICE, [TOTAL PRICE], SUMMARY, REMARKS) VALUES
		//	//('" + txtIntID.Text + "','" + dateInserted + "','" + sqlFormattedDate + "','" + cmbItemEmpList.Text + "','" + cmbProductList.Text + "','" + txtQuantity.Text + "','" + txtPrice.Text + "','" + txtTotalPrice.Text + "','" + txtSummary.Text + "','" + txtRemarks.Text + "')";
		//	using (SqlConnection conn = new SqlConnection(_dbConnection))
		//	{
		//
		//		switch (request)
		//		{
		//			case "Patch":
		//				{
		//					string message = String.Format("I made an update for {1}. Check the details below." +
		//						"\n\n\nReviewer ID: {0}\nInsurance Name: {1}\nPhone No: {2}\nFax No: {3}\nUR Phone No: {4}\nUR Fax No: {5}\nBR Phone No: {6}\nBR Fax No: {7}\nEmail: {8}\nRemarks: {9}",
		//						reviewerID.Text,
		//						insuranceName.Text,
		//						phoneNo.Text,
		//						faxNo.Text,
		//						urphoneNO.Text,
		//						urfaxNo.Text,
		//						brphoneNo.Text,
		//						brfaxNo.Text,
		//						email.Text,
		//						remarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("UPDATE [Bill Review Directory] SET [Insurance Name] = @INSURANCENAME, [Phone No.] = @PHONENO, [Fax No.] = @FAXNO, [UR Phone No.] = @URPHONENO," +
		//							"[UR Fax No.] = @URFAXNO, [BR Phone No.] = @BRPHONENO, [BR Fax No.] = @BRFAXNO, Email = @EMAIL, REMARKS = @REMARKS  WHERE [Reviewer ID] = @REVIEWERID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@REVIEWERID", reviewerID.Text);
		//							cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName.Text);
		//							cmd.Parameters.AddWithValue("@PHONENO", phoneNo.Text);
		//							cmd.Parameters.AddWithValue("@FAXNO", faxNo.Text);
		//							cmd.Parameters.AddWithValue("@URPHONENO", urphoneNO.Text);
		//							cmd.Parameters.AddWithValue("@URFAXNO", urfaxNo.Text);
		//							cmd.Parameters.AddWithValue("@BRPHONENO", brphoneNo.Text);
		//							cmd.Parameters.AddWithValue("@BRFAXNO", brfaxNo.Text);
		//							cmd.Parameters.AddWithValue("@EMAIL", email.Text);
		//							cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						string logs = empName + " updated Reviewer ID: " + reviewerID.Text;
		//						task.AddActivityLog(message, empName, logs, "UPDATED BILL REVIEWER INFORMATION");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						task.SendToastNotifDesktop(logs);
		//						//RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: BillReviewDBRequest \n Process: Patch \n Entry ID: " + reviewerID.Text + " \n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageUpdate + reviewerID.Text, "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
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
		//					//var addtoCart = "INSERT INTO [Pantry Listahan] ([INT ID], DATE, [TIME STAMP], NAME, PRODUCT, QUANTITY, PRICE, [TOTAL PRICE], SUMMARY, REMARKS) VALUES
		//					//('" + txtIntID.Text + "','" + dateInserted + "','" + sqlFormattedDate + "','" + cmbItemEmpList.Text + "','" + cmbProductList.Text + "','" + txtQuantity.Text + "','" + txtPrice.Text + "','" + txtTotalPrice.Text + "','" + txtSummary.Text + "','" + txtRemarks.Text + "')";
		//
		//					string message = String.Format("I added {1} in the records. Check the details below." +
		//						"\n\n\nReviewer ID: {0}\nInsurance Name: {1}\nPhone No: {2}\nFax No: {3}\nUR Phone No: {4}\nUR Fax No: {5}\nBR Phone No: {6}\nBR Fax No: {7}\nEmail: {8}\nRemarks: {9}",
		//						reviewerID.Text,
		//						insuranceName.Text,
		//						phoneNo.Text,
		//						faxNo.Text,
		//						urphoneNO.Text,
		//						urfaxNo.Text,
		//						brphoneNo.Text,
		//						brfaxNo.Text,
		//						email.Text,
		//						remarks.Text);
		//					try
		//					{
		//
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("INSERT INTO [Bill Review Directory] ([Insurance Name], [Phone No.], [Fax No.], [UR Phone No.]," +
		//							"[UR Fax No.], [BR Phone No.], [BR Fax No.], Email, [Remarks])" +
		//							"VALUES (@INSURANCENAME, @PHONENO, @FAXNO, @URPHONENO, @URFAXNO, @BRPHONENO, @BRFAXNO, @EMAIL, @REMARKS)", conn))
		//						{
		//							//cmd.Parameters.AddWithValue("@REVIEWERID", reviewerID.Text);
		//							cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName.Text);
		//							cmd.Parameters.AddWithValue("@PHONENO", phoneNo.Text);
		//							cmd.Parameters.AddWithValue("@FAXNO", faxNo.Text);
		//							cmd.Parameters.AddWithValue("@URPHONENO", urphoneNO.Text);
		//							cmd.Parameters.AddWithValue("@URFAXNO", urfaxNo.Text);
		//							cmd.Parameters.AddWithValue("@BRPHONENO", brphoneNo.Text);
		//							cmd.Parameters.AddWithValue("@BRFAXNO", brfaxNo.Text);
		//							cmd.Parameters.AddWithValue("@EMAIL", email.Text);
		//							cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						string logs = empName + " added Reviewer ID: " + reviewerID.Text;
		//						task.AddActivityLog(message, empName, logs, "ADDED BILL REVIEWER INFORMATION");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						task.SendToastNotifDesktop(logs);
		//						//RadMessageBox.Show("Record successfully added", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//
		//					}
		//
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: BillReviewDBRequest \n Process: Create \n Entry ID: " + reviewerID.Text + " \n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageCreate + reviewerID.Text, "Failed to Create", MessageBoxButtons.OK, RadMessageIcon.Error);
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
		//
		//					string message = String.Format("I removed {1} in the records. Check the details below." +
		//						"\n\n\nReviewer ID: {0}\nInsurance Name: {1}\nPhone No: {2}\nFax No: {3}\nUR Phone No: {4}\nUR Fax No: {5}\nBR Phone No: {6}\nBR Fax No: {7}\nEmail: {8}\nRemarks: {9}",
		//						reviewerID.Text,
		//						insuranceName.Text,
		//						phoneNo.Text,
		//						faxNo.Text,
		//						urphoneNO.Text,
		//						urfaxNo.Text,
		//						brphoneNo.Text,
		//						brfaxNo.Text,
		//						email.Text,
		//						remarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("DELETE FROM [Bill Review Directory] WHERE [Reviewer ID] = @REVIEWERID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@REVIEWERID", reviewerID.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						string logs = empName + " deleted Reviewer ID: " + reviewerID.Text;
		//						task.AddActivityLog(message, empName, logs, "DELETED BILL REVIEWER INFORMATION");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						task.SendToastNotifDesktop(logs);
		//						//RadMessageBox.Show("Record successfully Deleted", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: BillReviewDBRequest \n Process: Delete \n Entry ID: " + reviewerID.Text + "\n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageDelete + reviewerID.Text, "Failed to Delete", MessageBoxButtons.OK, RadMessageIcon.Error);
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
