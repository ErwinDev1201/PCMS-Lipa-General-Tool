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

		[DllImport("kernel32.dll")]
		static extern ErrorModes SetErrorMode(ErrorModes uMode);
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

		private void frmRTFEditor_Load(object sender, EventArgs e)
		{
			SetErrorMode(ErrorModes.SEM_FAILCRITICALERRORS); // set on startup
			editor ed = new();
			ed.AllowDiscAccess = true;
			ed.WindowTitle = "Quick Edit";
			ed.StartingFont = new Font("Courier New", 12.0f, FontStyle.Regular);
			ed.StartingWindowSize = new Size(1300, 800);
			ed.UseAdvancedPrintForm = true;
			ed.UseSpeechRecognition = true;
			ed.UseSpellCheck = true;

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
