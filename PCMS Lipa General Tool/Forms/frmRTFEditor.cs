using editform; // INCLUDE IN PROJECT AND REFERENCE
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmRTFEditor : Form
	{
		public string accessLevel;
		public frmRTFEditor()
		{
			InitializeComponent();
		}
		//public testonly(string[] arguments)
		//{
		//	if (arguments.Length > 0)
		//	{
		//		file = arguments[0];
		//	}
		//	InitializeComponent();
		//}

		// Prevent Low Level WINAPI errors from a recent disk removal
		// Revised: 04-10-2016
		// http://stackoverflow.com/questions/6080605/can-i-use-seterrormode-in-c-sharp-process
		// https://msdn.microsoft.com/en-us/library/aa288468%28v=vs.71%29.aspx#pinvoke_callingdllexport
		// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680621%28v=vs.85%29.aspx

		[DllImport("kernel32.dll")]
		private static extern ErrorModes SetErrorMode(ErrorModes uMode);
		[Flags]
		public enum ErrorModes : uint
		{
			SYSTEM_DEFAULT = 0x0,
			SEM_FAILCRITICALERRORS = 0x0001, // the one to use
			SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,
			SEM_NOGPFAULTERRORBOX = 0x0002,
			SEM_NOOPENFILEERRORBOX = 0x8000
		}
		public string file;

		public void testonly_Load(object sender, EventArgs e)
		{
			SetErrorMode(ErrorModes.SEM_FAILCRITICALERRORS); // set on startup
			editor ed = new();
			if (accessLevel == "User")
			{
				ed.AllowDiscAccess = false;
				ed.WindowTitle = "QuiQui RTF Editor";
				ed.StartingFont = new Font("Courier New", 12.0f, FontStyle.Regular);
				ed.StartingWindowSize = new Size(1300, 800);
				ed.FileToOpen = file;
				ed.UseAdvancedPrintForm = false;
				ed.UseSpeechRecognition = false;
				ed.UseSpellCheck = false;
			}
			else
			{
				ed.AllowDiscAccess = true;
				ed.WindowTitle = "QuiQui RTF Editor";
				ed.StartingFont = new Font("Courier New", 12.0f, FontStyle.Regular);
				ed.StartingWindowSize = new Size(1300, 800);
				ed.FileToOpen = file;
				ed.UseAdvancedPrintForm = true;
				ed.UseSpeechRecognition = true;
				ed.UseSpellCheck = true;
			}


			// ed.StartingWindowSize = new Size(1000, 532);
			if (file != String.Empty)
			{
				ed.FileToOpen = file;
			}

			ed.DisplayEditForm(this);
			this.Close();
		}
	}
}
