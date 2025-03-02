using PCMS_Lipa_General_Tool.Class;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmVersion_History : Telerik.WinControls.UI.RadForm
	{

		public string empName;
		public string accessLevel;

		public frmVersion_History()
		{
			InitializeComponent();
			versionHistory();
		}


		private void versionHistory()
		{
			txtVersionHistory.Text =
				"PCMS Lipa General Tool\n" + Global.PCMSLipaGenTool +
				"\n" +
				"\n" +
				"\n\n\n ================================================================================" +
				"\n\n\n PCMS Private Collector Tool\n" + Global.Description +
				"\n\n\n PCMS Workers Comp Tool\n" + Global.WCDescription +
				"\n\n\n PCMS Demo Tool Upper Lower Tool\n" + Global.DescDemo;

			//"\n\tInitial Release as Upper Lower case Tool (version 1.00.00)" +
			//"\n\tConverts lower upper case, word in output you desire" +
			//"\n07/30/2016" +
			//"\n12/07/2017" +
			//"\n\tUpper Lower case Tool (version 1.00.00)" +
			//"\n\tRemoves all the punctuation copied from the input, added Title Case" +
			//"\n05/01/2019" +
			//"\n\tBecome PCMS Demographer Tool with online Logins, Insurance information" +
			//"\n06/25/2019" +
			//"\n\tMerged with PCMS Private Collector and called as PCMS Private Tool" +
			//"\n07/16/2019 - Fixed updating error in logins" +
			//"\n09/04/2019 - Automatic Replace of ave, highway, etc" +
			//"\n09/18/2019 - Merged the Private Collector Tool, Demographer Tool, Workers Comp Tool" +
			//"\n09/24/2019 - Autobackup of online logins database on exit for Administrator. Modify Codes for checking instance of program running" +
			//"\r10/24/2019 - Additional Personal/General reminders in form of excel file and word Doc File. Modify the back Process, back up first in network drive, if network drive is not available. D:\\PCMSGenToolBackup\\Online Logins.csv is the default backup locationn\t\tFix UI with floating text box" +
			//"\n10/25/2019 - Modified Previous bug and code error of the previous version \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"11/07/2019 - Transfer database to VM Server and modified code to about login for web path \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"11/07/2019 - Fixed program crashed for demo windows \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"03/17/2020 - Overall Adjustment for administrator/Programmer Access & remodification of replacing function \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"03/23/2020 - Added Online Logins Count \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"07/24/2020 - Modify code in preparing Transfer in RD Web Test \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"08/11/2020 - Added security and modified some function as suggested by sir Ron. \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"08/14/2020 - Modified glitches and enhanced UI. \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"08/18/2020 - Updated Demo General Reminder, Private Collectors General Reminder and Personal Reminder. Modification of function Making password box covered all the time unless, user click show password. Modified all codes for faster execution. \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"08/21/2020 - Modified all the bugs and glitches from version 03.06.02. \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"09/29/2020 - Modified process of backup, autosend email to developer if there is an error. 03.06.05 the update was not documented. \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"09/30/2020 - Any changes in online access will be email by the Tool \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"10/15/2020 - Added ICD 10 Diagnosis Reference \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"10/29/2020 - Password Hidden in Table, Browser To used return \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"12/11/2020 - Update sending email. Host and email distrib list is now in config \\n\\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"12/20/2020 - Fixed Group login issue \\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"12/23/2020 - Updated UI Active X (mahapps version 2.4.3 to 2.4.9) \\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"11/13/2022 - Fixed bug missing show password \\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"11/13/2022 - Transfer all temporary textbox to memory cache \\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"11/13/2022 - Fixed missing notification in email if the password updating is from Collectors Windows \\n\" +\r\n\t\t\t\t\t\t\t\t\t\t\"12/23/2020 - Updated UI Active X (mahapps version 1.4 to 2.4.3) \\n\" +\r\n";
		}


	}
}
