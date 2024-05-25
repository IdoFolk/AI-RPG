using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
	public static UiManager instance;

	Canvas mainCanvas;

    Camera camera;

	[SerializeField]
    UI_PlayerHud playerHud;

	[SerializeField]
	UI_Menu playerMenu;

	[SerializeField]
	UI_Menu playerMachineMenu;

	[SerializeField]
	UI_Menu machineMenu;

	[SerializeField]
	UI_Menu NpcMenu;

	[SerializeField]
	Ui_EscapeMenu escapeMenu;
	// Start is called before the first frame update

	List<UI_Menu> currentOpenMenus = new();

	FrameInput frameInput;

	bool uiInputEnabled = false;
	private void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

	void Start ()
    {
        camera= Camera.main;
	    InputHandler.delegate_FrameInput += TakeInputs;

		playerMenu.gameObject.SetActive (false);
		playerMachineMenu.gameObject.SetActive (false);
		machineMenu.gameObject.SetActive (false);
		NpcMenu.gameObject.SetActive(false);
		OpenPlayerHud ();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void TakeInputs (FrameInput newFrameInput) {
		if (!uiInputEnabled) {
			frameInput = null;
			return;
		}

		frameInput = newFrameInput;

		if (frameInput.getEscape) {
			CloseCurrentOpenMenus ();
		}
	}

	public void CloseCurrentOpenMenus () {
		if (currentOpenMenus.Count < 1)
			return;

		foreach(UI_Menu menu in currentOpenMenus) {
			menu.gameObject.SetActive (false);
		}

		OpenPlayerHud ();
		currentOpenMenus.Clear ();
		uiInputEnabled = false;

		Cursor.lockState = CursorLockMode.Locked;
		PlayerController.instance.ToggleController (true);
	}

	public void OpenPlayerMenu (UI_Menu.MenuCategory category) {
		PlayerController.instance.ToggleController (false);

		Cursor.lockState = CursorLockMode.None;

		playerMenu.gameObject.SetActive (true);
		playerMenu.InitInventoryCategory (PlayerController.instance.getCharacterHandler.getInventoryHandler);
		playerMenu.OnSelectCategory (category);
		currentOpenMenus.Add (playerMenu);

		ClosePlayerHud ();

		uiInputEnabled = true;
	}


	public void OpenMachineMenu (Machine machine) {
		PlayerController.instance.ToggleController (false);

		Cursor.lockState = CursorLockMode.None;

		playerMachineMenu.gameObject.SetActive (true);
		machineMenu.gameObject.SetActive (true);

		playerMachineMenu.InitInventoryCategory (PlayerController.instance.getCharacterHandler.getInventoryHandler);
		machineMenu.InitInventoryCategory (machine.getMachineInventoryHandler);

		playerMachineMenu.OnSelectCategory (UI_Menu.MenuCategory.Inventory);
		machineMenu.OnSelectCategory (UI_Menu.MenuCategory.Inventory);

		currentOpenMenus.Add (playerMachineMenu);
		currentOpenMenus.Add (machineMenu);

		ClosePlayerHud ();

		uiInputEnabled = true;
	}


	public void OpenNpcMenu (NPC npc) {
		PlayerController.instance.ToggleController (false);

		Cursor.lockState = CursorLockMode.None;

		NpcMenu.gameObject.SetActive (true);

		NpcMenu.OnSelectCategory (UI_Menu.MenuCategory.Chat);
		
		currentOpenMenus.Add (NpcMenu);
		
		ClosePlayerHud ();

		uiInputEnabled = true;

		NpcMenu.delegate_OnMenuClosed += npc.CancelInteract;
	}
	void OpenPlayerHud () {
		playerHud.gameObject.SetActive (true);
    }

	void ClosePlayerHud () {
        playerHud.gameObject.SetActive(false);
	}


}
