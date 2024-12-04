using PCMS_Lipa_General_Tool.Class;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmAIAssistant : Telerik.WinControls.UI.RadForm
	{
		private readonly OpenAIService _openAIService;

		public frmAIAssistant()
		{
			InitializeComponent();
			_openAIService = new OpenAIService("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");

			// Subscribe to the model switch event to notify users
			_openAIService.OnModelSwitched += (message) =>
			{
				RadMessageBox.Show(message, "Model Switched");
			};
		}

		// Ensure this method is marked as 'async' and returns 'void'
		private async void btnAskAI_Click(object sender, EventArgs e)
		{
			try
			{
				// Get the user input from a RadTextBox
				string userInput = txtUserInput.Text;

				if (string.IsNullOrWhiteSpace(userInput))
				{
					RadMessageBox.Show("Please enter a query.", "Input Required");
					return;
				}

				// Show a loading message while waiting for the AI response
				txtResponse.Text = "Thinking...";

				// Call the AI service
				string aiResponse = await _openAIService.SendMessageAsync(userInput);

				// Display the response in a RadTextBox
				txtResponse.Text = aiResponse;
			}
			catch (Exception ex)
			{
				// Show error messages in case of an exception
				RadMessageBox.Show($"Error: {ex.Message}", "Error");
			}
		}

		private async Task HandleSendButtonClickAsync()
		{
			try
			{
				string userInput = txtUserInput.Text;

				if (string.IsNullOrWhiteSpace(userInput))
				{
					RadMessageBox.Show("Please enter a query.", "Input Required");
					return;
				}

				txtResponse.Text = "Fetching response...";
				string aiResponse = await _openAIService.SendMessageAsync(userInput);
				txtResponse.Text = aiResponse;
			}
			catch (Exception ex)
			{
				RadMessageBox.Show($"Error: {ex.Message}", "Error");
			}
		}

		private void NotifyModelSwitch(string message)
		{
			RadMessageBox.Show(message, "Model Switched");
		}
	}
}
