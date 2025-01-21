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
		private static readonly Error error = new();	


		public void UpdateDevPasswordAccess(string empName, string password)
		{
			var query = "UPDATE [User Information] SET DeveloperAccess = '" + password + "' WHERE USERNAME = 'Erwin'";
		
			try
			{
				using (var con = new SqlConnection(_dbConnection))
				{
					con.Open();
					using var command = new SqlCommand(query, con);
					command.ExecuteNonQuery();
				}
				//RadMessageBox.Show("Theme successfully updated", "Information", MessageBoxButtons.OK, RadMessageIcon.Info);
				fe.SendToastNotifDesktop("Password successfully updated", "Success");
			}
			catch (Exception ex)
			{
				error.LogError("UpdateDevPasswordAccess", empName, "DeveloperAccess", "N/A", ex);
				throw new InvalidOperationException($"Error during update operation. Please try again later.");
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
				error.LogError("FillComboDropdown", empName, "CommonTask", "N/A", ex);
			}
			return items;
		}

	}
}
