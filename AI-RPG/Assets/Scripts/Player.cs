using System;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase thirdPersonCamera;
    [SerializeField] private ThirdPersonController thirdPersonController;
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    private NPC _interactableNPC;
    private bool _isInteracting;

    private void OnValidate()
    {
        starterAssetsInputs ??= GetComponent<StarterAssetsInputs>();
        thirdPersonController ??= GetComponent<ThirdPersonController>();
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        if (_interactableNPC != null)
        {
            gameObject.SetActive(false);
            Cursor.visible = true;
            starterAssetsInputs.cursorLocked = false;
            Cursor.lockState = CursorLockMode.None;
            _interactableNPC.Interact(this);
            _isInteracting = true;
        }
    }

    public void CancelInteract()
    {
        if(!_isInteracting) return;
        gameObject.SetActive(true);
        Cursor.visible = false;
        starterAssetsInputs.cursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<NPC>() != null)
        {
            _interactableNPC = other.GetComponentInParent<NPC>();
            _interactableNPC.ToggleInteractUI(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<NPC>() != null)
        {
            _interactableNPC.ToggleInteractUI(false);
            _interactableNPC = null;
        }
    }
}
