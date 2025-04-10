
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using PCMS_Lipa_General_Tool__WinForm_;
using System;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class FrmLogin : RadForm
	{
		private readonly Login pushLogin = new();
		private static readonly Notification notif = new();
		private static readonly User user = new();


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
			//txtUsername.Text = "Erwin";
			//txtPassword.Text = "Pcms@1234";
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			UserLogin();
		}

		private async void UserLogin()
		{
			try
			{
				string username = txtUsername.Text.Trim();
				string password = txtPassword.Text.Trim();
				loginPanel.Enabled = false; // Disable panel to prevent multiple clicks

				// Execute login asynchronously
				var (isSuccess, alertMessage, empID, empName, userName, email, firstTime, userPosition, userAccess, userDept, officeLoc, theme, empStat, isLoginPanelEnabled, isLblAlertShow) = await pushLogin.UserLoginAsync(username, password);
				//Console.WriteLine(isSuccess);

				if (isSuccess)
				{
					if (firstTime == "Yes")
					{
						var changePassword = new frmResetPassword
						{
							Text = "Change Password",
							changePassword = "Yes",
							empName = empName,
							txtEmpID = { Text = userName },
							txtWorkEmail = { Text = email }
						};
						changePassword.ShowDialog();
					}
					else
					{
						if (userPosition == "Back Office")
						{

							var demoTool = new frmDemoTool();
							try
							{
								demoTool.EmpName = empName;
								demoTool.userName = userName;
								demoTool.accessLevel = userAccess;
								demoTool.statlblUsername.Text = empName;
								demoTool.statlblAccess.Text = userAccess;
								demoTool.statlblPosition.Text = userPosition;
								demoTool.officeLoc = officeLoc;
								demoTool.employmentStat = empStat;
								demoTool.Text = $"{lblProgName.Text} | {userPosition} | ({empName})";
								Hide();
								demoTool.Show();
							}
							catch (Exception ex)
							{
								notif.LogError("demoToolsetup", empName, "Login", $"Error setting up demo user: {ex.Message}", ex);
							}
							
						}
						else if (userPosition == "Collector")
						{
							try
							{
								//var collectors = new frmCollectors
								//{
								//	EmpName = empName,
								//	UserName = userName,
								//	accessLevel = userAccess
								//};
								var collectors = new frmCollectors(empName, userName, userAccess, empID, officeLoc, empStat, userPosition);
                                //collectors.EmpName = empName;
								//collectors.u = userName;
								//collectors.accessLevel = userAccess;
                                collectors.statlblUsername.Text = empName;
								collectors.statlblAccess.Text = userAccess;
								collectors.statlblPosition.Text = userPosition;
								//collectors.employeeStat = empStat;
								collectors.Position = userPosition;
								//collectors.OfficeLoc = officeLoc;
								//collectors.employeeStat = empStat;
								//collectors.employeeID = empID;
								collectors.Text = $"{lblProgName.Text} | {userPosition} | ({empName})";
								Hide();
								collectors.Show();
							}
							catch (Exception ex)
							{
								notif.LogError("CollectorToolsetup", empName, "Login", $"Error setting up collector user: {ex.Message}", ex);
							}
							
						}
						else
						{
							try
							{
								var mainApp = new frmMainApp();
								mainApp.ConfigureVisibility(userAccess, userDept, userPosition);
								mainApp.SetMainAppProperties(empID, empName, userName, userAccess, userPosition, officeLoc, theme, empStat);
								Hide();
								mainApp.Show();
							}
							catch (Exception ex)
							{
								notif.LogError("demoToolsetup", empName, "Login", $"Error setting up {userPosition} user: {ex.Message}", ex);
								//notif.LogError("frmMainApp", empName, "Login", null, ex);
								//RadMessageBox.Show($"An error occurred while loading the main application:\n{ex.Message}", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
							}
						}

					}
				}
				else
				{
					lblalert.Visible = true;// Show alert message in Telerik label
					lblalert.Text = alertMessage;
				}
			}
			catch (Exception ex)
			{
				// Handle unexpected errors
				notif.LogError("UserLogin", txtUsername.Text, "Login", null, ex);
				//RadMessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				// Re-enable login panel for retries
				loginPanel.Enabled = true;
				txtPassword.Clear();
				txtUsername.Focus();
			}
		}



		private void txtUsername_TextChanged(object sender, EventArgs e)
		{

			if (txtUsername.Text.Length > 2)
			{
				lblalert.Text = "";
				if (Text == "Disconnected | Login")
				{
					RadMessageBox.Show("Server is offline. Please contact your Sys Admin for assistance or Retry again later. \n Sorry for the inconvenience.", "Offine", MessageBoxButtons.OK, RadMessageIcon.Error);
					notif.LogError("txtUsername_TextChanged", "Server Offline", "FrmLogin", null, null);
				}
				else
				{
					if (txtUsername.Text.Contains(";") || txtUsername.Text.Contains("--") || txtUsername.Text.Contains("/*") || txtUsername.Text.Contains("xp_"))
					{
						RadMessageBox.Show("Invalid Input", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
					}
					else
					{
						string resultMessage = user.CheckIfExistinDB(txtUsername.Text, "Login", "Login");
						if (!string.IsNullOrEmpty(resultMessage))
						{
							lblalert.Text = resultMessage;
							lblalert.Visible = true;
						}
						else
						{
							lblalert.Visible = false;
						}
						//task.CheckIfExistinDB("[User Information]", "[Username]", txtUsername, "Login", "Login", lblalert);

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
			bool isInputValid = txtPassword.Text.Length > 0 && txtUsername.Text.Length > 0;
			btnLogin.Enabled = isInputValid;
			lblalert.Text = isInputValid ? "" : lblalert.Text;
		}

		private void frmLogin_Load(object sender, EventArgs e)
		{
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
			// Open the reset password dialog
			var changePassword = new frmResetPassword
			{
				Text = "Forgot Password",
				changePassword = "forgot"
			};
			changePassword.btnOK.Text = "Update Password";
			changePassword.lblTemp.Text = "Username";
			changePassword.ShowDialog();

			// Reset the login form using the refactored Login class
			var loginManager = new Login();
			var (username, password, isLoginPanelEnabled, alertMessage) = loginManager.DefaultLoginSet();

			// Update the UI after resetting
			txtUsername.Text = username;
			txtPassword.Text = password;
			loginPanel.Enabled = isLoginPanelEnabled;
			lblalert.Text = alertMessage;
		}


		private void txtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				UserLogin();
			}

		}
	}
}
