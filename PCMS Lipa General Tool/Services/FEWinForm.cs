using System.Drawing;
using System.Reflection;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Services
{
	public class FEWinForm
	{
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
