using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;




namespace PCMS_Lipa_General_Tool.Class
{
	public class Provider
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		private readonly CommonTask task = new();

		public DataTable ViewProviderAssignee(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Provider Collector]";
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
				task.LogError("ViewProviderAssignee", empName, "Provider", "N/A", ex);
			}

			return data;
		}

		public DataTable ViewProviderList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Provider Information]";
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
				task.LogError("ViewProviderList", empName, "Provider", "N/A", ex);
			}

			return data;
		}

		public void FillUpProvTxtBox(string query, RadGridView dgproviderInfo, RadTextBox txtIntProvId, RadTextBox txtProviderName, RadTextBox txtNPiNO, RadTextBox txtPTANNo, RadTextBox txtTaxIDNo, RadTextBox txtPalPTAN, RadTextBox txtPhysicalAdd, RadTextBox txtBillAdd, RadTextBox txtRemarks, string empName)
		{
			using var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				using SqlCommand cmd = new(query, con);
				cmd.ExecuteNonQuery();
				if (dgproviderInfo.SelectedRows.Count > 0)
				{
					var row = dgproviderInfo.SelectedRows[0];
					{
						//txtID = (row["NO"]).ToString();
						txtIntProvId.Text = row.Cells[0].Value + string.Empty;
						txtProviderName.Text = row.Cells[1].Value + string.Empty;
						txtNPiNO.Text = row.Cells[2].Value + string.Empty;
						txtPTANNo.Text = row.Cells[3].Value + string.Empty;
						txtTaxIDNo.Text = row.Cells[4].Value + string.Empty;
						txtPalPTAN.Text = row.Cells[5].Value + string.Empty;
						txtPhysicalAdd.Text = row.Cells[6].Value + string.Empty;
						txtBillAdd.Text = row.Cells[7].Value + string.Empty;
						txtRemarks.Text = row.Cells[8].Value + string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError("FillUpProvTxtBox", empName, "Provider", "N/A", ex);
			}
			finally
			{
				con.Close();
			}

		}

		public void ProviderInfoDBRequest(string request, RadTextBox provID, RadTextBox providerName, RadTextBox npiNo, RadTextBox ptanNo, RadTextBox taxID, RadTextBox railroadPTAN, RadTextBox physicalAddress, RadTextBox billingAddress, RadTextBox remarks, string empName) 
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
					"Update" => @"UPDATE [Provider Information] SET [Provider Name] = @PROVIDERNAME, [NPI] = @NPI, [PTAN] = @PTAN, [Tax ID] = @TAXID,
									[RailRoad PTAN] = @RRPTAN, [Physical Address] = @PHYSICALADD, [Billing Address] = @BILLINGADD, REMARKS = @REMARKS
								WHERE
									[Provider ID] = @PROVIDERID",
					"Create" => @"INSERT INTO [Provider Information] ([Provider ID], [Provider Name],  [NPI], [PTAN], [Tax ID], [RailRoad PTAN],
								[Physical Address], [Billing Address], [Remarks])
								VALUES (@PROVIDERID, @PROVIDERNAME, @NPI, @PTAN, @TAXID, @RRPTAN, @PHYSICALADD, @BILLINGADD, @REMARKS)",
					"Delete" => @"DELETE FROM [Provider Information]
								WHERE [Provider ID] = @PROVIDERID.",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@PROVIDERNAME", providerName.Text);
					cmd.Parameters.AddWithValue("@NPI", npiNo.Text);
					cmd.Parameters.AddWithValue("@PTAN", ptanNo.Text);
					cmd.Parameters.AddWithValue("@TAXID", taxID.Text);
					cmd.Parameters.AddWithValue("@RRPTAN", railroadPTAN.Text);
					cmd.Parameters.AddWithValue("@PHYSICALADD", physicalAddress.Text);
					cmd.Parameters.AddWithValue("@BILLINGADD", billingAddress.Text);
					cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);

				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@PROVIDERID", provID.Text);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Provider ID: {provID.Text}";
				message = $"Done! {provID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} PROVIDER INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError("ProviderInfoDBRequest", empName, "Provider", "N/A", ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}


		public void AssignProvider(
	string requestType,
	string assignID,
	string providerName,
	string employeeName,
	string remarks,
	string empName)
		{
			// Generate activity message and logs
			var message = GenerateActivityMessage(requestType, assignID, providerName, employeeName, null, null, remarks);
			var logs = $"{empName} {requestType.ToLower()}ed patientName ID: {assignID}";
			string sqlCommandText;

			// Determine SQL command based on the request type
			switch (requestType)
			{
				case "Patch":
					sqlCommandText = @"UPDATE [Provider Collector] 
                               SET [Provider Name] = @providerName, 
                                   [Employee Name] = @employeeName, 
                                   REMARKS = @REMARKS 
                               WHERE [Assigned ID] = @assignID";
					break;

				case "Create":
					sqlCommandText = @"INSERT INTO [Provider Collector] 
                               ([Assigned ID], [Provider Name], [Employee Name], [Remarks]) 
                               VALUES (@assignID, @providerName, @employeeName, @REMARKS)";
					break;

				case "Delete":
					sqlCommandText = @"DELETE FROM [Provider Collector] 
                               WHERE [Assigned ID] = @assignID";
					break;

				default:
					MessageBox.Show("Invalid request type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
			}

			// Execute the database request
			ExecuteAssignProviderRequest(sqlCommandText, requestType, assignID, providerName, employeeName, remarks, empName, message, logs);
		}

		private void ExecuteAssignProviderRequest(string sqlCommandText, string requestType, string assignID, string providerName, string employeeName, string remarks, string empName, string message, string logs)
		{
			using var conn = new SqlConnection(_dbConnection);
			try
			{
				conn.Open();
				using (var cmd = new SqlCommand(sqlCommandText, conn))
				{
					cmd.Parameters.AddWithValue("@assignID", assignID);
					cmd.Parameters.AddWithValue("@providerName", providerName);
					cmd.Parameters.AddWithValue("@employeeName", employeeName);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);

					cmd.ExecuteNonQuery();
				}

				task.AddActivityLog(message, empName, logs, $"{requestType.ToUpper()} patientName INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError("ExecuteAssignProviderRequest", empName, "Provider", "N/A", ex);
				RadMessageBox.Show($"Failed to {requestType.ToLower()} record for patientName ID: {assignID}", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private string GenerateActivityMessage(string requestType, string noteID, string providerName, string chartNo, string patientName, string Notes, string remarks)
		{
			return $"I {requestType.ToLower()}ed a record. Check the details below:\n\npatientName ID: {noteID}\nICD-10: {providerName}\nICD-9: {chartNo}\npatientName: {patientName}\nBody Parts: {Notes}\nRemarks: {remarks}";
		}

		public void FillupAssignProvider(RadGridView dgAssignProvider, RadTextBox txtAssignID, RadDropDownList cmbProviderName, RadDropDownList cmbEmployeeName, RadTextBoxControl txtRemarks, string empName)
		{
			var query = "SELECT * FROM [Provider Collector]";
			using var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				using SqlCommand cmd = new(query, con);
				cmd.ExecuteNonQuery();
				var row = dgAssignProvider.SelectedRows[0];
				if (dgAssignProvider.SelectedRows.Count > 0)
				{
					{
						txtAssignID.Text = row.Cells[0].Value + string.Empty;
						cmbEmployeeName.Text = row.Cells[1].Value + string.Empty;
						cmbProviderName.Text = row.Cells[2].Value + string.Empty;
						txtRemarks.Text = row.Cells[3].Value + string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError("GenerateActivityMessage", empName, "Provider", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}

		public List<string> GetProviderList(string empName)
		{
			var query = "SELECT [Provider Name] FROM [Provider Information]";
			var items = new List<string>();
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlCommand cmd = new(query, con);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					items.Add(reader.GetString(0));
				}
				con.Close();
			}
			catch (Exception ex)
			{
				task.LogError("GetProviderList", empName, "Pantry", "N/A", ex);
			}
			return items;
		}


		public List<string> GetProviderListperCollector(string empName)
		{
			var query = $"SELECT [Provider Name] FROM [Provider Collector] WHERE [Employee Name] = '{empName}'";
			var items = new List<string>();
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlCommand cmd = new(query, con);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					items.Add(reader.GetString(0));
				}
				con.Close();
			}
			catch (Exception ex)
			{
				task.LogError("GetProviderListperCollector", empName, "Pantry", "N/A", ex);
			}
			return items;
		}
	}
}
