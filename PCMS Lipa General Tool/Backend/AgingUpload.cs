using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace PCMS_Lipa_General_Tool.Backend
{
	public class AgingUpload
	{
		private static readonly Database db = new();
		private readonly string _dbConnection = db.GetAgingDbConnection();
		private static readonly Notification notif = new();

		//public DataTable ReadExcelToDataTable(string filePath)
		//{
		//	string connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
		//	using var excelConn = new OleDbConnection(connString);
		//
		//	try
		//	{
		//		excelConn.Open();
		//
		//		DataTable schemaTable = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
		//		if (schemaTable?.Rows.Count == 0)
		//			throw new InvalidOperationException("No sheets found in Excel file.");
		//
		//		// Get the first visible sheet and clean its name
		//		string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString();
		//
		//		// Ensure name is valid (sheets often include $ or quotes)
		//		sheetName = sheetName?.Trim('\''); // removes leading/trailing '
		//		if (!sheetName.EndsWith("$"))
		//			throw new InvalidOperationException("Excel sheet name is not valid or not a worksheet.");
		//
		//		string query = $"SELECT * FROM [{sheetName}]";
		//		using var cmd = new OleDbCommand(query, excelConn);
		//		using var adapter = new OleDbDataAdapter(cmd);
		//
		//		var dataTable = new DataTable();
		//		adapter.Fill(dataTable);
		//		return dataTable;
		//	}
		//	catch (OleDbException ex)
		//	{
		//		throw new InvalidOperationException("Failed to read Excel file. Ensure ACE.OLEDB.12.0 is installed.", ex);
		//	}
		//	//string connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
		//	//
		//	//using var excelConn = new OleDbConnection(connString);
		//	//excelConn.Open();
		//	//
		//	//DataTable schemaTable = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
		//	//string sheetName = schemaTable?.Rows[0]?["TABLE_NAME"].ToString();
		//	//
		//	//if (string.IsNullOrEmpty(sheetName))
		//	//	throw new InvalidOperationException("Cannot detect sheet name.");
		//	//
		//	//using var cmd = new OleDbCommand($"SELECT * FROM [{sheetName}]", excelConn);
		//	//using var adapter = new OleDbDataAdapter(cmd);
		//	//var dataTable = new DataTable();
		//	//adapter.Fill(dataTable);
		//	//
		//	//return dataTable;
		//}   //

		public DataTable ReadExcelToDataTable(string filePath)
		{
			var dataTable = new DataTable();
			string connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";

			try
			{
				using var excelConn = new OleDbConnection(connString);
				excelConn.Open();

				var schemaTable = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				if (schemaTable?.Rows.Count == 0)
					throw new InvalidOperationException("No sheets found in the Excel file.");

				string sheetName = schemaTable.Rows[0]["TABLE_NAME"]?.ToString()?.Trim('\'');

				if (string.IsNullOrEmpty(sheetName) || !sheetName.EndsWith("$"))
					throw new InvalidOperationException("The Excel sheet name is not valid.");

				string query = $"SELECT * FROM [{sheetName}]";

				using var cmd = new OleDbCommand(query, excelConn);
				using var adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dataTable);
			}
			catch (OleDbException oledbEx)
			{
				// Logging only in backend (optional)
				// logger.LogError(oledbEx, "Error reading Excel data");

				throw; // FE handles the message display
			}
			catch (Exception ex)
			{
				throw; // FE will present this
			}

			return dataTable;
		}



		public void CreateSqlTableFromDataTable(string tableName, DataTable data)
		{
			string safeTableName = $"[dbo].[{tableName.Replace("]", "]]")}]";

			var columnDefs = data.Columns.Cast<DataColumn>().Select(col =>
			{
				string sqlType = col.DataType == typeof(int) ? "INT" :
								 col.DataType == typeof(double) ? "FLOAT" :
								 col.DataType == typeof(decimal) ? "DECIMAL(18,2)" :
								 col.DataType == typeof(DateTime) ? "DATETIME" :
								 "NVARCHAR(MAX)";
				string safeCol = col.ColumnName.Replace("]", "]]");
				return $"[{safeCol}] {sqlType}";
			});

			string fixedColumns = @"
		[Notes ID] NVARCHAR(50) NULL,
		[Provider] NVARCHAR(MAX) NULL,
		[Date] DATETIME NULL,
		[Time Stamp] DATETIME NULL,
		[Notes] NVARCHAR(MAX) NULL,
		[Remarks] NVARCHAR(MAX) NULL,
		[Collector Name] NVARCHAR(100) NULL,
		[MONTH] NVARCHAR(100) NULL,
		[YEAR] NVARCHAR(4) NULL,
		[UploadedBy] NVARCHAR(100) NULL,
		[UploadDate] DATETIME DEFAULT GETDATE()";

			string checkExistQuery = $@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";

			using var conn = new SqlConnection(_dbConnection);
			using var checkCmd = new SqlCommand(checkExistQuery, conn);
			conn.Open();
			int exists = (int)checkCmd.ExecuteScalar();
			conn.Close();

			if (exists == 0)
			{
				// Now build and create the table
				string createSql = $@"
	CREATE TABLE {safeTableName} (
		[INT ID] INT IDENTITY(1,1) PRIMARY KEY,
		{string.Join(", ", columnDefs)},
		{fixedColumns}
	);";

				using var createConn = new SqlConnection(_dbConnection);
				using var createCmd = new SqlCommand(createSql, createConn);
				createConn.Open();
				createCmd.ExecuteNonQuery();
			}
		}

		//	string createSql = $@"
		//IF OBJECT_ID(N'{safeTableName}', N'U') IS NOT NULL DROP TABLE {safeTableName};
		//CREATE TABLE {safeTableName} (
		//	[INT ID] INT IDENTITY(1,1) PRIMARY KEY,
		//	{string.Join(", ", columnDefs)},
		//	{fixedColumns}
		//);";
		//
		//	try
		//	{
		//		using var conn = new SqlConnection(_dbConnection);
		//		using var cmd = new SqlCommand(createSql, conn);
		//		conn.Open();
		//		cmd.ExecuteNonQuery();
		//	}
		//	catch (SqlException sqlEx)
		//	{
		//		throw new InvalidOperationException($"SQL error during table creation: {sqlEx.Message}", sqlEx);
		//	}
		//}

		public int BulkInsertChunk(DataTable chunk, string tableName, string providerName, string uploadedBy)
		{
			string safeTableName = $"[dbo].[{tableName.Replace("]", "]]")}]";

			// Extend with extra columns if not already added outside
			if (!chunk.Columns.Contains("UploadedBy"))
			{
				chunk.Columns.Add("Provider", typeof(string));
				chunk.Columns.Add("Notes ID", typeof(int));
				chunk.Columns.Add("Date", typeof(DateTime));
				chunk.Columns.Add("Time Stamp", typeof(DateTime));
				chunk.Columns.Add("Notes", typeof(string));
				chunk.Columns.Add("Remarks", typeof(string));
				chunk.Columns.Add("Collector Name", typeof(string));
				chunk.Columns.Add("UploadedBy", typeof(string));
				chunk.Columns.Add("UploadDate", typeof(DateTime));
				chunk.Columns.Add("MONTH", typeof(string));
				chunk.Columns.Add("YEAR", typeof(string));
			}

			foreach (DataRow row in chunk.Rows)
			{
				row["Provider"] = providerName;
				row["Notes ID"] = DBNull.Value;
				row["Date"] = DBNull.Value;
				row["Time Stamp"] = DBNull.Value;
				row["Notes"] = DBNull.Value;
				row["Remarks"] = DBNull.Value;
				row["Collector Name"] = DBNull.Value;
				row["UploadedBy"] = uploadedBy;
				row["UploadDate"] = DateTime.Now;
				row["MONTH"] = DateTime.Now.ToString("MMMM");
				row["YEAR"] = DateTime.Now.ToString("yyyy");
			}

			using var conn = new SqlConnection(_dbConnection);
			using var bulk = new SqlBulkCopy(conn)
			{
				DestinationTableName = safeTableName,
				BatchSize = 1000,
				BulkCopyTimeout = 120
			};

			foreach (DataColumn col in chunk.Columns)
				bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);

			conn.Open();
			bulk.WriteToServer(chunk);

			return chunk.Rows.Count;
		}


		//public void BulkInsertToSqlServer(DataTable originalData, string tableName, string uploadedBy = "System", RadProgressBar progressBar = null)
		//{
		//	string safeTableName = $"[dbo].[{tableName.Replace("]", "]]")}]";
		//	int totalRows = originalData.Rows.Count;
		//
		//	// Extend with extra columns
		//	DataTable data = originalData.Copy();
		//	data.Columns.Add("NotesID", typeof(int));
		//	data.Columns.Add("Date", typeof(DateTime));
		//	data.Columns.Add("TimeStamp", typeof(DateTime));
		//	data.Columns.Add("NotesRemarks", typeof(string));
		//	data.Columns.Add("CollectorName", typeof(string));
		//	data.Columns.Add("UploadedBy", typeof(string));
		//	data.Columns.Add("UploadDate", typeof(DateTime));
		//
		//	foreach (DataRow row in data.Rows)
		//	{
		//		row["NotesID"] = DBNull.Value;
		//		row["Date"] = DBNull.Value;
		//		row["TimeStamp"] = DBNull.Value;
		//		row["NotesRemarks"] = DBNull.Value;
		//		row["CollectorName"] = DBNull.Value;
		//		row["UploadedBy"] = uploadedBy;
		//		row["UploadDate"] = DateTime.Now;
		//	}
		//
		//	try
		//	{
		//		using var conn = new SqlConnection(_dbConnection);
		//		using var bulk = new SqlBulkCopy(conn)
		//		{
		//			DestinationTableName = safeTableName,
		//			BatchSize = 5000,
		//			BulkCopyTimeout = 120
		//		};
		//
		//		foreach (DataColumn col in data.Columns)
		//			bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
		//
		//		conn.Open();
		//
		//		if (progressBar != null)
		//		{
		//			progressBar.Maximum = totalRows;
		//			progressBar.Value1 = 0;
		//			progressBar.Step = 1;
		//		}
		//
		//		// Optionally split insert into chunks
		//		int chunkSize = 1000;
		//		for (int i = 0; i < totalRows; i += chunkSize)
		//		{
		//			var tempTable = data.AsEnumerable().Skip(i).Take(chunkSize).CopyToDataTable();
		//			bulk.WriteToServer(tempTable);
		//
		//			if (progressBar != null)
		//			{
		//				progressBar.Value1 = Math.Min(i + chunkSize, totalRows);
		//				Application.DoEvents(); // allow UI update
		//			}
		//		}
		//
		//		RadMessageBox.Show($"✅ {totalRows} rows inserted into {tableName}", "Upload Complete", MessageBoxButtons.OK, RadMessageIcon.Info);
		//	}
		//	catch (SqlException sqlEx)
		//	{
		//		RadMessageBox.Show($"SQL bulk insert failed: {sqlEx.Message}", "SQL Error", MessageBoxButtons.OK, RadMessageIcon.Error);
		//	}
		//}




		//public void CreateSqlTableFromDataTable(string tableName, DataTable data)
		//{
		//	// Sanitize table name
		//	string safeTableName = $"[dbo].[{tableName.Replace("]", "]]")}]";
		//
		//	var columns = data.Columns.Cast<DataColumn>().Select(col =>
		//	{
		//		string sqlType = col.DataType == typeof(int) ? "INT" :
		//						 col.DataType == typeof(double) ? "FLOAT" :
		//						 col.DataType == typeof(decimal) ? "DECIMAL(18,2)" :
		//						 col.DataType == typeof(DateTime) ? "DATETIME" :
		//						 "NVARCHAR(MAX)";
		//		string safeColName = col.ColumnName.Replace("]", "]]");
		//		return $"[{safeColName}] {sqlType}";
		//	});
		//
		//	string createSql = $@"
		//IF OBJECT_ID(N'{safeTableName}', N'U') IS NOT NULL
		//	DROP TABLE {safeTableName};
		//CREATE TABLE {safeTableName} ({string.Join(", ", columns)});";
		//
		//	try
		//	{
		//		using var conn = new SqlConnection(_dbConnection);
		//		using var cmd = new SqlCommand(createSql, conn);
		//		conn.Open();
		//		cmd.ExecuteNonQuery();
		//	}
		//	catch (SqlException sqlEx)
		//	{
		//		// Add logging if needed
		//		throw new InvalidOperationException($"SQL error during table creation: {sqlEx.Message}", sqlEx);
		//	}
		//}
		//
		//public void BulkInsertToSqlServer(DataTable data, string tableName)
		//{
		//	string safeTableName = $"[dbo].[{tableName.Replace("]", "]]")}]";
		//
		//	try
		//	{
		//		using var conn = new SqlConnection(_dbConnection);
		//		using var bulk = new SqlBulkCopy(conn)
		//		{
		//			DestinationTableName = safeTableName,
		//			BatchSize = 5000,
		//			BulkCopyTimeout = 120
		//		};
		//
		//		foreach (DataColumn col in data.Columns)
		//		{
		//			string safeColName = col.ColumnName.Replace("]", "]]");
		//			bulk.ColumnMappings.Add(safeColName, safeColName);
		//		}
		//
		//		conn.Open();
		//		bulk.WriteToServer(data);
		//	}
		//	catch (SqlException sqlEx)
		//	{
		//		throw new InvalidOperationException($"SQL bulk insert failed: {sqlEx.Message}", sqlEx);
		//	}
		//}
		//

		//public void CreateSqlTableFromDataTable(string tableName, DataTable data)
		//{
		//	var columns = data.Columns.Cast<DataColumn>().Select(col =>
		//	{
		//		string sqlType = col.DataType == typeof(int) ? "INT" :
		//						 col.DataType == typeof(double) ? "FLOAT" :
		//						 col.DataType == typeof(decimal) ? "DECIMAL(18,2)" :
		//						 col.DataType == typeof(DateTime) ? "DATETIME" :
		//						 "NVARCHAR(MAX)";
		//		return $"[{col.ColumnName}] {sqlType}";
		//	});
		//
		//	string createSql = $"IF OBJECT_ID('[dbo].[{tableName}]', 'U') IS NOT NULL DROP TABLE [dbo].[{tableName}]; " +
		//					   $"CREATE TABLE [dbo].[{tableName}] ({string.Join(", ", columns)});";
		//
		//	using var conn = new SqlConnection(_dbConnection);
		//	using var cmd = new SqlCommand(createSql, conn);
		//	conn.Open();
		//	cmd.ExecuteNonQuery();
		//}
		//
		//public void BulkInsertToSqlServer(DataTable data, string tableName)
		//{
		//	using var conn = new SqlConnection(_dbConnection);
		//	using var bulk = new SqlBulkCopy(conn)
		//	{
		//		DestinationTableName = tableName,
		//		BatchSize = 5000,
		//		BulkCopyTimeout = 120
		//	};
		//
		//	foreach (DataColumn col in data.Columns)
		//		bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
		//
		//	conn.Open();
		//	bulk.WriteToServer(data);
		//}
	}   //
}
