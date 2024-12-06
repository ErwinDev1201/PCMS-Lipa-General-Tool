using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Telerik.WinControls;




namespace PCMS_Lipa_General_Tool.Class
{
	public class EasyPrint
	{
		
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private readonly CommonTask task = new();

		public DataTable ViewEasyPrintList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Easy Print Denial]";
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
				task.LogError("ViewEasyPrintList", empName, "EasyPrint", "N/A", ex);
			}

			return data;
		}


		public void EPDenialDBRequest(
			string request,
			string epdenialID,
			string easyprintCode,
			string insuranceName,
			string Description,
			string possibleResolution,
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

				cmd.CommandText = request switch
				{
					"Update" => @"UPDATE [Easy Print Denial]
								SET [Easy Print Code] = @EPCODE, [Insurance] = @INSURANCENAME,
									[Description] = @DESCRIPTION, [Possible Resolution] = @POSSIBLERESO,
									REMARKS = @REMARKS
								WHERE
									[EP ID] = @EPDENIALCODE",
					"Create" => @"INSERT INTO [Easy Print Denial]
								([EP ID], [Easy Print Code], [Insurance], [Description], [Possible Resolution], [Remarks])
								VALUES (@EPDENIALCODE, @EPCODE, @INSURANCENAME, @DESCRIPTION, @POSSIBLERESO, @REMARKS)",
					"Delete" => @"DELETE FROM [Easy Print Denial] WHERE [EP ID] = @EPDENIALCODE",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@EPDENIALCODE", epdenialID);
					cmd.Parameters.AddWithValue("@EPCODE", easyprintCode);
					cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName);
					cmd.Parameters.AddWithValue("@DESCRIPTION", Description);
					cmd.Parameters.AddWithValue("@POSSIBLERESO", possibleResolution);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@EPDENIALCODE", epdenialID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Denial ID: {epdenialID}";
				message = $"Done! {epdenialID} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} EASYPRINT INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError($"EPDenialDBRequest - {request}", empName, "EasyPrint", epdenialID, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
