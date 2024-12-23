using System.Data.SqlClient;
using System.Data;
using System;
using Telerik.WinControls.UI;
using System.Configuration;

namespace PCMS_Lipa_General_Tool.HelperClass
{
	public class Database
	{
		private readonly string _dbConnection;
		private static readonly Error error = new();

		readonly SqlConnection cn;

		public Database(string connectionString = null)
		{
			_dbConnection = connectionString ?? ConfigurationManager.AppSettings["serverpath"];
			cn = new SqlConnection(_dbConnection);
		}

		public bool IsConnected
		{
			get
			{
				if (cn.State != ConnectionState.Open)
				{
					cn.Open();
				}
				return true;
			}
		}

		public void AlterDBSequence(RadTextBox sequence, RadDropDownList databaseTable, string empName)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				using SqlCommand cmd = new("ALTER SEQUENCE " + databaseTable.Text + " RESTART WITH " + sequence.Text, conn);
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				error.LogError($"AlterDBSequence", empName, "CommonTask", "N/A", ex);
			}
			finally
			{
				conn.Close();
			}
		}

		public void GetSequenceNoPre(string query, RadLabel nextSequence)
		{
			//int Price;
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlCommand cmd = new(query, con);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					int currSeq = reader.GetInt32(0);
					//nextSequence.Text = preID + (currSeq + 1).ToString();
					nextSequence.Text = currSeq.ToString();// + ToString();
				}
				con.Close();


			}
			catch (Exception ex)
			{
				error.LogError($"GetSequenceNoPre", "N/A", "CommonTask", "N/A", ex);
			}
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
				error.LogError("GetSequenceNo", "N/A", "CommonTask", "N/A", ex);
				return null; // Return null in case of an error
			}
			finally
			{
				con.Close();
			}
		}
	}
}
