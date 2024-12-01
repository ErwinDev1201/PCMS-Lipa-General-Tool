using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using YourmeeAppLibrary.Security;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModConnection : Telerik.WinControls.UI.RadForm
	{
		private readonly string NameofUser = UserPrincipal.Current.DisplayName;
		readonly SecurityEncryption secEnc = new();
		readonly CommonTask task = new();
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
				task.LogError("AutoDecrpyt", empName, "frmModConnection", null, ex);
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
				task.LogError("AutoEncrypt", empName, "frmModConnection", null, ex);
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
				task.LogError("btnSave_Click", empName, "frmModConnection", null, ex);
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