using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuCategory_Inventory : UI_MenuCategory {
	
	[SerializeField]
	List<Ui_InventorySlot> inventorySlots;

	Ui_InventorySlot currentSelectedSlot;

	[SerializeField]
	UI_DataPreview dataPreview;

	public override void InitMenuCategory () {
		base.InitMenuCategory ();

		InitInventorySlots();
	}

	public void InitInventorySlots () {
		foreach(Ui_InventorySlot slot in inventorySlots) {
			slot.onSlotSelected += OnSlotSelected;
			slot.Init ();
		}
	}

	public void OnSlotSelected (Ui_InventorySlot selectedInventorySlot) {
		currentSelectedSlot = selectedInventorySlot;

		if(dataPreview)
		dataPreview.SetDataToPresent (selectedInventorySlot.getSlotData);
	}

	bool inventoryAlreadyContainsData;
	public void SyncUiInventoryToInventory (InventoryHandler InventoryHandler) {
		for (int i = 0; i < InventoryHandler.getMaxInventorySpace; i++) {
			inventoryAlreadyContainsData = false;

			if (inventorySlots.Count == i) {
				Debug.LogError (InventoryHandler.transform.root.name + " No Available UI Slot for " + InventoryHandler.getInventoryHeldObjects.Keys.ToArray ()[i].UISlotDataGetName ());
			}

			if (!(InventoryHandler.getInventoryHeldObjects.Keys.ToArray ().Length > i))
				return;

			//If slot already contains slotData
			foreach (Ui_InventorySlot ui_InventorySlot in inventorySlots) {
				if (ui_InventorySlot.getSlotData != null) {
					if (ui_InventorySlot.getSlotData == InventoryHandler.getInventoryHeldObjects.Keys.ToArray ()[i]) {
						ui_InventorySlot.SyncSlot (InventoryHandler.getInventoryHeldObjects.Keys.ToArray ()[i], InventoryHandler.getInventoryHeldObjects.Values.ToArray ()[i]);
						inventoryAlreadyContainsData = true;
						break;
					}
				}
			}

			if (!inventoryAlreadyContainsData) {
				foreach (Ui_InventorySlot ui_InventorySlot in inventorySlots) {
					if (ui_InventorySlot.getSlotData == null) {
						ui_InventorySlot.SyncSlot (InventoryHandler.getInventoryHeldObjects.Keys.ToArray ()[i], InventoryHandler.getInventoryHeldObjects.Values.ToArray ()[i]);
						break;
					}
				}
			}
		}
	}
}
