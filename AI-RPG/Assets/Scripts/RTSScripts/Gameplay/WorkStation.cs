using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces;

public class WorkStation : MonoBehaviour, Interactable {
	[SerializeField]
	int maxWorkers = 4;

	[SerializeField]
	float workPointsPerProduct = 10f;

	[SerializeField]
	float workerWorkPointsValue = 1f;

	[SerializeField]
	Slider slider;

	List<BasicUnit> storedUnits;

	[SerializeField]
	[ReadOnly]
	int storedProductCount = 0;

	float workPoints = 0;

	public void CancelInteract () {
		throw new System.NotImplementedException ();
	}

	public void Interact (Player player) {
		throw new System.NotImplementedException ();
	}

	public bool isInterractable () {
		return true;
	}

	public void OnInterractPerson () {
		
	}

	public void OnInterractRTS (Group group) {
		storedUnits = group.GetUnitList;

		foreach(BasicUnit basicUnit in storedUnits) {
			basicUnit.gameObject.SetActive(false);
		}
	}

	public void ToggleInteractUI (bool state) {
		throw new System.NotImplementedException ();
	}

	private void Update () {
		if (storedUnits == null)
			return;


		foreach(BasicUnit basicUnit in storedUnits) {
			workPoints += Time.deltaTime * workerWorkPointsValue;

			//save for bar value
			Debug.Log (workPoints % workPointsPerProduct);

			slider.value = (workPoints % workPointsPerProduct) / workPointsPerProduct;

			if (workPoints > workPointsPerProduct) {
				storedProductCount++;
				workPoints = 0;
			}
		}

	}
}
