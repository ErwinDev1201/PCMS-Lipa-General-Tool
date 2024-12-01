﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using YourmeeAppLibrary.Email;
using YourmeeAppLibrary.Security;

namespace PCMS_Lipa_General_Tool.Class
{
	public class Bundle
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		private static readonly CommonTask task = new();

		public DataTable ViewBundleCodes(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Bundle Codes]";
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

		public void FillBundleCodes(RadGridView dgBundleCodetbl, RadTextBox txtIntID, RadTextBox txtCPTCode, RadTextBoxControl txtDescription, RadTextBoxControl txtBundleCodes, RadTextBoxControl txtRemarks, RadRadioButton rdoYes, RadRadioButton rdoNo, string empName)
		{
			using SqlConnection con = new(_dbConnection);
			try
			{
				con.Open();
				{
					string bundleOptions;
					using (SqlCommand cmd = new("SELECT * FROM [Bundle Codes]", con))
					{
						cmd.ExecuteNonQuery();
						var dgRow = dgBundleCodetbl.SelectedRows[0];
						{
							txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
							txtCPTCode.Text = dgRow.Cells[1].Value + string.Empty;
							txtDescription.Text = dgRow.Cells[2].Value + string.Empty;
							bundleOptions = dgRow.Cells[3].Value + string.Empty;
							txtBundleCodes.Text = dgRow.Cells[4].Value + string.Empty;
							txtRemarks.Text = dgRow.Cells[6].Value + string.Empty;
						}
					}
					if (bundleOptions == "Y")
					{
						rdoYes.IsChecked = true;
					}
					else
					{
						rdoNo.IsChecked = true;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError($"FillBundleCodes", empName, "Bundle", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}


		public void BundleCodesDBRequest(string request, RadTextBox treatmentID, RadTextBox cptCode, RadTextBoxControl bundleCodes, RadTextBoxControl Description, string indicator, RadTextBoxControl remarks, string empName)
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

				cmd.CommandText = request switch
				{
					"Update" => @"
								UPDATE [Bundle Codes]
									SET [CPT Code] = @CPTCODE, [Bundle Codes] = @BUNDLECODE,
									[Description] = @DESCRIPTION, [Indicator] = @INDICATOR, 
									REMARKS = @REMARKS
								WHERE
									[Treatment ID] = @TREAMENTID",
					"Create" => @"
									INSERT INTO [Bundle Codes]
									([Treatment ID], [CPT Code], [Bundle Codes], [Description], [Indicator], [Remarks])
									VALUES (@TREAMENTID, @CPTCODE, @BUNDLECODE, @DESCRIPTION, @INDICATOR, @REMARKS)",
					"Delete" => @"DELETE FROM [Bundle Codes] WHERE [Reviewer ID] = @TREAMENTID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@TREAMENTID", treatmentID.Text);
					cmd.Parameters.AddWithValue("@CPTCODE", cptCode.Text);
					cmd.Parameters.AddWithValue("@BUNDLECODE", bundleCodes.Text);
					cmd.Parameters.AddWithValue("@DESCRIPTION", Description.Text);
					cmd.Parameters.AddWithValue("@INDICATOR", indicator);
					cmd.Parameters.AddWithValue("@REMARKS", remarks.Text);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@TREAMENTID", treatmentID.Text);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Treatment ID: {treatmentID.Text}";
				message = $"Done! {treatmentID.Text} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} BUNDLE CODES INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError("BundleCodesDBRequest", empName, "Bundle", treatmentID.Text, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
