using UnityEngine;
using static Interfaces;
public class GateTeleport : MonoBehaviour, Interactable
{
	[SerializeField] private Canvas interactCanvas;

	[SerializeField]
    GateTeleport corespondingTeleport;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    Transform targetPoint;

    public Transform getTargetPoint => targetPoint;

	private Player _player;

	#region Interactable
	public void Interact (Player player) {
		_player = player;
		player.gameObject.SetActive (false);
		interactCanvas.gameObject.SetActive (false);
		
		player.transform.position = corespondingTeleport.getTargetPoint.position;
	}
	public void CancelInteract () {
		interactCanvas.gameObject.SetActive (true);
		_player.CancelInteract ();
	}

	public void ToggleInteractUI (bool state) {
		interactCanvas.gameObject.SetActive (state);
	}
	#endregion
}
