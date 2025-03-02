using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data.SqlClient;

namespace PCMS_Lipa_General_Tool.Class
{
	public class Discord
	{
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Notification notif = new();
		private static readonly Database db = new();

		public bool UpdateDCPassword(string userName, string password, string empName, out string message)
		{
			message = "";

			// Ensure username and password are provided
			if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
			{
				message = "Username and password cannot be empty.";
				return false;
			}

			string query = "UPDATE [User Information] SET [Discord Password] = @Password WHERE [Discord Username] = @Username";

			try
			{
				using (var con = new SqlConnection(_dbConnection))
				{
					con.Open();
					using (var command = new SqlCommand(query, con))
					{
						// Use parameterized queries to prevent SQL Injection
						command.Parameters.AddWithValue("@Password", password);
						command.Parameters.AddWithValue("@Username", userName);

						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							message = "Discord password successfully updated.";
							return true;
						}
						else
						{
							message = "No records updated. Please check the username.";
							return false;
						}
					}
				}
			}
			catch (SqlException sqlEx)
			{
				notif.LogError("UpdateDCPassword", empName, "CommonTask", "N/A", sqlEx);
				message = "A database error occurred while updating the password.";
				return false;
			}
			catch (Exception ex)
			{
				notif.LogError("UpdateDCPassword", empName, "CommonTask", "N/A", ex);
				message = "An unexpected error occurred. Please try again.";
				return false;
			}
		}

	}
}
