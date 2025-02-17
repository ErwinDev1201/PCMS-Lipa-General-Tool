using ClosedXML.Excel;
using GemBox.Document;
using PCMS_Lipa_General_Tool.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMS_Lipa_General_Tool.HelperClass
{

	public class OfficeFiles
	{
		private static readonly Notification notif = new();


		public void CreateRtfFile(string filename)
		{
			ComponentInfo.SetLicense("FREE-LIMITED-KEY");

			// Create a new document
			var document = new DocumentModel();

			// Add a section to the document
			var section = new Section(document);
			document.Sections.Add(section);

			// Add a title "Personal Reminder" in bold format
			section.Blocks.Add(new Paragraph(document, new Run(document, "Personal Reminder")
			{
				CharacterFormat = new CharacterFormat
				{
					Bold = true,
					Size = 16, // Optional: Adjust font size for emphasis
					FontColor = Color.Black // Optional: Ensure black font color for clarity
				}
			})
			{
				ParagraphFormat = new ParagraphFormat
				{
					Alignment = HorizontalAlignment.Center // Center-align the title
				}
			});

			// Save the document as an RTF file
			document.Save(filename);
		}

		public void ExportTableToExcel(DataTable dataTable, string filename, string empName)
		{
			try
			{
				// Path to save the file on the desktop
				string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string filePath = Path.Combine(desktopPath, $"{filename}.xlsx");

				// Export to Excel using ClosedXML
				using (XLWorkbook workbook = new())
				{
					workbook.Worksheets.Add(dataTable, filename);
					workbook.SaveAs(filePath);
				}

				// Log success
				Console.WriteLine($"Export successful! File saved to: {filePath}");
			}
			catch (Exception ex)
			{
				notif.LogError("ExportTableToExcel", empName, "CommonTask", "N/A", ex);
				throw new Exception("An error occurred during the export process.", ex);
			}
		}
	}
}
