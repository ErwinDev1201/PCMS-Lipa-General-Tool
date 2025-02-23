using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace PCMS_Lipa_General_Tool.Class
{
	public class DeveloperAccess
	{

		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly FEWinForm fe = new();
		private static readonly Database db = new();
		private static readonly Notification notif = new();


		public void UpdateDevPasswordAccess(string empName, string password)
		{
			string query = "UPDATE [User Information] SET DeveloperAccess = @Password WHERE USERNAME = @Username";

			try
			{
				using (var con = new SqlConnection(_dbConnection))
				{
					con.Open();
					using (var command = new SqlCommand(query, con))
					{
						// Use parameterized queries to prevent SQL Injection
						command.Parameters.AddWithValue("@Password", password);
						command.Parameters.AddWithValue("@Username", "Erwin"); // Make configurable if needed

						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							fe.SendToastNotifDesktop("Password successfully updated", "Success");
						}
						else
						{
							fe.SendToastNotifDesktop("No records updated. Please check username.", "Warning");
						}
					}
				}
			}
			catch (SqlException sqlEx)
			{
				notif.LogError("UpdateDevPasswordAccess", empName, "DeveloperAccess", "N/A", sqlEx);
				throw new InvalidOperationException("A database error occurred while updating the password. Please try again later.");
			}
			catch (Exception ex)
			{
				notif.LogError("UpdateDevPasswordAccess", empName, "DeveloperAccess", "N/A", ex);
				throw new InvalidOperationException("An unexpected error occurred. Please try again.");
			}
		}



		public List<string> FillComboDropdown(string empName)
		{
			var query = "select name from [PCMS LIPA GENERAL TOOL].sys.sequences";
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
				notif.LogError("FillComboDropdown", empName, "CommonTask", "N/A", ex);
			}
			return items;
		}

	}
}
