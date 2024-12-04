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
	public class Attorney
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private static readonly CommonTask task = new();


		public DataTable ViewAttorneyList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Attorney Information]";
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
				task.LogError("ViewAttorneyList", empName, "Attorney", "N/A", ex);
			}

			return data;
		}



		public void AttorneyDBRequest(
			string request,
			string attyID, string
			cmbAttyType,
			string attyName,
			string phoneNo,
			string faxNo, string email, string remarks, string empName)
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
								UPDATE [Attorney Information] SET [Attorney Type] = @ATTYTYPE,
									[Attorney Name] = @ATTYNAME, [Phone No.] = @PHONENO,
									[Fax No.] = @FAXNO, [Email] = @EMAIL, REMARKS = @REMARKS
								WHERE
									[Attorney ID] = @ATTYID",
					"Create" => @"
								INSERT INTO [Attorney Informations] ([Attorney ID], [Attorney Type], [Attorney Name],
									[Phone No.], [Fax No.], [Email], [Remarks])
								VALUES
									(@ATTYID, @ATTYTYPE, @ATTYNAME, @PHONENO, @FAXNO, @EMAIL, @REMARKS)",
					"Delete" => @"DELETE FROM [Attorney Information] WHERE [Attorney ID] = @ATTYID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@ATTYID", attyID);
					cmd.Parameters.AddWithValue("@ATTYTYPE", cmbAttyType);
					cmd.Parameters.AddWithValue("@ATTYNAME", attyName);
					cmd.Parameters.AddWithValue("@PHONENO", phoneNo);
					cmd.Parameters.AddWithValue("@FAXNO", faxNo);
					cmd.Parameters.AddWithValue("@EMAIL", email);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@ATTYID", attyID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Atty ID: {attyID}";
				message = $"Done! {attyID} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} ATTORNEY INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError($"AttorneyDBRequest {request}", empName, "Attorney", attyID, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
