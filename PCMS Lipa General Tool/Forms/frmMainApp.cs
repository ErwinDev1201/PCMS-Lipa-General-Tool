using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Forms;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool__WinForm_
{

	public partial class frmMainApp : Telerik.WinControls.UI.RadForm
	{

		private bool isMessageShown = false;
		private static readonly string wcsupport = ConfigurationManager.AppSettings["wcsupportpath"];
		private readonly string _wcgenupdates = wcsupport + "WC_General_Updates.rtf";
		private readonly string _wclaborrebut = wcsupport + "WC_Labor_Code_and_Rebuttals.rtf";
		private readonly string _DemoGen = privSupport + @"\Demo_GenReminder.rtf";
		private readonly string _genR = privSupport + @"\Private_GenReminder.rtf";

		private static readonly string privSupport = ConfigurationManager.AppSettings["privsupportpath"];
		private readonly CommonTask task = new();
		private readonly User user = new();
		private readonly Leave leave = new();
		private readonly CollectorNotes collnotes = new();
		private readonly Provider provider = new();
		private readonly string _personalreminderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PersonalReminder.rtf";


		public string EmpName;
		public string accessLevel;
		public string userName;
		public string employeeID;
		public string officeLoc;
		public string themeName;
		public string position;

		public frmMainApp()
		{
			InitializeComponent();
			//SetTheme(themeName);	
			///this.EmpName = Empname;

			this.FormClosing += frmMainApp_FormClosing;
			PopulateTelerikThemes();
			InitializeApp();
			InitializeAllNotes();
		}


		private void InitializeApp()
		{
			//if (statlblPosition.Text == "Collector")
			//{
			viewNotesTab();
			mnuViewCollectorNotes.Text = collectorPanel.Visible ? "Close Collector Notes" : "Open Collector Notes";
			///dgCurrentNotes.ReadOnly = true;
			//}
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
			else
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
			if (accessLevel == "Management")
			{
				userMgmt.btnNew.Visible = false;
				userMgmt.btnRemove.Visible = false;
				userMgmt.btnUpdateSave.Visible = false;
			}
			userMgmt.Text = "User Management";
			userMgmt.empName = EmpName;
			userMgmt.accessLevel = accessLevel;
			userMgmt.Show();
		}

		private void radStatusStrip1_StatusBarClick(object sender, RadStatusBarClickEventArgs args)
		{

		}

		private void frmMainApp_Load(object sender, EventArgs e)
		{
			mainappTime.Start();
			ThemeResolutionService.ApplicationThemeName = themeName;
			viewNotesTab();
		}

		private void mainappTime_Tick(object sender, EventArgs e)
		{
			DateTime phTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time");
			statlbldateTime.Text = "PH Time: " + phTime.ToShortDateString() + " " + phTime.ToLongTimeString();
		}

		private void mnuLogout_Click(object sender, EventArgs e)
		{
			task.AddActivityLog($"User logout in the Application \n Time Logged Out: {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}", EmpName, $"{EmpName} logged out", "USER LOG OUT");
			Hide();
			var dlglogin = new FrmLogin();
			dlglogin.txtUsername.Focus();
			dlglogin.Show();
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				task.AddActivityLog($"User Exit the Application \n Time Exit: {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}", EmpName, $"{EmpName} exit the app", "USER CLOSE THE APP");
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
				task.LogError("mnuInsBilCollection_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuMedimedi_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuMemoColl_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuUnInsPolicies_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuModRefGuide_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuRejClaims_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuEvalCodes_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuPPOvsHMO_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuListofPT_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuICD10_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuNewDxCodes_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuNewDxCodes_Click", EmpName, "frmMainApp", null, ex);
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
				task.LogError("mnuNewDxCodes_Click", EmpName, "frmMainApp", null, ex); ;
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
			var wcMisc = new frmInsBillReviewDirectory();
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
			var queryUserProfile = "SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [USER ACCESS], POSITION, [RDWEB USERNAME], [RDWEB PASSWORD], [LYTEC USERNAME], [LYTEC PASSWORD], [EMAIL ADDRESS], [Broadvoice No.], [DATE OF BIRTH], [Discord Username], [Discord Password] FROM [User Information] WHERE USERNAME='" + userName + "'";
			user.FillUserProfile(queryUserProfile, userProfile.txtIntID, userProfile.txtEmpName, userProfile.txtUsername, userProfile.txtUserAccess, userProfile.txtUserPosition, userProfile.txtRDWebUsername, userProfile.txtRDWebPassword, userProfile.txtLytecUsername, userProfile.txtLytecPassword, userProfile.txtWorkEmail, userProfile.txtBVNo, userProfile.txtDateofBirth, userProfile.txtDiscordUsername, userProfile.txtDiscordPassword, EmpName);
			userProfile.Text = "User Profile Information";
			userProfile.empName = EmpName;
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
				EmpName = EmpName,
				accessLevel = accessLevel,
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
			}
			catch (Exception ex)
			{
				task.LogError("PopulateTelerikThemes", EmpName, "frmMainApp", null, ex);
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
			user.UpdateUserTheme(EmpName, menuItem.Tag.ToString());
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

		private void mnuActivityLog_Click(object sender, EventArgs e)
		{
			var dlgActivyLogs = new frmViewActivityLogs
			{
				empName = EmpName,
				accessLevel = accessLevel,
				Text = "Activity Logs"
			};
			dlgActivyLogs.ShowDialog();

		}

		private void mnuDBSequence_Click(object sender, EventArgs e)
		{
			var dbUtility = new frmDBUtility()
			{
				empName = EmpName,
				accessLevel = accessLevel,
				Text = "Database Utility"
			};
			dbUtility.Show();
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
			var dlgRTFEditor = new frmRTFEditor
			{
				file = _personalreminderPath
			};
			dlgRTFEditor.Show();
		}

		private void mnuDevAccountAccess_Click(object sender, EventArgs e)
		{
			var frmDevPasswordUpdate = new frmDeveloperAccess
			{
				Text = "Update Developer Password Access"
			};
			frmDevPasswordUpdate.ShowDialog();
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
			modleave.GetDBListID();
			leave.FillUpSupportLeaveForm(modleave.txtEmpID, modleave.txtEmployeeName, modleave.txtPosition, modleave.txtEmploymentStatus, EmpName);
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
			else if (accessLevel == "Management")
			{
				leave.cmbFilterName.Enabled = true;
				leave.cmbFilterName.Text = EmpName;
			}
			leave.Text = "Leave";
			leave.EmpName = EmpName;
			leave.accessLevel = accessLevel;
			leave.empID = employeeID;
			leave.ShowLeaveList();
			leave.ShowDialog();
		}

		private void mnuBVAvailablity_Click(object sender, EventArgs e)
		{
			if (accessLevel == "Programmer")
			{
				var frmBVAvailityCheck = new frmBVChecker()
				{
					empName = EmpName,
					Text = "Check Broadvoice Availability",

				};
				frmBVAvailityCheck.ShowDialog();
			}
			else
			{
				RadMessageBox.Show("Feature not yet ready")
;
			}

		}


		// this is the start of CollectorNotes


		private void btnAddTransaction_Click(object sender, EventArgs e)
		{
			var notesTran = new frmModifyNotes(EmpName, position) // Pass EmpName here
			{
				Text = "Add Notes",
				//position = position,
			};
			notesTran.btnDelete.Visible = false;
			notesTran.btnUpdateSave.Text = "Save";
			notesTran.GetDBID();
			notesTran.ShowDialog();
			viewNotesTab();
		}


		private void mnuAssignProvider_Click(object sender, EventArgs e)
		{
			var assignProvider = new frmAssignProvider()
			{
				Text = "Assing Provider to Employee",
				empName = EmpName,
			};
			assignProvider.GetDBID();
			assignProvider.ShowDialog();

		}

		public void viewNotesTab()
		{
			collnotes.ViewNotesToday(dgCurrentNotes, lblCountNotes, EmpName);
			collnotes.ViewNotesMonth(lblMonthly, lblAverage, EmpName);
			collnotes.ViewNotes(dgallNotesView, EmpName, position);
		}

		private void radButton2_Click(object sender, EventArgs e)
		{
			viewNotesTab();
		}

		private void InitializeAllNotes()
		{
			rdoSearch.IsChecked = true;
			rdoFilter.IsChecked = false;
			grpSearch.Enabled = true;
			grpProvider.Enabled = false;
			grpPatientName.Enabled = false;
			grpDate.Enabled = false;
			dgallNotesView.ReadOnly = true;
			FillProviderDropdown();  
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

		private void radbuttonAction()
		{
			if (rdoSearch.IsChecked == true)
			{
				grpSearch.Enabled = true;
				grpProvider.Enabled = false;
				grpPatientName.Enabled = false;
				grpDate.Enabled = false;
				viewNotesTab();
			}
			else
			{
				grpSearch.Enabled = false;
				grpProvider.Enabled = true;
				grpPatientName.Enabled = true;
				grpDate.Enabled = true;
				txtSearch.Clear();
				viewNotesTab();
			}
		}

		private void rdoSearch_ToggleStateChanged(object sender, StateChangedEventArgs args)
		{
			radbuttonAction();
		}

		private void txtSearch_TextChanging(object sender, TextChangingEventArgs e)
		{
			collnotes.SearchTextAcrossColumns(dgallNotesView, "[COLLECTOR NOTES]", txtSearch.Text, lblresultCount, EmpName);
		}

		private void useFilterSearch()
		{

			collnotes.FilterCollectorNotes(dgallNotesView, "[COLLECTOR NOTES]", lblresultCount, cmbAllProvider.Text, dtpEndate.Value.ToString("yyyy-MM-dd"), dtpStartDate.Value.ToString("yyyy-MM-dd"), txtPatientName.Text, EmpName);
		}

		private void txtPatientName_TextChanged(object sender, EventArgs e)
		{
			useFilterSearch();
		}


		private void cmbAllProvider_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			useFilterSearch();
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
				useFilterSearch();
			}
		}

		private void dtpStartDate_ValueChanged(object sender, EventArgs e)
		{
			if (dtpStartDate.Value > dtpEndate.Value)
			{
				RadMessageBox.Show("Oops! It looks like the start date is later than the end date. Could you please check and update the dates?", "Invalid Date", MessageBoxButtons.OK, RadMessageIcon.Error);
				return;
			}
			else
			{
				useFilterSearch();
			}
		}

		private void btnallRefresh_Click(object sender, EventArgs e)
		{
			if (rdoSearch.IsChecked == true)
			{
				collnotes.FilterCollectorNotes(dgallNotesView, "[COLLECTOR NOTES]", lblresultCount, cmbAllProvider.Text, dtpEndate.Value.ToString("yyyy-MM-dd"), dtpStartDate.Value.ToString("yyyy-MM-dd"), txtPatientName.Text, EmpName);
			}
			else
			{
				useFilterSearch();
			}
		}

		private void btnExportExcel_Click(object sender, EventArgs e)
		{
			task.ExportTabletoExcel(dgallNotesView, "CollectorNotes-" + EmpName, EmpName);
		}

		private void dgCurrentNotes_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dgCurrentNotes.SelectedRows.Count > 0)
			{
				var modNotes = new frmModifyNotes(EmpName, position);
				collnotes.FillNotesInfo(dgCurrentNotes, modNotes.txtIntID, modNotes.cmbProviderList, modNotes.txtChartNo, modNotes.txtPatientName, modNotes.txtNotes, modNotes.txtRemarks, EmpName);
				modNotes.Text = "View/Update Adjuster Information";
				//modAdj.btnDelete.Visible = false;
				modNotes.btnUpdateSave.Text = "Update";
				modNotes.btnDelete.Visible = false;
				modNotes.ShowDialog();
			}
			viewNotesTab();
		}

		private void dgallNotesView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (position == "Collector")
			{
				return;
			}
			else
			{
				if (dgallNotesView.SelectedRows.Count > 0)
				{
					var modNotes = new frmModifyNotes(EmpName, position);
					collnotes.FillNotesInfo(dgallNotesView, modNotes.txtIntID, modNotes.cmbProviderList, modNotes.txtChartNo, modNotes.txtPatientName, modNotes.txtNotes, modNotes.txtRemarks, EmpName);
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
			pictureBox1.SendToBack();

			if (mnuViewCollectorNotes.Text == "Close Collector Notes")
			{
				pictureBox1.BringToFront();
				mnuViewCollectorNotes.Text = "Open Collector Notes";
			}
			else
			{
				pictureBox1.SendToBack();
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
	}
}