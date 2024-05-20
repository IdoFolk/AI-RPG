using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class UI_Menu : SerializedMonoBehaviour
{
    public enum MenuCategory { 
        Inventory = 10,
        Crafting = 20,
        Map = 30,
        Stats = 40,
    }

    [SerializeField]
    [HideReferenceObjectPicker]
    [ListDrawerSettings]
    Dictionary<MenuCategory, UI_MenuCategory> menuCategorys = new();

    InventoryHandler userInventoryHandler;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
