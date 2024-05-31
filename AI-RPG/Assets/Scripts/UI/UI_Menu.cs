using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using System;
using UnityEngine.UI;

public class UI_Menu : SerializedMonoBehaviour
{
    public enum MenuCategory { 
        Inventory = 10,
        Crafting = 20,
        Map = 30,
        Stats = 40,
		Chat = 50,
	}

    [SerializeField]
    [HideReferenceObjectPicker]
    [ListDrawerSettings]
    Dictionary<MenuCategory, UI_MenuCategory> menuCategorys = new();

	public Dictionary<MenuCategory, UI_MenuCategory> getMenuCategories => menuCategorys;

    InventoryHandler userInventoryHandler;

    public Delegates.Delegate_Void delegate_OnMenuClosed;


	GraphicRaycaster graphicRaycaster;
	public GraphicRaycaster getGraphicRaycaster => graphicRaycaster;


	private void Start () {
		graphicRaycaster = GetComponent<GraphicRaycaster> ();
	}

	public void InitInventoryCategory (InventoryHandler _userInventoryHandler = null) {
        if (menuCategorys.Keys.Contains (MenuCategory.Inventory))
            if (_userInventoryHandler) {
                if(userInventoryHandler != _userInventoryHandler) {

                    if(userInventoryHandler)
                    userInventoryHandler.delegate_OnInventoryChange -= OnUserInventoryChange;

                    userInventoryHandler = _userInventoryHandler;
					userInventoryHandler.delegate_OnInventoryChange += OnUserInventoryChange;
				}
            } else {
                Debug.LogError("Menu :" +gameObject.name + "has inventory category but did not get a");
            }
    }

    public void OnSelectCategory (MenuCategory selectedCategory) {

        foreach(MenuCategory menuCategory in menuCategorys.Keys) {
            if(selectedCategory == menuCategory) {
                menuCategorys[menuCategory].gameObject.SetActive(true);
                menuCategorys[menuCategory].InitMenuCategory ();

				if(menuCategorys[menuCategory] is UIMenuCategory_Inventory inventoryCategory) {
                    inventoryCategory.SyncUiInventoryToInventory (userInventoryHandler);
                }


			} else {
                menuCategorys[menuCategory].gameObject.SetActive(false);
            }
        }
    }
    
    public void OnUserInventoryChange () {
		foreach (MenuCategory menuCategory in menuCategorys.Keys) {
			if (menuCategory == MenuCategory.Inventory) {
				if (menuCategorys[menuCategory] is UIMenuCategory_Inventory inventoryCategory) {
					inventoryCategory.SyncUiInventoryToInventory (userInventoryHandler);
				}
			} 
		}
		
	}

	private void OnDisable () {
        delegate_OnMenuClosed?.Invoke ();

        //remove all listed delegates???
        delegate_OnMenuClosed = null;
	}
}
