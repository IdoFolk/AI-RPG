using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Data_Ability : ScriptableObject
{
	[SerializeField]
	InputConfig inputConfig;
	public InputConfig getInputConfig => inputConfig;

	[Space(5)]

	[SerializeField]
	string animation;

    PlayerController ownerPlayerController;
	public PlayerController getOwnerPlayerController => ownerPlayerController;
    public virtual void init (PlayerController playerController) {
		ownerPlayerController = playerController;
		

    }

	public abstract void UseAbility ();
}

[Serializable]
public class InputConfig {
	[ValueDropdown ("getInputNames")]
	[SerializeField]
	string actionName;
	public string getActionName => actionName;

	public List<string> getInputNames => GetAllInputNames ();

#if UNITY_EDITOR

	public List<string> GetAllInputNames () {
		GameControls inputActions = new GameControls ();
		List<string> inputStrings = new List<string> ();

		foreach (var action in inputActions) {
			inputStrings.Add (action.name);
		}

		return inputStrings;
	}

#endif
}
