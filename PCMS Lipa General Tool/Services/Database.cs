using System.Data.SqlClient;
using System.Data;
using System;
using System.Configuration;

namespace PCMS_Lipa_General_Tool.Services
{
	public class Database
	{
		private readonly string _dbConnection;
		private static readonly Notification notif = new();
		private static readonly Lazy<string> _machineName = new(() => Environment.MachineName);
		private static readonly Lazy<string> _homeServerPath = new(() => ConfigurationManager.AppSettings["homeserverpath"]);
		private static readonly Lazy<string> _officeServerPath = new(() => ConfigurationManager.AppSettings["serverpath"]);
		private static readonly Lazy<string> _agingDbHome = new(() => ConfigurationManager.AppSettings["agingdbhome"]);
		private static readonly Lazy<string> _agingDbOffice = new(() => ConfigurationManager.AppSettings["agingdboffice"]);

		readonly SqlConnection cn;

		public Database(string connectionString = null)
		{
			// Use GetDbConnection to handle conditional logic
			_dbConnection = connectionString ?? GetDbConnection();
			cn = new SqlConnection(_dbConnection);
		}

		public bool IsConnected
		{
			get
			{
				try
				{
					if (cn.State != ConnectionState.Open)
					{
						cn.Open();
					}
					return true;
				}
				catch (Exception ex)
				{
					// Log error and return false
					notif.LogError("IsConnected", null, "Database", null, ex); // Assuming Error has a Log method
					return false;
				}
			}
		}

		//public string GetDbConnection(bool forAging = false)
		//{
		//	try
		//	{
		//		bool isHome = _machineName.Value.Equals("ERWIN-PC", StringComparison.OrdinalIgnoreCase);
		//		return forAging
		//			? (isHome ? _agingDbHome.Value : _agingDbOffice.Value)
		//			: (isHome ? _homeServerPath.Value : _officeServerPath.Value);
		//	}
		//	catch (ConfigurationErrorsException ex)
		//	{
		//		// Log error if necessary
		//		throw new ApplicationException("Error retrieving database configuration.", ex);
		//	}
		//}
		public string GetDbConnection()
		{
			string machineName = Environment.MachineName;
		
			return machineName == "ERWIN-PC"
				? ConfigurationManager.AppSettings["homeserverpath"]
				: ConfigurationManager.AppSettings["serverpath"];
		}

		public string GetAgingDbConnection()
		{
			string machineName = Environment.MachineName;
			return machineName == "ERWIN-PC"
				? ConfigurationManager.AppSettings["agingdbhome"]
				: ConfigurationManager.AppSettings["agingdboffice"];
		}

		// Dispose the SqlConnection when done
		public void CloseConnection()
		{
			if (cn.State == ConnectionState.Open)
			{
				cn.Close();
			}
		}

		//private string _dbConnection;
		//
		//public Database(string connectionString = null)
		//{
		//	_dbConnection = connectionString ?? ConfigurationManager.AppSettings["serverpath"];
		//	cn = new SqlConnection(_dbConnection);
		//}
		//
		//public bool IsConnected
		//{
		//	get
		//	{
		//		if (cn.State != ConnectionState.Open)
		//		{
		//			cn.Open();
		//		}
		//		return true;
		//	}
		//}
		//
		//public string GetDbConnection()
		//{
		//	string machineName = Environment.MachineName;
		//
		//	_dbConnection = machineName == "Erwin-PC"
		//		? ConfigurationManager.AppSettings["homeserverpath"]
		//		: ConfigurationManager.AppSettings["serverpath"];
		//
		//	return _dbConnection;
		//}

		public void AlterDBSequence(string sequence, string databaseTable, string empName)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				using SqlCommand cmd = new("ALTER SEQUENCE " + databaseTable + " RESTART WITH " + sequence, conn);
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				notif.LogError($"AlterDBSequence", empName, "Database", "N/A", ex);
			}
			finally
			{
				conn.Close();
			}
		}

		public int GetSequenceNoPre(string cmbTable)
		{
			int currSeq = 0; // Default value in case of an error

			using SqlConnection con = new(_dbConnection);
			try
			{
				con.Open();
				using SqlCommand cmd = new($"SELECT current_value FROM sys.sequences WHERE name = '{cmbTable}'", con);
				using SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					currSeq = reader.GetInt32(0);
				}
			}
			catch (Exception ex)
			{
				notif.LogError("GetSequenceNoPre", "N/A", "Database", "N/A", ex);
			}

			return currSeq; // Return the sequence number
		}


		public string GetSequenceNo(string sequenceName, string preID)
		{
			var query = "SELECT NEXT VALUE FOR " + sequenceName;
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlCommand cmd = new(query, con);
				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					int currSeq = reader.GetInt32(0);
					string result = preID + (currSeq + 1).ToString();

					// Return the formatted sequence string
					return result;
				}

				// If no sequence value is found, return null or an empty string
				return null;
			}
			catch (Exception ex)
			{
				notif.LogError("GetSequenceNo", "N/A", "Database", "N/A", ex);
				return null; // Return null in case of an error
			}
			finally
			{
				con.Close();
			}
		}
	}
}
