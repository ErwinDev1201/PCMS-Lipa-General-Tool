using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Telerik.WinControls.UI;
using System.IO;



namespace PCMS_Lipa_General_Tool.Class
{
	public class Pantry
	{
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();
		private readonly emailSender _emailSender = new();


		public void GetDBListID(out string ID, string empName)
		{
			ID = string.Empty;

			string nextSequence = db.GetSequenceNo("PantryListSeq", "PL-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					ID = nextSequence;
				}
			}
			catch (Exception ex)
			{
				notif.LogError("GetDBListID", empName, "Pantry", "N/A", ex);
			}

			//db.GetSequenceNo("textbox", "PantryListSeq", txtIntID.Text, null, "PL-");
		}


		public string GetRecipientEmail(string empName)
		{
			return Environment.MachineName == "ERWIN-PC"
				? (empName == "Erwin Alcantara" ? "yourmeeappnoreply@gmail.com" : "edimson@yopmail.com")
				: (empName == "Erwin Alcantara" ? "yourmeeappnoreply@gmail.com" : "edimson@pcmsbilling.net");
		}

		public bool SendEmailWithAttachment(string mailContent, string filePath, string mailSubject, string recipientEmail)
		{
			try
			{
				if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
					throw new FileNotFoundException("Attachment file not found.", filePath);

				//_emailSender.SendEmail("yesAttach", mailContent, filePath, mailSubject, recipientEmail, "TM Pantry Store");
				_emailSender.SendEmail(recipientEmail,mailSubject, mailContent, null, "TM Pantry Store", filePath);
				return true;
			}
			catch (Exception ex)
			{
				notif.LogError("SendEmailWithAttachment", "System", "BackendService", null, ex);
				return false;
			}
		}

