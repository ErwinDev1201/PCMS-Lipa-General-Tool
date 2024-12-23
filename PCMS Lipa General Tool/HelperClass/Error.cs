using PCMS_Lipa_General_Tool.Class;
using System;

namespace PCMS_Lipa_General_Tool.HelperClass
{
	public class Error
	{
		readonly WinDiscordAPI dc = new();
		public void LogError(string processName, string empName, string module, string ID, Exception ex)
		{
			int maxlengthforDC = 399;


			// Ensure the substring length does not exceed the actual string length
			string detailedError = ex.ToString().Length > maxlengthforDC
				? ex.ToString().Substring(0, maxlengthforDC)
				: ex.ToString();

			var errorMessage = $@"
				Error: {ex.Message}
				Name: {empName}
				Module: {module}
				Process: {processName}
				ID: {ID}
				Detailed Error: {detailedError}";

			dc.PublishtoDiscord(
				Global.errorNameSender,
				string.Empty,
				errorMessage,
				empName,
				Global.DCErrorWebHook,
				Global.DCErrorInvite);
		}
	}
}
