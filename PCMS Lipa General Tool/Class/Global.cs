using System.Reflection;

namespace PCMS_Lipa_General_Tool.Class
{
	public class Global
	{


		#region UI Info
		public static string ProgName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
		public static string ProgVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		public static string Dev = "PCMS-0081 - Erwin Alcantara";
		public static string errorNameSender = "PCMS Lipa Gen Tool - App Error Support";
		public static string AppLogger = "PCMS Lipa Gen Tool - User Activity Logs";
		public static string Onloginwebhook = "https://discord.com/api/webhooks/1069052004271407235/X1u8oGuPi9RJoMK2TP3B2sUW2ABTZfOYt0AqHGA4K04e4PSKwL21mtJdi3MRelH2Kbq_";
		public static string onlogininvite = "https://discord.gg/cx3zY9XF";
		public static string leavewebhook = "https://discord.com/api/webhooks/1307793257295511602/cz1UqZYccVWqi3Ax5ku3hjxjm5iZDrbRNvfpcJMTnEPZ7NE-0GowjlrKgS5xOUYc5d0a";
		public static string leaveinvite = "https://discord.gg/TZtjnUQW/p47YCfs5";
		public static string DCErrorWebHook = "https://discord.com/api/webhooks/1069116543734140989/DlyeR6-MZZSMd1q06fV1w3hGFjcYOONCcthuP18bOFqhbMX9d_8C1_S-8N1Pa9UE-jy2";
		public static string DCErrorInvite = "https://discord.gg/4FtdrUhJ";
		public static string DCLogsWebhook = "https://discord.com/api/webhooks/1069107476764577832/_LfIkZvikzQ3o_nFm3rLNBJRNAT2OLgyIAhO48uQQAq8u-D8az1b2Q6Vty8iaqIkG1U-";
		public static string DCLogsInvite = "https://discord.gg/cx3zY9X0";
		public static string DCActivityLoggerWebhook = "https://discord.com/api/webhooks/1069107476764577832/_LfIkZvikzQ3o_nFm3rLNBJRNAT2OLgyIAhO48uQQAq8u-D8az1b2Q6Vty8iaqIkG1U-";
		public static string DCActivityLoggerInvite = "https://discord.gg/TZtjnUQW";
		public static string DescDemo = "A tool created to help the Demographers. Enhance their productivity/speed in doing the Demo. Thanks to the Management Team and Demo Team for the support in creating this tool. Open for suggestion and feedback. \n \n" +
										"07/30/2016 - Initial Release as Upper Lower case Tool (version 1.00.00) \n \t \t Converts lower upper case, word in output you desire \n" +
										"12/07/2017 - Upper Lower case Tool (version 1.00.00) \n \t \t Removes all the punctuation copied from the input, added Title Case \n" +
										"05/01/2019 - Become PCMS Demographer Tool with online Logins, Insurance information \n\n" +
										"06/25/2019 - Merged with PCMS Private Collector and called as PCMS Private Tool \n\n" +
										"07/16/2019 - Fixed updating error in logins \n\n" +
										"09/04/2019 - Automatic Replace of ave, highway, etc \n\n" +
										"09/18/2019 - Merged the Private Collector Tool, Demographer Tool, Workers Comp Tool \n\n" +
										"09/24/2019 - Autobackup of online logins database on exit for Administrator. Modify Codes for checking instance of program running \n\n" +
										"10/24/2019 - Additional Personal/General reminders in form of excel file and word Doc File. Modify the back Process, back up first in network drive, if network drive is not available. D:\\PCMSGenToolBackup\\Online Logins.csv is the default backup location \n \t \t Fix UI with floating text box \n\n" +
										"10/25/2019 - Modified Previous bug and code error of the previous version \n\n" +
										"11/07/2019 - Transfer database to VM Server and modified code to about login for web path \n\n" +
										"11/07/2019 - Fixed program crashed for demo windows \n\n" +
										"03/17/2020 - Overall Adjustment for administrator/Programmer Access & remodification of replacing function \n\n" +
										"03/23/2020 - Added Online Logins Count \n\n" +
										"07/24/2020 - Modify code in preparing Transfer in RD Web Test \n\n" +
										"08/11/2020 - Added security and modified some function as suggested by sir Ron. \n\n" +
										"08/14/2020 - Modified glitches and enhanced UI. \n\n" +
										"08/18/2020 - Updated Demo General Reminder, Private Collectors General Reminder and Personal Reminder. Modification of function Making password box covered all the time unless, user click show password. Modified all codes for faster execution. \n\n" +
										"08/21/2020 - Modified all the bugs and glitches from version 03.06.02. \n\n" +
										"09/29/2020 - Modified process of backup, autosend email to developer if there is an error. 03.06.05 the update was not documented. \n\n" +
										"09/30/2020 - Any changes in online access will be email by the Tool \n\n" +
										"10/15/2020 - Added ICD 10 Diagnosis Reference \n\n" +
										"10/29/2020 - Password Hidden in Table, Browser To used return \n\n" +
										"12/11/2020 - Update sending email. Host and email distrib list is now in config \n\n" +
										"12/20/2020 - Fixed Group login issue \n" +
										"12/23/2020 - Updated UI Active X (mahapps version 2.4.3 to 2.4.9) \n" +
										"11/13/2022 - Fixed bug missing show password \n" +
										"11/13/2022 - Transfer all temporary textbox to memory cache \n" +
										"11/13/2022 - Fixed missing notification in email if the password updating is from Collectors Windows \n" +
										"12/23/2020 - Updated UI Active X (mahapps version 1.4 to 2.4.3) \n" +
										"12/23/2020 - Fixed bug missing show password";




