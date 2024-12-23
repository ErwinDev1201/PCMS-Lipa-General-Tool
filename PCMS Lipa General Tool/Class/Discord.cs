using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace PCMS_Lipa_General_Tool.Class
{
	public class Discord
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();

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
				fe.SendToastNotifDesktop("Discord Password successfully updated", "Success");
			}
			catch (Exception ex)
			{
				error.LogError("UpdateValues", empName, "CommonTask", "N/A", ex);
			}
		}
	}
}
