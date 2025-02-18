using PCMS_Lipa_General_Tool.Class;
using System;

namespace PCMS_Lipa_General_Tool.HelperClass
{
	public class Notification
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
		
		public void NotifyTask(string empName, string taskName, string taskID, string taskStatus, string descripiton, string reportedby, string assignedTo)
		{
			string message = $@"
			Task ID: {taskID}
			Task Name: {taskName}
			Description: {descripiton}
			Status: {taskStatus}
			Assigned To: {assignedTo}
			Reported by: {reportedby}";
			dc.PublishtoDiscord(
				Global.TaskNameSender,
				string.Empty,
				message,
				empName,
				Global.DCTaskWebHook,
				Global.DCTaskInvite);
		}
	}
}