		public static string Description =
					  "A tool created to help the Private Collectors. Equipped with Online Logins, Provider Info, Private Insurance Info, Reminders and more. Thanks to the Management Team and Private Collectors Team for the support in creating this tool. Open for suggestion and feedback. \n \n " +
					  "Online Logins: Collection of Portal Logins \n Easy Print Denial: Easy Print Denial and possible Solution\n Provider info: Provider Information\n Reminders: Reminders and Training Material for Private\n Private Insurance Info: Insurance information address, payer id etc.\n\n" +
					  "12/21/2018 -  Initial Release (version 1.00.00) \n" +
					  "\t \t - Thanks to Lisa and Ms. April for their feeback \n " +
					  "\t \t - Online Logins, Reminders, Easy Print Denial in 1 Tool \n \n" +
					  " 01/08/2019 -  Second Release (version 1.02.00) \n" +
					  "\t \t - Added Memo Collection Guideline in Reminder Tab [requested by Ms. April]\n" +
					  "\t \t - Added Memo Rejected Claims - Private in Reminder Tab \n" +
					  "\t \t - Added Medicare vs Medicaid Definition - Private in Reminder Tab \n" +
					  "\t \t - Added List of Eval and Management Code - Private in Reminder Tab \n" +
					  "\t \t - Added List of PPO vs HMO definition - Private in Reminder Tab \n" +
					  "\t \t - Added List of PT Codes- Private in Reminder Tab \n" +
					  "\t \t - Added Provider Info Tab [requested by Ms. April] \n \n" +
					  " 02/15/2019 -  Third Release (version 1.03.00) \n" +
					  "\t \t - Added Private Insurance Info [requested by Ms. April]\n" +
					  "\t \t - Fixes Bugs and Repair UI\n" +
					  "\t \t - Allow modification in Provider Info\n" +
					  "\t \t - Added Manage User for Administrator  \n \n" +
					  " 04/04/2019 -  Fourth Release (version 1.03.02) \n" +
					  "\t \t - Added Payer ID in Priv INs Info \n" +
					  "\t \t - Activated General Reminder  \n \n" +
					  " 05/15/2019 -  Fifth Release (version 1.04.01) \n" +
					  "\t \t - Fixed Database Corruption Error \n" +
					  "\t \t - Fixed UI Interface \n" +
					  "\t \t - Activate all the buttons in Reminder Training Materials \n" +
					  "\t \t - Database Transferred \n" +
					  "\t \t - Converted SqlLite Database Structure to SQL Server Client Structure \n" +
					  "\t \t - Name of the user appear in Top of the Program \n" +
					  "\t \t - Merged Database of PCMS Demographer Tool and PCMS Private Collector Tool \n\n" +
					  " 06/25/2019 -  Sixth Release (version 2.01.03) \n" +
					  "\t \t - Merged Program (PCMS Demographer Tool and PCMS Private Collector Tool) and called as PCMS Private Tool \n\n" +
					  " 09/09/2019 -  Eight Release (version 2.01.05) \n" +
					  "\t \t - Fixed Real Time Database Updating \n\n";







