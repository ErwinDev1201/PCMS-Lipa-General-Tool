
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Class
{
	public class Leave
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private readonly CommonTask task = new();



		public void FillUpSupportLeaveForm(RadTextBox empID, RadTextBox employeeName, RadTextBox position, RadTextBox empStat, string empName)
		{
			using var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				var query = "SELECT [Employee Name], Position, [Employment Status] FROM [User Information] WHERE [Employee ID] = @empID";
				using var cmd = new SqlCommand(query, con);

				// Use the Text property of RadTextBox for the parameter value
				cmd.Parameters.AddWithValue("@empID", empID.Text);

				using var reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					employeeName.Text = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
					position.Text = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
					empStat.Text = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
				}
			}
			catch (Exception ex)
			{
				task.LogError("FillUpSupportLeaveForm", empName, "Leave", empID.Text, ex);
			}
		}


		//public void FillUpSupportLeaveForm(RadTextBox empID, RadTextBox employeeName, RadTextBox position, RadTextBox empStat, string empName)
		//{
		//	using var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		con.Open();
		//		var query = "SELECT [Employee Name], Position, [Employment Status] FROM [User Information] WHERE [Employee ID] = @empID";
		//		using var cmd = new SqlCommand(query, con);
		//		cmd.Parameters.AddWithValue("@empID", empID);
		//
		//		using var reader = cmd.ExecuteReader();
		//		if (reader.Read())
		//		{
		//			employeeName.Text = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
		//			position.Text = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
		//			empStat.Text = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		string errorMessage = $"Error: {ex.Message}\n\n Name: {empName}\nModule: DBQuery \nProcess: FillUpSupportLeaveForm\n\nDetails: {ex}";
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", errorMessage, empName, Global.DCErrorWebHook, Global.DCErrorInvite);
		//		task.ExecutedbCollBackupCsv(empName);
		//	}
		//}
		//
		public void FillUpLeaveFields(
			RadGridView dgLeave, RadLabel leaveID,
			RadTextBox employeeID, RadTextBox employeeName, RadDateTimePicker startDate, RadDateTimePicker endDate, RadTextBoxControl reason,
									  RadRadioButton rdowithPay, RadRadioButton withOutPay, RadRadioButton sick, RadRadioButton vacation, RadRadioButton paternity,
									  RadRadioButton maternity, RadRadioButton birthday, RadRadioButton bereavement, RadDropDownList cmbApproval, RadTextBoxControl remarks, string empName)
		{
			if (dgLeave.SelectedRows.Count == 0) return;

			using var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();

				var query = "SELECT [Leave ID], [Employee ID], [Employee Name], [Start Date], [End Date], Payment, [Applied Leave], Reason, Status, Remarks FROM Leave WHERE [Leave ID] = @leaveID";
				using var cmd = new SqlCommand(query, con);
				var row = dgLeave.SelectedRows[0];
				string selectedLeaveID = row.Cells[0].Value.ToString();
				cmd.Parameters.AddWithValue("@leaveID", selectedLeaveID);

				using var reader = cmd.ExecuteReader();
				if (reader.Read())
				{

					leaveID.Text = reader["Leave ID"].ToString();
					employeeID.Text = reader["Employee ID"].ToString() ?? string.Empty;
					employeeName.Text = reader["Employee Name"].ToString();

					if (!reader.IsDBNull(reader.GetOrdinal("Start Date")) && DateTime.TryParse(reader.GetString(reader.GetOrdinal("Start Date")), out DateTime parsedStartDate))
					{
						startDate.Value = parsedStartDate;
					}
					else
					{
						startDate.Value = DateTime.MinValue; // or null if your control supports it
					}

					if (!reader.IsDBNull(reader.GetOrdinal("End Date")) &&
						DateTime.TryParse(reader.GetString(reader.GetOrdinal("End Date")), out DateTime parsedEndDate))
					{
						endDate.Value = parsedEndDate;
					}
					else
					{
						endDate.Value = DateTime.MinValue; // or null if your control supports it
					}
					if (!reader.IsDBNull(reader.GetOrdinal("Start Date")) && DateTime.TryParse(reader.GetString(reader.GetOrdinal("Start Date")), out parsedStartDate))
					{
						startDate.Value = parsedStartDate;
					}
					else
					{
						startDate.Value = DateTime.MinValue; // or null if your control supports it
					}

					if (!reader.IsDBNull(reader.GetOrdinal("End Date")) &&
						DateTime.TryParse(reader.GetString(reader.GetOrdinal("End Date")), out parsedEndDate))
					{
						endDate.Value = parsedEndDate;
					}
					else
					{
						endDate.Value = DateTime.MinValue;
					}
					//startDate.Value = reader.GetDateTime(reader.GetOrdinal("Start Date"));
					//startDate.Value = reader.IsDBNull(reader.GetOrdinal("Start Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Start Date"));
					//endDate.Value = reader.IsDBNull(reader.GetOrdinal("End Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("End Date"));

					reason.Text = reader["Reason"].ToString();
					cmbApproval.Text = reader["Status"].ToString();
					remarks.Text = reader["Remarks"].ToString();

					// Set Payment Option
					bool isPaidLeave = reader["Payment"].ToString() == "With Pay";
					rdowithPay.IsChecked = isPaidLeave;
					withOutPay.IsChecked = !isPaidLeave;

					// Set Type of Leave
					string typeOfLeave = reader["Applied Leave"].ToString();
					SetLeaveTypeRadioButton(typeOfLeave, sick, vacation, paternity, maternity, birthday, bereavement);
				}
			}
			catch (Exception ex)
			{
				task.LogError("FillUpLeaveFields", empName, "Leave", leaveID.Text, ex);
			}
		}

		private void SetLeaveTypeRadioButton(string typeOfLeave, RadRadioButton sick, RadRadioButton vacation, RadRadioButton paternity, RadRadioButton maternity, RadRadioButton birthday, RadRadioButton bereavement)
		{
			sick.IsChecked = (typeOfLeave == "Sick");
			vacation.IsChecked = (typeOfLeave == "Vacation");
			paternity.IsChecked = (typeOfLeave == "Paternity");
			maternity.IsChecked = (typeOfLeave == "Maternity");
			birthday.IsChecked = (typeOfLeave == "Birthday");
			bereavement.IsChecked = (typeOfLeave == "Bereavement");
		}

		public string GetLeaveQuery(string filterName, string filterStatus, bool isElevatedAccess)
		{
			return (filterName, filterStatus, isElevatedAccess) switch
			{
				// Both filters provided
				(var name, var status, _) when !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(status) =>
					$"SELECT * FROM [Leave] WHERE [Employee Name] = '{name}' AND Status = '{status}'",

				// Name filter provided only
				(var name, _, _) when !string.IsNullOrWhiteSpace(name) =>
					$"SELECT * FROM [Leave] WHERE [Employee Name] = '{name}'",

				// Status filter provided only
				(_, var status, _) when !string.IsNullOrWhiteSpace(status) =>
					$"SELECT * FROM [Leave] WHERE Status = '{status}'",

				// No filters and elevated access (fetch all records)
				(_, _, true) => "SELECT * FROM [Leave]",

				// Default case (non-elevated access with no filters)
				_ => "SELECT * FROM [Leave] WHERE 1 = 0" // Prevents fetching all data for non-elevated users
			};
		}

		//public void FillUpSupportLeaveForm(string empID, RadTextBox employeeName, RadTextBox position, RadTextBox empStat, string empName)
		//{
		//	using (var con = new SqlConnection(_dbConnection))
		//	{
		//		try
		//		{
		//			con.Open();
		//			using (SqlCommand cmd = new SqlCommand("SELECT [Employee Name], Position, [Employment Status] FROM [User Information] WHERE [Employee ID] = '" + empID + "'", con))
		//			{
		//				using (var reader = cmd.ExecuteReader())
		//				{
		//					if (reader.Read())
		//					{
		//						employeeName.Text = reader.IsDBNull(0) ? null : reader.GetString(0);
		//						position.Text = reader.IsDBNull(1) ? null : reader.GetString(1);
		//						empStat.Text = reader.IsDBNull(2) ? null : reader.GetString(2);
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: DBQuery \n Process: FillUpSupportLeaveForm \n\n Detailed Error: " + ex.ToString();
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, NameofUser, Global.DCErrorWebHook, Global.DCErrorInvite);
		//			task.ExecutedbCollBackupCsv(empName);
		//		}
		//		finally
		//		{
		//			con.Close();
		//		}
		//	}
		//}
		//
		//public void FillUpLeaveFields(RadGridView dgLeave, RadLabel leaveID, RadTextBox employeeName, RadDateTimePicker startDate, RadDateTimePicker endDate, RadTextBoxControl Reason,
		//	 RadRadioButton rdowithPay, RadRadioButton withOutPay, RadRadioButton sick, RadRadioButton vacation, RadRadioButton paternity, RadRadioButton maternity, RadRadioButton birthday,
		//	 RadRadioButton bereavement, RadDropDownList cmbApproval, RadTextBoxControl remarks, string empName)
		//{
		//	using (var con = new SqlConnection(_dbConnection))
		//	{
		//		try
		//		{
		//			con.Open();
		//			using (SqlCommand cmd = new SqlCommand("SELECT * FROM Leave", con))
		//			{
		//				cmd.ExecuteNonQuery();
		//				if (dgLeave.SelectedRows.Count > 0)
		//				{
		//					string paymentOption;
		//					string typeOfLeave;
		//					var row = dgLeave.SelectedRows[0];
		//					{
		//						leaveID.Text = row.Cells[0].Value + string.Empty;
		//						employeeName.Text = row.Cells[1].Value + string.Empty;
		//						startDate.Text = row.Cells[2].Value + string.Empty;
		//						endDate.Text = row.Cells[3].Value + string.Empty;
		//						paymentOption = row.Cells[4].Value + string.Empty;
		//						typeOfLeave = row.Cells[5].Value + string.Empty;
		//						Reason.Text = row.Cells[6].Value + string.Empty;
		//						cmbApproval.Text = row.Cells[7].Value + string.Empty;
		//						remarks.Text = row.Cells[8].Value + string.Empty;
		//					}
		//					if (paymentOption == "With Pay")
		//					{
		//						rdowithPay.IsChecked = true;
		//					}
		//					else
		//					{
		//						withOutPay.IsChecked = true;
		//					}
		//					if (typeOfLeave == "Sick")
		//					{
		//						sick.IsChecked = true;
		//					}
		//					else if (typeOfLeave == "Bereavement")
		//					{
		//						bereavement.IsChecked = true;
		//					}
		//					else if (typeOfLeave == "Paternity")
		//					{
		//						paternity.IsChecked = true;
		//					}
		//					else if (typeOfLeave == "Maternity")
		//					{
		//						maternity.IsChecked = true;
		//					}
		//					else if (typeOfLeave == "Birthday")
		//					{
		//						birthday.IsChecked = true;
		//					}
		//					else
		//					{
		//						vacation.IsChecked = true;
		//					}
		//				}
		//			};
		//		}
		//		catch (Exception ex)
		//		{
		//			var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: DBQuery \n Process: FillUpLeaveFields \n\n Detailed Error: " + ex.ToString();
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, NameofUser, Global.DCErrorWebHook, Global.DCErrorInvite);
		//			task.ExecutedbCollBackupCsv(empName);
		//		}
		//		finally
		//		{
		//			con.Close();
		//		}
		//	}
		//}
		//

		public void ViewLeave(RadGridView dataGrid, string query, RadLabel lblcount, string empName)
		{
			var con = new SqlConnection(_dbConnection);
			try
			{
				using (var adp = new SqlDataAdapter(query, con))
				{
					var data = new DataTable();
					adp.Fill(data);
					adp.Update(data);
					dataGrid.DataSource = data.DefaultView;
					lblcount.Text = $"Total records: {dataGrid.RowCount}";
				}
				dataGrid.BestFitColumns(BestFitColumnMode.AllCells);
			}
			catch (Exception ex)
			{
				task.LogError("ViewDatagrid", empName, "CommonTask", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}

		public void LeaveFiling(
			string request, RadLabel leaveID,
			RadTextBox empID, RadTextBox EmployeeName, RadDateTimePicker startDate,
			RadDateTimePicker endDate, string paymentOption, string typeofLeave, RadTextBoxControl reason, RadDropDownList approvalStatus,
			RadTextBoxControl remarks, string empName, RadTextBox approverPosition)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs, message, notifmessage, mailContent;
				string emailRequestType = request.Equals("Create", StringComparison.OrdinalIgnoreCase) ? "file" : "response";

				cmd.CommandText = request switch
				{
					"Update" => @"UPDATE Leave SET [Employee Name] = @EmpName, [Start Date] = @StartDate,
									[End Date] = @EndDate, Payment = @Payment, [Applied Leave] = @AppliedLeave,
									Reason = @Reason, Status = @Status, Remarks = @Remarks, [Employee ID] = @EmpID, 
									[Approved by] = @Approver
								WHERE
									[Leave ID] = @LeaveID",
					"Create" => @"INSERT INTO Leave
										([Leave ID], [Employee ID], [Employee Name], [Start Date], [End Date], Payment, [Applied Leave], Reason, Status)
									VALUES
										(@LeaveID, @EmpID, @EmpName, @StartDate, @EndDate, @Payment, @AppliedLeave, @Reason, 'FOR APPROVAL')",
					"Delete" => @"DELETE FROM Leave WHERE [Leave ID] = @LeaveID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@EmpID", empID.Text);
					cmd.Parameters.AddWithValue("@EmpName", EmployeeName.Text);
					cmd.Parameters.AddWithValue("@StartDate", startDate.Text);
					cmd.Parameters.AddWithValue("@EndDate", endDate.Text);
					cmd.Parameters.AddWithValue("@Payment", paymentOption);
					cmd.Parameters.AddWithValue("@AppliedLeave", typeofLeave);
					cmd.Parameters.AddWithValue("@Reason", reason.Text);
					cmd.Parameters.AddWithValue("@Status", approvalStatus.Text);
					cmd.Parameters.AddWithValue("@Approver", empName);
					cmd.Parameters.AddWithValue("@Remarks", remarks.Text);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@leaveID", leaveID.Text);

				// Execute query
				cmd.ExecuteNonQuery();

				message = GenerateMessage(request, leaveID.Text, EmployeeName.Text, startDate.Text, endDate.Text, paymentOption, typeofLeave, reason.Text, approvalStatus.Text, remarks.Text);
				mailContent = GenerateEmailContent(EmployeeName.Text, startDate.Text, endDate.Text, paymentOption, typeofLeave, reason.Text, emailRequestType, approvalStatus.Text, approverPosition.Text, empName, remarks.Text);
				// Determine the request type and notify via email
				task.NotifyEmail(emailRequestType, mailContent, EmployeeName.Text, empName, approverPosition.Text);
				logs = $"{empName} {request.ToLower()}d leave ID: {leaveID.Text}";
				notifmessage = $"Done! {leaveID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} ADJUSTER INFORMATION");
				task.SendToastNotifDesktop(notifmessage);
			}
			catch (Exception ex)
			{
				task.LogError($"LeaveFiling ({request})", empName, "Leave", leaveID.Text, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}

		private string GenerateMessage(string request, string leaveID, string employeeName, string startDate, string endDate, string paymentOption, string typeofLeave, string reason, string approvalStatus, string remarks)
		{
			return request switch
			{
				"Update" => $"I made an update for file Leave ID {leaveID}. Check the details below.\n\nLeave ID: {leaveID}\nEmployee Name: {employeeName}\nStart Date: {startDate}\nEnd Date: {endDate}\nPayment Option: {paymentOption}\nType of Leave: {typeofLeave}\nReason: {reason}\nApproval Status: {approvalStatus}\nRemarks: {remarks}",
				"Create" => $"I am filing my leave with Leave ID {leaveID}. Check the details below.\n\nLeave ID: {leaveID}\nEmployee Name: {employeeName}\nStart Date: {startDate}\nEnd Date: {endDate}\nPayment Option: {paymentOption}\nType of Leave: {typeofLeave}\nReason: {reason}\nApproval Status: For Approval",
				"Delete" => $"I deleted file Leave ID {leaveID}. Check the details below.\n\nLeave ID: {leaveID}\nStart Date: {startDate}\nEnd Date: {endDate}\nPayment Option: {paymentOption}\nType of Leave: {typeofLeave}\nReason: {reason}\nApproval Status: {approvalStatus}\nRemarks: {remarks}",
				_ => throw new ArgumentException("Invalid request type")
			};
		}

		private string GenerateEmailContent(string employeeName, string startDate, string endDate, string paymentOption, string typeofLeave, string reason, string request, string approvalStatus, string apposition, string empName, string remarks)
		{
			if (request.Equals("response", StringComparison.OrdinalIgnoreCase))
			{
				string responseHtml;

				if (approvalStatus.Equals("APPROVED", StringComparison.OrdinalIgnoreCase))
				{
					responseHtml = $@"
            <html>
                <body>
                    <p>Dear {employeeName},</p>
                    <p>Your leave request from {startDate} to {endDate} has been <strong>approved</strong>. We hope you enjoy this time off and return feeling refreshed.</p>
                    <p>Please ensure that all pending tasks are either completed or delegated before your leave begins. Should any urgent matters arise, you may reach out to your immediate supervisor.</p>
                    <p>If you have further questions, feel free to contact the HR department.</p>
                    <br/>
                    <p>Best regards,</p>
                    <p>{empName}</p>
					<p>{apposition}</p>
                </body>
            </html>";
				}
				else if (approvalStatus.Equals("NOT APPROVED", StringComparison.OrdinalIgnoreCase))
				{
					responseHtml = $@"
            <html>
                <body>
                    <p>Dear {employeeName},</p>
                    <p>We regret to inform you that your leave request for the period {startDate} to {endDate} has <strong>not been approved</strong> due to {remarks}.</p>
                    <p>We understand the importance of your request and encourage you to discuss alternative options with your supervisor or the HR department if needed.</p>
                    <p>Thank you for your understanding. If you have any questions or require further clarification, please do not hesitate to reach out.</p>
                    <br/>
                    <p>Best regards,</p>
                    <p>{empName}</p>
					<p>{apposition}</p>
                </body>
            </html>";
				}
				else if (approvalStatus.Equals("NEED SUP. DOC", StringComparison.OrdinalIgnoreCase))
				{
					responseHtml = $@"
            <html>
                <body>
                    <p>Dear {employeeName},</p>
                    <p>Thank you for submitting your leave request for the period {startDate} to {endDate}. In order to proceed with the approval process, we kindly request the following supplemental documents:</p>
                    <p>{remarks}</p>
                    <p>Please submit the required documents at your earliest convenience to ensure timely processing of your leave request. If you have any questions or need assistance, feel free to contact the HR department.</p>
                    <br/>
                    <p>Thank you for your cooperation.</p>
                    <br/>
                    <p>Best regards,</p>
                    <p>{empName}</p>
					<p>{apposition}</p>
                </body>
            </html>";
				}
				else
				{
					responseHtml = $@"
            <html>
                <body>
                    <p>This is an automated response to your leave request. Your request is under review. Thank you.</p>
                </body>
            </html>";
				}

				return responseHtml;
			}

			var leaveMessage = typeofLeave switch
			{
				"Sick" => $@"
        <html>
            <body>
                <p>To Whom It May Concern,</p>
                <p>I would like to formally request Sick Leave from {startDate} to {endDate} due to {reason}. I will make every effort to recover fully during this time and look forward to resuming my duties as soon as I am able.</p>
                <p>Regards,<br/>{employeeName}</p>
            </body>
        </html>",

				"Bereavement" => $@"
        <html>
            <body>
                <p>To Whom It May Concern,</p>
                <p>I am respectfully requesting Bereavement Leave from {startDate} to {endDate} due to a recent loss in my family. I appreciate your understanding during this difficult time and will ensure to return to work once I am able.</p>
                <p>Regards,<br/>{employeeName}</p>
            </body>
        </html>",

				_ => $@"
        <html>
            <body>
                <p>To Whom It May Concern,</p>
                <p>Kindly consider my request for <strong>{typeofLeave} Leave</strong> from  <strong>{startDate}</strong> to  <strong>{endDate}</strong>, with <strong>{paymentOption}</strong> as the preferred payment option.<br/> The reason for this request is:  <strong>{reason}</strong>.</p
                <p>Regards,<br/>{employeeName}</p>
            </body>
        </html>"
			};

			return leaveMessage;

		}
		//private string GenerateEmailContent(string employeeName, string startDate, string endDate, string paymentOption, string typeofLeave, string reason)
		//{
		//	return typeofLeave switch
		//	{
		//		"Sick" => $"To Whom It May Concern,<br/><br/>I am requesting a Sick Leave from {startDate} through {endDate} due to {reason}. I hope to recover fully before resuming work.<br/><br/>Regards,<br/>{employeeName}",
		//		"Bereavement" => $"To Whom It May Concern,<br/><br/>I am requesting a Bereavement Leave from {startDate} to {endDate} due to a recent family loss.<br/><br/>Regards,<br/>{employeeName}",
		//		_ => $"To Whom It May Concern,<br/><br/>I am requesting a {typeofLeave} Leave from {startDate} to {endDate} with {paymentOption}.<br/>Reason: {reason}.<br/><br/>Regards,<br/>{employeeName}"
		//	};
		//}

		//public void LeaveFiling(string request, RadLabel leaveID, RadTextBox EmployeeName, RadDateTimePicker startDate, RadDateTimePicker endDate, string paymentOption, string typeofLeave, RadTextBoxControl reason, RadDropDownList approvalStatus, RadTextBoxControl remarks, RadTextBox position, string empName)
		//{
		//	using (SqlConnection conn = new SqlConnection(_dbConnection))
		//	{
		//		switch (request)
		//		{
		//
		//			case "Patch":
		//				{
		//					string message = String.Format("I made an update for file Leave ID {0}. Check the details below." +
		//						"\n\n\nLeave ID: {0}\nStart Date: {2} and End Date {3} \nPayment Option: {4}\nType of Leave: {5}\nReason: {6}\nApproval Status: {7}\nRemarks: {8}",
		//						leaveID.Text,
		//						EmployeeName.Text,
		//						startDate.Text,
		//						endDate.Text,
		//						paymentOption,
		//						typeofLeave,
		//						reason.Text,
		//						approvalStatus.Text,
		//						remarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("UPDATE Leave SET [Employee Name] = @EmpName, [Start Date] = @StartDate, [End Date] = @EndDate, Payment = @Payment, " +
		//							"[Applied Leave] = @AppliedLeave, Reason = @Reason, Status = @Status, Remarks = @Remarks, [Approved by] = @Approver WHERE [Leave ID] = @LeaveID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@LeaveID", leaveID.Text);
		//							cmd.Parameters.AddWithValue("@EmpName", EmployeeName.Text);
		//							cmd.Parameters.AddWithValue("@StartDate", startDate.Text);
		//							cmd.Parameters.AddWithValue("@EndDate", endDate.Text);
		//							cmd.Parameters.AddWithValue("@Payment", paymentOption);
		//							cmd.Parameters.AddWithValue("@AppliedLeave", typeofLeave);
		//							cmd.Parameters.AddWithValue("@Reason", reason.Text);
		//							cmd.Parameters.AddWithValue("@Status", approvalStatus.Text);
		//							cmd.Parameters.AddWithValue("@Approver", empName);
		//							cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						// bug fixed  0322 not approve leave getting approved email
		//
		//
		//						if (approvalStatus.Text == "NOT APPROVED")
		//						{
		//							string mailcontent = "Hi " + EmployeeName.Text + ", <br/><br/>I regret to inform you that we are unable to approve your leave request at this time due to operational constraints. We understand the importance of your request and apologize for any inconvenience caused. Please feel free to discuss alternative dates or options, and we will do our best to accommodate your needs.<br/><br/>Best Regards, <br/>" + empName + "<br/>" + position.Text;
		//							task.NotifyEmail("response", mailcontent, EmployeeName.Text, empName, position.Text);
		//							string logs = empName + " updated file leave information: " + leaveID.Text;
		//							task.AddActivityLog(message, empName, logs, "USER UPDATED A FILE LEAVE");
		//							winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						}
		//						else
		//						{
		//							string mailcontent = "Hi " + EmployeeName.Text + ", <br/><br/>This is to inform you that your leave request has been approved. You are authorized to take time off from work starting on " + startDate.Text + "  and end on " + endDate.Text + ". Please ensure that all essential work is completed and continued in your absence.<br/><br/>If you have any questions or concerns, please do not hesitate to contact me. Iâ€™m happy to discuss this further with you if needed. <br/><br/>Best Regards, <br/>" + empName + "<br/>" + position.Text;
		//							task.NotifyEmail("response", mailcontent, EmployeeName.Text, empName, position.Text);
		//							string logs = empName + " updated file leave information: " + leaveID.Text;
		//							task.AddActivityLog(message, empName, logs, "USER UPDATED A FILE LEAVE");
		//							winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						}
		//						RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: LeaveFiling \n Process: Patch  \n Entry ID: " + leaveID.Text + "\n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageUpdate + leaveID.Text, "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
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
		//					string message = String.Format("Im filing my leave with Leave ID {0}. Check the details below." +
		//						"\n\n\nLeave ID: {0}\nEmployee Name: {1}\nStart Date: {2} \nEnd Date {3} \nPayment Option: {4}\nType of Leave: {5}\nReason: {6}\nApproval Status: For Approval",
		//						leaveID.Text,
		//						EmployeeName.Text,
		//						startDate.Text,
		//						endDate.Text,
		//						paymentOption,
		//						typeofLeave,
		//						reason.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("INSERT INTO Leave ([Employee Name], [Start Date], [End Date], Payment, [Applied Leave], Reason, Status) VALUES (@EmpName, @StartDate, @EndDate, @Payment, " +
		//							"@AppliedLeave, @Reason, 'FOR APPROVAL')", conn))
		//						{
		//							//cmd.Parameters.AddWithValue("@LeaveID", leaveID);
		//							cmd.Parameters.AddWithValue("@EmpName", EmployeeName.Text);
		//							cmd.Parameters.AddWithValue("@StartDate", startDate.Text);
		//							cmd.Parameters.AddWithValue("@EndDate", endDate.Text);
		//							cmd.Parameters.AddWithValue("@Payment", paymentOption);
		//							cmd.Parameters.AddWithValue("@AppliedLeave", typeofLeave);
		//							cmd.Parameters.AddWithValue("@Reason", reason.Text);
		//							//cmd.Parameters.AddWithValue("@Status", approvalStatus.Text);
		//							//cmd.Parameters.AddWithValue("@Approver", EmployeeName.Text);
		//							//cmd.P11arameters.AddWithValue("@REMARKS", remarks.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						{
		//							string mailcontent;
		//							if (typeofLeave == "Sick")
		//							{
		//								mailcontent = "To Whom It May Concern, <br/><br/>I want to request a Sick Leave from " + startDate.Text + " through " + endDate.Text + " and filing it with pay.<br/>I was suffering from a " + reason.Text + ". I want my body to be fully recovered so it will not affect my performance during work.<br/><br/>I hope you understand my plea for this leave.<br/><br/>Regards, <br/>" + EmployeeName.Text;
		//							}
		//							else if (typeofLeave == "Bereavement")
		//							{
		//								mailcontent = "To Whom It May Concern, <br/><br/>I am writing to request a leave of absence from work due to a recent family bereavement. I would like to take leave from " + startDate.Text + " to " + endDate.Text + ".<br/><br/> Thank you for your understanding and support during this difficult time.<br/><br/>  Regards, <br/>" + EmployeeName.Text;
		//							}
		//							else
		//							{
		//								mailcontent = "To Whom It May Concern, <br/><br/>I want to request a " + typeofLeave + " leave from " + startDate.Text + " through " + endDate.Text + " and filing it " + paymentOption + ".\nThe reason for my leave is " + reason.Text + ". I want to inform you that before I leave, I'll complete all assigned task on my end <br/><br/>Regards, <br/>" + EmployeeName.Text;
		//							}
		//							task.NotifyEmail("file", mailcontent, EmployeeName.Text, empName, position.Text);
		//						}
		//						string logs = empName + " file a leave with leave ID: " + leaveID.Text;
		//						task.AddActivityLog(message, empName, logs, "USER FILED A LEAVE");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: LeaveFiling \n Process: Create  \n Entry ID: " + leaveID.Text + "\n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageUpdate + leaveID.Text, "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
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
		//					string message = String.Format("I deleted file Leave ID {0}. Check the details below." +
		//						"\n\n\nLeave ID: {0}\nStart Date: {2} and End Date {3} \nPayment Option: {4}\nType of Leave: {5}\nReason: {6}\nApproval Status: {7}\nRemarks: {8}",
		//						leaveID,
		//						EmployeeName.Text,
		//						startDate.Text,
		//						endDate.Text,
		//						paymentOption,
		//						typeofLeave,
		//						reason.Text,
		//						approvalStatus.Text,
		//						remarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("DELETE FROM [Leave] WHERE [Leave ID] = @leaveID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@leaveID", leaveID.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						string logs = empName + " deleted filed leave ID: " + leaveID.Text;
		//						task.AddActivityLog(message, empName, logs, "USER DELETED FILED LEAVED");
		//						winDiscordAPI.PublishtoDiscord("PCMS Lipa 9General Tool - Activity Logger", "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						RadMessageBox.Show("Record successfully Deleted", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: LeaveFiling \n Process: Delete  \n Entry ID: " + leaveID.Text + "\n\n Detailed Error: " + ex.ToString();
		//						winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//						RadMessageBox.Show(ErrorMessageUpdate + leaveID, "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
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
