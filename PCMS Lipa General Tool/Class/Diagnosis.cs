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
	public class Diagnosis
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private readonly CommonTask task = new();

		public DataTable ViewDxList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Diagnosis]";
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
				task.LogError("ViewDxList", empName, "CommonTask", "N/A", ex);
			}

			return data;
		}

		public void FillBillDiagnosisInfo(RadGridView dgBilDxInfotbl, RadTextBox txtIntID, RadTextBox txtICD10, RadTextBox txtICD9, RadTextBox txtDiagnosis, RadTextBox txtBodyPart, RadTextBoxControl txtRemarks, string empName)
		{
			using SqlConnection con = new(_dbConnection);
			try
			{
				con.Open();
				{
					using SqlCommand cmd = new("SELECT * FROM [Diagnosis]", con);
					cmd.ExecuteNonQuery();
					var dgRow = dgBilDxInfotbl.SelectedRows[0];
					{
						txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
						txtICD10.Text = dgRow.Cells[1].Value + string.Empty;
						txtICD9.Text = dgRow.Cells[2].Value + string.Empty;
						txtDiagnosis.Text = dgRow.Cells[3].Value + string.Empty;
						txtBodyPart.Text = dgRow.Cells[4].Value + string.Empty;
						txtRemarks.Text = dgRow.Cells[5].Value + string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError($"FillBillDiagnosisInfo", empName, "Diagnosis", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}

		public void DiagnosisDBRequest(string request, RadTextBox dxID, RadTextBox icd10, RadTextBox icd9, RadTextBox Diagnosis, RadTextBox BodyParts, RadTextBoxControl remarks, string empName)
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
					"Update" => @"UPDATE [Diagnosis]
								SET [ICD-10] = @ICD10, [ICD-9] = @ICD9, [Diagnosis] = @DIAGNOSIS,
								[Body Parts] = @BODYPARTS, REMARKS = @REMARKS 
								WHERE
								[Diagnosis ID] = @DXID",
					"Create" => @"INSERT INTO [Diagnosis]
								([Diagnosis ID], [ICD-10], [ICD-9], [Diagnosis], [Body Parts], [Remarks])
								VALUES ( @DXID, @ICD10, @ICD9, @DIAGNOSIS, @BODYPARTS, @REMARKS)",
					"Delete" => @"DELETE FROM [Diagnosis] WHERE [Diagnosis ID] = @DXID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@DXID", dxID.Text);
					cmd.Parameters.AddWithValue("@ICD10", icd10.Text);
					cmd.Parameters.AddWithValue("@ICD9", icd9.Text);
					cmd.Parameters.AddWithValue("@DIAGNOSIS", Diagnosis.Text);
					cmd.Parameters.AddWithValue("@BODYPARTS", BodyParts.Text);
					cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@DXID", dxID.Text);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Diagnosis ID: {dxID.Text}";
				message = $"Done! {dxID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} DIAGNOSIS INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError($"DiagnosisDBRequest - {request}", empName, "Diagnosis", dxID.Text, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
