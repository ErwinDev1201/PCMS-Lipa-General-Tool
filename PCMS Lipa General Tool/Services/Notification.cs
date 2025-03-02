using PCMS_Lipa_General_Tool.Class;
using System;
using System.Drawing;
using System.Reflection;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Services
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
		
		public void NotifyTask(string empName, string taskName, string taskID, string taskStatus, string descripiton, string reportedby, string assignee)
		{
			string message = $@"
			{taskID} has been assigned to {assignee}.

			Task Name: {taskName}
			Description: {descripiton}
			Status: {taskStatus}
			Reported by: {reportedby}";
			dc.PublishtoDiscord(
				Global.TaskNameSender,
				string.Empty,
				message,
				empName,
				Global.DCTaskWebHook,
				Global.DCTaskInvite);
		}

		public void SendToastNotifDesktop(string message, string alertType)
		{
			RadDesktopAlert alert = new()
			{
				AutoClose = true,
				AutoCloseDelay = 7, //seconds to
				CaptionText = Assembly.GetExecutingAssembly().GetName().Name.ToString(),
				//AutoSize = true,
				FixedSize = new Size(380, 120),
				ScreenPosition = AlertScreenPosition.BottomRight,
				Opacity = 0.9f,

			};

			Bitmap icon = null;

			icon = alertType switch
			{
				"Success" => SystemIcons.Information.ToBitmap(),
				"Warning" => SystemIcons.Warning.ToBitmap(),
				"Error" => SystemIcons.Error.ToBitmap(),
				_ => SystemIcons.Application.ToBitmap(),
			};
			if (icon != null)
			{
				RadLabelElement imageElement = new()
				{
					Image = icon,
					ImageAlignment = ContentAlignment.MiddleLeft,
					TextAlignment = ContentAlignment.MiddleRight,
					Text = message,
					TextWrap = true,
					StretchHorizontally = true
				};

				// Replace the content of the popup with a custom layout
				alert.Popup.AlertElement.ContentElement.Children.Clear();
				alert.Popup.AlertElement.ContentElement.Children.Add(imageElement);
			}
			alert.Show();
		}
	}
}