using System;
using Unity.Cinemachine;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase dialogCamera;
    [SerializeField] private Canvas dialogCanvas;
    [SerializeField] private Canvas interactCanvas;
    private Player _player;

    private void Awake()
    {
        dialogCanvas.gameObject.SetActive(false);
        interactCanvas.gameObject.SetActive(false);
        dialogCamera.gameObject.SetActive(false);
    }

    public void Interact(Player player)
    {
        _player = player;
        dialogCanvas.gameObject.SetActive(true);
        dialogCamera.gameObject.SetActive(true);
        interactCanvas.gameObject.SetActive(false);
    }
    public void CancelInteract()
    {
        dialogCanvas.gameObject.SetActive(false);
        dialogCamera.gameObject.SetActive(false);
        interactCanvas.gameObject.SetActive(true);
        _player.CancelInteract();
    }

    public void ToggleInteractUI(bool state)
    {
        interactCanvas.gameObject.SetActive(state);
    }
}
