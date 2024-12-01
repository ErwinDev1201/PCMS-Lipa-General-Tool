using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using YourmeeAppLibrary.Email;
using YourmeeAppLibrary.Security;

namespace PCMS_Lipa_General_Tool.Class
{
	public class HearingRep
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private readonly CommonTask task = new();

		public DataTable ViewHearinRepList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Hearing Representative]";
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
				task.LogError("ViewAttorneyList", empName, "CommonTask", "N/A", ex);
			}

			return data;
		}

		public void FillHearingRep(RadGridView dgHearingRep, RadTextBox txtIntID, RadTextBox txtBoard, RadTextBox txtHearingRep, RadTextBox txtemailAddress, RadTextBox txtPhoneNo, RadTextBoxControl txtRemarks, string empName)
		{
			using SqlConnection con = new(_dbConnection);
			try
			{
				con.Open();
				{
					using SqlCommand cmd = new("SELECT * FROM [Hearing Representative]", con);
					cmd.ExecuteNonQuery();
					var dgRow = dgHearingRep.SelectedRows[0];
					{
						txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
						txtBoard.Text = dgRow.Cells[1].Value + string.Empty;
						txtHearingRep.Text = dgRow.Cells[2].Value + string.Empty;
						txtemailAddress.Text = dgRow.Cells[3].Value + string.Empty;
						txtPhoneNo.Text = dgRow.Cells[4].Value + string.Empty;
						txtRemarks.Text = dgRow.Cells[5].Value + string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError($"FillHearingRep", empName, "Pantry", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}


		public void HRRepDBRequest(string request, RadTextBox repID, RadTextBox boardNo, RadTextBox hearRepName, RadTextBox email, RadTextBox phoneNo, RadTextBoxControl remarks, string empName)
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
								UPDATE [Hearing Representative] SET [Board] = @BOARD,
									[Name] = @NAME,[Email] = @EMAIL,
									[Phone No.] = @PHONENO, REMARKS = @REMARKS 
								WHERE
									[Rep ID] = @REPID",
					"Create" => @"
								INSERT INTO [Hearing Representative] ([Rep ID], [Board], [Name], [Email], [Phone No.], [Remarks])
									VALUES 
								(@REPID, @BOARD, @NAME, @EMAIL, @PHONENO, @REMARKS",
					"Delete" => @"DELETE FROM [Hearing Representative] WHERE [Rep ID] = @REPID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@REPID", repID.Text);
					cmd.Parameters.AddWithValue("@BOARD", boardNo.Text);
					cmd.Parameters.AddWithValue("@NAME", hearRepName.Text);
					cmd.Parameters.AddWithValue("@EMAIL", email.Text);
					cmd.Parameters.AddWithValue("@PHONENO", phoneNo.Text);
					cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@REPID", repID.Text);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Hearing Rep ID: {repID.Text}";
				message = $"Done! {repID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} HEARING REP INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError($"HRRepDBRequest - {request}", empName, "Hearing Rep", repID.Text, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
