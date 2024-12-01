using SautinSoft.Document;
using System;
using System.Diagnostics;
using System.IO;
using ThTask = System.Threading.Tasks;

using System.Threading;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.HelperClass
{
	public class cmdTasks
	{

		//public void OpeninExcel(string filename, string userName)
		//{
		//	ExcelApp.Application app = new ExcelApp.Application();
		//	if (!File.Exists(filename + @"\PersonalReminder.xls"))
		//	{
		//		var wb = app.Workbooks.Add();
		//		wb.SaveAs(filename + @"\PersonalReminder.xls");
		//		wb.Close();
		//		app.Visible = true;
		//		app.Workbooks.Open(filename + @"\PersonalReminder.xls");
		//
		//	}
		//	else
		//	{
		//		app.Visible = true;
		//		app.Workbooks.Open(filename + @"\PersonalReminder.xls");
		//	}
		//}


		public void CreateRTFfile(string filename)
		{
			DocumentCore dc = new();
			dc.Content.End.Insert("Personal Reminder", new CharacterFormat() { FontName = "Segoe UI", Size = 16, FontColor = Color.Orange });
			dc.Save(filename);
		}


		//public bool CloseWord(string _filename)
		//{
		//	WordApp.Application app = (WordApp.Application)Marshal.GetActiveObject("Word.Application");
		//	if (app == null)
		//		return true;
		//	object saveOption = WordApp.WdSaveOptions.wdDoNotSaveChanges;
		//	object originalFormat = WordApp.WdOriginalFormat.wdOriginalDocumentFormat;
		//	object routeDocument = false;
		//	foreach (WordApp.Document d in app.Documents)
		//	{
		//		if (d.FullName.ToLower() == _filename.ToLower())
		//		{
		//			d.Close(ref saveOption, ref originalFormat, ref routeDocument);
		//			return true;
		//		}
		//	}
		//	return true;
		//}

		public void CloseallOpenDocbyProgram(string docfilename)
		{
			string processName = Path.GetFileNameWithoutExtension(docfilename);
			Process[] processes = Process.GetProcessesByName(processName);
			if (processes.Length > 1)
			{
				foreach (Process process in processes)
				{
					process.Kill();
				}
			}
		}

		//public void BackupOnlineLogins(string empName)
		//{
		//	ThTask.Task task = new ThTask.Task(() =>
		//	{
		//		while (true)
		//		{
		//			MyMethod(empName);
		//			Thread.Sleep(3600000);
		//		}
		//	});
		//	task.Start();
		//}
		//
		//private void MyMethod(string empName)
		//{
		//	try
		//	{
		//		_dbQuery.ExecutedbCollBackupCsv(empName);
		//		//cmdWorker.SendEmail("Database Backup Successfully.");
		//	}
		//	catch (Exception ex)
		//	{
		//		//mailSender.SendEmail("An error occured while running up schedule backup. See below \n\n" + ex.ToString());
		//		var ErrorMessage = "An error occured while running up schedule backup. \n Process: MyMethod \n See below \n\n" + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(ProgramVar.errorEmailNameSender, "", ErrorMessage, "", ProgramVar.DCErrorWebHook, ProgramVar.DCErrorInvite);
		//	}
		//}
		//
		//public void CreatingSheet(string pathFile, string fileName, RadGridView dgPantryList, string mailAttachment, string empName)
		//{
		//	
		//	if (File.Exists(Path.Combine(pathFile, fileName)))
		//	{
		//		File.Delete(Path.Combine(pathFile, fileName));
		//	}
		//	// creating Excel Application  			
		//	_Application app = new Application();
		//	// creating new WorkBook within Excel application  
		//	_Workbook workbook = app.Workbooks.Add(Type.Missing);
		//	// creating new Excelsheet in workbook  
		//	_Worksheet worksheet = null;
		//	// see the excel sheet behind the program  
		//	app.Visible = false;
		//	// get the reference of first sheet. By default its name is Sheet1.  
		//	// store its reference to worksheet  
		//	worksheet = workbook.Sheets["Sheet1"];
		//	worksheet = workbook.ActiveSheet;
		//	// changing the name of active sheet  
		//	worksheet.Name = "Exported from Sarisari ni Tm";
		//	// storing header part in Excel  
		//	int sum = 0;
		//	for (int i = 1; i < dgPantryList.Columns.Count + 1; i++)
		//	{
		//		worksheet.Cells[1, i] = dgPantryList.Columns[i - 1].HeaderText;
		//	}
		//	// storing Each row and column value to excel sheet  
		//
		//	for (int i = 0; i < dgPantryList.Rows.Count - 1; i++)
		//	{
		//		for (int j = 0; j < dgPantryList.Columns.Count; j++)
		//		{
		//
		//			worksheet.Cells[i + 2, j + 1] = dgPantryList.Rows[i].Cells[j].Value.ToString();
		//		}
		//		sum += Convert.ToInt32(dgPantryList.Rows[i].Cells[7].Value);
		//	}								
		//	//This code is used to save a workbook as an attachment to an email. The first line of code saves the workbook as an attachment with the specified parameters. The second			
		//	workbook.SaveAs(mailAttachment, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
		//	// line of code closes the workbook.
		//	workbook.Close(0);
		//	app.Quit();
		//}
	}
}
