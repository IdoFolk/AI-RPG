using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UtilityPart : Machine
{
	public bool isActivated;

	[SerializeField]
	float requiredPower;

	LineRenderer portLineRenderer;

	// Update is called once per frame

	public delegate void Delegate_Default ();

	[HideInInspector]
	public Delegate_Default onPerformUtility;

	[HideInInspector]
	public Delegate_Default onShutDown;

	[HideInInspector]
	public Delegate_Default onActivation;

	public void PerformUtility () {
		if (!CheckActivationTerms()) {
			OnShutDown ();
			return;
		}

		onPerformUtility?.Invoke ();
	}

	public void OnShutDown () {
		isActivated = false;
		onShutDown?.Invoke ();
	}

	public bool Activate () {
		if (!CheckActivationTerms ())
			return false;

		isActivated = true;
		onActivation?.Invoke ();
		return true;
	}

	bool CheckActivationTerms () {
		if (!getOwnerMachineBase.RequestPower (requiredPower))
			return false;

		return true;
	}
}
