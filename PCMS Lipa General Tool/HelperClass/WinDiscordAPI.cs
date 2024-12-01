﻿using DiscordMessenger;
using System;
using System.IO;
using System.Net;
using System.Threading;
using YourmeeAppLibrary.Email;

public class WinDiscordAPI
{
	public void sendDisWebhook(string URL, string json)
	{
		var wr = WebRequest.Create(URL);
		wr.ContentType = "application/json";
		wr.Method = "POST";
		using (var sw = new StreamWriter(wr.GetRequestStream()))
			sw.Write(json);
		wr.GetResponse();
	}

	public void PublishtoDiscord(string AppName, string AppTitle, string AppMessage, string authorUpdate, string WebHook, string dcinvite)
	{
		try
		{
			string message = AppMessage.Length <= 4096 ? AppMessage : AppMessage.Substring(0, 4096);
			string avatar = "https://cdn.discordapp.com/attachments/1068553855463325868/1069087016404394044/pcms.png";

			// Create a new Discord message
			var discordMessage = new DiscordMessage()
				.SetUsername(AppName)
				.SetAvatar(avatar)
				.AddEmbed()
				   .SetTimestamp(DateTime.Now)
				   .SetTitle(AppTitle)
				   .SetAuthor(authorUpdate, avatar, dcinvite)
				   .SetDescription(message)
				   .SetColor(16) // Customizable color (16 in this case)
				   .SetFooter("This is an autogenerated message.")
				   .Build();

			// Send the message to the specified webhook
			discordMessage.SendMessage(WebHook);
		}
		catch (Exception ex)
		{
			// Handle exception by sending a failure email
			emailSender mailSender = new();
			mailSender.SendEmailAppFailure(ex.ToString());
		}
	}
}
