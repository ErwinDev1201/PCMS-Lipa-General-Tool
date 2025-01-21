using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmDemoTool : Telerik.WinControls.UI.RadForm
	{
		private static readonly string privSupport = ConfigurationManager.AppSettings["privsupportpath"];
		//private readonly MailSender mailSender = new MailSender();
		private readonly string _DemoGen = privSupport + @"\Demo_GenReminder.rtf";

		private readonly string _personalreminderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PersonalReminder.rtf";
		private readonly Leave leave = new();
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();
		private readonly User user = new();
		private readonly OfficeFiles office = new();

		public string EmpName;
		public string accessLevel;
		public string userName;
		public string officeLoc;
		public string employeeID;
		public string employmentStat;
		public string _dbConnection;


		public frmDemoTool()
		{
			InitializeComponent();
			PopulateTelerikThemes();
			rdoUpper.IsChecked = true;
		}

		private void mnuEmpInfo_Click(object sender, EventArgs e)
		{
			var empInfo = new frmEmployeeInfo
			{
				EmpName = EmpName,
				Text = "Employee Information"
			};
			empInfo.ShowDialog();
		}

		private void mnuLogout_Click(object sender, EventArgs e)
		{
			Hide();
			var login = new FrmLogin();
			login.Show();
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure want to exit?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				Application.Exit();
			}
		}

		private void mnuDiagnosis_Click(object sender, EventArgs e)
		{
			var dlgDiagnosis = new frmDiagnosis();
			if (accessLevel == "User")
			{
				dlgDiagnosis.Text = "Diagnosis";
				dlgDiagnosis.btnNew.Visible = false;
			}
			dlgDiagnosis.ShowDialog();
		}

		private void mnuBundleCodes_Click(object sender, EventArgs e)
		{
			var dlgBundleCodes = new frmBundlecodes();
			if (accessLevel == "User")
			{
				dlgBundleCodes.btnNew.Visible = false;
				dlgBundleCodes.accessLevel = accessLevel;
				dlgBundleCodes.EmpName = EmpName;
			}
			else
			{
				dlgBundleCodes.accessLevel = accessLevel;
				dlgBundleCodes.EmpName = EmpName;

			}
			dlgBundleCodes.Text = "Procedure Bundle Codes";
			dlgBundleCodes.ShowDialog();
		}

		private void mnuProviderInfo_Click(object sender, EventArgs e)
		{
			var dlgProvider = new frmProvider
			{
				EmpName = EmpName,
				accessLevel = accessLevel,
				Text = "Provider Information"
			};
			dlgProvider.ShowDialog();

		}

		private void mnuICD10_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.icd10data.com/ICD10CM/Codes/");
		}

		private void CopyAllMyText()
		{
			try
			{
				if (txtInputText.Text.Length > 0)
				{
					Clipboard.SetText(txtOutputText.Text);
				}
			}
			catch (Exception ex)
			{
				RadMessageBox.Show(ex.ToString());
			}

			//try
			//{
			//	if (txtOutputText.SelectionLength == 0)
			//	{
			//		txtOutputText.SelectAll();
			//	}
			//	txtOutputText.Copy();
			//}
			//catch
			//{
			//	Process[] procs = Process.GetProcessesByName("rdpclip.exe");
			//	foreach (Process p in procs) { p.Kill(); }
			//
			//	// pcms gen tool v05.1.3
			//	RetryCopy();
			//}

		}

		//private void RetryCopy()
		//{
		//	try
		//	{
		//		if (txtOutputText.SelectionLength == 0)
		//		{
		//			txtOutputText.SelectAll();
		//		}
		//		txtOutputText.Copy();
		//	}
		//	catch (Exception ex)
		//	{
		//		var ErrorMessage = ex.Message + "\n\n Name: " + EmpName + "\n Module: frmDemoTool \n Process: CopyAllMyText \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//
		//}
		//
		private void txtInputText_TextChanged(object sender, EventArgs e)
		{
			ProcessInputText();
		}


		private void ProcessInputText()
		{
			try
			{
				string processedText = txtInputText.Text;

				if (rdoUpper.IsChecked == true)
				{
					processedText = processedText.ToUpper();
				}
				else if (rdoLower.IsChecked == true)
				{
					processedText = processedText.ToLower();
				}
				else if (rdoTitle.IsChecked == true)
				{
					var textInfo = new CultureInfo("en-US", false).TextInfo;
					processedText = textInfo.ToTitleCase(processedText.ToLower());
				}

				processedText = ReplaceCommonAbbreviations(processedText);
				processedText = Regex.Replace(processedText, @"[^\w\s]", "").Trim(); // Remove special characters
				processedText = Regex.Replace(processedText, @"\s{2,}", " "); // Replace multiple spaces with a single space

				txtOutputText.Text = processedText;
				CopyAllMyText();
			}
			catch (Exception ex)
			{
				error.LogError("ProcessInputText", EmpName,"frmDemoTool", null, ex);
			}
		}

		/// <summary>
		/// Replaces common long-form words with their abbreviations.
		/// </summary>
		/// <param name="input">The input string.</param>
		/// <returns>The processed string with abbreviations.</returns>
		private string ReplaceCommonAbbreviations(string input)
		{
			return input
				.Replace(" # ", " APT ")
				.Replace(" BOULEVARD ", " BLVD ")
				.Replace(" AVENUE ", " AVE ")
				.Replace(" CIRCLE ", " CIR ")
				.Replace(" DRIVE ", " DR ")
				.Replace(" LANE ", " LN ")
				.Replace(" ROAD ", " RD ")
				.Replace(" ROUTE ", " RTE ")
				.Replace(" STREET ", " ST ")
				.Replace(" SPACE ", " SPC ")
				.Replace(" HIGHWAY ", " HWY ")
				.Replace(" COURT ", " CT ")
				.Replace(" SUITE ", " STE ")
				.Replace(" CENTER ", " CTR ")
				.Replace("#", " APT ")
				.Replace(" APT APT", " APT "); // Handle double abbreviations
		}

		
		//No bugs found.

		private void btnProcessText_Click(object sender, EventArgs e)
		{
			txtInputText.Text = "";
			txtOutputText.Text = "";
			txtInputText.Paste();

		}

		private void mnuOnlineLogins_Click(object sender, EventArgs e)
		{
			var onlineLogins = new frmOnlineLogins
			{
				accessLevel = accessLevel,
				empName = EmpName,
				officeLoc = officeLoc,
				Text = "Online Logins"

			};
			if (officeLoc == "SAN DIMAS")
			{
				onlineLogins.cmbBrowser.Visible = false;
				onlineLogins.txtRemarks.Height = 75;
				onlineLogins.lblbrowsertouse.Visible = false;
				onlineLogins.chkUpdateDiscord.Location = new System.Drawing.Point(147, 320);
			}
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				onlineLogins.btnNew.Enabled = false;
				onlineLogins.btnDelete.Enabled = false;
				onlineLogins.chkUpdateDiscord.Visible = false;
			}
			onlineLogins.Show();
		}

		private void btnInsuranceLogin_Click(object sender, EventArgs e)
		{

			var dlgOnlineLogins = new frmOnlineLogins
			{
				accessLevel = accessLevel,
				empName = EmpName,
				officeLoc = officeLoc
			};
			//onlineLogins.lblLevel.Content = accessLevel;
			//onlineLogins.lblName.Content = EmpName;
			if (officeLoc == "SAN DIMAS")
			{
				dlgOnlineLogins.cmbBrowser.Visible = false;
				dlgOnlineLogins.txtRemarks.Height = 56;
				dlgOnlineLogins.lblbrowsertouse.Visible = false;
			}
			if (dlgOnlineLogins.accessLevel == "User" || dlgOnlineLogins.accessLevel == "Power User")
			{
				dlgOnlineLogins.btnNew.Enabled = false;
				dlgOnlineLogins.btnDelete.Enabled = false;
				dlgOnlineLogins.chkUpdateDiscord.Visible = false;
			}
			dlgOnlineLogins.Show();
		}

		private void btnInsuranceInfo_Click(object sender, EventArgs e)
		{
			if (accessLevel == "User")
			{
				var privateIns = new frmInsuranceInfo()
				{
					EmpName = EmpName,
					accessLevel = accessLevel
				};
				privateIns.btnNew.Visible = false;
				privateIns.ShowDialog();
			}
			else
			{
				var privateIns = new frmInsuranceInfo()
				{
					EmpName = EmpName,
					accessLevel = accessLevel
				};
				privateIns.btnNew.Visible = false;
				privateIns.ShowDialog();
			}
		}


		private void btnDemoGen_Click(object sender, EventArgs e)
		{
			var testForm = new frmRTFEditor
			{
				file = _DemoGen
			};
			testForm.Show();
		}

		private void mnuPantry_Click(object sender, EventArgs e)
		{
			var dlgPantryList = new frmPantry
			{
				accessLevel = accessLevel,
				EmpName = EmpName,
				Text = "Tm Pantry Store List"
			};
			dlgPantryList.ShowDialog();
		}

		private void PopulateTelerikThemes()
		{
			CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
			CreateThemeMenuItem(mnuYourmeeThemes, "Dark", "CrystalDark");
			CreateThemeMenuItem(mnuYourmeeThemes, "Fluent", "Fluent");
			CreateThemeMenuItem(mnuYourmeeThemes, "Fluent Dark", "FluentDark");
			CreateThemeMenuItem(mnuYourmeeThemes, "Material Pink", "MaterialPink");
			CreateThemeMenuItem(mnuYourmeeThemes, "Windows 7 Feel", "Windows7");
			CreateThemeMenuItem(mnuYourmeeThemes, "Windows 8 Feel", "Windows8");
			CreateThemeMenuItem(mnuYourmeeThemes, "Office 2010 (Blue)", "Office2010Blue");
			CreateThemeMenuItem(mnuYourmeeThemes, "Windows 11 Feel", "Windows11");
			//CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
			//CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
			//CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
			//CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
			//CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
			//CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
			//CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
			//CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
		}

		private void CreateThemeMenuItem(RadMenuItem parentMenu, string menuName, string themeName)
		{
			RadMenuItem newMenuItem = new(menuName, themeName);
			newMenuItem.Click += new EventHandler(themeMenuItem_Click);
			parentMenu.Items.Add(newMenuItem);
		}

		private void themeMenuItem_Click(object sender, EventArgs e)
		{
			RadMenuItem menuItem = sender as RadMenuItem;
			ThemeResolutionService.ApplicationThemeName = menuItem.Tag as string;
			user.UpdateUserTheme(menuItem.Tag.ToString(), EmpName, out string message);
			fe.SendToastNotifDesktop(message, "Success");
		}

		private void mnuUseProfile_Click(object sender, EventArgs e)
		{
			var userProfile = new frmUserProfile();      
			user.FillUserProfile
				(
				employeeID,
				out string txtName,
				out string txtUsername,
				out string cmbLevel,
				out string cmbRole,
				out string txtRDWebUsername,
				out string txtRDWebPassword,
				out string txtLytecUsername,
				out string txtLytecPassword,
				out string txtEmail,
				out string txtBroadvoice,
				out string txtDateOfBirth,
				out string dcUsername,
				out string dcPassword,
				EmpName);

			userProfile.txtIntID.Text = employeeID.ToString();
			userProfile.txtEmpName.Text = txtName;
			userProfile.txtUsername.Text = txtUsername;
			userProfile.txtUserAccess.Text = cmbLevel;
			userProfile.txtUserPosition.Text = cmbRole;
			userProfile.txtRDWebUsername.Text = txtRDWebUsername;
			userProfile.txtRDWebPassword.Text = txtRDWebPassword;
			userProfile.txtLytecUsername.Text = txtLytecUsername;
			userProfile.txtLytecPassword.Text = txtLytecPassword;
			userProfile.txtWorkEmail.Text = txtEmail;
			userProfile.txtBVNo.Text = txtBroadvoice;
			userProfile.txtDateofBirth.Text = txtDateOfBirth;
			userProfile.txtDiscordUsername.Text = dcUsername;
			userProfile.txtDiscordPassword.Text = dcPassword;  
			userProfile.empName = EmpName;
			userProfile.Text = "My Profile";
			userProfile.ShowDialog();
		}

		private void btnPersonalReminder_Click(object sender, EventArgs e)
		{
			if (!File.Exists(_personalreminderPath))
			{
				office.CreateRtfFile(_personalreminderPath);
				
			}
			var editor = new frmRTFEditor
			{
				file = _personalreminderPath
			};
			editor.Show();
		}

		private void mnuFileLeave_Click(object sender, EventArgs e)
		{
			var modleave = new frmModLeave()
			{
				Text = "Leave",
				EmpName = EmpName,
				accessLevel = accessLevel
			};
			modleave.btnDelete.Visible = false;
			modleave.btnCancel.Location = new System.Drawing.Point(880, 223);
			modleave.dtpEndDate.Text = DateTime.Now.AddDays(1).ToString();
			modleave.dtpStartdate.Text = DateTime.Now.ToString();
			modleave.txtEmpID.Text = employeeID;
			leave.GetDBListID(out string ID, EmpName);
			modleave.lblLeaveID.Text = ID;
			//modleave.GetDBListID();
			string position = modleave.txtPosition.Text;
			string empStat = modleave.txtEmploymentStatus.Text;
			//string empName = EmpName;

			leave.FillUpSupportLeaveForm(employeeID, ref position, ref empStat, EmpName);

			modleave.txtEmployeeName.Text = EmpName;
			modleave.txtPosition.Text = position;
			modleave.txtEmploymentStatus.Text = empStat;
			modleave.dtpStartdate.Focus();
			//var modleave = new frmModLeave()
			//{
			//	Text = "Leave",
			//	EmpName = EmpName,
			//	accessLevel = accessLevel,
			//};
			//modleave.dtpEndDate.Text = DateTime.Now.AddDays(1).ToString();
			//modleave.dtpStartdate.Text = DateTime.Now.ToString();
			//modleave.txtEmpID.Text = employeeID;
			//leave.GetDBListID(out string ID, EmpName);
			//modleave.leaveID = ID;
			//string employeeName = modleave.txtEmployeeName.Text;
			//string position = modleave.txtPosition.Text;
			//string empStat = modleave.txtEmploymentStatus.Text;
			//string empName = EmpName;
			//
			//leave.FillUpSupportLeaveForm(employeeID, ref position, ref empStat, empName);
			//
			//modleave.txtEmployeeName.Text = employeeName;
			//modleave.txtPosition.Text = position;
			//modleave.txtEmploymentStatus.Text = empStat;
			/////leave.FillUpSupportLeaveForm(modleave.txtEmpID.Text, modleave.txtEmployeeName.Text, modleave.txtPosition.Text, modleave.txtEmploymentStatus.Text, EmpName);
			modleave.ShowDialog();
		}

		private void mnuViewLeave_Click(object sender, EventArgs e)
		{
			var leave = new frmLeave();
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				leave.cmbFilterName.Enabled = false;
				leave.cmbFilterName.Text = EmpName;
			}
			else
			{
				leave.cmbFilterName.Enabled = true;
			}
			leave.Text = "Leave";
			leave.EmpName = EmpName;
			leave.accessLevel = accessLevel;
			leave.empID = employeeID;
			leave.ShowDialog();
			leave.ShowLeaveList();
		}
	}
}