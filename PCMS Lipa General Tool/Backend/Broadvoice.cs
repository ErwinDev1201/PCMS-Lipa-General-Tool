﻿using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PCMS_Lipa_General_Tool.Class
{
	public class Broadvoice
	{
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Notification notif = new();
		private static readonly Database db = new();

		public DataTable ViewBroadvoiceList(string empName, out string lblCount)
		{
			var query = "SELECT DISTINCT [BROADVOICE NO.], [Broadvoice Username]," +
				"[Broadvoice Password] FROM [User Information] WHERE [Broadvoice Status] = 'Available' ORDER BY [BROADVOICE NO.]";
			
			var data = new DataTable();
			lblCount = string.Empty;

			try
			{
				using var con = new SqlConnection(_dbConnection);
				using var adp = new SqlDataAdapter(query, con);

				// Fill the DataTable with data from the query
				adp.Fill(data);

				// Calculate the record count
				lblCount = $"Total records: {data.Rows.Count}";
			}
			catch (Exception ex)
			{
				notif.LogError("ViewBroadvoiceList", empName, "Broadvoice", "N/A", ex);
			}

			return data;
		}


	}
}
