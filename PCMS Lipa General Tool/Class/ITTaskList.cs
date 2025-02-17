using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMS_Lipa_General_Tool.Class
{
	public class ITTaskList
	{
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();

		public void GetDBID(out string ID, string empName)
		{
			ID = string.Empty;

			string nextSequence = db.GetSequenceNo("ITTaskSeq", "IT-");

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
				notif.LogError("GetDBID", empName, "ITTaskList", "N/A", ex);
			}
			/////db.GetSequenceNo("textbox", "DiagSeq", txtIntID.Text, null, "DX-");
		}

		public DataTable ViewTaskList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [IT Task]";
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
				notif.LogError("ViewTaskList", empName, "ITTaskList", "N/A", ex);
			}

			return data;
		}

		public DataTable SearchData(
			string searchItem,
			string bodyParts,
			out string searchCount, string empName)
		{
			DataTable resultTable = new();

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();

				// Define the base query
				string query = $@"
SELECT *
FROM [Diagnosis]
WHERE Diagnosis LIKE @searchItem";

				// Add the STATUS filter only if statusColumn is not "All"
				if (bodyParts != "All")
				{
					query += " AND [Body Parts] LIKE @bodyParts";
				}

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@bodyParts", $"%{bodyParts}%");

				// Add the @statusSearch parameter only if statusColumn is not "All"
				if (bodyParts == "")
				{
					cmd.Parameters.AddWithValue("@searchItem", $"%{searchItem}%");
				}

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				// Log the error and provide feedback
				notif.LogError("SearchEmpTwoColumnOneFieldText", empName, "CommonTask", "N/A", ex);
				searchCount = "Error occurred while fetching records.";
			}

			return resultTable;
		}

		public bool TaskDBRequest(
			string request,
			string taskID,
			string category,
			string priority,
			string summary,
			string description,
			string assignedTo,
			string status,
			string reporter,
			string createdDate,
			string updatedDate,
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
					"Update" => @"UPDATE [IT Task]
								SET [Category] = @Category, [Priority] = @Priority,
								[Summary] = @Summary, [Description] = @Description, [Assigned To] = @AssignedTo,
`								[Status] = @Status, [Reporter] = @Reporter, [Created Date] = @CreatedDate, [Updated Date] = @UpdatedDate
								WHERE [Task ID] = @TaskID",
					"Create" => @"INSERT INTO [IT Task]
								([Task ID], [Category], [Priority], [Summary], [Description], [Assigned To],
								[Status], [Reporter], [Created Date], [Updated Date])
								VALUES (@TaskID, @Category, @Priority, @Summary, @Description, @AssignedTo,
								@Status, @Reporter, @CreatedDate, @UpdatedDate)",
					"Delete" => @"DELETE FROM [IT Task] WHERE [Task ID] = @TaskID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@Category", category);
					cmd.Parameters.AddWithValue("@Priority", priority);
					cmd.Parameters.AddWithValue("@Summary", summary);
					cmd.Parameters.AddWithValue("@Description", description);
					cmd.Parameters.AddWithValue("@AssignedTo", assignedTo);
					cmd.Parameters.AddWithValue("@Status", status);
					cmd.Parameters.AddWithValue("@Reporter", reporter);
					cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
					cmd.Parameters.AddWithValue("@UpdatedDate", updatedDate);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@TaskID", taskID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Diagnosis ID: {taskID}";
				message = $"Done! {taskID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} IT TASK INFORMATION");
				///fe.SendToastNotifDesktop(message, "Success");
				return true;

			}
			catch (Exception ex)
			{
				notif.LogError($"DiagnosisDBRequest - {request}", empName, "Diagnosis", taskID, ex);
				message = $"Failed to {request.ToLower()} {taskID}, Please try again later";
				return false;
				//RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
