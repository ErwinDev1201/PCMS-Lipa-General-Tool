using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Forms;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using System.Drawing;
using System.Data.SqlClient;
using static Telerik.WinControls.VistaAeroTheme.ComboBox;

namespace PCMS_Lipa_General_Tool__WinForm_
{

	public partial class frmCollectors : RadForm
	{

		private bool isMessageShown = false;
		private static readonly string wcsupport = ConfigurationManager.AppSettings["wcsupportpath"];
		private readonly string _wcgenupdates = wcsupport + "WC_General_Updates.rtf";
		private readonly string _wclaborrebut = wcsupport + "WC_Labor_Code_and_Rebuttals.rtf";
		private readonly string _DemoGen = privSupport + @"\Demo_GenReminder.rtf";
		private readonly string _genR = privSupport + @"\Private_GenReminder.rtf";

		private static readonly string privSupport = ConfigurationManager.AppSettings["privsupportpath"];
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();
		private readonly User user = new();
		private readonly Leave leave = new();
		private readonly CollectorNotes notes = new();
		private readonly Provider provider = new();
		private readonly OfficeFiles office = new();
		private readonly string _personalreminderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PersonalReminder.rtf";



		public string EmpName;
		public string accessLevel;
		public string UserName;
		public string employeeID;
		public string OfficeLoc;
		public string employeeStat;

		//public string ThemeName;
		public string Position;
		//public string _dbConnection;

		//private string _empName;

		//public string EmpName
		//{
		//	get => _empName;
		//	set => _empName = value;
		//}


		public frmCollectors(string empName, string userName, string userAccess, string empID, string officeLoc, string empStat, string position)
		{
			InitializeComponent();
            //SetTheme(themeName);	
            ///this.EmpName = Empname;
            EmpName = empName;
            UserName = userName;
			accessLevel = userAccess;
            employeeID = empID;
            OfficeLoc = officeLoc;
            employeeStat = empStat;
            Position = position;


            this.FormClosing += frmMainApp_FormClosing;
			PopulateTelerikThemes();
			InitializeApp();
			InitializeAllNotes();
		}

		#region importantUICOnfigurations
		public void ConfigureVisibility(string role, string department, string position)
		{
			try
			{
				var rolesMapping = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
			{

				{ "Administrator_All Department_Supervisor", () => HideAdminControls() },
				{ "Administrator_All Department_Operations Manager", () => HideAdminControls() },
				{ "Administrator_IT_Operations Manager", () => HideAdminControls() },

				{ "Management_All Department_Supervisor", () => { HideManagementControls(); mnuManageProduct.Visibility = ElementVisibility.Collapsed; } },
				{ "Management_All Department_Operations Manager", () => { HideManagementControls(); mnuManageProduct.Visibility = ElementVisibility.Collapsed; } },

				{ "Power User_Private_Collector", () => HidePrivateCollectorControls() },
				{ "User_Private_Collector", () => HidePrivateCollectorControls() },

				{ "Power User_Workers Comp_Private_Collector", () => HideWorkersCollectorControls() },
				{ "User_Workers Comp_Collector", () => HideWorkersCollectorControls() }
			};

				string key = $"{role}_{department}_{position}";

				// Execute action if key exists
				if (rolesMapping.TryGetValue(key, out var action))
				{
					action?.Invoke();
				}
				else
				{
					RadMessageBox.Show($"No visibility rule defined for {key}", "Info", MessageBoxButtons.OK, RadMessageIcon.Info);
				}
			}
			catch (Exception ex)
			{
				RadMessageBox.Show($"Error configuring visibility: {ex.Message}", "Error", MessageBoxButtons.OK, RadMessageIcon.Info);
			}
		}


		private void HideAdminControls()
		{
			Console.WriteLine("Hiding Admin controls");
			mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
			mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
			mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
		}

		private void HideManagementControls()
		{
			Console.WriteLine("Hiding management controls");
			mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
			mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
			mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
		}

		private void HidePrivateCollectorControls()
		{
			Console.WriteLine("Hiding Collector controls");
			mnuWorkcomp.Visibility = ElementVisibility.Collapsed;
			mnuBackOffice.Visibility = ElementVisibility.Collapsed;
			separator1.Visibility = ElementVisibility.Collapsed;
			seperator2.Visibility = ElementVisibility.Collapsed;
			mnuOpenConfig.Visibility = ElementVisibility.Collapsed;
			mnuManageProduct.Visibility = ElementVisibility.Collapsed;
			mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
			//pictureBox1.BringToFront();
		}

		private void HideWorkersCollectorControls()
		{
			Console.WriteLine("Hiding Collector controls");
			mnuPrivateCollectors.Visibility = ElementVisibility.Collapsed;
			mnuBackOffice.Visibility = ElementVisibility.Collapsed;
			separator1.Visibility = ElementVisibility.Collapsed;
			seperator2.Visibility = ElementVisibility.Collapsed;
			mnuManageProduct.Visibility = ElementVisibility.Collapsed;
			mnuViewCollectorNotes.Visibility = ElementVisibility.Collapsed;
		}

		//public void SetMainAppProperties(string empID, string empName, string userName, string userAccess, string userPosition, string officeLoc, string theme)
		//{
		//	employeeID = empID;
		//	EmpName = empName;
		//	UserName = userName;
		//	accessLevel = userAccess;
		//	statlblUsername.Text = empName;
		//	statlblAccess.Text = userAccess;
		//	statlblPosition.Text = Position;
		//	OfficeLoc = officeLoc;
		//	ThemeName = theme;
		//	Position = userPosition;
		//	//_dbConnection = _dbConnection;
		//
		//	//log.AddActivityLog(logMessage, EmpName, $"{EmpName} logged in", "USER LOGGED IN");
		//}

		#endregion


		private void mnuOnlineLogins_Click(object sender, EventArgs e)
		{
			var onlineLogins = new frmOnlineLogins
			{
				accessLevel = accessLevel,
				empName = EmpName,
				officeLoc = OfficeLoc,
				Text = "Online Logins"

			};
			if (OfficeLoc == "SAN DIMAS")
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
			else if (accessLevel == "Administrator")
			{
				onlineLogins.chkUpdateDiscord.Visible = true;

			}
			onlineLogins.Show();
		}

		private void mnuDemoTool_Click(object sender, EventArgs e)
		{
			var demoTool = new frmDemoTool
			{
				Text = "Demographer Tool"
			};
			demoTool.mnuDemoMain.Visible = false;
			demoTool.statbottom.Visible = false;
			demoTool.Show();
		}

		private void mnuUserMgmt_Click(object sender, EventArgs e)
		{
			var userMgmt = new frmUserManagement();
			if (accessLevel == "Administrator")
			{
				userMgmt.btnNewUser.Visible = false;
			}
			userMgmt.Text = "User List";
			userMgmt.empName = EmpName;
			userMgmt.accessLevel = accessLevel;
			userMgmt.ShowDialog();
		}

		private void radStatusStrip1_StatusBarClick(object sender, RadStatusBarClickEventArgs args)
		{

		}



		private void mainappTime_Tick(object sender, EventArgs e)
		{
			DateTime phTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time");
			statlbldateTime.Text = "PH Time: " + phTime.ToShortDateString() + " " + phTime.ToLongTimeString();
		}

		private void mnuLogout_Click(object sender, EventArgs e)
		{
			log.AddActivityLog($"{EmpName} logout in the Application \n Time Logged Out: {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}", EmpName, $"{EmpName} logged out", "USER LOG OUT");
			Hide();
			var dlglogin = new FrmLogin();
			dlglogin.txtUsername.Focus();
			dlglogin.ShowDialog();
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				log.AddActivityLog($"{EmpName} has exit the Application \n Time Exit: {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}", EmpName, $"{EmpName} exit the app", "USER CLOSE THE APP");
				Application.Exit();
			}
		}

