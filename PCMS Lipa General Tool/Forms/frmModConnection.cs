﻿using PCMS_Lipa_General_Tool.Services;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModConnection : Telerik.WinControls.UI.RadForm
	{
		readonly SecurityEncryption secEnc = new();
		private static readonly Notification notif = new();
		public string empName;
		public string accessLevel;

		public frmModConnection()
		{
			InitializeComponent();
			LoadFiles();
		}

		private void LoadFiles()
		{
			AutoDecrpyt();
			txtConfiguration.Text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + Assembly.GetExecutingAssembly().GetName().Name + ".exe.config");
		}

		private void AutoDecrpyt()
		{
			try
			{
				string path = AppDomain.CurrentDomain.BaseDirectory + Assembly.GetExecutingAssembly().GetName().Name + ".exe";
				secEnc.DecryptConfigSection(path);
			}
			catch (Exception ex)
			{
				notif.LogError("AutoDecrpyt", empName, "frmModConnection", null, ex);
			} 

		}

		private void AutoEncrypt()
		{
			try
			{
				string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".exe");
				secEnc.EncryptConfigSection(configPath);
			}
			catch (Exception ex)
			{
				notif.LogError("AutoEncrypt", empName, "frmModConnection", null, ex);
			}

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + Assembly.GetExecutingAssembly().GetName().Name + ".exe.config", txtConfiguration.Text);
				AutoEncrypt();
				RadMessageBox.Show("Database Configuration File is Saved", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
			}
			catch (Exception ex)
			{
				notif.LogError("btnSave_Click", empName, "frmModConnection", null, ex);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnCancel_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}