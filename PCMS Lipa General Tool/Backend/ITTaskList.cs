using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

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

		public DataTable ViewTaskList(string empName, out string lblCount, string accessLevel, string taskStatus)
		{
			string query = string.Empty; // Initialize query to avoid "use of unassigned local variable" error
			var data = new DataTable();
			lblCount = string.Empty;

			try
			{
				using var con = new SqlConnection(_dbConnection);
				using var cmd = new SqlCommand();
				cmd.Connection = con;

				// Allow only "Administrator" and "Programmer" to see all tasks
				if (accessLevel == "Administrator" || accessLevel == "Programmer")
				{
					query = "SELECT * FROM [IT Task] WHERE [STATUS] = @Status";
					cmd.Parameters.AddWithValue("@Status", taskStatus);
				}
				else
				{
					query = "SELECT * FROM [IT Task] WHERE [Reporter] = @empName AND [STATUS] = @Status";
					cmd.Parameters.AddWithValue("@empName", empName);
					cmd.Parameters.AddWithValue("@Status", taskStatus);
				}

				cmd.CommandText = query;

				using var adp = new SqlDataAdapter(cmd);
				adp.Fill(data);

				// Format record count
				lblCount = $"Total records: {data.Rows.Count}";
			}
			catch (SqlException sqlEx)
			{
				notif.LogError("SQL Error in ViewTaskList", empName, "ITTaskList", query ?? "Query Unavailable", sqlEx);
			}
			catch (Exception ex)
			{
				notif.LogError("General Error in ViewTaskList", empName, "ITTaskList", "N/A", ex);
			}

			return data;
		}


		public DataTable GetSearch(
	string itemToSearch,
	string statusColumn,
	out string searchCount,
	string empName, string accessLevel)
		{
			DataTable resultTable = new();
			searchCount = "No records found";

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				using SqlCommand cmd = new();
				cmd.Connection = conn;

				// Base Query
				string query = "SELECT * FROM [IT Task]";

				List<string> conditions = new();
				if (accessLevel != "Administrator" && accessLevel != "Programmer")
				{
					conditions.Add("[Assigned To] = @empName");
					cmd.Parameters.Add("@empName", SqlDbType.NVarChar).Value = empName;
				}

				if (!string.IsNullOrEmpty(itemToSearch))
				{
					conditions.Add("(Description LIKE @itemToSearch OR [Summary] LIKE @itemToSearch)");
					cmd.Parameters.Add("@itemToSearch", SqlDbType.NVarChar).Value = $"%{itemToSearch}%";
				}

				if (!string.IsNullOrEmpty(statusColumn) && statusColumn != "All")
				{
					conditions.Add("STATUS = @statusSearch");
					cmd.Parameters.Add("@statusSearch", SqlDbType.NVarChar).Value = statusColumn;
				}

				if (conditions.Count > 0)
				{
					query += " WHERE " + string.Join(" AND ", conditions);
				}

				cmd.CommandText = query;

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Set search count message
				searchCount = resultTable.Rows.Count > 0
					? $"Total records: {resultTable.Rows.Count}"
					: "No records found";
			}
			catch (SqlException sqlEx)
			{
				notif.LogError("GetSearch", empName, "Database", "SQL Error", sqlEx);
				searchCount = "Database error occurred while fetching records.";
			}
			catch (Exception ex)
			{
				notif.LogError("GetSearch", empName, "Application", "General Error", ex);
				searchCount = "Unexpected error occurred.";
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
			string commment,
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
								[Summary] = @Summary, [Description] = @Description, [Comment] = @Comment, [Assigned To] = @AssignedTo,
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
					cmd.Parameters.AddWithValue("@Comment", commment);
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
