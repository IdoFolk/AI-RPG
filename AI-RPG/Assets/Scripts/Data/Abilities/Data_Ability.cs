using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Data_Ability : DataCreator_Data
{

	[SerializeField]
	AbilityConditions abilityConditions;

	public AbilityConditions getAbilityConditions => abilityConditions;

	[Space(5)]

	[SerializeField]
	InputConfig inputConfig;
	public InputConfig getInputConfig => inputConfig;

	[Space(5)]

	[SerializeField]
	string animation;
	public string getAnimation => animation;

	[SerializeField]
	float animationTransitionDuration;
	public float getAnimationTransitionDuration => animationTransitionDuration;


	[Space(5)]

	[SerializeField]
	AbilityMovementConfig abilityMovementConfig;

	public AbilityMovementConfig getAbilityMovementConfig => abilityMovementConfig;






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


[Tooltip ("Confines the terms for using this ability.")]
[Serializable]
public class AbilityMovementConfig {
	
	public bool holdPosition;

	[Space(5)]

	public bool useAnimationDuration;

	[HideIf ("useAnimationDuration")]
	public float  duration;

}


[Tooltip("Confines the conditions for using this ability.")]
[Serializable]
public class AbilityConditions {

	[SerializeField]
	public bool isGrounded;

}
