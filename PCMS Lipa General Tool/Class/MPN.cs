using System;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using YourmeeAppLibrary.Email;
using YourmeeAppLibrary.Security;

namespace PCMS_Lipa_General_Tool.Class
{
	public class MPN
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		private readonly CommonTask task = new();

		public void FillUpMPNWeblink(RadGridView dataGrid, RadTextBox Link, string empName)
		{
			//SELECT INT_ID, INSURANCE, MPN, USERNAME, PASSWORD, WEBSITE, REMARKS FROM [MPN LIST]
			using var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				using SqlCommand cmd = new("SELECT * FROM [MPN Information]", con);
				cmd.ExecuteNonQuery();
				if (dataGrid.SelectedRows.Count > 0)
				{
					Link.Text = dataGrid.SelectedRows[0].Cells[5].Value + string.Empty;
				}
			}
			catch (Exception ex)
			{
				task.LogError($"FillUpMPNWeblink", empName, "MPN", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}



		public void FillMPNData(RadGridView dgMPN, RadTextBox txtIntID, RadTextBox txtInsuranceName, RadTextBox txtMPNName, RadTextBox txtMPNUsername, RadTextBox txtPassword, RadTextBox txtWebLink, RadTextBoxControl txtRemarks, string empName)
		{
			using SqlConnection con = new(_dbConnection);
			try
			{
				con.Open();
				{
					using SqlCommand cmd = new("SELECT * FROM [MPN Information]", con);
					cmd.ExecuteNonQuery();
					var dgRow = dgMPN.SelectedRows[0];
					{
						txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
						txtInsuranceName.Text = dgRow.Cells[1].Value + string.Empty;
						txtMPNName.Text = dgRow.Cells[2].Value + string.Empty;
						txtMPNUsername.Text = dgRow.Cells[3].Value + string.Empty;
						txtPassword.Text = dgRow.Cells[4].Value + string.Empty;
						txtWebLink.Text = dgRow.Cells[5].Value + string.Empty;
						txtRemarks.Text = dgRow.Cells[6].Value + string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError("FillMPNData", empName, "MPN", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
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
