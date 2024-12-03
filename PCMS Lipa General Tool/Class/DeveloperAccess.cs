using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMS_Lipa_General_Tool.Class
{
	public class DeveloperAccess
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		readonly CommonTask task = new();


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
				task.SendToastNotifDesktop("Password successfully updated");
			}
			catch (Exception ex)
			{
				task.LogError("UpdateDevPasswordAccess", empName, "DeveloperAccess", "N/A", ex);
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
				task.LogError("FillComboDropdown", empName, "CommonTask", "N/A", ex);
			}
			return items;
		}

	}
}
