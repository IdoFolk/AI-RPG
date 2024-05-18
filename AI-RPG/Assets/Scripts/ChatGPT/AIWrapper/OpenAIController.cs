using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEditor.Recorder;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class OpenAIController : MonoBehaviour 
{
	[SerializeField] private TMP_Text nameField;
	[SerializeField] private TMP_Text textField;
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private Button okButton;

	private OpenAIAPI _api;
	private NPCAIConfig _config;
	private List<ChatMessage> _messages;
	
	private const int MAX_TOKENS = 100;
	
	void Start () {
		// This line gets your API key (and could be slightly different on Mac/Linux)
		_api = new OpenAIAPI (Environment.GetEnvironmentVariable ("OPENAI_API_KEY", EnvironmentVariableTarget.User));
	}

	public void Init (NPCAIConfig config) {
		
		this._config = config;
		_messages = new List<ChatMessage> {
			new ChatMessage(ChatMessageRole.System, "You are an NPC in a fantasy rpg video game"),
			new ChatMessage(ChatMessageRole.System, $"The world setting for the game: {config.WorldSetting.WorldDiscription}"),
			new ChatMessage(ChatMessageRole.System, $"your name is {config.Name}, " + config.NpcRoleDiscription),
			new ChatMessage(ChatMessageRole.System, $"The location you are at currently is: {config.Location.Name}," +
			                                        $" The location Description: {config.Location.LocationDiscription}, " +
			                                        $"other NPC's currently at the location as well: {config.Location.GetNPCsStringFormat()}")
		};
		
		
		nameField.text = config.Name;
		inputField.text = "";
		string startString = config.StartingDialog;
		textField.text = startString;
		
		okButton.onClick.AddListener (() => GetResponse ());
	}

	private async void GetResponse () {
		if (inputField.text.Length < 1) {
			return;
		}

		// Disable the OK button
		okButton.enabled = false;

		// Fill the user message from the input field
		ChatMessage userMessage = new ChatMessage ();
		userMessage.Role = ChatMessageRole.User;
		userMessage.TextContent = inputField.text;
		if (userMessage.TextContent.Length > 100) {
			// Limit messages to 100 characters
			userMessage.TextContent = userMessage.TextContent.Substring (0, 100);
		}
		//Debug.Log (string.Format ("{0}: {1}", userMessage.rawRole, userMessage.Content));

		// Add the message to the list
		_messages.Add (userMessage);

		// Update the text field with the user message
		textField.text = string.Format ("You: {0}", userMessage.TextContent);

		// Clear the input field
		inputField.text = "";

		// Send the entire chat to OpenAI to get the next message
		var chatResult = await _api.Chat.CreateChatCompletionAsync (new ChatRequest () {
			Model = Model.ChatGPTTurbo,
			Temperature = _config.Creativity,
			MaxTokens = MAX_TOKENS,
			Messages = _messages
		});

		// Get the response message
		ChatMessage responseMessage = new ChatMessage ();
		responseMessage.Role = chatResult.Choices[0].Message.Role;
		responseMessage.TextContent = chatResult.Choices[0].Message.TextContent;
		//Debug.Log (string.Format ("{0}: {1}", responseMessage.rawRole, responseMessage.Content));
		
		Debug.Log($"Input Tokens used: {chatResult.Usage.PromptTokens}, Output Tokens used: {chatResult.Usage.CompletionTokens}, total: {chatResult.Usage.TotalTokens}");
		
		// Add the response to the list of messages
		_messages.Add (responseMessage);

		// Update the text field with the response
		textField.text = string.Format ("You: {0}\n\nGuard: {1}", userMessage.TextContent, responseMessage.TextContent);


		//var testost = await api.TextToSpeech.SaveSpeechToFileAsync (responseMessage.Content, "D:\\Assets\\UnityAiSpechTest");
			
		// You can open it in the defaul audio player like this:
		//Process.Start ("D:\\Assets\\UnityAiSpechTest");

		// Re-enable the OK button
		okButton.enabled = true;
	}


	private async void GetTextToSpeach () {
		if (inputField.text.Length < 1) {
			return;
		}

		// Disable the OK button
		okButton.enabled = false;




		// Fill the user message from the input field
		ChatMessage userMessage = new ChatMessage ();
		userMessage.Role = ChatMessageRole.User;
		userMessage.TextContent = inputField.text;
		if (userMessage.TextContent.Length > 100) {
			// Limit messages to 100 characters
			userMessage.TextContent = userMessage.TextContent.Substring (0, 100);
		}
		Debug.Log (string.Format ("{0}: {1}", userMessage.rawRole, userMessage.TextContent));

		// Add the message to the list
		_messages.Add (userMessage);

		// Update the text field with the user message
		textField.text = string.Format ("You: {0}", userMessage.TextContent);

		// Clear the input field
		inputField.text = "";

		// Send the entire chat to OpenAI to get the next message
		var chatResult = await _api.Chat.CreateChatCompletionAsync (new ChatRequest () {
			Model = Model.ChatGPTTurbo,
			Temperature = 0.9,
			MaxTokens = MAX_TOKENS,
			Messages = _messages
		});

		// Get the response message
		ChatMessage responseMessage = new ChatMessage ();
		responseMessage.Role = chatResult.Choices[0].Message.Role;
		responseMessage.TextContent = chatResult.Choices[0].Message.TextContent;
		Debug.Log (string.Format ("{0}: {1}", responseMessage.rawRole, responseMessage.TextContent));

		// Add the response to the list of messages
		_messages.Add (responseMessage);

		// Update the text field with the response
		textField.text = string.Format ("You: {0}\n\nGuard: {1}", userMessage.TextContent, responseMessage.TextContent);

		// Re-enable the OK button
		okButton.enabled = true;
	}
}