		private void mnuOpenConfig_Click(object sender, EventArgs e)
		{
			var dlgConnection = new frmModConnection
			{
				Text = "Connection configuration",
				empName = EmpName,
				accessLevel = accessLevel,
			};
			dlgConnection.ShowDialog();
		}


		private void mnuEasyPrint_Click(object sender, EventArgs e)
		{
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				var dlgEasyprint = new frmEasyPrint
				{
					btnNew = { Visible = false },
					accessLevel = accessLevel,
					EmpName = EmpName,
					//dlgEasyprint.txtSearch.Focus();
					Text = "Easy Print"
				};
				dlgEasyprint.ShowDialog();
			}
			else
			{
				var dlgEasyprint = new frmEasyPrint
				{
					EmpName = EmpName,
					accessLevel = accessLevel,
				};
				//dlgEasyprint.txtSearch.Focus();
				dlgEasyprint.ShowDialog();
			}

		}

		private void mnuInsBilCollection_Click(object sender, EventArgs e)
		{

			var filepath = privSupport + @"\Insurance Billing and Collections.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				notif.LogError("mnuInsBilCollection_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show("Unable to open the file, please check with the developer/programmer", "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuMedimedi_Click(object sender, EventArgs e)
		{
			var filepath = privSupport + @"\MEDICARE vs MEDICAID.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnMedimedi_Click \n\n Detailed Error: " + ex.ToString());
				notif.LogError("mnuMedimedi_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuMemoColl_Click(object sender, EventArgs e)
		{
			var filepath = privSupport + @"\MEMO_Collection Guideline.12.21.18.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnMemoCollection_Click \n\n Detailed Error: " + ex.ToString());
				notif.LogError("mnuMemoColl_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuUnInsPolicies_Click(object sender, EventArgs e)
		{

			var filepath = privSupport + @"\Understanding Insurance Policies.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnInsPolicies_Click \n\n Detailed Error: " + ex.ToString());
				notif.LogError("mnuUnInsPolicies_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuModRefGuide_Click(object sender, EventArgs e)
		{
			var filepath = privSupport + @"\modifier-reference-guide.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnmodRef_Click \n\n Detailed Error: " + ex.ToString());
				notif.LogError("mnuModRefGuide_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuRejClaims_Click(object sender, EventArgs e)
		{
			var filepath = privSupport + @"\Presentation - Rejected Claims PRIVATE.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnRejectClaims_Click \n\n Detailed Error: " + ex.ToString());
				notif.LogError("mnuRejClaims_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuEvalCodes_Click(object sender, EventArgs e)
		{
			var filepath = privSupport + @"\Evaluation and Management code.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnEvalCodes_Click \n\n Detailed Error: " + ex.ToString());
				notif.LogError("mnuEvalCodes_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuPPOvsHMO_Click(object sender, EventArgs e)
		{
			var filepath = privSupport + @"\PPO vs HMO.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				notif.LogError("mnuPPOvsHMO_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show("Unable to open the file, please check with the developer/programmer", "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuListofPT_Click(object sender, EventArgs e)
		{
			var filepath = privSupport + @"\THERAPY-CPT-CODES-2018-UPDATE.pdf";
			try
			{
				Process.Start(filepath);
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail("Unable to locate file \n\n File: " + filepath + "\n Module: CollectorWindows \n Process: btnPTCodes_Click \n\n Detailed Error: " + ex.ToString());
				notif.LogError("mnuListofPT_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuICD10_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("https://www.icd10data.com/ICD10CM/Codes");
			}
			catch (Exception ex)
			{
				notif.LogError("mnuICD10_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open link", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuNewDxCodes_Click(object sender, EventArgs e)
		{
			try
			{
				if (accessLevel == "Programmer")
				{
					Process.Start("https://www.icd10data.com/ICD10CM/Codes/Changes/New_Codes");
				}
				else
				{
					Process.Start("chrome.exe", "https://www.icd10data.com/ICD10CM/Codes/Changes/New_Codes");
				}
			}
			catch (Exception ex)
			{
				notif.LogError("mnuNewDxCodes_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuRevisedDxCode_Click(object sender, EventArgs e)
		{
			try
			{
				if (accessLevel == "Programmer")
				{
					Process.Start("https://www.icd10data.com/ICD10CM/Codes/Changes/Revised_Codes");
				}
				else
				{
					Process.Start("chrome.exe", "https://www.icd10data.com/ICD10CM/Codes/Changes/Revised_Codes");
				}
			}
			catch (Exception ex)
			{
				notif.LogError("mnuNewDxCodes_Click", EmpName, "frmMainApp", null, ex);
				RadMessageBox.Show(ex.Message, "Failed to Open link", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuDeletedCodes_Click(object sender, EventArgs e)
		{
			try
			{
				if (accessLevel == "Programmer")
				{
					Process.Start("https://www.icd10data.com/ICD10CM/Codes/Changes/Deleted_Codes");
				}
				else
				{
					Process.Start("chrome.exe", "https://www.icd10data.com/ICD10CM/Codes/Changes/Deleted_Codes");
				}
			}
			catch (Exception ex)
			{
				//mailSender.SendEmail(ex.Message +"\n\n Name: " + EmpName + "\n Module: CollectorWindows \n Process: btnDelCodesDx_Click \n\n Detailed Error: " + ex.ToString());
				notif.LogError("mnuNewDxCodes_Click", EmpName, "frmMainApp", null, ex); ;
				RadMessageBox.Show(ex.Message, "Failed to Open link", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void mnuGenReminder_Click(object sender, EventArgs e)
		{
			var dlgRTFEditor = new frmRTFEditor
			{
				file = _genR
			};
			dlgRTFEditor.Show();
		}

		private void mnuwcgenUpdates_Click(object sender, EventArgs e)
		{
			var dlgRTFEditor = new frmRTFEditor
			{
				file = _wcgenupdates
			};
			dlgRTFEditor.Show();
		}

		private void mnuwcRebut_Click(object sender, EventArgs e)
		{
			var dlgRTFEditor = new frmRTFEditor
			{
				file = _wclaborrebut
			};
			dlgRTFEditor.Show();
		}

		private void mnuMPN_Click(object sender, EventArgs e)
		{
			var mpnList = new frmMPN();
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				mpnList.btnNew.Visible = false;

			}
			mpnList.accessLevel = accessLevel;
			mpnList.EmpName = EmpName;
			mpnList.Text = "MPN List";
			mpnList.ShowDialog();
		}

		private void mnuAttorney_Click(object sender, EventArgs e)
		{
			var attyEmail = new frmAttorneyInformation();
			if (accessLevel == "User" || accessLevel == "Power User")
			{

				attyEmail.btnNew.Visible = false;

			}
			attyEmail.accessLevel = accessLevel;
			attyEmail.EmpName = EmpName;
			attyEmail.Text = "Attorney Information";
			attyEmail.ShowDialog();
		}
		//Bug: The variable "accessLevel" is not declared anywhere in the code.

		private void mnuHearingRep_Click(object sender, EventArgs e)
		{
			var hearingRep = new frmHearingRep();
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				hearingRep.btnNew.Visible = false;
			}
			hearingRep.accessLevel = accessLevel;
			hearingRep.EmpName = EmpName;
			hearingRep.Text = "Hearing Representative";
			hearingRep.ShowDialog();
		}

		private void mnuInsBRDir_Click(object sender, EventArgs e)
		{
			var wcMisc = new frmBillReviewDirectory();
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				wcMisc.btnNew.Visible = false;
			}
			wcMisc.accessLevel = accessLevel;
			wcMisc.EmpName = EmpName;
			wcMisc.Text = "Insurance Bill Review Directory";
			wcMisc.ShowDialog();
		}

		private void mnuAdjInformation_Click(object sender, EventArgs e)
		{
			var dlgAdjInformation = new FrmAdjusterinformation();
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				dlgAdjInformation.btnNew.Visible = false;
			}
			dlgAdjInformation.accessLevel = accessLevel;
			dlgAdjInformation.EmpName = EmpName;
			dlgAdjInformation.Text = "Adjuster Information";
			dlgAdjInformation.ShowDialog();
		}

		private void munBundleCodes_Click(object sender, EventArgs e)
		{
			var dlgBundleCodes = new frmBundlecodes();
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				dlgBundleCodes.btnNew.Visible = false;
			}
			dlgBundleCodes.accessLevel = accessLevel;
			dlgBundleCodes.EmpName = EmpName;
			dlgBundleCodes.Text = "Procedure Bundle Codes";
			dlgBundleCodes.ShowDialog();
		}

		private void mnuDemoGenReminders_Click(object sender, EventArgs e)
		{
			var dlgRTFEditor = new frmRTFEditor
			{
				file = _DemoGen
			};
			dlgRTFEditor.Show();
		}

		private void mnuUserProfile_Click(object sender, EventArgs e)
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

		private void mnuEmpInformation_Click(object sender, EventArgs e)
		{
			var dlgEmpInfo = new frmEmployeeInfo()
			{
				Text = "Employee Information/Directory",
				EmpName = EmpName,
				accessLevel = accessLevel
			};
			dlgEmpInfo.ShowDialog();

		}

		private void mnuPantryList_Click(object sender, EventArgs e)
		{
			var dlgPantryList = new frmPantry
			{
				_empName = EmpName,
				_accessLevel = accessLevel,
				Text = "Tm Pantry Store List"
			};
			dlgPantryList.ShowDialog();

		}

		private void mnuManageProduct_Click(object sender, EventArgs e)
		{

			// fix issue with user with admin access can manage product.
			if (EmpName == "Erwin Alcantara" || EmpName == "Edimson Escalona" || EmpName == "Dimz Escalona")
			{
				var dlgManageProduct = new frmManageproduct
				{
					empName = EmpName,
					accessLevel = accessLevel,
					Text = "Manage Product"
				};
				dlgManageProduct.ShowDialog();
			}
			else
			{
				RadMessageBox.Show("You don't have access in the menu, Please ask sir Dimz", "Notice", MessageBoxButtons.OK, RadMessageIcon.Info);
			}

		}

		private void mnuTraining_Click(object sender, EventArgs e)
		{
			var dlgTraining = new frmWCTrainingtools
			{
				Text = "Training Tools",
				empName = EmpName,
				accessLevel = accessLevel,
			};
			dlgTraining.ShowDialog();
		}

		private void mnuAdjEmailFormat_Click(object sender, EventArgs e)
		{
			var dlgAdjEmailFormat = new frmEmailFormat();
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				dlgAdjEmailFormat.btnNew.Visible = false;
			}
			dlgAdjEmailFormat.accessLevel = accessLevel;
			dlgAdjEmailFormat.EmpName = EmpName;
			dlgAdjEmailFormat.Text = "Insurance Adjuster Email Format";
			dlgAdjEmailFormat.ShowDialog();
		}

		private void mnuWCTool_Click(object sender, EventArgs e)
		{
			var dlgTraining = new frmWCTool
			{
				Text = "Workers Comp Tools",
				empName = EmpName,
				accessLevel = accessLevel,
			};
			dlgTraining.ShowDialog();
		}

		private void mnuAbout_Click(object sender, EventArgs e)
		{
			var dlgAbout = new frmAbout()
			{
				empName = EmpName,
				accessLevel = accessLevel
			};
			dlgAbout.ShowDialog();
		}


		private void PopulateTelerikThemes()
		{
			try
			{
				CreateThemeMenuItem(mnuYourmeeThemes, "Default", "Crystal");
				CreateThemeMenuItem(mnuYourmeeThemes, "Dark", "CrystalDark");
				CreateThemeMenuItem(mnuYourmeeThemes, "Fluent", "Fluent");
				CreateThemeMenuItem(mnuYourmeeThemes, "Fluent Dark", "FluentDark");
				CreateThemeMenuItem(mnuYourmeeThemes, "Material Pink", "MaterialPink");
				CreateThemeMenuItem(mnuYourmeeThemes, "Windows 7 Feel", "Windows7");
				CreateThemeMenuItem(mnuYourmeeThemes, "Windows 8 Feel", "Windows8");
				CreateThemeMenuItem(mnuYourmeeThemes, "Breeze", "Breeze");
				CreateThemeMenuItem(mnuYourmeeThemes, "Office 2010 (Blue)", "Office2010Blue");
				CreateThemeMenuItem(mnuYourmeeThemes, "Windows 11 Feel", "Windows11");
				CreateThemeMenuItem(mnuYourmeeThemes, "Desert", "Desert");
			}
			catch (Exception ex)
			{
				notif.LogError("PopulateTelerikThemes", EmpName, "frmMainApp", null, ex);
			}
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
			user.UpdateUserTheme(EmpName, menuItem.Tag.ToString(), out string message);
			fe.SendToastNotifDesktop(message, "success");
		}

		private void mnuSuggestion_Click(object sender, EventArgs e)
		{
			var dlgSuggestion = new frmSuggestion()
			{
				EmpName = EmpName,
				Text = "Suggestion",
				accessLevel = accessLevel,

			};
			dlgSuggestion.ShowDialog();
		}

		private void mnuDiagnosis_Click(object sender, EventArgs e)
		{
			var dlgDiagnosis = new frmDiagnosis();
			if (accessLevel == "User")
			{
				dlgDiagnosis.btnNew.Visible = false;
			}
			dlgDiagnosis.accessLevel = accessLevel;
			dlgDiagnosis.EmpName = EmpName;
			dlgDiagnosis.Text = "Diagnosis Information";
			dlgDiagnosis.ShowDialog();
		}

		private void mnuverHistory_Click(object sender, EventArgs e)
		{
			var verHist = new frmVersion_History()
			{
				Text = "Version History",
				empName = EmpName,
				accessLevel = accessLevel,
			};
			verHist.ShowDialog();
		}

		private void mnuProvider_Click(object sender, EventArgs e)
		{
			var dlgProviderInfo = new frmProvider();
			if (accessLevel == "User" || accessLevel == "Power User")
			{
				dlgProviderInfo.btnUpdateSave.Enabled = false;
			}
			dlgProviderInfo.accessLevel = accessLevel;
			dlgProviderInfo.EmpName = EmpName;
			dlgProviderInfo.Text = "Provider Information Information";
			dlgProviderInfo.ShowDialog();

		}

		private void mnuwcDX_Click(object sender, EventArgs e)
		{
			var dlgDiagnosis = new frmDiagnosis();
			if (accessLevel == "User")
			{
				dlgDiagnosis.btnNew.Visible = false;
			}
			dlgDiagnosis.accessLevel = accessLevel;
			dlgDiagnosis.EmpName = EmpName;
			dlgDiagnosis.Text = "Diagnosis Information";
			dlgDiagnosis.ShowDialog();
		}

		private void mnuPersonaReminders_Click(object sender, EventArgs e)
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
			modleave.txtPosition.Text = Position;
			modleave.txtEmploymentStatus.Text = employeeStat;
			//modleave.GetDBListID();
			//string position = modleave.txtPosition.Text;
			//string empStat = modleave.txtEmploymentStatus.Text;
			////string empName = EmpName;

			//leave.FillUpSupportLeaveForm(employeeID, ref Position, ref  , EmpName);

			modleave.txtEmployeeName.Text = EmpName;
			//modleave.txtPosition.Text = position;
			//modleave.txtEmploymentStatus.Text = empStat;
			modleave.dtpStartdate.Focus();
			///leave.FillUpSupportLeaveForm(modleave.txtEmpID.Text, modleave.txtEmployeeName.Text, modleave.txtPosition.Text, modleave.txtEmploymentStatus.Text, EmpName);
			modleave.ShowDialog();
		}

		private void mnuYourmeeThemes_Click(object sender, EventArgs e)
		{

		}

		private void mnuAIAssist_Click(object sender, EventArgs e)
		{
			RadMessageBox.Show("Still under development", "Under Development");
			//var ai = new frmAIAssistant();
			//ai.ShowDialog();
		}

		private void mnuITHelp_Click(object sender, EventArgs e)
		{
			var dlgITask = new frmITTask
			{
				_empName = EmpName,
				_accessLevel = accessLevel,
				Text = "Assign Task to IT"
			};
			//var dlgITask = new frmITTask()
			//{
			//	empName = EmpName,
			//	accessLevel = accessLevel,
			//	Text = "Assign Task to IT"
			//};
			dlgITask.ShowDialog();
		}


		private void mnuViewLeave_Click(object sender, EventArgs e)
		{
			var leave = new frmLeave
			{
				Text = "Leave",
				EmpName = EmpName,
				accessLevel = accessLevel,
				empID = employeeID,
				position = Position,
				empStat = employeeStat,
			};

			// Configure `cmbFilterName` and `cmbFilterStatus` based on access level.
			switch (accessLevel)
			{
				case "User":
				case "Power User":
					leave.cmbFilterName.Enabled = false;
					leave.cmbFilterName.Text = EmpName;
					break;

				case "Management":
				case "Administrator":
				case "Programmer":
					leave.cmbFilterName.Enabled = true;
					//leave.cmbFilterName.Text = EmpName;
					leave.cmbFilterStatus.Text = "FOR APPROVAL";
					leave.ShowLeaveList();
					break;
			}

			// Show the leave form and leave list.
			leave.ShowDialog();
			leave.ShowLeaveList();



			//var leave = new frmLeave();
			//if (accessLevel == "User" || accessLevel == "Power User")
			//{
			//	leave.cmbFilterName.Enabled = false;
			//	leave.cmbFilterName.Text = EmpName;
			//}
			//else if (accessLevel == "Management")
			//{
			//	leave.cmbFilterName.Enabled = true;
			//	leave.cmbFilterName.Text = EmpName;
			//	leave.cmbFilterStatus.Text = "FOR APPROVAL";
			//}
			//else if 
			//leave.Text = "Leave";
			//leave.EmpName = EmpName;
			//leave.accessLevel = accessLevel;
			//leave.empID = employeeID;
			//leave.ShowDialog();
			//leave.ShowLeaveList();
		}


		#region CollectorNotes
		private void InitializeApp()
		{
			//viewNotesTab();
			//mnuViewCollectorNotes.Text = collectorPanel.Visible ? "Close Collector Notes" : "Open Collector Notes";
			cmbProviderList.SelectedIndex = cmbProviderList.Items.Count > 0 ? 0 : -1;
			rdoToday.IsChecked = true;
			rdoTransmode.IsChecked = true;
			//LoadNotesCountsAndAverage();
			//LoadNotes();
			StartUpSetting();

		}

		private void LoadNotesCountsAndAverage()
		{

			//if (cmbProviderList.Text == "-- Select Provider --" || string.IsNullOrEmpty(cmbProviderList.Text))
			//{
			//	return;
			//}
			//string tableName = $"{cmbProviderList.Text.Trim()}";
            string tableName = EmpName;

            (int todayNotes, int monthNotes, double avgNotesPerDay) = notes.GetNotesCountsAndAverage(tableName);

			lblCountNotes.Text = $"Today's Notes: {todayNotes:N0}";
			lblMonthly.Text = $"Month's Notes: {monthNotes:N0}";
			lblAverage.Text = $"Average per Day: {avgNotesPerDay:N2}";
		}



		//private void UpdateNotesCounter()
		//{
		//	string mode = rdoToday.IsChecked ? "Today" : rdothisMonth.IsChecked ? "Month" : string.Empty;
		//	if (string.IsNullOrEmpty(mode)) return;
		//
		//	string tableName = $"{cmbProviderList.Text.Trim()}_{DateTime.Now:yyyy_MM}";
		//
		//	int notesCount = notes.GetNotesCount(tableName, mode);
		//
		//	lblNotesCounter.Text = $"Total Notes: {notesCount:N0}";
		//}


		private void btnAddTransaction_Click(object sender, EventArgs e)
		{
			var notesTran = new frmModifyNotes(EmpName, Position) // Pass EmpName here
			{
				Text = "Add Notes",
				//position = position,
			};
			notesTran.btnDelete.Visible = false;
			notesTran.btnUpdateSave.Text = "Save";
			notes.GetDBID(out string ID, EmpName);
			notesTran.txtIntID.Text = ID;
			notesTran.ShowDialog();
			LoadNotes();
			LoadNotesCountsAndAverage();
			//ViewNotesToday();
			//viewNotesTab();
		}

		//private void ViewNotesToday()
		//{
		//	string mode = rdoAgingMode.IsChecked ? "Aging" :
		//				  rdoTransmode.IsChecked ? "Trans" : string.Empty;
		//
		//	if (string.IsNullOrWhiteSpace(mode))
		//	{
		//		RadMessageBox.Show("Please select a mode: Aging or Trans.", "Missing Mode", MessageBoxButtons.OK, RadMessageIcon.Info);
		//		return;
		//	}
		//
		//	if (mode == "Aging")
		//	{
		//		rdoToday.Enabled = false;
		//	}
		//	else
		//	{
		//		rdoToday.Enabled = true;
		//	}
		//
		//	//string fullTableName = $"{cmbProviderList.Text.Trim()}_{DateTime.Now:yyyy_MM}";
		//	string fullTableName = $"{cmbProviderList.Text.Trim()}";
		//	string keyword = txtSearch.Text.Trim();
		//
		//	// Fetch data from BE
		//	var dataTable = notes.NotesToday(EmpName, out string lblCount, fullTableName, keyword, mode);
		//
		//	// Clear and reload data source
		//	dgCurrentNotes.DataSource = null;
		//	dgCurrentNotes.DataSource = dataTable;
		//	dgCurrentNotes.BestFitColumns(BestFitColumnMode.DisplayedCells);
		//
		//	lblCounterdgCurrent.Text = lblCount;
		//}

		private void LoadNotes()
		{
			//if (cmbProviderList.Text == "-- Select Provider --" || string.IsNullOrEmpty(cmbProviderList.Text))
			//	return;
			//
			string tableName = EmpName;
			//string providerName = cmbProviderList.Text.Trim();
			string providerName = string.IsNullOrWhiteSpace(cmbProviderList.Text) ? "All" : cmbProviderList.Text.Trim();
            string keyword = txtSearchTable.Text.Trim();
			string modeOption = rdoTransmode.IsChecked ? "Trans" : "Aging";
			string exclNotes = chkExclude.Checked ? "excludeNotes" : string.Empty;
			string dateMode = rdoToday.IsChecked ? "Today" :
							  rdothisMonth.IsChecked ? "Month" : "Today";

			// Call the routing method with explicit types
			(bool isSuccess, DataTable result, string errorMessage) = notes.GetNotesForTable(
				tableName: tableName,
				keyword: keyword,
				mode: modeOption,
				dateMode: dateMode,
				exNotes: exclNotes,
				startDate: null,
				endDate: null,
				pageTab: null,
				insurance: null,
				patientType: null,
				provider: providerName
            );

			if (!isSuccess)
			{
				RadMessageBox.Show(errorMessage, "Notes Loader", MessageBoxButtons.OK, RadMessageIcon.Info);
				dgCurrentNotes.DataSource = null;
				lblCounterdgCurrent.Text = "No notes available.";
				return;
			}

			// Bind the result
			dgCurrentNotes.Columns.Clear();
			dgCurrentNotes.DataSource = result;
			dgCurrentNotes.BestFitColumns(BestFitColumnMode.DisplayedCells);
			dgCurrentNotes.Columns["DOB"].FormatString = "{0:MM/dd/yyyy}";
			dgCurrentNotes.Columns["DOB"].FormatInfo = CultureInfo.InvariantCulture;
			if (modeOption == "Aging")
			{
				dgCurrentNotes.Columns["Last Visit"].FormatString = "{0:MM/dd/yyyy}";
				dgCurrentNotes.Columns["Last Visit"].FormatInfo = CultureInfo.InvariantCulture;
			}

			lblCounterdgCurrent.Text = result.Rows.Count > 0
				? $"Total records: {result.Rows.Count:N0}"
				: "No notes available.";
		}




		//private void ViewNotesToday()
		//{
		//	string mode = rdoAgingMode.IsChecked ? "Aging" : rdoTransmode.IsChecked ? "Trans" : string.Empty;
		//	string fullTableName = $"{cmbProviderList.Text.Trim()}_{DateTime.Now:yyyy_MM}";
		//
		//	string keyword = txtSearch.Text.Trim();
		//	var dataTable = notes.NotesToday(EmpName, out string lblCount, fullTableName, keyword, mode);
		//
		//	dgCurrentNotes.DataSource = dataTable;
		//	dgCurrentNotes.BestFitColumns(BestFitColumnMode.DisplayedCells);
		//	lblCounterdgCurrent.Text = lblCount;
		//}
		//


		private void mnuAssignProvider_Click(object sender, EventArgs e)
		{
			var assignProvider = new frmAssignProvider()
			{
				Text = "Assing Provider to Employee",
				empName = EmpName,
			};
			assignProvider.GetDBListID();
			assignProvider.ShowDialog();

		}

		//public void viewNotesTab()
		//{
		//	notes.ViewNotesToday(dgCurrentNotes, lblCountNotes, EmpName);
		//	LoadAllNotes();
		//	LoadNotesToday();
		//	notes.ViewNotesMonth(lblMonthly.Text, lblAverage.Text, EmpName);
		//	//notes.ViewNotes(dgallNotesView, EmpName, Position);
		//}

		//private void LoadNotesToday()
		//{
		//	var dataTable = notes.ViewNotesToday(EmpName, out string lblCount);
		//	dgCurrentNotes.DataSource = dataTable;
		//	lblCountNotes.Text = lblCount;
		//}
		//
		//private void LoadAllNotes()
		//{
		//	var dataTable = notes.ViewNotes(EmpName, out string lblCount, Position);
		//	dgCurrentNotes.DataSource = dataTable;
		//	lblCountNotes.Text = lblCount;
		//}
		//
		//private void radButton2_Click(object sender, EventArgs e)
		//{
		//	viewNotesTab();
		//}

		private void InitializeAllNotes()
		{
			//grpPatientName.Enabled = false;
			//grpDate.Enabled = false;
			dgallNotesView.ReadOnly = true;
			//FillProviderDropdown();

			//ViewNotesToday();
		}

		private void StartUpSetting()
		{
			cmbProviderList.Text = "All";
			//grpAverage.Enabled = false;
			//grpThisMonth.Enabled = false;
			//grpMode.Enabled = false;
			//grpToday.Enabled = false;
			//txtSearchTable.Enabled = false;
			//rdoToday.Enabled = false;
			//rdothisMonth.Enabled = false;
			//lblCounterdgCurrent.Enabled = false;
			//dgCurrentNotes.Enabled = false;
			//btnAddTransaction.Enabled = false;
			//btnRefresh.Enabled = false;
			LoadNotes();
			LoadNotesCountsAndAverage();


			// Tooltip
			RadToolTip toolTip = new();

            toolTip.SetToolTip(txtSearchTable, "Search by patient name or chart number.");
            toolTip.SetToolTip(cmbProviderList, "Currently selected provider.");
            toolTip.SetToolTip(chkExclude, "Exclude entries that already have notes added this month.");
            toolTip.SetToolTip(grpDateFilter, "Filter based on the date the note was added.");
			toolTip.SetToolTip(txtInsurance, "Search by insurance name.");
			toolTip.SetToolTip(txtSearch, "Search by patient name, chart number, notes and previouse notes");

        }

		private void FillProviderDropdown()
		{
			List<string> items = provider.GetProviderList(EmpName);
			cmbAllProvider.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbAllProvider.Items.Add(item);
			}
		}

		//private void radbuttonAction()
		//{
		//	if (rdoSearch.IsChecked == true)
		//	{
		//		grpSearch.Enabled = true;
		//		grpProvider.Enabled = false;
		//		grpPatientName.Enabled = false;
		//		grpDate.Enabled = false;
		//		//viewNotesTab();
		//	}
		//	else
		//	{
		//		grpSearch.Enabled = false;
		//		grpProvider.Enabled = true;
		//		grpPatientName.Enabled = true;
		//		grpDate.Enabled = true;
		//		txtSearch.Clear();
		//		//viewNotesTab();
		//	}
		//}
		//
		//private void rdoSearch_ToggleStateChanged(object sender, StateChangedEventArgs args)
		//{
		//	radbuttonAction();
		//}
		//
		//private void txtSearch_TextChanging(object sender, TextChangingEventArgs e)
		//{
		//	notes.SearchTextAcrossColumns(dgallNotesView, "[COLLECTOR NOTES]", txtSearch.Text, lblresultCount, EmpName);
		//}

		//private void useFilterSearch()
		//{
		//
		//	notes.FilterCollectorNotes(dgallNotesView, "[COLLECTOR NOTES]", lblresultCount, cmbAllProvider.Text, dtpEndate.Value.ToString("yyyy-MM-dd"), dtpStartDate.Value.ToString("yyyy-MM-dd"), txtPatientName.Text, EmpName);
		//}
		//
		////private void txtPatientName_TextChanged(object sender, EventArgs e)
		////{
		////	useFilterSearch();
		////}
		//
		//
		////private void cmbAllProvider_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		////{
		////	useFilterSearch();
		////}
		//
		//private void dtpEndate_ValueChanged(object sender, EventArgs e)
		//{
		//	if (dtpStartDate.Value > dtpEndate.Value)
		//	{
		//		RadMessageBox.Show("Oops! It looks like the start date is later than the end date. Could you please check and update the dates?", "Invalid Date", MessageBoxButtons.OK, RadMessageIcon.Error);
		//		return;
		//	}
		//	else
		//	{
		//		useFilterSearch();
		//	}
		//}
		//
		//private void dtpStartDate_ValueChanged(object sender, EventArgs e)
		//{
		//	if (dtpStartDate.Value > dtpEndate.Value)
		//	{
		//		RadMessageBox.Show("Oops! It looks like the start date is later than the end date. Could you please check and update the dates?", "Invalid Date", MessageBoxButtons.OK, RadMessageIcon.Error);
		//		return;
		//	}
		//	else
		//	{
		//		useFilterSearch();
		//	}
		//}
		//
		//private void btnallRefresh_Click(object sender, EventArgs e)
		//{
		//	if (rdoSearch.IsChecked == true)
		//	{
		//		notes.FilterCollectorNotes(dgallNotesView, "[COLLECTOR NOTES]", lblresultCount, cmbAllProvider.Text, dtpEndate.Value.ToString("yyyy-MM-dd"), dtpStartDate.Value.ToString("yyyy-MM-dd"), txtPatientName.Text, EmpName);
		//	}
		//	else
		//	{
		//		useFilterSearch();
		//	}
		//}
		//
		//private void btnExportExcel_Click(object sender, EventArgs e)
		//{
		//
		//}

		//public DataTable GetDataTableFromRadGridView(RadGridView gridView)
		//{
		//	DataTable dataTable = new();
		//
		//	// Add columns
		//	foreach (GridViewDataColumn column in gridView.Columns)
		//	{
		//		dataTable.Columns.Add(column.HeaderText, column.DataType);
		//	}
		//
		//	// Add rows
		//	foreach (GridViewRowInfo row in gridView.Rows)
		//	{
		//		if (!row.IsVisible) continue; // Skip hidden rows
		//		DataRow dataRow = dataTable.NewRow();
		//		foreach (GridViewDataColumn column in gridView.Columns)
		//		{
		//			dataRow[column.HeaderText] = row.Cells[column.Name].Value ?? DBNull.Value;
		//		}
		//		dataTable.Rows.Add(dataRow);
		//	}
		//
		//	return dataTable;
		//}

		private void dgCurrentNotes_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (rdoAgingMode.IsChecked == true)
			{
				if (dgCurrentNotes.SelectedRows.Count > 0)
				{
					//var modNotes = new frmModifyNotes(EmpName, Position);
					// will be supported soon
					RadMessageBox.Show("Aging Mode is not yet supported", "Aging Mode", MessageBoxButtons.OK, RadMessageIcon.Info);
				}
			}
			else
			{
				//if (dgCurrentNotes.SelectedRows.Count > 0)
				//{
				//	var modNotes = new frmModifyNotes(EmpName, Position);
				//	notes.FillNotesInfo(dgCurrentNotes, modNotes.txtIntID, modNotes.cmbProviderList, modNotes.txtChartNo, modNotes.txtPatientName, modNotes.txtPrevNotes, modNotes.txtNotes, EmpName);
				//	modNotes.Text = "View/Update Adjuster Information";
				//	//modAdj.btnDelete.Visible = false;
				//	modNotes.btnUpdateSave.Text = "Update";
				//	modNotes.btnDelete.Visible = false;
				//	modNotes.ShowDialog();
				//}
				try
				{
					if (dgCurrentNotes.SelectedRows.Count == 0)
						return;

					var selectedRow = dgCurrentNotes.SelectedRows[0];
					var modAdj = new frmModifyNotes(EmpName, Position)
					{
						Text = "View/Update Adjuster Information",
						empName = EmpName,
						tableName = cmbProviderList.Text,
						btnDelete = { Visible = false },
						btnUpdateSave = { Text = "Update" },



                        //cmbProviderList = { Text = selectedRow.Cells["Provider"].Value?.ToString() ?? string.Empty, ReadOnly = true, },
                        //txtPatientType = { Text = selectedRow.Cells["Type"].Value?.ToString() ?? string.Empty },
                        //txtPatientName = { Text = selectedRow.Cells["Name"].Value?.ToString() ?? string.Empty },
                        //rdtpDOB = { Text = selectedRow.Cells["DOB"].Value?.ToString() ?? string.Empty },
                        //txtInsurance = { Text = selectedRow.Cells["Insurance"].Value?.ToString() ?? string.Empty },
                        //txtPrevNotes = { Text = selectedRow.Cells["Note Description"].Value?.ToString() ?? string.Empty },
                        txtIntID = { Text = selectedRow.Cells["Notes ID"].Value?.ToString() ?? string.Empty, Enabled = false },
                        txtNotes = { Text = selectedRow.Cells["Notes"].Value?.ToString() ?? string.Empty },
						txtRemarks = { Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty },
						txtPrevNotes = { Enabled = false },
						txtPatientType = {Enabled = false }
					
                    };
                    //modAdj.txtIntID.Text = selectedRow.Cells["Notes ID"].Value?.ToString() ?? string.Empty;
                    //modAdj.txtIntID.ReadOnly = string.Equals(accessLevel, "Collector", StringComparison.OrdinalIgnoreCase);
                    //modAdj.txtPrevNotes.Text = selectedRow.Cells["Chart"].Value?.ToString() ?? string.Empty;
                    modAdj.txtPrevNotes.Enabled = string.Equals(accessLevel, "Collector", StringComparison.OrdinalIgnoreCase);
					//modAdj.cmbProviderList.Text = selectedRow.Cells["Provider"].Value?.ToString() ?? string.Empty;
					modAdj.txtPatientType.Enabled = string.Equals(accessLevel, "Collector", StringComparison.OrdinalIgnoreCase);
                    modAdj.txtChartNo.Text = selectedRow.Cells["Chart"].Value?.ToString() ?? string.Empty;
                    modAdj.txtChartNo.Enabled = string.Equals(accessLevel, "Collector", StringComparison.OrdinalIgnoreCase);
                    modAdj.cmbProviderList.Text = selectedRow.Cells["Provider"].Value?.ToString() ?? string.Empty;
                    modAdj.cmbProviderList.Enabled = string.Equals(accessLevel, "Collector", StringComparison.OrdinalIgnoreCase);
                    if (accessLevel != "User")
					{
						modAdj.Text = "View/Update Notes";
						modAdj.btnUpdateSave.Text = "Update";
					}
					else
					{
						modAdj.Text = "View Notes";
						modAdj.btnDelete.Visible = false;
						modAdj.btnUpdateSave.Visible = false;
					}
					modAdj.ShowDialog();
					if (DateTime.TryParse(selectedRow.Cells["Time Stamp"].Value?.ToString(), out DateTime timeStamp))
					{
						if (timeStamp.Date != DateTime.Today)
						{
							modAdj.btnUpdateSave.Visible = false;
							modAdj.lblWarning.Visible = true;
						}
						else
						{
							modAdj.lblWarning.Visible = false;
						}
					}
					LoadNotes();
					LoadNotesCountsAndAverage();
				}
				catch (Exception ex)
				{
					notif.LogError("dgCurrentNotes_MouseDoubleClick", EmpName, "frmAdjusterInfo", null, ex);
				}
			}

			//viewNotesTab();


		}

		private void dgallNotesView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (Position == "Collector")
			{
				return;
			}
			else
			{
				if (dgallNotesView.SelectedRows.Count > 0)
				{
					var modNotes = new frmModifyNotes(EmpName, Position);
					notes.FillNotesInfo(dgallNotesView, modNotes.txtIntID, modNotes.cmbProviderList, modNotes.txtChartNo, modNotes.txtPatientName, modNotes.txtPrevNotes, modNotes.txtNotes, EmpName);
					modNotes.Text = "View/Update Adjuster Information";
					//modAdj.btnDelete.Visible = false;
					modNotes.btnUpdateSave.Text = "Update";
					modNotes.btnDelete.Visible = true;
					modNotes.ShowDialog();
				}

			}
		}



		private void mnuViewCollectorNotes_Click(object sender, EventArgs e)
		{

			if (mnuViewCollectorNotes.Text == "Close Collector Notes")
			{
				mnuViewCollectorNotes.Text = "Open Collector Notes";
			}
			else
			{
				mnuViewCollectorNotes.Text = "Close Collector Notes";
			}

		}

		private void frmMainApp_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Check if the user attempted to close using the X button
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (!isMessageShown) // Check if the message has already been displayed
				{
					e.Cancel = true; // Cancel the close operation

					RadMessageBox.Show(
						this,
						"Please use File -> Exit or File -> Logout to close the application.",
						"Action Required",
						MessageBoxButtons.OK,
						RadMessageIcon.Info
					);

					isMessageShown = true; // Mark that the message has been shown
				}
				else
				{
					// Reset the flag after some time (optional)
					Timer resetTimer = new()
					{
						Interval = 1000 // 1 second delay before resetting
					};
					resetTimer.Tick += (s, args) =>
					{
						isMessageShown = false;
						resetTimer.Stop();
					};
					resetTimer.Start();
				}
			}
		}


		private void cmbProviderList_PopupOpened(object sender, EventArgs e)
		{
			List<string> items = provider.GetProviderListperCollector(EmpName);

			if (items?.Any() != true)
				return;

			foreach (var item in items)
			{
				if (!cmbProviderList.Items.Contains(item))
					cmbProviderList.Items.Add(item);

			}
			//List<string> items = provider.GetProviderListperCollector(EmpName);
			//cmbProviderList.Items.Clear(); // Clear existing items, if any
			//foreach (var item in items)
			//{
			//	cmbProviderList.Items.Add(item);
			//}
		}

		private void cmbProviderList_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			//EnableComponents();
			LoadNotes();
			LoadNotesCountsAndAverage();
		}

		private void EnableComponents()
		{
			grpAverage.Enabled = true;
			grpThisMonth.Enabled = true;
			grpMode.Enabled = true;
			grpToday.Enabled = true;
			txtSearchTable.Enabled = true;
			rdoToday.Enabled = true;
			rdothisMonth.Enabled = true;
			lblCounterdgCurrent.Enabled = true;
			dgCurrentNotes.Enabled = true;
			btnAddTransaction.Enabled = true;
			btnRefresh.Enabled = true;
		}

		private void frmCollectors_Load(object sender, EventArgs e)
		{
			rdoToday.ToggleStateChanged += RadioToggleChanged;
			rdothisMonth.ToggleStateChanged += RadioToggleChanged;
			rdoAgingMode.ToggleStateChanged += RadioToggleChanged;
			rdoTransmode.ToggleStateChanged += RadioToggleChanged;
			mainappTime.Start();
			ThemeResolutionService.ApplicationThemeName = ThemeName;
			//rdoAgingMode.ToggleStateChanged += (s, e) => { if (e.ToggleState == ToggleState.On) LoadNotes(); };
			//rdoTransmode.ToggleStateChanged += (s, e) => { if (e.ToggleState == ToggleState.On) LoadNotes(); };
			InitializeApp();
		}

		private void RadioToggleChanged(object sender, StateChangedEventArgs e)
		{
			if (e.ToggleState != ToggleState.On) return;

			if (rdoAgingMode.IsChecked)
			{
				rdoToday.Enabled = false;
				rdothisMonth.Enabled = false;

				LoadNotes();
			}
			else if (rdoTransmode.IsChecked)
			{
				rdoToday.Enabled = true;
				rdothisMonth.Enabled = true;

				LoadNotes(); // Auto-detect Today/Month
			}
		}


		private void txtSearchTable_TextChanged(object sender, EventArgs e)
		{
			LoadNotes();
		}

		private void mnuAgingUploader_Click(object sender, EventArgs e)
		{
			var dlgAgingUploader = new frmAgingUploader
			{
				_empName = EmpName,
				_accessLevel = accessLevel,
				Text = "Aging Uploader"
			};
			dlgAgingUploader.ShowDialog();
		}

		private void rdoToday_ToggleStateChanged(object sender, StateChangedEventArgs args)
		{
			LoadNotes();
		}

		private void rdoTransmode_ToggleStateChanged(object sender, StateChangedEventArgs args)
		{
			chkExclude.Enabled = false;
			LoadNotes();
		}

		private void rdoAgingMode_ToggleStateChanged(object sender, StateChangedEventArgs args)
		{
			LoadNotes();
			rdothisMonth.IsChecked = true;
			chkExclude.Enabled = true;
		}

		private void chkExclude_ToggleStateChanged(object sender, StateChangedEventArgs args)
		{
			LoadNotes();
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			LoadNotes();
		}


		#endregion Collector Notes


		private void StartUpAllNotes()
		{
			
			StartUpCondition();
            cmbAllProvider.Focus();
            //dtpEndate.Value = DateTime.Now;
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
    DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            //dtpStartDate.Value = DateTime.Now;
            //tglMode.Value = true;
        }

        private void StartUpCondition()
        {
            bool isEnabled = !string.IsNullOrEmpty(cmbAllProvider.Text);

            dgallNotesView.TableElement.Text = isEnabled ? string.Empty : "Please select Provider";

            SetControlsEnabled(isEnabled);
            LoadAllNotesPerProvider();
        }

        private void SetControlsEnabled(bool isEnabled)
        {
            // Always enable/disable base controls depending on provider selection
            btnExportExcel.Enabled = isEnabled;
            btnallRefresh.Enabled = isEnabled;
            tglMode.Enabled = isEnabled;
            txtPatientName.Enabled = isEnabled;
            txtInsurance.Enabled = isEnabled;
            dtpStartDate.Enabled = isEnabled;
            dtpEndate.Enabled = isEnabled;
            txtSearch.Enabled = isEnabled;

            // chkDateFilter is enabled only when a provider is selected
            chkDateFilter.Enabled = isEnabled;

            // grpDateFilter is only enabled if both:
            // a) Provider is selected, and
            // b) chkDateFilter is checked
            grpDateFilter.Enabled = isEnabled && chkDateFilter.Checked;
        }



        private void AllDateCategorytoggle()
		{
			if(tglMode.Value == true)
			{
				lblAllAging.Enabled = true;
                lblAllTrans.Enabled = false;
                lblAllAging.Font = new Font(lblAllAging.Font, FontStyle.Bold);
            }
			else
			{

                lblAllAging.Enabled = false;
                lblAllTrans.Enabled = true;
                lblAllTrans.Font = new Font(lblAllTrans.Font, FontStyle.Bold);
            }
            LoadAllNotesPerProvider();
        }


        private void GetAllNotesinAllProvider()
		{
            //if (cmbAllProvider.Text == "-- Select Provider --" || string.IsNullOrEmpty(cmbAllProvider.Text))
            //    return;
			//
            string tableName = EmpName;
            string keyword = txtSearch.Text.Trim();
            string modeOption = tglMode.Value ? "Trans" : "Aging";
			//string exclNotes = chkExclude.Checked ? "excludeNotes" : string.Empty;
			//string dateMode = rdoToday.IsChecked ? "Today" :
			//                  rdothisMonth.IsChecked ? "Month" : "Today";

			// Call the routing method with explicit types
			(bool isSuccess, DataTable result, string errorMessage) = notes.GetNotesForTable(
				tableName: tableName,
				keyword: keyword,
				mode: modeOption,
				dateMode: null,
				exNotes: null,
				startDate: grpDateFilter.Enabled ? dtpStartDate.Value : (DateTime?)null,
				endDate: grpDateFilter.Enabled ? dtpEndate.Value : (DateTime?)null,
				pageTab: "AllNotes",
				insurance: txtInsurance.Text.Trim(),
				patientType: cmbPatientType.Text.Trim(),
				provider: cmbAllProvider.Text.Trim()
			);


            if (!isSuccess)
            {
                RadMessageBox.Show(errorMessage, "Notes Loader", MessageBoxButtons.OK, RadMessageIcon.Info);
                dgCurrentNotes.DataSource = null;
                lblCounterdgCurrent.Text = "No notes available.";
                return;
            }

            // Bind the result
            dgallNotesView.Columns.Clear();
            dgallNotesView.DataSource = result;
            dgallNotesView.BestFitColumns(BestFitColumnMode.DisplayedCells);
            dgallNotesView.Columns["DOB"].FormatString = "{0:MM/dd/yyyy}";
            dgallNotesView.Columns["DOB"].FormatInfo = CultureInfo.InvariantCulture;
            if (modeOption == "Aging")
            {
                dgallNotesView.Columns["Last Visit"].FormatString = "{0:MM/dd/yyyy}";
                dgallNotesView.Columns["Last Visit"].FormatInfo = CultureInfo.InvariantCulture;
            }

            lblresultCount.Text = result.Rows.Count > 0
                ? $"Total records: {result.Rows.Count:N0}"
                : "No notes available.";
        }

        private void LoadAllNotesPerProvider()
        {
           // if (cmbAllProvider.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbAllProvider.Text))
           //     return;

            string tableName = EmpName;
            string modeOption = tglMode.Value ? "Trans" : "Aging";

            try
            {
                // Output variable from the method
                string resultCount;
				string insurance = txtInsurance.Text.Trim();
				string patientName = txtPatientName.Text.Trim();
				string keyword = txtSearch.Text.Trim();
				string patientType = cmbPatientType.Text.Trim();
				string provider = cmbAllProvider.Text.Trim();

                // Backend call
                var notesTable = notes.GetAllNotes(tableName, modeOption, out resultCount, EmpName, patientName, insurance, keyword, patientType, provider);

                // UI Handling
                dgallNotesView.Columns.Clear();
                dgallNotesView.DataSource = notesTable;
                dgallNotesView.BestFitColumns(BestFitColumnMode.DisplayedCells);

                // Format DOB if exists
                if (dgallNotesView.Columns.Contains("DOB"))
                {
                    dgallNotesView.Columns["DOB"].FormatString = "{0:MM/dd/yyyy}";
                    dgallNotesView.Columns["DOB"].FormatInfo = CultureInfo.InvariantCulture;
                }

                // Format Last Visit if Aging Mode
                if (modeOption == "Aging" && dgallNotesView.Columns.Contains("Last Visit"))
                {
                    dgallNotesView.Columns["Last Visit"].FormatString = "{0:MM/dd/yyyy}";
                    dgallNotesView.Columns["Last Visit"].FormatInfo = CultureInfo.InvariantCulture;
                }

                lblresultCount.Text = resultCount;
            }
            catch (SqlException sqlEx)
            {
                RadMessageBox.Show("Database error occurred while retrieving notes.", "Database Error", MessageBoxButtons.OK, RadMessageIcon.Info);
                notif.LogError("FE_LoadAllNotesPerProvider_SQL", EmpName, "NotesView", "N/A", sqlEx);
                dgallNotesView.DataSource = null;
                lblresultCount.Text = "No notes loaded.";
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("An unexpected error occurred.", "System Error", MessageBoxButtons.OK, RadMessageIcon.Info);
                notif.LogError("FE_LoadAllNotesPerProvider", EmpName, "NotesView", "N/A", ex);
                dgallNotesView.DataSource = null;
                lblresultCount.Text = "No notes loaded.";
            }
        }


        private void pgViewCollectorNotes_Click(object sender, EventArgs e)
        {
			StartUpAllNotes();
        }

        private void cmbAllProvider_PopupOpened(object sender, EventArgs e)
        {
            List<string> items = Position == "Collector"
			 ? provider.GetProviderListperCollector(EmpName)
			 : provider.GetProviderList(EmpName);

            if (items?.Any() != true)
                return;
            //var comboBox = Position == "Collector" ? cmbProviderList : cmbAllProvider;

            // Add only new items
            foreach (var item in items.Where(item => !cmbAllProvider.Items.Contains(item)))
            {
				cmbAllProvider.Items.Add(item);
            }
        }

        private void cmbAllProvider_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            bool isEnabled = !string.IsNullOrEmpty(cmbAllProvider.Text);

            dgallNotesView.TableElement.Text = isEnabled ? string.Empty : "Please select Provider";

            btnExportExcel.Enabled = isEnabled;
            btnallRefresh.Enabled = isEnabled;
            tglMode.Enabled = isEnabled;
            txtPatientName.Enabled = isEnabled;
            txtInsurance.Enabled = isEnabled;
            dtpStartDate.Enabled = isEnabled;
            dtpEndate.Enabled = isEnabled;
            txtSearch.Enabled = isEnabled;
			chkDateFilter.Enabled = isEnabled;
			if (chkDateFilter.Checked)
			{
				GetAllNotesinAllProvider();
			}
			else
			{
                LoadAllNotesPerProvider();
            }
				

        }

        private void tglMode_ValueChanged(object sender, EventArgs e)
        {
			//LoadAllNotesPerProvider();
			txtInsurance.Clear();
			txtPatientName.Clear();
			AllDateCategorytoggle();
        }

		private void chkDateFilter_ToggleStateChanged(object sender, StateChangedEventArgs args)
		{
            grpDateFilter.Enabled = chkDateFilter.Checked && !string.IsNullOrEmpty(cmbAllProvider.Text);

			if (!chkDateFilter.Checked)
				LoadAllNotesPerProvider();
			else
				GetAllNotesinAllProvider();

        }


        private void txtPatientName_TextChanged(object sender, EventArgs e)
        {
			if (grpDateFilter.Enabled)
			{
				GetAllNotesinAllProvider();
			}
			else
			{
				LoadAllNotesPerProvider();
            }
        }

        private void dtpEndate_ValueChanged(object sender, EventArgs e)
        {
			if (dtpStartDate.Value > dtpEndate.Value)
			{
                RadMessageBox.Show("Oops! It looks like the start date is later than the end date. Could you please check and update the dates?", "Invalid Date", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
            else
            {
                GetAllNotesinAllProvider();
            }
        }

        private void txtInsurance_TextChanged(object sender, EventArgs e)
        {
            if (grpDateFilter.Enabled)
            {
                GetAllNotesinAllProvider();
            }
            else
            {
                LoadAllNotesPerProvider();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            if (grpDateFilter.Enabled)
            {
                GetAllNotesinAllProvider();
            }
            else
            {
                LoadAllNotesPerProvider();
            }
        }

        private void cmbPatientType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (grpDateFilter.Enabled)
            {
                GetAllNotesinAllProvider();
            }
            else
            {
                LoadAllNotesPerProvider();
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
			txtInsurance.Clear();
			txtPatientName.Clear();
			txtSearch.Clear();
			cmbPatientType.Text = "";
			chkDateFilter.Checked = false;
			LoadAllNotesPerProvider();
        }
    }
}