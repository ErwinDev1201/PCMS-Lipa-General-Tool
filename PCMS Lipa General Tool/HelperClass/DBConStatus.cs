using System.Data.SqlClient;
using System.Data;

namespace PCMS_Lipa_General_Tool.HelperClass
{
	public class DBConStatus
	{
		readonly SqlConnection cn;
		public DBConStatus(string connectionString)
		{
			cn = new SqlConnection(connectionString);
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
	}
}
