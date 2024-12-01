using PCMS_Lipa_General_Tool.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PCMS_Lipa_General_Tool
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FrmLogin());
		}
	}
}