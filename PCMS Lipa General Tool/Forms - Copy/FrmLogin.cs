﻿using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using YourmeeAppLibrary.Discord;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class FrmLogin : Telerik.WinControls.UI.RadForm
	{
		private readonly Login pushLogin = new();
		private readonly CommonTask task = new();

		public FrmLogin()
		{
			InitializeComponent();
			txtUsername.Focus();
			//CenterText(txtUsername);

			lblalert.Visible = false;
			lblProgName.Text = "Program Name: " + Global.ProgName;
			lblProgVersion.Text = "Program Version: " + Global.ProgVer;
			lblDeveloper.Text = "Developer: " + Global.Dev;
			btnLogin.Enabled = false;

			// show password eye
			this.txtPassword.UseSystemPasswordChar = true;
			RadButtonElement btn = new();
			btn.Click += btn_Click;
			this.txtPassword.RightButtonItems.Add(btn);
			btn.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
			btn.BorderElement.Visibility = ElementVisibility.Collapsed;
			var font1 = ThemeResolutionService.GetCustomFont("Font Awesome 5 Free Regular");
			btn.CustomFont = font1.Name;
			btn.CustomFontSize = 8;
			//btn.Text = "\ue13D";
			btn.Text = "\ue052";
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			var loginUI = new FrmLogin();
			pushLogin.UserLogin(txtUsername, txtPassword, loginPanel, lblalert, loginUI);
			if (lblalert.Text == "")
			{
				loginUI.Hide();
				Hide();
			}
		}


		private void txtUsername_TextChanged(object sender, EventArgs e)
		{

			if (txtUsername.Text.Length > 2)
			{
				lblalert.Text = "";
				if (Text == "Disconnected | Login")
				{
					RadMessageBox.Show("Server is Offline, Please check with Sys Admin", "Offine", MessageBoxButtons.OK, RadMessageIcon.Error);
					winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", "Erwin PCMS GenTool Server is Offline", "", Global.DCErrorWebHook, Global.DCErrorInvite);
				}
				else
				{
					if (txtUsername.Text.Contains(";") || txtUsername.Text.Contains("--") || txtUsername.Text.Contains("/*") || txtUsername.Text.Contains("xp_"))
					{
						RadMessageBox.Show("Invalid Input", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
					}
					else
					{
						task.CheckIfExistinDB("[User Information]", "[Username]", txtUsername, "Login", "Login", lblalert);
					}
				}
			}
			else if (txtPassword.Text.Length > 3 && txtUsername.Text.Length > 0)
			{
				btnLogin.Enabled = true;
			}
			else
			{
				btnLogin.Enabled = false;
				lblalert.Visible = false;
			}
		}

		private void txtPassword_TextChanged(object sender, EventArgs e)
		{
			if (txtPassword.Text.Length > 0 && txtUsername.Text.Length > 0)
			{
				btnLogin.Enabled = true;
				lblalert.Text = "";
			}
			else
			{
				btnLogin.Enabled = false;
				//lblalert.Text = "";
			}
		}

		private void frmLogin_Load(object sender, EventArgs e)
		{
			//txtUsername.ShowEmbeddedLabel = true;
			//txtUsername.EmbeddedLabelText = "Username:";

			//txtPassword.ShowEmbeddedLabel = true;
			//txtPassword.EmbeddedLabelText = "Password:"
			frmLoginTimer.Start();
			pushLogin.CheckConnectivity();
			Text = pushLogin.conStatus.ToString();
		}


		private void btn_Click(object sender, EventArgs e)
		{
			this.txtPassword.UseSystemPasswordChar = !this.txtPassword.UseSystemPasswordChar;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var changePassword = new frmResetPassword
			{
				Text = "Forgot Password",
				changePassword = "forgot"

			};
			changePassword.btnOK.Text = "Update Password";
			changePassword.lblTemp.Text = "Username";
			changePassword.ShowDialog();
			pushLogin.DefaultLoginSet(txtUsername, txtPassword, loginPanel, lblalert);
		}

		private void txtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				var loginUI = new FrmLogin();
				pushLogin.UserLogin(txtUsername, txtPassword, loginPanel, lblalert, loginUI);
				if (lblalert.Text == "")
				{
					loginUI.Hide();
					Hide();

				}
			}
		}

		private void frmLoginTimer_Tick(object sender, EventArgs e)
		{
			//DateTime phTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time");
			//string time = phTime.ToString();
		}

		private void radButton1_Click(object sender, EventArgs e)
		{
			winDiscordAPI.PublishtoDiscord("ApName", "AppTitle", "AppMessage", "AppAuthor", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
		}
	}
}
