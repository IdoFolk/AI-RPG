using System;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.Events;
using TMPro;

public class ChatGPTManager : MonoBehaviour
{
    public OnResponseEvent OnResponse;
    
    private OpenAIApi _openAIApi = new();
    private List<ChatMessage> messages = new();

    public async void AskChatGPT(string text)
    {
        ChatMessage message = new();
        message.Content = text;
        message.Role = "user";
        
        messages.Add(message);

        CreateChatCompletionRequest request = new();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo-0125";

        var response = await _openAIApi.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            OnResponse?.Invoke(chatResponse.Content);
        }
    }
}

[Serializable]
public class OnResponseEvent : UnityEvent<string> { }