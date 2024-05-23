using System;
using Unity.Cinemachine;
using UnityEngine;
using static Interfaces;
public class NPC : MonoBehaviour , Interactable 
{
    [SerializeField] private NPCAIConfig aiConfig;
    [SerializeField] private OpenAIController openAIController;
    [SerializeField] private CinemachineCamera dialogCamera;
    [SerializeField] private Canvas dialogCanvas;
    [SerializeField] private Canvas interactCanvas;
    private Player _player;

    private void Awake()
    {
        dialogCanvas.gameObject.SetActive(false);
        interactCanvas.gameObject.SetActive(false);
        dialogCamera.gameObject.SetActive(false);
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        openAIController.Init(aiConfig);
    }

	#region Interactable

	public void Interact(Player player)
    {
		_player = player;
        UiManager.instance.OpenNpcMenu (this);
        
        //dialogCanvas.gameObject.SetActive(true);
        dialogCamera.gameObject.SetActive(true);
        interactCanvas.gameObject.SetActive(false);
    }
    public void CancelInteract()
    {
        //dialogCanvas.gameObject.SetActive(false);
        dialogCamera.gameObject.SetActive(false);
        interactCanvas.gameObject.SetActive(true);
        UiManager.instance.CloseCurrentOpenMenus ();

        _player.CancelInteract();
    }

    public void ToggleInteractUI(bool state)
    {
        interactCanvas.gameObject.SetActive(state);
    }

	public void OnInterractRTS (Group group) {
		throw new NotImplementedException ();
	}

	public void OnInterractPerson () {
		throw new NotImplementedException ();
	}

	public bool isInterractable () {
		throw new NotImplementedException ();
	}

	#endregion
}
