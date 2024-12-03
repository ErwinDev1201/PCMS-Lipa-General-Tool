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
	public class MPN
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		private readonly CommonTask task = new();

		public DataTable ViewMPNList(string empName, out string lblCount)
		{
			const string query = "SELECT * FROM [MPN Information]";
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
				task.LogError("ViewMPNList", empName, "MPN", "N/A", ex);
			}

			return data;
		}


		public void MPNDBRequest(string request, RadTextBox mpnID, RadTextBox insuranceName, RadTextBox mpn, RadTextBox userName, RadTextBox passWord, RadTextBox webSite, RadTextBoxControl remarks, string empName)
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
								UPDATE [MPN Information] SET [Insurance Name] = @INSURANCENAME,
									[MPN] = @MPN, [Username] = @USERNAME, [Password] = @PASSWORD,
									[Website] = @WEBSITE, REMARKS = @REMARKS 
								WHERE
									[MPN ID] = @MPNID",
					"Create" => @"
								INSERT INTO [MPN Information] ([MPN ID], [Insurance Name], [MPN], [Username],
									[Password], [Website], [Remarks])
								VALUES
									(@MPNID, @INSURANCENAME, @MPN, @USERNAME, @PASSWORD,  @WEBSITE, @REMARKS)",
					"Delete" => @"DELETE FROM [MPN Information] WHERE [MPN ID] = @MPNID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@MPNID", mpnID.Text);
					cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName.Text);
					cmd.Parameters.AddWithValue("@MPN", mpn.Text);
					cmd.Parameters.AddWithValue("@USERNAME", userName.Text);
					cmd.Parameters.AddWithValue("@PASSWORD", passWord.Text);
					cmd.Parameters.AddWithValue("@WEBSITE", webSite.Text);
					cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);

				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@MPNID", mpnID.Text);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d MPN ID: {mpnID.Text}";
				message = $"Done! {mpnID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} MPN INUSURANCE INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError("MPNDBRequest", empName, "MPN", mpnID.Text, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}

	}
}
