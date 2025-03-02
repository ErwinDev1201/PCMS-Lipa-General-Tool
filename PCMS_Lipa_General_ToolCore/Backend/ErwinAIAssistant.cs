using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Class
{
	public class OpenAIService
	{
		private readonly string _apiKey;
		private readonly RestClient _client;
		private string _currentModel = "gpt-3.5-turbo"; // Default to GPT-4
		private const int MaxTokensPerRequest = 4096; // GPT-4 token limit
		private const int MaxMessageHistory = 5; // Retain 5 recent messages for context
		private readonly List<dynamic> _messages;

		public event Action<string> OnModelSwitched; // Event to notify when the model switches

		public OpenAIService(string apiKey)
		{
			_apiKey = apiKey;
			_client = new RestClient("https://api.openai.com/v1");
			_messages =
		[
			new { role = "system", content = "You are a helpful assistant." }
		];
		}

		public async Task<string> SendMessageAsync(string userInput)
		{
			try
			{
				// Add user message to history
				_messages.Add(new { role = "user", content = userInput });

				// Trim older messages if the history exceeds the limit
				if (_messages.Count > MaxMessageHistory)
				{
					_messages.RemoveAt(1); // Remove the oldest non-system message
				}

				// Prepare the request body
				var body = new
				{
					model = _currentModel,
					messages = _messages.ToArray(),
					max_tokens = 500,
					temperature = 0.7
				};

				// Make the API request
				var request = new RestRequest("chat/completions", RestSharp.Method.Post);
				request.AddHeader("Authorization", $"Bearer {_apiKey}");
				request.AddJsonBody(body);

				var response = await _client.ExecuteAsync(request);

				if (!response.IsSuccessful)
				{
					// Check for model not found or other specific errors
					if (response.Content.Contains("model 'gpt-4' does not exist"))
					{
						_currentModel = "gpt-3.5-turbo"; // Fallback to gpt-3.5-turbo
						RadMessageBox.Show("GPT-4 is not available. Switching to GPT-3.5-turbo.", "Model Fallback");
						return await SendMessageAsync(userInput); // Retry with the fallback model
					}

					throw new ApplicationException($"API Error: {response.StatusCode} - {response.Content}");
				}

				// Parse the response
				dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);
				string aiResponse = result.choices[0].message.content.ToString();

				// Add AI response to the message history
				_messages.Add(new { role = "assistant", content = aiResponse });

				return aiResponse;
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Error while processing request: {ex.Message}", ex);
			}
		}


		private async Task SwitchToGpt35Async()
		{
			_currentModel = "gpt-3.5-turbo";
			OnModelSwitched?.Invoke("Switched to GPT-3.5-turbo due to GPT-4 quota limit.");
			await Task.CompletedTask; // Simulate async behavior
		}

		public void ResetHistory()
		{
			_messages.Clear();
			_messages.Add(new { role = "system", content = "You are a helpful assistant." });
		}
	}
}