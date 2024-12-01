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
				task.LogError("ViewAttorneyList", empName, "CommonTask", "N/A", ex);
			}

			return data;
		}

		public void FillEasyPrint(RadGridView dgEasyPrinttbl, RadTextBox txtIntID, RadTextBox txtEPCode, RadTextBox txtInsuranceName, RadTextBoxControl txtdenialDescription, RadTextBoxControl txtPossibleSolution, RadTextBoxControl txtRemarks, string empName)
		{
			using SqlConnection con = new(_dbConnection);
			try
			{
				con.Open();
				{
					using SqlCommand cmd = new("SELECT * FROM [Easy Print Denial]", con);
					cmd.ExecuteNonQuery();
					var dgRow = dgEasyPrinttbl.SelectedRows[0];
					{
						txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
						txtEPCode.Text = dgRow.Cells[1].Value + string.Empty;
						txtInsuranceName.Text = dgRow.Cells[2].Value + string.Empty;
						txtdenialDescription.Text = dgRow.Cells[3].Value + string.Empty;
						txtPossibleSolution.Text = dgRow.Cells[4].Value + string.Empty;
						txtRemarks.Text = dgRow.Cells[5].Value + string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError($"FillEasyPrint", empName, "EasyPrint", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}

		public void EPDenialDBRequest(string request, RadTextBox epdenialID, RadTextBox easyprintCode, RadTextBox insuranceName, RadTextBoxControl Description, RadTextBoxControl possibleResolution, RadTextBoxControl remarks, string empName)
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
					cmd.Parameters.AddWithValue("@EPDENIALCODE", epdenialID.Text);
					cmd.Parameters.AddWithValue("@EPCODE", easyprintCode.Text);
					cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName.Text);
					cmd.Parameters.AddWithValue("@DESCRIPTION", Description.Text);
					cmd.Parameters.AddWithValue("@POSSIBLERESO", possibleResolution.Text);
					cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@EPDENIALCODE", epdenialID.Text);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Denial ID: {epdenialID.Text}";
				message = $"Done! {epdenialID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} EASYPRINT INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError($"EPDenialDBRequest - {request}", empName, "EasyPrint", epdenialID.Text, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
