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
				task.LogError("ViewAttorneyList", empName, "CommonTask", "N/A", ex);
			}

			return data;
		}



		public void FillAttyEmpInfo(
			RadGridView dgAttytbl,
			RadTextBox txtIntID,
			RadDropDownList cmbAttyType,
			RadTextBox txtAttyName,
			RadTextBox txtPhoneNo,
			RadTextBox txtFax,
			RadTextBox txtemailAdd,
			RadTextBoxControl txtRemarks,
			string empName)
		{
			//const string query = "SELECT * FROM [Attorney Information]";

			try
			{
				using var con = new SqlConnection(_dbConnection);
				con.Open();

				//using var cmd = new SqlCommand(query, con);
				//cmd.ExecuteNonQuery(); // Executes the query (although it's unnecessary in this context).

				if (dgAttytbl.SelectedRows.Count > 0)
				{
					var dgRow = dgAttytbl.SelectedRows[0];

					// Fill the fields from the selected row
					txtIntID.Text = dgRow.Cells[0]?.Value?.ToString() ?? string.Empty;
					cmbAttyType.Text = dgRow.Cells[1]?.Value?.ToString() ?? string.Empty;
					txtAttyName.Text = dgRow.Cells[2]?.Value?.ToString() ?? string.Empty;
					txtPhoneNo.Text = dgRow.Cells[3]?.Value?.ToString() ?? string.Empty;
					txtFax.Text = dgRow.Cells[4]?.Value?.ToString() ?? string.Empty;
					txtemailAdd.Text = dgRow.Cells[5]?.Value?.ToString() ?? string.Empty;
					txtRemarks.Text = dgRow.Cells[6]?.Value?.ToString() ?? string.Empty;
				}
				else
				{
					ClearFields(txtIntID, cmbAttyType, txtAttyName, txtPhoneNo, txtFax, txtemailAdd, txtRemarks);
				}
			}
			catch (Exception ex)
			{
				task.LogError("FillAttyEmpInfo", empName, "Attorney", "N/A", ex);
			}
		}

		private void ClearFields(
			RadTextBox txtIntID,
			RadDropDownList cmbAttyType,
			RadTextBox txtAttyName,
			RadTextBox txtPhoneNo,
			RadTextBox txtFax,
			RadTextBox txtemailAdd,
			RadTextBoxControl txtRemarks)
		{
			txtIntID.Text = string.Empty;
			cmbAttyType.Text = string.Empty;
			txtAttyName.Text = string.Empty;
			txtPhoneNo.Text = string.Empty;
			txtFax.Text = string.Empty;
			txtemailAdd.Text = string.Empty;
			txtRemarks.Text = string.Empty;
		}


		public void AttorneyDBRequest(
			string request,
			RadTextBox attyID, RadDropDownList
			cmbAttyType,
			RadTextBox attyName,
			RadTextBox phoneNo,
			RadTextBox faxNo, RadTextBox email, RadTextBoxControl remarks, string empName)
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
					cmd.Parameters.AddWithValue("@ATTYID", attyID.Text);
					cmd.Parameters.AddWithValue("@ATTYTYPE", cmbAttyType.Text);
					cmd.Parameters.AddWithValue("@ATTYNAME", attyName.Text);
					cmd.Parameters.AddWithValue("@PHONENO", phoneNo.Text);
					cmd.Parameters.AddWithValue("@FAXNO", faxNo.Text);
					cmd.Parameters.AddWithValue("@EMAIL", email.Text);
					cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@ATTYID", attyID.Text);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Atty ID: {attyID.Text}";
				message = $"Done! {attyID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} ATTORNEY INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError($"AttorneyDBRequest {request}", empName, "Attorney", attyID.Text, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
