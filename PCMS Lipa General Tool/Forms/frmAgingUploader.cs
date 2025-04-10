using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System.Data;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmAgingUploader : Telerik.WinControls.UI.RadForm
	{

		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private readonly Database db = new();


		public string _empName;
		public string _accessLevel;


		public frmAgingUploader()
		{
			InitializeComponent();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			RadOpenFileDialog openFileDialog = new()
			{
				Filter = "Excel Files|*.xls;*.xlsx"
				//Title= "Select Excel File"
			};

			if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
			txtFilePath.Text = openFileDialog.FileName;
			txtFilePath.ReadOnly = true;

		}

		private void btnLoadPreview_Click(object sender, EventArgs e)
		{
			var previewForm = new frmAgingExcelPreview
			{
				empName = _empName,
				accessLevel = _accessLevel,
				filePath = txtFilePath.Text
			};
			previewForm.LoadPreviewSheet();
			previewForm.ShowDialog();
		}
	}
}