		public static string WCDescription =
					 "A tool created to help the Workers Comp Collector. Equipped with MPN List, BR Directory, Adjuster Information, General Updates and more. Thanks to the Management Team for the support, specially Ms. Arlene Alhea for iniating and helping me with every details that I need to develop this tool. Open for suggestion and feedback. \n Thanks to the Management Team and WC Peeps for their feedback \n \n " +
					 "07/17/2018 -  Initial Release (version 1.02.00) \n" +
					 "\t \t - INS BR Directory, Adjuster INformation, Bundle Codes \n \n" +
					 "07/27/2018 -  Version 2.0 Release \n" +
					 "\t \t - Additional MPN List, WC REbuttals & Labor Codes, General Updates, Training Tool as Requested by Ms. Alhea" +
								 "\n \n " +
					 "09/17/2018 -  Version 2.03.00 Release \n" +
								 "\t \t-  Additional Attorney info, Email Format and Miscellaneous Tool, Additional Manage User for admin to add, delete, update user account. \n" +
								 "\t \t-  Modified MPN List Table." +
								 "\n \n " +
					 "11/14/2018 -  Version 2.04.00 Release \n" +
								 "\t \t-  Additional Hearing Rep List Information. Requested By Ms Klaire \n \n" +
					 "11/15/2018 -  Version 2.04.01 Release \n" +
								 "\t \t-  Bug Fixes - Developer Audit \n" +
								 "\t \t-  Additional Hearing Rep List Information. Requested By Ms Klaire \n \n" +
					 "12/26/2018 -  Version 2.05.00 Release \n" +
								 "\t \t-  Additional MEMO_Collection Guideline in Training Tools - Requested By Ms. Klaire \n" +
								 "\t \t-  Additional Search function in WC Rebuttals &amp; Labor Codes Tab and General Updates " +
					 "12/26/2018 -  Version 2.05.01 Release \n" +
								 "\t \t-  Added clear Text button in search box for Gen update and rebuttal \n" +
					 			 "\t \t-  Enhance Find and search script \n \n" +
					 "05/10/2019 -  Version 2.05.02 Release  \n" +
								 "\t \t-  Minor Fix and Database Transfer \n \n" +
					 "09/10/2019 -  Version 2.05.03 Release  \n" +
								 "\t \t-  Added Check Connection at start up and Running instance. \n \n" +
					 "09/18/2019 -  Version 3.00.00 Release  \n" +
								 "\t \t-  Merged the Private Collector Tool, Demographer Tool, Workers Comp Tool. \n \n" +

					 "03/27/2020 -  Version 3.03.00 Release  \n" +
								 "\t \t-  Overall adjustment for programmer/administrator access. \n \n" +
					 "03/27/2020 -  Version 3.03.01 Release  \n" +
								 "\t \t-  NO changes made to work comp tool. \n \n" +
					 "07/24/2020 -  Version 3.03.02 Release  \n" +
								 "\t \t-  NO changes made to work comp tool. \n \n" +
					 "08/11/2020 -  Version 3.04.00 Release  \n" +
								 "\t \t-  Added security and modified some function as suggested by sir Ron. \n \n" +
					 "08/14/2020 -  Version 3.05.01 Release  \n" +
								 "\t \t-  Modified glitches and enhanced UI. \n \n" +
					 "08/14/2020 -  Version 3.06.01 Release  \n" +
								 "\t \t-  Updated Demo General Reminder, Private Collectors General Reminder and Personal Reminder. Modfication of function Making password box covered all the time unless, user click show password. Modified all codes for faster execution. \n \n" +
					 "08/21/2020 -  Version 3.06.03 Release  \n" +
								 "\t \t-  Modified all the bugs and glitches from version  03.06.02. \n \n" +
					 "09/29/2020 -  Version 3.06.05 Release  \n" +
								 "\t \t-  Modified process of backup, autosend email to developer if there is an error. 03.06.04 the update was not documented. \n \n " +
					 "09/29/2020 -  Version 3.06.06 Release  \n" +
								 "\t \t-  Any changes in online access will be email by the Tool. \n " +
					 "09/29/2020 -  Version 3.08.00 Release  \n" +
								 "\t \t-  Additional ICD 10 Dx Reference. \n " +
					"09/29/2020 -  Version 3.09.00 Release  \n" +
								 "\t \t-  Update sending email. Host and email distrib list is now in config. \n " +
					"12/21/2020 -  Version 3.10.01 Release  \n" +
								 "\t \t-  Fixed Group login issue. \n " +
					"12/21/2020 -  Version 3.10.02 Release  \n" +
								 "\t \t-  Updated UI Active X(mahapps version 1.4 to 2.4.3). \n " +
					"12/21/2020 -  Version 3.10.03 Release  \n" +
								 "\t \t-  Fixed bug missing show password;. \n ";


