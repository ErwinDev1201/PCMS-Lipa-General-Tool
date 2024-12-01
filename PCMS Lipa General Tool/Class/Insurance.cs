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
	public class Insurance
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		private readonly CommonTask task = new();

		public DataTable ViewInsuraceList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Insurance Info]";
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


		public void FillInsuranceInfo(RadGridView dgInsInfo, RadTextBox txtIntID, RadTextBox txtInsuranceName, RadTextBox txtInsCode, RadTextBoxControl txtInsuranceAdress, RadTextBox txtPayerID, RadTextBoxControl txtRemarks, string empName)
		{
			using SqlConnection con = new(_dbConnection);
			try
			{
				con.Open();
				{
					using SqlCommand cmd = new("SELECT * FROM [Insurance Info]", con);
					cmd.ExecuteNonQuery();
					var dgRow = dgInsInfo.SelectedRows[0];
					{
						txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
						txtInsuranceName.Text = dgRow.Cells[1].Value + string.Empty;
						txtInsCode.Text = dgRow.Cells[2].Value + string.Empty;
						txtInsuranceAdress.Text = dgRow.Cells[3].Value + string.Empty;
						txtPayerID.Text = dgRow.Cells[4].Value + string.Empty;
						txtRemarks.Text = dgRow.Cells[5].Value + string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError($"FillInsuranceInfo", empName, "Insurance", "N/A", ex);

			}
			finally
			{
				con.Close();
			}
		}

		public void InsuranceInfoDBRequest(string request, RadTextBox insID, RadTextBox insuranceName, RadTextBox insCode, RadTextBoxControl address, RadTextBox payerID, RadTextBoxControl remarks, string empName)
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
								UPDATE [Insurance Info] SET [Insurance Name] = @INSURANCENAME
									[Insurance Code] = @INSCODE, [Address] = @ADDRESS,
									[Payer ID] = @PAYERID, REMARKS = @REMARKS
								WHERE
									[Insurance ID] = @INSID",
					"Create" => @"
								INSERT INTO [Insurance Info] ([Insurance ID], [Insurance Name], [MPN],
									[Username], [Password], [Website], [Remarks])
								VALUES
									(@INSID, @INSURANCENAME, @MPN, @USERNAME, @PASSWORD,  @WEBSITE, REMARKS",
					"Delete" => @"DELETE FROM [Insurance Info] WHERE [MPN ID] = @INSID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@INSID", insID.Text);
					cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName.Text);
					cmd.Parameters.AddWithValue("@INSCODE", insCode.Text);
					cmd.Parameters.AddWithValue("@ADDRESS", address.Text);
					cmd.Parameters.AddWithValue("@PAYERID", payerID.Text);
					cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);

				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@INSID", insID.Text);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Insurance ID: {insID.Text}";
				message = $"Done! {insID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} INSURANCE INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError($"InsuranceInfoDBRequest - {request}", empName, "Insurance", insID.Text, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
