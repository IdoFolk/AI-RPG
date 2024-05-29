using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interfaces;
public class Data_Item : DataCreator_Data, Equipable {

	[SerializeField]
	string itemName;
	public string getItemName => itemName;

	[Space (20)]

	[SerializeField]
	string description;
	public string getDescription => description;

	[Space (20)]

	[SerializeField]
	Sprite uiSprite;


	[Space (20)]

	[SerializeField]
	Equipable.EquipmentSlot allowedEquipmentSlot;


	public string UISlotDataGetDescription () {
		return description;
	}

	public string UISlotDataGetName () {
		return itemName;
	}

	public Sprite UISlotDataGetSprite () {
		return uiSprite;
	}

	public Equipable.EquipmentSlot GetAllowedEquipmentSlot () => allowedEquipmentSlot;



}
