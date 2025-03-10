﻿using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace PCMS_Lipa_General_Tool.Services
{
	public  class emailSender
	{
		//private static readonly string EmailHost = ConfigurationManager.AppSettings["smtpserver"];
		//private static readonly string PrivTeamEmails = ConfigurationManager.AppSettings["privateTeamEmail"];
		//
		private static readonly string DCErrorWebHook = "https://discord.com/api/webhooks/1069116543734140989/DlyeR6-MZZSMd1q06fV1w3hGFjcYOONCcthuP18bOFqhbMX9d_8C1_S-8N1Pa9UE-jy2";
		private static readonly string DCErrorInvite = "https://discord.gg/4FtdrUhJ";
		private readonly WinDiscordAPI dc = new();

		public void SendEmail(string recipient, string subject, string body, string CC, string senderName, string attachmentPath)
		{
			const int maxRetryAttempts = 3;
			const int initialDelayMilliseconds = 5000; // 5 seconds
			int retryCount = 0;
			int delay = initialDelayMilliseconds;

			try
			{
				string senderEmail = "yourmeeappnoreply@gmail.com";
				string appPassword = "qrgd alxm otut euuq";

				// Use the App Password generated from Google
				
				using (SmtpClient smtpClient = new("smtp.gmail.com", 587))
				{
					smtpClient.Credentials = new NetworkCredential(senderEmail, appPassword);
					smtpClient.EnableSsl = true;

					using (MailMessage mailMessage = new())
					{
						mailMessage.From = new MailAddress(senderEmail, senderName); // Custom Sender Name
						mailMessage.To.Add(new MailAddress(recipient));
						mailMessage.Subject = subject;
						mailMessage.Body = body;
						mailMessage.IsBodyHtml = true;
						mailMessage.Bcc.Add("mr.erwinalcantara@gmail.com");
						if (!string.IsNullOrWhiteSpace(CC))
							mailMessage.CC.Add(CC);
						if (!string.IsNullOrWhiteSpace(attachmentPath) && File.Exists(attachmentPath))
						{
							mailMessage.Attachments.Add(new Attachment(attachmentPath));
						}
						smtpClient.Send(mailMessage);
					}
				}

				//RadMessageBox.Show("Email Sent Successfully!", "Success", MessageBoxButtons.OK, RadMessageIcon.Info);
			}
			catch (Exception ex)
			{
				retryCount++;
				if (retryCount >= maxRetryAttempts)
				{
					var ErrorMessage = $"Failed to send email after {retryCount} attempts.\nModule: Email Sender\nDetailed Error:\n{ex}";
					dc.PublishtoDiscord("EmailSender", "", ErrorMessage, null, DCErrorWebHook, DCErrorInvite);
					return;
					// Re-throw exception after max retries
				}
				var afterAttempt = $"Email sending failed. Retrying attempt {retryCount}/{maxRetryAttempts} in {delay / 1000} seconds...";
				dc.PublishtoDiscord("Email Sender", "", afterAttempt, null, DCErrorWebHook, DCErrorInvite);
				Thread.Sleep(delay); // Exponential backoff delay
				delay *= 2;
			}
		}

		//private readonly string _smtpServer = "smtp.gmail.com";
		//private readonly int _smtpPort = 587;
		//private readonly string _username;
		//private readonly string _password;
		//private readonly int _maxRetries;
		//private readonly int _retryDelayMilliseconds;
		//
		//public GmailEmailSender(string username, string password, int maxRetries = 3, int retryDelayMilliseconds = 2000)
		//{
		//	_username = username ?? throw new ArgumentNullException(nameof(username));
		//	_password = password ?? throw new ArgumentNullException(nameof(password));
		//	_maxRetries = maxRetries;
		//	_retryDelayMilliseconds = retryDelayMilliseconds;
		//}
		//
		//public void SendEmail(string toEmail, string subject, string body, bool isHtml = true, string attachmentPath = null)
		//{
		//	if (string.IsNullOrWhiteSpace(toEmail))
		//		throw new ArgumentException("Recipient email address cannot be null or empty.", nameof(toEmail));
		//
		//	int attempt = 0;
		//	while (true)
		//	{
		//		try
		//		{
		//			using (var client = new SmtpClient(_smtpServer, _smtpPort))
		//			{
		//				client.Credentials = new NetworkCredential(_username, _password);
		//				client.EnableSsl = true;
		//
		//				using (var message = new MailMessage(_username, toEmail, subject, body))
		//				{
		//					message.IsBodyHtml = isHtml;
		//
		//					if (!string.IsNullOrWhiteSpace(attachmentPath))
		//					{
		//						var attachment = new Attachment(attachmentPath);
		//						message.Attachments.Add(attachment);
		//					}
		//
		//					client.Send(message);
		//				}
		//			}
		//
		//			Console.WriteLine("Email sent successfully.");
		//			break;
		//		}
		//		catch (Exception ex)
		//		{
		//			attempt++;
		//			Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");
		//
		//			if (attempt >= _maxRetries)
		//			{
		//				Console.WriteLine("Max retry attempts reached. Email sending failed.");
		//				throw;
		//			}
		//
		//			Thread.Sleep(_retryDelayMilliseconds);
		//		}
		//	}
		//}




			//public void SendEmailToPrivateTeam(string content, string subject)
			//{
			//	SmtpClient SmtpServer = new("smtp." + EmailHost)
			//	{
			//		Port = 587,
			//		Credentials = new System.Net.NetworkCredential("erwin@pcmsbilling.net", "W3yHzy-j-A")
			//	};
			//	MailMessage mail = new()
			//	{
			//		From = new MailAddress("erwin@pcmsbilling.net", "PCMS Lipa General Tool")
			//	};
			//	var addresses = PrivTeamEmails;
			//	//var addresses = "Lisa@pcmsbilling.net;Gerald@pcmsbilling.net;April@pcmsbilling.net;Klaire@pcmsbilling.net;Cecile@pcmsbilling.net;Kristine@pcmsbilling.net;sarah@pcmsbilling.net;Edimson@pcmsbilling.net;joven@pcmsbilling.net;janice@pcmsbilling.net;amy@pcmsbilling.net;Ranz@pcmsbilling.net;nikki@pcmsbilling.net;Resty@pcmsbilling.net;
			//	foreach (var address in addresses.Split([";"], StringSplitOptions.RemoveEmptyEntries))
			//	{
			//		mail.To.Add(address);
			//	}
			//	mail.Bcc.Add("erwin@pcmsbilling.net");
			//	mail.Subject = subject;
			//	mail.Body = "<p style=\"font-family:Segoe UI;font-size:14px\">Hi,<br/><br/>" + content + "<br/><br/><b><i>This is an autogenerated email. Please do not reply.</i></b><br/><br/> Thank you!<br/><br/> Regards, <br/> System Administrator</p><p style=\"font-family:Segoe UI;font-size:10px\"><b>CONFIDENTIALITY NOTICE:</b> This email communication and any attachments may contain confidential and privileged information, i.e., protected patient health information, for the use of the designated recipient(s) named above. If you are not the intended recipient, (or authorized to receive for the recipient) you are hereby notified that you have received this communication in error and that any review, disclosure, dissemination, distribution or copying of it or its contents is prohibited. The Health Insurance Portability and Accountability Act of 1996 (HIPAA) protects and secures the privacy of an individual's medical information. Therefore, if you have received this communication in error please destroy all copies of this communication and any attachments and contact the sender by reply email. Thank you for your cooperation.</p>";
			//	mail.IsBodyHtml = true;
			//	SmtpServer.EnableSsl = false;
			//	SmtpServer.Send(mail);
			//}

			//public void SendEmailAppFailure(string content)
			//{
			//	string smtpHost = "smtp." + EmailHost;
			//	int smtpPort = 587;
			//	string senderEmail = "erwin@pcmsbilling.net";
			//	string senderPassword = "W3yHzy-j-A";
			//	string recipientEmail = "erwin@pcmsbilling.net";
			//	int retryCount = 3; // Number of retries
			//	int retryDelay = 2000; // Delay in milliseconds between retries
			//
			//	for (int attempt = 1; attempt <= retryCount; attempt++)
			//	{
			//		try
			//		{
			//			using SmtpClient SmtpServer = new(smtpHost);
			//			SmtpServer.Port = smtpPort;
			//			SmtpServer.Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword);
			//			SmtpServer.EnableSsl = true;
			//
			//			using MailMessage mail = new();
			//			mail.From = new MailAddress(senderEmail, "PCMS Lipa General Tool");
			//			mail.To.Add(recipientEmail);
			//			mail.Subject = "PCMS Lipa General Tool Status";
			//			mail.Body = "Hi, \n\n" + content + "\n\nRegards, \nSystem Administrator";
			//
			//			SmtpServer.Send(mail);
			//			return; // Exit the loop on success
			//		}
			//		catch (Exception ex)
			//		{
			//			if (attempt == retryCount)
			//			{
			//				var ErrorMessage = $"Failed to send email after {retryCount} attempts.\nModule: Email Sender\nDetailed Error:\n{ex}";
			//				dc.PublishtoDiscord("Email Sender", "", ErrorMessage, null, DCErrorWebHook, DCErrorInvite);
			//				return;
			//			}
			//
			//			// Delay before retrying
			//			Task.Delay(retryDelay).Wait();
			//		}
			//	}
			//}
			//
			//
			////public void SendEmailAppFailure(string content)
			////{
			////	try
			////	{
			////
			////	}
			////	catch (Exception)
			////	{
			////
			////		throw;
			////	}
			////	SmtpClient SmtpServer = new SmtpClient("smtp." + EmailHost)
			////	{
			////		Port = 587,
			////		Credentials = new System.Net.NetworkCredential("erwin@pcmsbilling.net", "W3yHzy-j-A")
			////	};
			////	MailMessage mail = new MailMessage
			////	{
			////		From = new MailAddress("erwin@pcmsbilling.net", "PCMS Lipa General Tool")
			////	};
			////	mail.To.Add("erwin@pcmsbilling.net");
			////	mail.Subject = "PCMS Lipa General Tool Status";
			////	mail.Body = "Hi, \n\n " + content + "\n\n Regards, \n System Administrator";
			////	SmtpServer.EnableSsl = false;
			////	SmtpServer.Send(mail);
			////}
			////
			//public void SendPasswordEmail(string content, string email, string subject)
			//{
			//	SmtpClient SmtpServer = new("smtp." + EmailHost)
			//	{
			//		Port = 587,
			//		Credentials = new System.Net.NetworkCredential("erwin@pcmsbilling.net", "W3yHzy-j-A")
			//	};
			//	MailMessage mail = new()
			//	{
			//		From = new MailAddress("erwin@pcmsbilling.net", "PCMS Lipa General Tool")
			//	};			
			//	mail.To.Add(email);
			//	mail.Bcc.Add("erwin@pcmsbilling.net");
			//	mail.Subject = subject;
			//	mail.Body = "<p style=\"font-family:Segoe UI;font-size:14px\">Hi,<br/><br/>" + content + "<br/><br/><b><i>This is an autogenerated email. Please do not reply.</i></b><br/><br/> Thank you!<br/><br/> Regards, <br/> System Administrator</p><p style=\"font-family:Segoe UI;font-size:10px\"><b>CONFIDENTIALITY NOTICE:</b> This email communication and any attachments may contain confidential and privileged information, i.e., protected patient health information, for the use of the designated recipient(s) named above. If you are not the intended recipient, (or authorized to receive for the recipient) you are hereby notified that you have received this communication in error and that any review, disclosure, dissemination, distribution or copying of it or its contents is prohibited. The Health Insurance Portability and Accountability Act of 1996 (HIPAA) protects and secures the privacy of an individual's medical information. Therefore, if you have received this communication in error please destroy all copies of this communication and any attachments and contact the sender by reply email. Thank you for your cooperation.</p>";
			//	mail.IsBodyHtml = true;
			//	SmtpServer.EnableSsl = false;
			//	SmtpServer.Send(mail);
			//}
			//
			//public void SendEmail(
			//	string attachmentOption,
			//	string content,
			//	string filename,
			//	string subject,
			//	string recipientEmail,
			//	string senderName,
			//	string ccEmail1 = null,
			//	string ccEmail2 = null)
			//{
			//	const int maxRetryAttempts = 3;
			//	const int initialDelayMilliseconds = 2000; // 2 seconds
			//	int retryCount = 0;
			//	int delay = initialDelayMilliseconds;
			//
			//	while (retryCount < maxRetryAttempts)
			//	{
			//		try
			//		{
			//			using SmtpClient smtpClient = new($"smtp.{EmailHost}")
			//			{
			//				Port = 587,
			//				Credentials = new System.Net.NetworkCredential("erwin@pcmsbilling.net", "W3yHzy-j-A"),
			//				EnableSsl = true // Update based on your provider's requirements
			//			};
			//
			//			using MailMessage mail = new()
			//			{
			//				From = new MailAddress("erwin@pcmsbilling.net", senderName),
			//				Subject = subject,
			//				IsBodyHtml = true,
			//				Body = GenerateEmailBody(content)
			//			};
			//
			//			// Add recipients
			//			mail.To.Add(recipientEmail);
			//			mail.Bcc.Add("mr.erwinalcantara@gmail.com"); // Mandatory BCC for logging or backup
			//
			//			// Add CC recipients if provided
			//			if (!string.IsNullOrWhiteSpace(ccEmail1))
			//				mail.CC.Add(ccEmail1);
			//
			//			if (!string.IsNullOrWhiteSpace(ccEmail2))
			//				mail.CC.Add(ccEmail2);
			//
			//			// Handle attachments
			//			if (attachmentOption == "yesAttach" && !string.IsNullOrWhiteSpace(filename))
			//			{
			//				mail.Attachments.Add(new Attachment(filename));
			//			}
			//
			//			// Send the email
			//			smtpClient.Send(mail);
			//			return; // Exit method if successful
			//		}
			//		catch (Exception ex)
			//		{
			//			retryCount++;
			//			if (retryCount >= maxRetryAttempts)
			//			{
			//				var ErrorMessage = $"Failed to send email after {retryCount} attempts.\nModule: Email Sender\nDetailed Error:\n{ex}";
			//				dc.PublishtoDiscord("EmailSender", "", ErrorMessage, null, DCErrorWebHook, DCErrorInvite);
			//				return;
			//				// Re-throw exception after max retries
			//			}
			//			var afterAttempt = $"Email sending failed. Retrying attempt {retryCount}/{maxRetryAttempts} in {delay / 1000} seconds...";
			//			dc.PublishtoDiscord("Email Sender", "", afterAttempt, null, DCErrorWebHook, DCErrorInvite);
			//			Thread.Sleep(delay); // Exponential backoff delay
			//			delay *= 2;
			//		}
			//	}
			//}
			////
			/////// <summary>
			/////// Generates the email body with a confidentiality notice and styles.
			/////// </summary>
			/////// <param name="content">The main content of the email.</param>
			/////// <returns>Formatted HTML email body.</returns>
			//private string GenerateEmailBody(string content)
			//{
			//	return $@"
			//		<p style='font-family:Segoe UI; font-size:14px;'>{content}</p>
			//		<br/><br/>
			//		<b><i>This is an autogenerated email. Please do not reply.</i></b>
			//		<br/><br/>
			//		<p style='font-family:Segoe UI; font-size:10px;'>
			//		    <b>CONFIDENTIALITY NOTICE:</b> This email communication and any attachments may contain confidential 
			//		    and privileged information, i.e., protected patient health information, for the use of the designated 
			//		    recipient(s) named above. If you are not the intended recipient (or authorized to receive for the recipient), 
			//		    you are hereby notified that you have received this communication in error and that any review, disclosure, 
			//		    dissemination, distribution, or copying of it or its contents is prohibited. The Health Insurance Portability 
			//		    and Accountability Act of 1996 (HIPAA) protects and secures the privacy of an individual's medical information. 
			//		    Therefore, if you have received this communication in error, please destroy all copies of this communication 
			//		    and any attachments and contact the sender by reply email. Thank you for your cooperation.
			//		</p>";
			//}

			//	public void SendEmail(
			//string content,
			//string subject,
			//string recipientEmail,
			//string senderName,
			//string attachmentPath = null, // Optional attachment path
			//string ccEmail1 = null,
			//string ccEmail2 = null)
			//	{
			//		const int MaxRetryAttempts = 3;
			//		const int InitialDelayMilliseconds = 2000; // 2 seconds
			//		const string SmtpServer = "smtp.gmail.com"; // Replace with your actual server
			//		const string SenderEmail = "yourmeeappnoreply@gmail.com"; // Replace with your actual email
			//		const string Password = "Pcm$Global"; // Use a secure method to retrieve this
			//		const int Port = 587;
			//
			//		int retryCount = 0;
			//		int delay = InitialDelayMilliseconds;
			//
			//		while (retryCount < MaxRetryAttempts)
			//		{
			//			try
			//			{
			//				using var smtpClient = new SmtpClient(SmtpServer)
			//				{
			//					Port = Port,
			//					Credentials = new System.Net.NetworkCredential(SenderEmail, Password),
			//					EnableSsl = true // Set based on your provider's requirements
			//				};
			//
			//				using var mailMessage = new MailMessage
			//				{
			//					From = new MailAddress(SenderEmail, senderName),
			//					Subject = subject,
			//					IsBodyHtml = true,
			//					Body = GenerateEmailBody(content)
			//				};
			//
			//				// Add recipient
			//				mailMessage.To.Add(recipientEmail);
			//
			//				// Add BCC for logging or backup
			//				mailMessage.Bcc.Add("mr.erwinalcantara@gmail.com");
			//
			//				// Add CCs if provided
			//				if (!string.IsNullOrWhiteSpace(ccEmail1))
			//					mailMessage.CC.Add(ccEmail1);
			//
			//				if (!string.IsNullOrWhiteSpace(ccEmail2))
			//					mailMessage.CC.Add(ccEmail2);
			//
			//				// Add attachment if provided
			//				if (!string.IsNullOrWhiteSpace(attachmentPath) && File.Exists(attachmentPath))
			//				{
			//					mailMessage.Attachments.Add(new Attachment(attachmentPath));
			//				}
			//
			//				// Send the email
			//				smtpClient.Send(mailMessage);
			//				return; // Exit if successful
			//			}
			//			catch (Exception ex)
			//			{
			//				retryCount++;
			//				if (retryCount >= MaxRetryAttempts)
			//				{
			//					string errorMessage = $@"
			//                Failed to send email after {retryCount} attempts.
			//                Module: Email Sender
			//                Detailed Error: {ex}";
			//					dc.PublishtoDiscord("EmailSender", "", errorMessage, null, DCErrorWebHook, DCErrorInvite);
			//					return;
			//				}
			//
			//				string retryMessage = $@"
			//            Email sending failed. Retrying attempt {retryCount}/{MaxRetryAttempts} in {delay / 1000} seconds...";
			//				dc.PublishtoDiscord("EmailSender", "", retryMessage, null, DCErrorWebHook, DCErrorInvite);
			//
			//				Thread.Sleep(delay);
			//				delay *= 2; // Exponential backoff
			//			}
			//		}
			//	}
			//
			//	/// <summary>
			//	/// Generates the email body with a confidentiality notice and styles.
			//	/// </summary>
			//	/// <param name="content">The main content of the email.</param>
			//	/// <returns>Formatted HTML email body.</returns>
		private string GenerateEmailBody(string content)
		{
			return $@"
        <p style='font-family:Segoe UI; font-size:14px;'>{content}</p>
        <br/><br/>
        <b><i>This is an autogenerated email. Please do not reply.</i></b>
        <br/><br/>
        <p style='font-family:Segoe UI; font-size:10px;'>
            <b>CONFIDENTIALITY NOTICE:</b> This email communication and any attachments may contain confidential 
            and privileged information, i.e., protected patient health information, for the use of the designated 
            recipient(s) named above. If you are not the intended recipient (or authorized to receive for the recipient), 
            you are hereby notified that you have received this communication in error and that any review, disclosure, 
            dissemination, distribution, or copying of it or its contents is prohibited. The Health Insurance Portability 
            and Accountability Act of 1996 (HIPAA) protects and secures the privacy of an individual's medical information. 
            Therefore, if you have received this communication in error, please destroy all copies of this communication 
            and any attachments and contact the sender by reply email. Thank you for your cooperation.
        </p>";
		}


		//public void SendEmail(string attachmentoption, string content, string filename, string subject, string email, string SenderName, string ccEmail1, string ccEmail2)
		//{
		//	if (attachmentoption == "yesAttach")
		//	{
		//		SmtpClient SmtpServer = new("smtp." + EmailHost)
		//		{
		//			Port = 587,
		//			Credentials = new System.Net.NetworkCredential("erwin@pcmsbilling.net", "W3yHzy-j-A")
		//		};
		//		MailMessage mail = new()
		//		{
		//			From = new MailAddress("erwin@pcmsbilling.net", SenderName)
		//		};
		//		//mail.To.Add("Edimson@pcmsbilling.net");
		//		mail.To.Add(email);
		//		//mail.To.Add("erwin@pcmsbilling.net");
		//		mail.Bcc.Add("mr.erwinalcantara@gmail.com");
		//		mail.Subject = subject;
		//		mail.Attachments.Add(new Attachment(filename));
		//		//mail.Body = "Hi, \n\n" + content;
		//		mail.IsBodyHtml = true;
		//		SmtpServer.EnableSsl = false;
		//		SmtpServer.Send(mail);
		//	}
		//	else
		//	{
		//		SmtpClient SmtpServer = new("smtp." + EmailHost)
		//		{
		//			Port = 587,
		//			Credentials = new System.Net.NetworkCredential("erwin@pcmsbilling.net", "W3yHzy-j-A")
		//		};
		//		MailMessage mail = new()
		//		{
		//			From = new MailAddress("erwin@pcmsbilling.net", SenderName)
		//		};
		//		//mail.To.Add("Edimson@pcmsbilling.net");
		//		mail.To.Add(email);
		//		//mail.To.Add("erwin@pcmsbilling.net");
		//		if (email == "erwin@pcmsbilling.net")
		//		{
		//			mail.Bcc.Add("erwin@pcmsbilling.net");
		//		}
		//		if (ccEmail1 != null)
		//		{
		//			mail.CC.Add(ccEmail1);
		//		}
		//		if (ccEmail2 != null)
		//		{
		//			mail.CC.Add(ccEmail2);
		//		}
		//
		//		//if (ccEmail != null)
		//		//{
		//		//	if (ccEmail.Contains("Angeline") || ccEmail.Contains("Shalah"))
		//		//	{
		//		//		mail.CC.Add(ccEmail);
		//		//	}
		//		//}
		//		mail.Subject = subject;
		//		mail.Body = "<p style=\"font-family:Segoe UI;font-size:14px\">" + content + "<br/><br/><b><i>This is an autogenerated email. Please do not reply.</i></b><br/><br/><p style=\"font-family:Segoe UI;font-size:10px\"><b>CONFIDENTIALITY NOTICE:</b> This email communication and any attachments may contain confidential and privileged information, i.e., protected patient health information, for the use of the designated recipient(s) named above. If you are not the intended recipient, (or authorized to receive for the recipient) you are hereby notified that you have received this communication in error and that any review, disclosure, dissemination, distribution or copying of it or its contents is prohibited. The Health Insurance Portability and Accountability Act of 1996 (HIPAA) protects and secures the privacy of an individual's medical information. Therefore, if you have received this communication in error please destroy all copies of this communication and any attachments and contact the sender by reply email. Thank you for your cooperation.</p>";
		//		mail.IsBodyHtml = true;
		//		//mail.Body = "<p style=\"font-family:Segoe UI;font-size:14px\">" + content;
		//		SmtpServer.EnableSsl = false;
		//		SmtpServer.Send(mail);
		//	}
		//	
		//}
	}
}
