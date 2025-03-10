﻿using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;




namespace PCMS_Lipa_General_Tool.Class
{
	public class Provider
	{
		private readonly string _dbConnection = db.GetDbConnection();

		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();


		public void GetProvID(out string ID, string empName)
		{
			ID = string.Empty;

			string nextSequence = db.GetSequenceNo("ProvInfoSeq", "PROV-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					ID = nextSequence;
					return;
				}
			}
			catch (Exception ex)
			{
				notif.LogError("GetProvID", empName, "Provider", ID, ex);
			}
			//db.GetSequenceNo("textbox", "ProvInfoSeq", txtNoProv.Text, null, "PROV-");
		}

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
				notif.LogError("ViewProviderAssignee", empName, "Provider", "N/A", ex);
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
				notif.LogError("ViewProviderList", empName, "Provider", "N/A", ex);
			}

			return data;
		}


		public bool ProviderInfoDBRequest(
			string request,
			string provID,
			string providerName,
			string npiNo,
			string ptanNo,
			string taxID,
			string railroadPTAN,
			string physicalAddress,
			string billingAddress,
			string remarks,
			string empName,
			out string message) 
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs;

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
								WHERE [Provider ID] = @PROVIDERID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@PROVIDERNAME", providerName);
					cmd.Parameters.AddWithValue("@NPI", npiNo);
					cmd.Parameters.AddWithValue("@PTAN", ptanNo);
					cmd.Parameters.AddWithValue("@TAXID", taxID);
					cmd.Parameters.AddWithValue("@RRPTAN", railroadPTAN);
					cmd.Parameters.AddWithValue("@PHYSICALADD", physicalAddress);
					cmd.Parameters.AddWithValue("@BILLINGADD", billingAddress);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@PROVIDERID", provID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Provider ID: {provID}";
				message = $"Done! {provID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} PROVIDER INFORMATION");
				return true;
			}
			catch (Exception ex)
			{
				notif.LogError("ProviderInfoDBRequest", empName, "Provider", "N/A", ex);
				message = $"Failed to {request.ToLower()} {provID}, Please try again later";
				return false;
				//throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
			}
			finally
			{
				conn.Close();
			}
		}


		public bool AssignProvider(
	string request,
	string assignedID,
	string providerName,
	string employeeName,
	string remarks,
	string empName,
	out string message)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};
				string logs;

				cmd.CommandText = request switch
				{
					"Update" => @"
								UPDATE [Provider Collector] 
								SET [Provider Name] = @providerName, 
                                   [Employee Name] = @employeeName, 
                                   REMARKS = @REMARKS 
								WHERE [Assigned ID] = @assignID",
					"Create" => @"
								INSERT INTO [Provider Collector] 
                               ([Assigned ID], [Provider Name], [Employee Name], [Remarks]) 
                               VALUES (@assignID, @providerName, @employeeName, @REMARKS)",
					"Delete" => @"DELETE FROM [Provider Collector] 
                               WHERE [Assigned ID] = @assignID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					///cmd.Parameters.AddWithValue("@REPID", repID);
					//cmd.Parameters.AddWithValue("@assignID", assignedID);
					cmd.Parameters.AddWithValue("@providerName", providerName);
					cmd.Parameters.AddWithValue("@employeeName", employeeName);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@assignID", assignedID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				message = GenerateActivityMessage(request, assignedID, providerName, employeeName, remarks);
				logs = $"{empName} {request.ToLower()}ed assignment ID: {assignedID}"; ;
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} HEARING REP INFORMATION");
				return true;
				//fe.SendToastNotifDesktop(message, "Success");
			}
			catch (Exception ex)
			{
				notif.LogError($"HRRepDBRequest - {request}", empName, "Provider", assignedID, ex);
				message = $"Failed to {request.ToLower()} {assignedID}, Please try again later";
				return false;
				//throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
			}
			finally
			{
				conn.Close();
			}
		}

		private string GenerateActivityMessage(string requestType, string providerID, string providerName, string employeeName, string remarks)
		{
			return $@"
I {requestType.ToLower()}ed a record.
Check the details below:
{employeeName} is assigned to {providerName} ({providerID})
Additional Remarks: {remarks}";
		}

		public DataTable SearchData(
			string searchTerm,
			out string searchCount,
			string empName)
		{
			DataTable resultTable = new();

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				string query = $@"
					SELECT *
						FROM [Provider Information]
					WHERE
						[Provider Name]
					LIKE @searchTerm
					OR [Remarks]
					LIKE @searchTerm";

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				notif.LogError("SearchData", empName, "Adjuster", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
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
				notif.LogError("GetProviderList", empName, "Pantry", "N/A", ex);
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
				notif.LogError("GetProviderListperCollector", empName, "Pantry", "N/A", ex);
			}
			return items;
		}
	}
}
