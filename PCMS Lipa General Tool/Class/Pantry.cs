using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;



namespace PCMS_Lipa_General_Tool.Class
{
	public class Pantry
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private readonly CommonTask task = new();
	


		public void FillUpPantryListField(string query, RadGridView dataGrid, RadDropDownList cmbProducts, RadTextBox quantity, RadTextBox price, RadTextBox Remarks, RadTextBox TransactionNo, RadTextBox Summary, RadTextBox totalPrices, string empName)
		{
			using var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				using (SqlCommand cmd = new(query, con))
				{
					cmd.ExecuteNonQuery();
					if (dataGrid.SelectedRows.Count > 0)
					{
						var row = dataGrid.SelectedRows[0];
						{
							cmbProducts.Text = row.Cells[0].Value + string.Empty;
							quantity.Text = row.Cells[1].Value + string.Empty;
							price.Text = row.Cells[2].Value + string.Empty;
							Remarks.Text = row.Cells[3].Value + string.Empty;
							TransactionNo.Text = row.Cells[4].Value + string.Empty;
							Summary.Text = row.Cells[5].Value + string.Empty;
							totalPrices.Text = row.Cells[6].Value + string.Empty;
						}
					}
				};
			}
			catch (Exception ex)
			{
				task.LogError("FillUpPantryListField", empName, "Pantry", "N/A", ex);

			}
			finally
			{
				con.Close();
			}
		}

		public void PantryAutoFillUp(RadTextBox intID, RadDropDownList Name, RadDropDownList productName, RadTextBox price, RadTextBox quantity, RadTextBox totalPrice, RadTextBox summary, RadTextBox remarks, RadGridView dgPantryList,
			RadButton addItem, RadButton cancel, RadButton removeItem, RadButton newItem, RadDateTimePicker dtpTo, RadDateTimePicker dtpFrom, RadGroupBox grpItems, string permission, string empName)
		{
			try
			{
				if (permission == "Admin")
				{
					grpItems.Enabled = true;
					price.ReadOnly = false;
					Name.Enabled = false;
					productName.Enabled = true;
					productName.Enabled = true;
					productName.Visible = true;
					productName.ReadOnly = false;
					price.Enabled = true;
					quantity.Enabled = true;
					totalPrice.Enabled = true;
					summary.Enabled = true;
					remarks.Enabled = true;
					addItem.Enabled = false;
					cancel.Visible = true;
					cancel.Enabled = true;
					newItem.Enabled = false;
					removeItem.Enabled = true;
					removeItem.Enabled = true;
					addItem.Text = "Update";
					removeItem.Visible = true;
					addItem.Visible = true;
					dtpFrom.Visible = true;
					dtpTo.Visible = true;

					if (dgPantryList.SelectedRows.Count > 0)
					{
						intID.Text = dgPantryList.SelectedRows[0].Cells[0].Value + string.Empty;
						Name.Text = dgPantryList.SelectedRows[0].Cells[3].Value + string.Empty;
						productName.Text = dgPantryList.SelectedRows[0].Cells[4].Value + string.Empty;
						price.Text = dgPantryList.SelectedRows[0].Cells[6].Value + string.Empty;
						quantity.Text = dgPantryList.SelectedRows[0].Cells[5].Value + string.Empty;
						totalPrice.Text = dgPantryList.SelectedRows[0].Cells[7].Value + string.Empty;
						summary.Text = dgPantryList.SelectedRows[0].Cells[8].Value + string.Empty;
						remarks.Text = dgPantryList.SelectedRows[0].Cells[9].Value + string.Empty;
					}
				}
				else
				{
					grpItems.Enabled = true;
					price.ReadOnly = true;
					productName.ReadOnly = true;
					quantity.ReadOnly = true;
					remarks.ReadOnly = true;
					summary.ReadOnly = true;
					totalPrice.ReadOnly = true;
					Name.Enabled = false;
					productName.Enabled = true;
					price.Enabled = true;
					quantity.Enabled = true;
					totalPrice.Enabled = true;
					summary.Enabled = true;
					remarks.Enabled = true;
					addItem.Enabled = false;
					cancel.Visible = true;
					cancel.Enabled = true;
					cancel.Width = 366;
					cancel.Location = new System.Drawing.Point(22, 467);
					newItem.Enabled = false;
					remarks.Height = 94;
					addItem.Text = "Update";
					removeItem.Visible = false;
					addItem.Visible = false;
					dtpFrom.Visible = true;
					dtpTo.Visible = true;

					if (dgPantryList.SelectedRows.Count > 0)
					{
						intID.Text = dgPantryList.SelectedRows[0].Cells[0].Value + string.Empty;
						Name.Text = dgPantryList.SelectedRows[0].Cells[3].Value + string.Empty;
						productName.Text = dgPantryList.SelectedRows[0].Cells[4].Value + string.Empty;
						price.Text = dgPantryList.SelectedRows[0].Cells[6].Value + string.Empty;
						quantity.Text = dgPantryList.SelectedRows[0].Cells[5].Value + string.Empty;
						totalPrice.Text = dgPantryList.SelectedRows[0].Cells[7].Value + string.Empty;
						summary.Text = dgPantryList.SelectedRows[0].Cells[8].Value + string.Empty;
						remarks.Text = dgPantryList.SelectedRows[0].Cells[9].Value + string.Empty;
					}
				}


			}
			catch (Exception ex)
			{
				task.LogError("PantryAutoFillUp", empName, "Pantry", "N/A", ex);
			}
		}

		public void FillItemPrice(RadTextBox priceTag, RadDropDownList cmbProduct, string empName)
		{
			try
			{
				// if (string.IsNullOrWhiteSpace(cmbProduct.Text))
				// {
				// 	throw new ArgumentException("Product selection cannot be empty.");
				// }
				// 
				// 
				var query = $"SELECT [Product Name], PRICE from [Pantry Product] WHERE [Product Name]= '{cmbProduct.Text}'";
				using SqlConnection con = new(_dbConnection);
				using SqlCommand cmd = new(query, con);
				con.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						priceTag.Text = reader.GetValue(1).ToString();
					}
				}
				con.Close();
			}
			catch (Exception ex)
			{
				task.LogError("FillItemPrice", empName, "Pantry", "N/A", ex);
			}
		}

		public void FillProductInfo(RadGridView dgPantry, RadTextBox txtIntID, RadTextBox txtProductName, RadTextBox txtPrice, RadTextBoxControl txtRemarks, string empName)
		{
			using SqlConnection con = new(_dbConnection);
			try
			{
				con.Open();
				{
					using SqlCommand cmd = new("SELECT * FROM [Pantry Product]", con);
					cmd.ExecuteNonQuery();
					var dgRow = dgPantry.SelectedRows[0];
					{
						txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
						txtProductName.Text = dgRow.Cells[1].Value + string.Empty;
						txtPrice.Text = dgRow.Cells[2].Value + string.Empty;
						txtRemarks.Text = dgRow.Cells[3].Value + string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError("FillProductInfo", empName, "Pantry", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}

		public void CheckProductExist(string query, RadTextBox txtIntID, RadTextBox txtName, RadTextBox txtPrice, RadTextBoxControl txtRemarks, string empName)
		{
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlDataAdapter adapter = new(query, con);
				DataTable dt = new();
				adapter.Fill(dt);
				if (dt.Rows.Count >= 1)
				{
					RadMessageBox.Show("Item already exist", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
				}
				else
				{
					PantryProductDBRequest("Create", txtIntID, txtName, txtPrice, txtRemarks, empName);
				}
				con.Close();
			}
			catch (Exception ex)
			{

				task.LogError("FillProductInfo", empName, "Pantry", txtIntID.Text, ex);
			}
		}

		public void PantryProductDBRequest(string request, RadTextBox itemID, RadTextBox itemName, RadTextBox itemPrice, RadTextBoxControl itemRemarks, string empName)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs, message;

				// Prepare query based on request type
				cmd.CommandText = request switch
				{
					"Update" => @"UPDATE [Pantry Product] 
                          SET [Product Name] = @ITEMNAME, PRICE = @ITEMPRICE, [REMARKS] = @ITEMREMARKS
                          WHERE [Product ID] = @ITEMID",
					"Create" => @"INSERT INTO [Pantry Product] ([Product ID], [Product Name], PRICE, REMARKS)
                          VALUES (@ITEMID, @ITEMNAME, @ITEMPRICE, @ITEMREMARKS)",
					"Delete" => @"DELETE FROM [Pantry Product]
                          WHERE [PRODUCT ID] = @ITEMID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters
				cmd.Parameters.AddWithValue("@ITEMID", itemID.Text);

				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@ITEMNAME", itemName.Text);
					if (decimal.TryParse(itemPrice.Text, out decimal price))
					{
						cmd.Parameters.AddWithValue("@ITEMPRICE", price);
					}
					else
					{
						throw new FormatException("Invalid price format. Please enter a numeric value.");
					}
					//cmd.Parameters.AddWithValue("@ITEMPRICE", itemPrice.Text);
					cmd.Parameters.AddWithValue("@ITEMREMARKS", itemRemarks.Text);
				}

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Product ID: {itemID.Text}";
				message = $"Done! {itemID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()}D PRODUCT LIST INFORMATION");
				task.SendToastNotifDesktop(message);
			}
			catch (Exception ex)
			{
				task.LogError($"PantryProductDBRequest - {request}", empName, "Pantry", itemID.Text, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
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
				task.LogError("ViewAttorneyList", empName, "CommonTask", "N/A", ex);
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
				dataGrid.BestFitColumns(BestFitColumnMode.AllCells);
			}
			catch (Exception ex)
			{
				// Log errors for debugging and monitoring
				task.LogError("ViewPantryList", empName, "Pantry", "N/A", ex);
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
				task.LogError("FillComboDropdown", empName, "Pantry", "N/A", ex);
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


		//public void ViewPantryList(RadGridView dataGrid, string filter, RadLabel lblCount, string empName, string employeNameOrde, DateTime fromDate, DateTime toDate)
		//{
		//	if (filter == "withFilter")
		//	{
		//		string query = BuildPantryListQuery(employeNameOrde, fromDate, toDate);
		//		try
		//		{
		//			using var con = new SqlConnection(_dbConnection);
		//			using var adp = new SqlDataAdapter(query, con);
		//			var data = new DataTable();
		//
		//			adp.Fill(data);
		//			dataGrid.DataSource = data.DefaultView;
		//
		//			lblCount.Text = $"Total records: {data.Rows.Count}";
		//
		//			// Optimize column width for better visibility
		//			dataGrid.BestFitColumns(BestFitColumnMode.AllCells);
		//		}
		//		catch (Exception ex)
		//		{
		//			task.LogError("ViewDatagrid", empName, "Pantry", "N/A", ex);
		//		}
		//	}
		//	else
		//	{
		//		string query = $"SELECT * FROM [Pantry Listahan] WHERE [Employee Name] LIKE '%{employeNameOrde}%' AND [DATE] BETWEEN '{fromDate}' AND '{toDate}' order by [List ID] DESC";
		//		try
		//		{
		//			using var con = new SqlConnection(_dbConnection);
		//			using var adp = new SqlDataAdapter(query, con);
		//			var data = new DataTable();
		//
		//			adp.Fill(data);
		//			dataGrid.DataSource = data.DefaultView;
		//
		//			lblCount.Text = $"Total records: {data.Rows.Count}";
		//
		//			// Optimize column width for better visibility
		//			dataGrid.BestFitColumns(BestFitColumnMode.AllCells);
		//		}
		//		catch (Exception ex)
		//		{
		//			task.LogError("ViewDatagrid", empName, "Pantry", "N/A", ex);
		//		}
		//	}
		//
		//}
		//

		//public string BuildPantryListQuery(string employeeName, DateTime fromDate, DateTime toDate)
		//{
		//	string baseQuery = $"SELECT * FROM [Pantry Listahan] WHERE [DATE] BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'";
		//
		//	// Add employee name filter if applicable
		//	if (!string.IsNullOrWhiteSpace(employeeName) && employeeName != "Edimson Escalona")
		//	{
		//		baseQuery += $" AND [Employee Name] LIKE '%{employeeName}%'";
		//	}
		//
		//	return baseQuery + " ORDER BY [List ID] DESC";
		//}
		//

		//public void PantryProductDBRequest(string request, RadTextBox itemID, RadTextBox itemName, RadTextBox itemPrice, RadTextBoxControl itemRemarks, string empName)
		//{
		//	//var queryadd = $"INSERT INTO [Pantry Product]([PRODUCT ID], NAME, PRICE, REMARKS) VALUES ('{txtIntID.Text}','{txtName.Text}','{txtPrice.Text}','{txtRemarks.Text}')";
		//	using (SqlConnection conn = new SqlConnection(_dbConnection))
		//	{
		//
		//		switch (request)
		//		{
		//			case "Patch":
		//				{
		//					string message = String.Format("I made an update for {1} . Check the details below." +
		//						"\n\n\nItem ID: {0}\nProduct Name: {1}\nProduct Price: {2}\nRemarks: {3}",
		//						itemID.Text,
		//						itemName.Text,
		//						itemPrice.Text,
		//						itemRemarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("UPDATE [Pantry Product] SET [Product Name] = @ITEMNAME, PRICE = @ITEMPRICE, [REMARKS] = @ITEMREMARKS" +
		//							" WHERE [Product ID] = @ITEMID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@ITEMID", itemID.Text);
		//							cmd.Parameters.AddWithValue("@ITEMNAME", itemName.Text);
		//							//cmd.Parameters.AddWithValue("@ITEMPRICE", Convert.ToDecimal(itemPrice.Text));
		//							cmd.Parameters.AddWithValue("@ITEMPRICE", itemPrice.Text);
		//							cmd.Parameters.AddWithValue("@ITEMREMARKS", itemRemarks.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						string logs = empName + " updated information for Product ID: " + itemID.Text;
		//						task.AddActivityLog(message, empName, logs, "UPDATED PRODUCT INFORMATION");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						task.SendToastNotifDesktop(logs);
		//						//RadMessageBox.Show("Record successfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						task.LogError($"PantryProductDBRequest - {request}", empName, "Pantry", itemID.Text, ex);
		//						RadMessageBox.Show(ErrorMessageUpdate + itemID.Text, "Failed to Update", MessageBoxButtons.OK, RadMessageIcon.Error);
		//					}
		//					finally
		//					{
		//						conn.Close();
		//					}
		//
		//					break;
		//				}
		//
		//			case "Create":
		//				{
		//					string message = String.Format("I added {1} in the records. Check the details below." +
		//						"\n\n\nItem ID: {0}\nProduct Name: {1}\nProduct Price: {2}\nRemarks: {3}",
		//						itemID.Text,
		//						itemName.Text,
		//						itemPrice.Text,
		//						itemRemarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("INSERT INTO [Pantry Product] ([Product Name], PRICE, REMARKS)" +
		//							"VALUES (@ITEMNAME, @ITEMPRICE, @ITEMREMARKS)", conn))
		//						{
		//							//cmd.Parameters.AddWithValue("@ITEMID", itemID.Text);
		//							cmd.Parameters.AddWithValue("@ITEMNAME", itemName.Text);
		//							cmd.Parameters.AddWithValue("@ITEMPRICE", itemPrice.Text);
		//							cmd.Parameters.AddWithValue("@ITEMREMARKS", itemRemarks.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						string logs = empName + " added Product ID: " + itemID.Text;
		//						task.AddActivityLog(message, empName, logs, "ADDED PRODUCT INFORMATION");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						task.SendToastNotifDesktop(logs);
		//						//RadMessageBox.Show("Record successfully Added", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//
		//					catch (Exception ex)
		//					{
		//						task.LogError($"PantryProductDBRequest - {request}", empName, "Pantry", itemID.Text, ex);
		//						RadMessageBox.Show(ErrorMessageCreate + itemID.Text + "Please try again later", "Failed to Create", MessageBoxButtons.OK, RadMessageIcon.Error);
		//					}
		//					finally
		//					{
		//						conn.Close();
		//					}
		//
		//					break;
		//				}
		//
		//			case "Delete":
		//				{
		//					string message = String.Format("I removed {1} in the records. Check the details below." +
		//						"\n\n\nItem ID: {0}\nProduct Name: {1}\nProduct Price: {2}\nRemarks: {3}",
		//						itemID.Text,
		//						itemName.Text,
		//						itemPrice.Text,
		//						itemRemarks.Text);
		//					try
		//					{
		//						conn.Open();
		//						using (SqlCommand cmd = new SqlCommand("DELETE FROM [Pantry Product] WHERE [PRODUCT ID] = @ITEMID", conn))
		//						{
		//							cmd.Parameters.AddWithValue("@ITEMID", itemID.Text);
		//							cmd.ExecuteNonQuery();
		//						}
		//						string logs = empName + " deleted Product ID: " + itemID.Text;
		//						task.AddActivityLog(message, empName, logs, "DELETED PRODUCT INFORMATION");
		//						winDiscordAPI.PublishtoDiscord(Global.AppLogger, "", logs, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		//						task.SendToastNotifDesktop(logs);
		//						//RadMessageBox.Show("Record successfully Deleted", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
		//					}
		//					catch (Exception ex)
		//					{
		//						task.LogError($"PantryProductDBRequest - {request}", empName, "Pantry", itemID.Text, ex);
		//						RadMessageBox.Show(ErrorMessageDelete + itemID.Text, "Failed to Delete", MessageBoxButtons.OK, RadMessageIcon.Error);
		//					}
		//					finally
		//					{
		//						conn.Close();
		//					}
		//
		//					break;
		//				}
		//		}
		//	}
		//}
		//

		public void PantryListDBRequest(
	string request,
	string itemID,
	string empName,
	string product,
	int quantity,
	decimal itemPrice,
	decimal totalPrice,
	string summary,
	string remarks,
	string authorName)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs, message;

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
				task.AddActivityLog(message, authorName, logs, $"{request.ToUpper()}D PANTRY LIST INFORMATION");
				task.SendToastNotifDesktop(message);
			}
			catch (Exception ex)
			{
				task.LogError("PantryListDBRequest", authorName, "Pantry", itemID, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
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
		//		task.AddActivityLog(message, authorName, logs, $"{request.ToUpper()}D PANTRY LIST INFORMATION");
		//		task.SendToastNotifDesktop(message);
		//	}
		//	catch (Exception ex)
		//	{
		//		task.LogError("PantryListDBRequest", authorName, "Pantry", itemID.Text, ex);
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
				task.LogError($"FillPanindaniTmCombo", empName, "Pantry", "N/A", ex);
			}
		}



		public void SumTotalAmount(string query, RadTextBoxControl totalPrice)
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
					totalPrice.Text = reader.GetValue(0).ToString();
				}
				con.Close();


			}
			catch (Exception ex)
			{
				task.LogError($"SumTotalAmount", "N/A", "Pantry", "N/A", ex);
			}
		}

	}
}