		public string FillItemPrice(string cmbProduct, string empName)
		{
			try
			{
				var query = $"SELECT [Product Name], PRICE from [Pantry Product] WHERE [Product Name]= '{cmbProduct}'";
				using SqlConnection con = new(_dbConnection);
				using SqlCommand cmd = new(query, con);
				con.Open();
				using SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read()) // If data exists
				{
					return reader.GetValue(1).ToString();
				}
			}
			catch (Exception ex)
			{
				notif.LogError("FillItemPrice", empName, "Pantry", "N/A", ex);
			}
			return string.Empty; // Return empty string if no result or error occurs
		}

		public void CheckProductExist(string txtName, string empName, out string message)
		{
			using SqlConnection con = new(_dbConnection);
			try
			{
				string query = $"SELECT [Product Name] FROM [Pantry Product] WHERE [Product Name] = @ProductName";
				con.Open();
				using SqlCommand cmd = new(query, con);
				cmd.Parameters.AddWithValue("@ProductName", txtName.Trim());

				using SqlDataAdapter adapter = new(cmd);
				DataTable dt = new();
				adapter.Fill(dt);

				if (dt.Rows.Count >= 1)
				{
					message = "Item already exists";
				}
				else
				{
					message = null;
				}	
			}
			catch (Exception ex)
			{
				notif.LogError("CheckProductExist", empName, "Pantry", txtName, ex);
				message = "\"An error occurred while checking the product";
			}
		}

		public bool PantryProductDBRequest(
			string request,
			string itemID,
			string itemName,
			string itemPrice,
			string itemRemarks,
			string empName,
			out string message)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				using SqlCommand cmd = new()
				{
					Connection = conn,
					CommandText = request switch
					{
						"Update" => @"UPDATE [Pantry Product] 
                                SET [Product Name] = @ITEMNAME, 
                                    PRICE = @ITEMPRICE, 
                                    REMARKS = @ITEMREMARKS
                                WHERE [Product ID] = @ITEMID",

						"Create" => @"INSERT INTO [Pantry Product] ([Product ID], [Product Name], PRICE, REMARKS)
                                VALUES (@ITEMID, @ITEMNAME, @ITEMPRICE, @ITEMREMARKS)",

						"Delete" => @"DELETE FROM [Pantry Product]
                                WHERE [Product ID] = @ITEMID",

						_ => throw new ArgumentException("Invalid request type."),
					}
				};

				cmd.Parameters.AddWithValue("@ITEMID", itemID.Trim());

				if (!string.Equals(request, "Delete", StringComparison.OrdinalIgnoreCase))
				{
					cmd.Parameters.AddWithValue("@ITEMNAME", itemName.Trim());

					if (decimal.TryParse(itemPrice.Trim(), out decimal price))
					{
						cmd.Parameters.AddWithValue("@ITEMPRICE", price);
					}
					else
					{
						throw new FormatException("Invalid price format. Please enter a numeric value.");
					}

					cmd.Parameters.AddWithValue("@ITEMREMARKS", itemRemarks.Trim());
				}

				int affectedRows = cmd.ExecuteNonQuery();
				if (affectedRows > 0)
				{
					string logs = $"{empName} {request.ToLower()}d Product ID: {itemID}";
					message = $"Done! Product ID: {itemID} has been successfully {request.ToLower()}d.";

					log.AddActivityLog(message, empName, logs, $"{request.ToUpper()}D PRODUCT LIST INFORMATION");
					return true;
					//fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					message = $"Failed to {request.ToLower()} {itemID}, Please try again later";
					return false;
					//throw new InvalidOperationException("No rows were affected by the operation. Please verify the input data.");
				}
			}
			catch (Exception ex)
			{
				notif.LogError($"PantryProductDBRequest - {request}", empName, "Pantry", itemID, ex);
				message = $"Failed to {request.ToLower()} {itemID}, Please try again later";
				return false;
				//throw new InvalidOperationException($"Error during {request} operation: {ex.Message}", ex);
			}
		}

		public DataTable ViewProductList(string empName, out string lblCount)
		{
			const string query = "SELECT * FROM [Pantry Product] ORDER BY [PRODUCT ID] DESC";
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
				notif.LogError("ViewAttorneyList", empName, "CommonTask", "N/A", ex);
			}

			return data;
		}

		public void ViewPantryList(RadGridView dataGrid, string filter, RadLabel lblCount, string empName, string employeeNameFilter, DateTime fromDate, DateTime toDate)
		{
			string query = filter == "withFilter"
				? BuildPantryListQuery(employeeNameFilter, fromDate, toDate)
				: $"SELECT * FROM [Pantry Listahan] WHERE [Employee Name] LIKE '%{employeeNameFilter}%' AND [DATE] BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}' ORDER BY [List ID] DESC";

			try
			{
				using var connection = new SqlConnection(_dbConnection);
				using var adapter = new SqlDataAdapter(query, connection);
				var dataTable = new DataTable();

				// Populate data table
				adapter.Fill(dataTable);

				// Bind data to RadGridView
				dataGrid.DataSource = dataTable.DefaultView;

				// Update record count label
				lblCount.Text = $"Total records: {dataTable.Rows.Count}";

				// Optimize column visibility
				dataGrid.BestFitColumns(BestFitColumnMode.DisplayedCells);
			}
			catch (Exception ex)
			{
				// Log errors for debugging and monitoring
				notif.LogError("ViewPantryList", empName, "Pantry", "N/A", ex);
			}
		}

		public List<string> GetProductList(string empName)
		{
			var query = "SELECT DISTINCT [Product Name] from [Pantry Product] ORDER BY [Product Name] DESC";
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
				notif.LogError("FillComboDropdown", empName, "Pantry", "N/A", ex);
			}
			return items;
		}

		private string BuildPantryListQuery(string employeeName, DateTime fromDate, DateTime toDate)
		{
			var queryBuilder = new StringBuilder();

			queryBuilder.Append($"SELECT * FROM [Pantry Listahan] WHERE [DATE] BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'");

			// Apply employee name filter if provided
			if (!string.IsNullOrWhiteSpace(employeeName) && employeeName != "Edimson Escalona")
			{
				queryBuilder.Append($" AND [Employee Name] LIKE '%{employeeName}%'");
			}

			queryBuilder.Append(" ORDER BY [List ID] DESC");

			return queryBuilder.ToString();
		}


		public bool PantryListDBRequest(
			string request,
			string itemID,
			string empName,
			string product,
			int quantity,
			decimal itemPrice,
			decimal totalPrice,
			string summary,
			string remarks,
			string authorName,
			out string message)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs;

				// Determine SQL command based on the request type
				cmd.CommandText = request switch
				{
					"Update" => @"UPDATE [Pantry Listahan] SET [Employee Name] = @EMPNAME, PRODUCT = @PRODUCT,
                            QUANTITY = @QUANTITY, PRICE = @PRICE,
                            [TOTAL PRICE] = @TOTALPRICE, SUMMARY = @SUMMARY, REMARKS = @REMARKS
                          WHERE
                            [List ID] = @ITEMID",
					"Create" => @"INSERT INTO [Pantry Listahan] ([List ID], DATE, [Time Stamp], [Employee Name],
                            PRODUCT, QUANTITY, PRICE, [TOTAL PRICE], SUMMARY, REMARKS)
                          VALUES
                            (@ITEMID, @DATEINSERT, @TIMESTAMP, @EMPNAME, @PRODUCT, @QUANTITY, @PRICE, @TOTALPRICE, @SUMMARY, @REMARKS)",
					"Delete" => @"DELETE FROM [Pantry Listahan]
                          WHERE
                            [List ID] = @ITEMID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters for Update and Create
				if (request != "Delete")
				{
					var currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy-MM-dd");
					var currentTimeStamp = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy-MM-dd hh:mm tt");

					cmd.Parameters.AddWithValue("@DATEINSERT", currentDate);
					cmd.Parameters.AddWithValue("@TIMESTAMP", currentTimeStamp);
					cmd.Parameters.AddWithValue("@EMPNAME", empName ?? string.Empty);
					cmd.Parameters.AddWithValue("@PRODUCT", product ?? string.Empty);
					cmd.Parameters.AddWithValue("@QUANTITY", quantity);
					cmd.Parameters.AddWithValue("@PRICE", itemPrice);
					cmd.Parameters.AddWithValue("@TOTALPRICE", totalPrice);
					cmd.Parameters.AddWithValue("@SUMMARY", summary ?? string.Empty);
					cmd.Parameters.AddWithValue("@REMARKS", remarks ?? string.Empty);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@ITEMID", itemID ?? string.Empty);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Pantry List ID: {itemID}";
				message = $"Done! {itemID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, authorName, logs, $"{request.ToUpper()}D PANTRY LIST INFORMATION");
				return true;
				
			}
			catch (Exception ex)
			{
				notif.LogError("PantryListDBRequest", authorName, "Pantry", itemID, ex);
				message = $"Failed to {request.ToLower()} {itemID}, Please try again later";
				return false;
				//RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}


		//public void PantryListDBRequest(string request, RadTextBox itemID, RadDropDownList empName, RadDropDownList product, RadTextBox quantity, RadTextBox itemPrice, RadTextBoxControl totalPrice, RadTextBoxControl summary, RadTextBoxControl remarks, string authorName)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		conn.Open();
		//		SqlCommand cmd = new()
		//		{
		//			Connection = conn
		//		};
		//
		//		string logs, message;
		//
		//		cmd.CommandText = request switch
		//		{
		//			"Update" => @"UPDATE [Pantry Listahan] SET [Employee Name] = @EMPNAME, PRODUCT = @PRODUCT,
		//							QUANTITY = @QUANTITY, PRICE = @PRICE,
		//							[TOTAL PRICE] = @TOTALPRICE, SUMMARY = @SUMMARY, REMARKS = @REMARKS
		//						WHERE
		//							[List ID] = @ITEMID",
		//			"Create" => @"INSERT INTO [Pantry Listahan] ([List ID], DATE, [Time Stamp], [Employee Name],
		//							PRODUCT, QUANTITY, PRICE, [TOTAL PRICE], SUMMARY, REMARKS)
		//						VALUES
		//							(@ITEMID, @DATEINSERT, @TIMESTAMP, @EMPNAME, @PRODUCT, @QUANTITY, @PRICE, @TOTALPRICE, @SUMMARY, @REMARKS)",
		//			"Delete" => @"DELETE FROM [Pantry Listahan]
		//						WHERE
		//							[List ID] = @ITEMID",
		//			_ => throw new ArgumentException("Invalid request type."),
		//		};
		//
		//		// Add parameters common to Patch and Create
		//		if (request != "Delete")
		//		{
		//
		//			var currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy-MM-dd");
		//			var currentTimeStamp = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy-MM-dd hh:mm tt");  // date with time
		//
		//			cmd.Parameters.AddWithValue("@DATEINSERT", currentDate);
		//			cmd.Parameters.AddWithValue("@TIMESTAMP", currentTimeStamp);
		//			cmd.Parameters.AddWithValue("@EMPNAME", empName.Text);
		//			cmd.Parameters.AddWithValue("@PRODUCT", product.Text);
		//			cmd.Parameters.AddWithValue("@QUANTITY", quantity.Text);
		//			if (decimal.TryParse(itemPrice.Text, out decimal price))
		//			{
		//				cmd.Parameters.AddWithValue("@PRICE", price);
		//			}
		//			else
		//			{
		//				throw new FormatException("Invalid price format. Please enter a numeric value.");
		//			}
		//			if (decimal.TryParse(totalPrice.Text, out decimal tprice))
		//			{
		//				cmd.Parameters.AddWithValue("@TOTALPRICE", tprice);
		//			}
		//			else
		//			{
		//				throw new FormatException("Invalid price format. Please enter a numeric value.");
		//			}
		//			cmd.Parameters.AddWithValue("@SUMMARY", summary.Text);
		//			cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
		//		}
		//
		//		// Common parameter for all requests
		//		cmd.Parameters.AddWithValue("@ITEMID", itemID.Text);
		//
		//		// Execute query
		//		cmd.ExecuteNonQuery();
		//
		//		// Log activity
		//		logs = $"{empName.Text} {request.ToLower()}d Pantry List ID: {itemID.Text}";
		//		message = $"Done! {itemID.Text} has been successfully {request.ToLower()}d.";
		//		log.AddActivityLog(message, authorName, logs, $"{request.ToUpper()}D PANTRY LIST INFORMATION");
		//		fe.SendToastNotifDesktop(message, "Success");
		//	}
		//	catch (Exception ex)
		//	{
		//		notif.LogError("PantryListDBRequest", authorName, "Pantry", itemID.Text, ex);
		//		RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
		//	}
		//	finally
		//	{
		//		conn.Close();
		//	}
		//}
		//

		public void FillPanindaniTmCombo(string query, RadDropDownList pantryList, string empName)
		{
			try
			{
				using SqlConnection con = new(_dbConnection);
				using SqlCommand cmd = new(query, con);
				con.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string paninda = reader.IsDBNull(0) ? null : reader.GetString(0);
						pantryList.Items.Add(paninda);
					}
				}
				con.Close();
			}
			catch (Exception ex)
			{
				notif.LogError($"FillPanindaniTmCombo", empName, "Pantry", "N/A", ex);
			}
		}

		public void CalculateTotalPantryExpense(string employeeName, DateTime startDate, DateTime endDate, out decimal totalAmount)
		{
			var con = new SqlConnection(_dbConnection);
			try
			{
				// Build query dynamically based on conditions
				string query = string.IsNullOrWhiteSpace(employeeName) || employeeName == "Edimson Escalona"
					? $"SELECT SUM([TOTAL PRICE]) FROM [Pantry Listahan] WHERE DATE BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'"
					: $"SELECT SUM([TOTAL PRICE]) FROM [Pantry Listahan] WHERE [Employee Name] LIKE '%{employeeName}%' AND DATE BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'";

				con.Open();
				using SqlCommand cmd = new(query, con);
				using SqlDataReader reader = cmd.ExecuteReader();

				if (reader.Read() && reader.GetValue(0) != DBNull.Value)
				{
					totalAmount = reader.GetDecimal(0);
				}
				else
				{
					totalAmount = 0;
				}

				Console.WriteLine($"Total List is ₱{totalAmount:0.00}");
			}
			catch (Exception ex)
			{
				notif.LogError("CalculateTotalPantryExpense", "System", "frmPantry", null, ex);
				totalAmount = 0;
				throw;
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open)
					con.Close();
			}
		}

		public DataTable SearchData(
	string searchTerm,
	out string searchCount,
	string empName)
		{
			DataTable resultTable = new();

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				string query = $@"
SELECT *
FROM [Pantry Product]
WHERE [Product Name] LIKE @searchTerm
OR [Remarks] LIKE @searchTerm";

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				notif.LogError("SearchData", empName, "Adjuster", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}


		//public void CalculateTotalPantryExpense(string employeeName, DateTime startDate, DateTime endDate, out decimal totalAmount)
		//{
		//	try
		//	{
		//		string query;
		//
		//		if (string.IsNullOrWhiteSpace(employeeName) || employeeName == "Edimson Escalona")
		//		{
		//			query = $"SELECT SUM([TOTAL PRICE]) FROM [Pantry Listahan] WHERE DATE BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'";
		//		}
		//		else
		//		{
		//			query = $"SELECT SUM([TOTAL PRICE]) FROM [Pantry Listahan] WHERE [Employee Name] LIKE '%{employeeName}%' AND DATE BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'";
		//		}
		//
		//		totalAmount = SumTotalAmount(query);
		//		Console.WriteLine($"Total List is ₱{totalAmount:0.00}");
		//	}
		//	catch (Exception ex)
		//	{
		//		notif.LogError("SumPantryExpense", "System", "frmPantry", null, ex);
		//		totalAmount = 0;
		//		throw;
		//	}
		//}
		//
		//
		//public void SumTotalAmount(string query, string totalPrice)
		//{
		//	//int Price;
		//	var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		con.Open();
		//		SqlCommand cmd = new(query, con);
		//		SqlDataReader reader = cmd.ExecuteReader();
		//		while (reader.Read())
		//		{
		//			totalPrice = reader.GetValue(0).ToString();
		//		}
		//		con.Close();
		//
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		notif.LogError($"SumTotalAmount", "N/A", "Pantry", "N/A", ex);
		//	}
		//}

	}
}