		public static string PCMSLipaGenTool = "09/18/2019 \n\t\t- Ninth Release (version 3.00.00)" +
			"\n\t\t- Merged the Private Collector Tool, Demographer Tool, Workers Comp Tool" +
			"\n09/25/2019" +
			"\n\t\t- Tenth Release (version 3.01.00)" +
			"\n\t\t- Modify Codes for checking instance of program running" +
			"\n\t\t- Autobackup of online logins database on exit for Administrator." +
			"\n10/24/2019" +
			"\n\t\t- Eleventh Release (version 3.02.01)" +
			"\n\t\t- Additional Personal / General reminders in form of excel file and word Doc File" +
			"\n\t\t- Modify the back Process, back up first in network drive, if network drive is not available. D:\\\\PCMSGenToolBackup\\\\Online Logins.csv is the default backup location" +
			"\n10/24/2019" +
			"\n\t\t- Eleventh Release(version 3.02.02)" +
			"\n\t\t-  Modified Previous bug / code error of the previous version." +
			"\n11/01/2019" +
			"\n\t\t- twelfth Release(version 3.02.03)" +
			"\n\t\t- Modified backup code to back up every crash of program." +
			"\n11/07/2019" +
			"\n\t\t- thirteenth Release(version 3.02.05)" +
			"\n\t\t- Transfer database to VM Server and modified code to about login for web path" +
			"\n01/28/2020" +
			"\n\t\t- fourteenth  Release(version 3.02.07)" +
			"\n\t\t- Fixed program crashing when component of program not properly load" +
			"\n\t\t- Fixed empty back up created cause by code execution even when the server is not available" +
			"\n\t\t- Added employee information in menu." +
			"\n02/04/2020" +
			"\n\t\t- fifteenth Release(version 3.02.08)" +
			"\n\t\t- Fixed Bug login" +
			"\n03/17/2020" +
			"\n\t\t- Sixteenth Release(version 3.03.00)" +
			"\n\t\t- Fixed Bug login" +
			"\n03/23/2020" +
			"\n\t\t- seventeenth Release(version 3.03.01)" +
			"\n\t\t- Added Total Number of Login Count Accounts" +
			"\n03/27/2020" +
			"\n\t\t- Version 3.03.00 Release" +
			"\n\t\t- Overall adjustment for programmer/administrator access." +
			"\n07/24/2020" +
			"\n\t\t- eightenth Release (version 3.03.02)" +
			"\n\t\t- Modify code in preparing Transfer in RD Web Testing" +
			"\n08/11/2020" +
			"\n\t\t- Eightenth Release (version 3.04.00)" +
			"\n\t\t- Added security and modified some function as suggested by sir Ron" +
			"\n08/14/2020" +
			"\n\t\t- nineteenth Release (version 3.05.01)" +
			"\n\t\t- Modified glitches and enhanced UI." +
			"\n08/18/2020" +
			"\n\t\t- twentieth Release (version 3.06.01)" +
			"\n\t\t- Updated Demo General Reminder, Private Collectors General Reminder and Personal Reminder." +
			"\n\t\t- Modfication of function Making password box covered all the time unless, user click show password." +
			"\n\t\t- Modified all codes for faster execution." +
			"\n08/21/2020" +
			"\n\t\t- 21st Release (version 3.06.03)" +
			"\n\t\t- Modified all the bugs and glitches from version  03.06.02." +
			"\n09/29/2020" +
			"\n\t\t- 22nd Release (version 3.06.04)" +
			"\n\t\t- Modified process of backup, autosend email to developer if there is an error. 03.06.05 the update was not documented." +
			"\n09/30/2020" +
			"\n\t\t- 23rd Release (version 3.06.04)" +
			"\n\t\t- Any changes in online access will be email by the Tool." +
			"\n10/15/2020" +
			"\n\t\t - 24th Release (version 3.06.04)" +
			"\n\t\t- Added ICD 10 Diagnosis Reference" +
			"\n10/28/2020 - 25th Release (version 3.09.00)" +
			"\n\t\t- Password Hidden in Table, Browser To used return" +
			"\n12/11/2020" +
			"\n\t\t- 26th Release (version 3.09.00)" +
			"\n\t\t- Update sending email. Host and email distrib list is now in config." +
			"\n12/21/2020  - 27th Release (version 3.10.01)" +
			"\n\t\t- Fixed Group login issue." +
			"\n12/23/2020  - 28th Release (version 3.10.02)" +
			"\n\t\t- Updated UI Active X(mahapps version 1.4 to 2.4.3)." +
			"\n12/23/2020  - 29th Release (version 3.10.03)" +
			"\n\t\t- Fixed bug missing show password." +
			"\n10/20/2021" +
			"\n\t\t- 30th Release (version 3.10.05)" +
			"\n\t\t- Fixed missing notification in email if the password updating is from Collectors Windows" +
			"\n\t\t- Fixed issue about sending email when an error occur" +
			"\n\t\t- Added Rejected Claims Tab in Private Collector Windows." +
			"\n11/17/2021" +
			"\n\t\t- 31th Release (version 3.11)" +
			"\n\t\t- Transfer all temporary textbox to cache" +
			"\n\t\t- Manage User can now directly Update IT Information Tool" +
			"\n03/02/2023" +
			"\n\t\t- 32th Release (version 4.00)" +
			"\n\t\t- Multiple Bugs Fixed " +
			"\n\t\t- UI Changes for Collector and Administrator Windows" +
			"\n\t\t- Removing sending update in Email" +
			"\n\t\t- Used Discord to send Update via Newly created Library MayorDiscordLib.dll" +
			"\n07/10/2023" +
			"\n\t\t- 40th Major Release" +
			"\n\t\t- Overall Recoding and transfer from WPF to Windows Form (Telerik)." +
			"\n\t\t- Mahapps UI plugin stop updating last 2020. Developer already stop working on this plugin causing Major UI issue" +
			"\n\t\t- Used Telerik Plugins for UI Support" +
			"\n\t\t- Added Pantry Store to PCMS Lipa General Tool" +
			"\n\t\t- MergedUser database of PCMS Lipa General Tool and Tm Pantry Store" +
			"\n07/12/2023" +
			"\n\t\t- 41th Minor Release (ver. 05.01" +
			"\n\t\t- Fixed Bug unable to generate ID for Pantry List (Jeff) findings" +
			"\n\t\t- Reset Password Getting an error, implement logs for error checking" +
			"\n\t\t- Resize Demo Tool to smaller" +
			"\n07/14/2023" +
			"\n\t\t- 42th Minor Release (ver. 05.02)" +
			"\n\t\t- Fixed Pantry List unable to see the summary" +
			"\n\t\t- After selecting price automatic go to the quantity if quantity is missing" +
			"\n\t\t- Give the admin access to edit the pantry transaction" +
			"\n\t\t- Added Windows 7 Feel and Windows 8 Feel Themes" +
			"\n07/17/2023" +
			"\n\t\t- 43th Minor Release (ver. 05.03)" +
			"\n\t\t- Fixed issue with Manage Product not inserting the autogenerated ID" +
			"\n\t\t- Add a catch in case adding products encounter and issue" +
			"\n\t\t- Merge Libraries, to one" +
			"\n\t\t- Added Windows 7 Feel and Windows 8 Feel Themes" +
			"\n07/24/2023" +
			"\n\t\t- 44th Minor Release (ver. 05.03)" +
			"\n\t\t- Fixed ID Generation in Adding Product" +
			"\n\t\t- Fixed issue with DB Null Values" +
			"\n08/07/2023" +
			"\n\t\t- 44th Minor Release (ver. 05.05)" +
			"\n\t\t- Fixed Editing Products from Search result from TM" +
			"\n\t\t- Fixed issue with forgot password, not updating the database, Issue from jovit using forgot password feature" +
			"\n08/25/2023" +
			"\n\t\t- 45th Minor Release (ver. 05.06)" +
			"\n\t\t- error handling improvement" +
			"\n\t\t- Added office 2010 blue theme" +
			"\n\t\t- fixed bugs";
		#endregion

		#region Global Process



		#endregion

	}
}
