using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMS_Lipa_General_Tool.Class
{
	public class Discord
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		readonly CommonTask task = new();

		public void UpdateDCPassword(string userName, string password, string empName)
		{
			var query = $"UPDATE [User Information] SET [Discord Password]='{password}' WHERE [Discord Username]='{userName}'";
			try
			{
				using (var con = new SqlConnection(_dbConnection))
				{
					con.Open();
					using var command = new SqlCommand(query, con);
					command.ExecuteNonQuery();
				}
				//RadMessageBox.Show("Theme successfully updated", "Information", MessageBoxButtons.OK, RadMessageIcon.Info);
				task.SendToastNotifDesktop("Discord Password successfully updated");
			}
			catch (Exception ex)
			{
				task.LogError("UpdateValues", empName, "CommonTask", "N/A", ex);
			}
		}
	}
}
