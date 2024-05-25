using System;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static Interfaces;

public class Player : MonoBehaviour
{
 //   [SerializeField] private CinemachineVirtualCameraBase thirdPersonCamera;
 //   [SerializeField] private ThirdPersonController thirdPersonController;
 //   [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    private Interactable interactable;
    private bool _isInteracting;

    private void OnValidate()
    {
     //   starterAssetsInputs ??= GetComponent<StarterAssetsInputs>();
     //   thirdPersonController ??= GetComponent<ThirdPersonController>();
    }

    //public void OnInteract(InputAction.CallbackContext callbackContext)
    //{
    //    if (interactable != null)
    //    {
    //        if (interactable is NPC) {
    //            gameObject.SetActive (false);
    //            Cursor.visible = true;
    //           // starterAssetsInputs.cursorLocked = false;
    //            Cursor.lockState = CursorLockMode.None;
    //        }
    //            _isInteracting = true;
    //            interactable.Interact (this);
    //    }
    //}

	public void OnInteract () {
		if (interactable != null) {
			if (interactable is NPC) {
				//gameObject.SetActive (false);
				Cursor.visible = true;
				// starterAssetsInputs.cursorLocked = false;
				Cursor.lockState = CursorLockMode.None;
			}
			_isInteracting = true;
			interactable.Interact ();
		}
	}


	public void CancelInteract()
    {
        if(!_isInteracting) return;

        if (interactable is NPC) {
            //gameObject.SetActive (true);
            Cursor.visible = false;
            //starterAssetsInputs.cursorLocked = true;
            Cursor.lockState = CursorLockMode.Locked;
            _isInteracting = false;

		}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Interactable>() != null)
        {
			interactable = other.GetComponentInParent<Interactable> ();
			//interactable.ToggleInteractUI(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Interactable> () != null)
        {
			//interactable.ToggleInteractUI(false);
			//nteractable = null;
        }
    }
}
