using System.Collections;
using UnityEngine;
using static Interfaces;
public class GateTeleport : MonoBehaviour, Interactable , OnPointed
{
	[SerializeField] private Canvas interactCanvas;

	[SerializeField]
    GateTeleport corespondingTeleport;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    Transform targetPoint;

    public Transform getTargetPoint => targetPoint;

	#region Interactable
	public void Interact () {
		
		Debug.Log ("interract");
		//player.gameObject.SetActive (false);
		StartCoroutine (TeleportRoutine ());
	}

	IEnumerator TeleportRoutine () {
		interactCanvas.gameObject.SetActive (false);
		PlayerController.instance.ToggleController (false);
		//PlayerController.instance.DisableMovement ();
		PlayerController.instance.transform.position = corespondingTeleport.getTargetPoint.position;
		yield return null;

		CancelInteract ();
	}
	public void CancelInteract () {
		interactCanvas.gameObject.SetActive (true);
		PlayerController.instance.ToggleController (true);
	}

	public void ToggleInteractUI (bool state) {
		interactCanvas.gameObject.SetActive (state);
	}

	public void OnInterractRTS (Group group) {
		throw new System.NotImplementedException ();
	}

	public void OnInterractPerson () {
		throw new System.NotImplementedException ();
	}

	public bool isInterractable () {
		return true;
	}

	public void onPointed () {
		interactCanvas.gameObject.SetActive (true);
	}

	public void onPointRemove () {
		interactCanvas.gameObject.SetActive (false);
	}

	#endregion
}
