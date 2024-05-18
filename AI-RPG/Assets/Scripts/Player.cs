using System;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase _thirdPersonCamera;
    [SerializeField] private ThirdPersonController _thirdPersonController;
    private bool _canInteract;

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        if (_canInteract)
        {
            _thirdPersonCamera.Priority = 0;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            _canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            _canInteract = false;
        }
    }
}
