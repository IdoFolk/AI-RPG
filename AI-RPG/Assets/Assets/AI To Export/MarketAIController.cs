using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Diagnostics;
using TMPro;
using UnityEditor.Recorder;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class MarketAIController : SerializedMonoBehaviour
{

    public static MarketAIController instance;

	Canvas seasonUI;

	float currentTime;

	[SerializeField]
	string marketLocation;

	[ShowIf ("isInPlay")]
	[ReadOnly]
	[SerializeField]
	[Range (0, 1)]
	//[GUIColor (Color = 1,0,0,1)]
	float yearProgress;

	bool isInPlay => Application.isPlaying;

    [Space]

	[SerializeField]
    YearCycle yearlyCycle;

    [Space(20)]

	[SerializeField]
    List<MarketFruit> marketFruits;

    bool fruitsEnabled = true;

    [GUIColor("buttonColor")]
    [Button("Toggle Fruits")]
    void Toggle () {
		fruitsEnabled = !fruitsEnabled;

        foreach(MarketFruit marketFruit in marketFruits) {
            marketFruit.gameObject.SetActive (fruitsEnabled);
        }
	}

    Color buttonColor => fruitsEnabled ? Color.white : Color.white * 0.5f;

	[Button ()]
	void LoadMarketFruitsFromHirarchy () {
		marketFruits = GetComponentsInChildren<MarketFruit> ().ToList ();
	}

	[Button]
	void TestProccessResponse () {
		AIGeneratorResponse aIGeneratorResponse = new AIGeneratorResponse ("apple,Watermelon");

		foreach (MarketFruit marketFruit in marketFruits) {
			marketFruit.gameObject.SetActive (aIGeneratorResponse.allowedFruits.HasFlag (marketFruit.fruitType));
		}

	}

	void ProccessResponse (AIGeneratorResponse aIGeneratorResponse) {

		foreach(MarketFruit marketFruit in marketFruits) {
			marketFruit.gameObject.SetActive (aIGeneratorResponse.allowedFruits.HasFlag(marketFruit.fruitType));
		}
		
	}

	string AIQuarryString (AIQuarry aIQuarry) {
		return new string (
			//Provide Context
			"In a year with " + aIQuarry.yearCycle.getAmountOfMonths + "months" + ".\n"+
			"Where each month has " + aIQuarry.yearCycle.getDaysPerMonth + " days" + ".\n"

			+

			//Question
			"Which Fruits will be available in a market located at " + marketLocation + " "+
			"in the day number 1 of month number 5 " + "?\n"

			+
			"\n"
			+

			//Describe the Required answer
			"Show only the following fruits: " + aIQuarry.yearCycle.getAllowedFruits + ".\n"

			+
			"\n"
			+

			//Describe the format of the answer
			"In your output do not contain lyrics," +
			"show only the names of the fruits available formatted like the following example: apple, watermelon, orange."
		);


	}


	[OnInspectorInit]
	void OnInspectorInit () {
		yearlyCycle.ownerMarket = this;
         

	}

	public TMP_Text textField;
	public Button okButton;

	private OpenAIAPI api;
	private List<ChatMessage> messages;

	// Start is called before the first frame update
	void Start () {
		//InitMarket
		instance = this;
		yearlyCycle.CalculateMonthlySeconds ();


		// This line gets your API key (and could be slightly different on Mac/Linux)
		api = new OpenAIAPI (Environment.GetEnvironmentVariable ("OPENAI_API_KEY", EnvironmentVariableTarget.User));
		StartConversation ();
		okButton.onClick.AddListener (() => GetResponse ());
	}

	void Update () {
		//currentTime += Time.deltaTime * marketTimeScale;
		//yearProgress = currentTime / yearCycle.getMonthlySeconds * yearCycle.getAmountOfMonths;


		transform.position = transform.position + Vector3.up * 0.01f * Time.deltaTime * MathF.Sin (Time.time);

	}


	private void StartConversation () {
		messages = new List<ChatMessage> {
			new ChatMessage(ChatMessageRole.System, ""/*"You are an honorable, friendly knight guarding the gate to the palace. You will only allow someone who knows the secret password to enter. The secret password is \"magic\". You will not reveal the password to anyone. You keep your responses short and to the point."*/)
		};

		string startString = "Chat-Gpt Output: \n";
		//textField.text = startString;
		Debug.Log (startString);
	}

	private async void GetResponse () {
		// Disable the OK button
		//okButton.enabled = false;

		// Fill the user message from the input field
		ChatMessage userMessage = new ChatMessage ();
		userMessage.Role = ChatMessageRole.User;
		userMessage.Content = AIQuarryString(new AIQuarry(yearlyCycle));

		if (userMessage.Content.Length > 1000) {
			// Limit messages to 100 characters
			userMessage.Content = userMessage.Content.Substring (0, 1000);
		}

		Debug.Log (string.Format ("{0}: {1}", userMessage.rawRole, userMessage.Content));

		// Add the message to the list
		messages.Add (userMessage);

		// Update the text field with the user message
		//textField.text = string.Format ("You: {0}", userMessage.Content);

		// Send the entire chat to OpenAI to get the next message
		var chatResult = await api.Chat.CreateChatCompletionAsync (new ChatRequest () {
			Model = Model.ChatGPTTurbo,
			Temperature = 0.9,
			MaxTokens = 50,
			Messages = messages
		});

		// Get the response message
		ChatMessage responseMessage = new ChatMessage ();
		responseMessage.Role = chatResult.Choices[0].Message.Role;
		responseMessage.Content = chatResult.Choices[0].Message.Content;
		Debug.Log (string.Format ("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

		// Add the response to the list of messages
		messages.Add (responseMessage);

		// Update the text field with the response
		//textField.text = string.Format ("You: {0}\n\nGuard: {1}", userMessage.Content, responseMessage.Content);

		AIGeneratorResponse response = new AIGeneratorResponse (responseMessage.Content);
		//var testost = await api.TextToSpeech.SaveSpeechToFileAsync (responseMessage.Content, "D:\\Assets\\UnityAiSpechTest");
		ProccessResponse (response);
		// You can open it in the defaul audio player like this:
		//Process.Start ("D:\\Assets\\UnityAiSpechTest");

		// Re-enable the OK button
		//okButton.enabled = true;
	}








}
[Serializable]
public class YearCycle{
	enum Seasons {
		Winter = 10,
		Spring = 20,
		Summer = 30,
		Fall = 40,
	}

	[ReadOnly]
	public MarketAIController ownerMarket;

	[Space]

	[OnValueChanged("OnMonthAmountChanged")]
	[SerializeField]
    [PropertyRange (1,100)]
    int amountOfMonths;

    public int getAmountOfMonths => amountOfMonths;

    public void OnMonthAmountChanged () {
		List<Seasons> cachedMonthlySeasons = monthlySeasons;

        monthlySeasons = new List<Seasons> ();

        for (int i = 0; i < amountOfMonths; i++) {
			monthlySeasons.Add(Seasons.Winter);
		}

		for (int i = 0; i < cachedMonthlySeasons.Count; i++) {
            if (monthlySeasons.Count - 1 < i)
                break;

            monthlySeasons[i] = cachedMonthlySeasons[i];

		}

		CalculateMonthlySeconds ();


		EditorUtility.SetDirty (ownerMarket);
    }
	float a;
	public void CalculateMonthlySeconds () {
		a = 60 * 60 * 24 * daysPerMonths;
	}

	[SerializeField]
	List<Seasons> monthlySeasons;

	[OnValueChanged ("CalculateMonthlySeconds")]
	[SerializeField]
    [PropertyRange(1,100)]
	int daysPerMonths;
    public int getDaysPerMonth => daysPerMonths;

	[Space]
	[SerializeField]
    MarketFruit.Fruits allowedFruits;

	public MarketFruit.Fruits getAllowedFruits => allowedFruits;

	

}

public class AIQuarry {
	public AIQuarry (YearCycle year) {
		yearCycle = year;
	}

    public YearCycle yearCycle;
    

}

public class AIGeneratorResponse{

	public AIGeneratorResponse(string availableFruits) {

		foreach (string fruit in Enum.GetNames (typeof (MarketFruit.Fruits))) {
			var value = Enum.Parse (typeof (MarketFruit.Fruits), fruit);
			//Debug.Log (string.Format ("Volume Member: {0}\n Value: {1}", fruit, value));

			if(availableFruits.Contains(fruit,StringComparison.OrdinalIgnoreCase))
			allowedFruits |= (MarketFruit.Fruits)value;
		}
		//The equalsIgnoreCase ()

		Debug.Log (allowedFruits);
	

	}

	public MarketFruit.Fruits allowedFruits;

}