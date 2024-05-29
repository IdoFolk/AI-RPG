using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Handler : MonoBehaviour
{

	[SerializeField]
	private InventoryHandler inventoryHandler;
	public InventoryHandler getInventoryHandler => inventoryHandler;

	[SerializeField]
	private Character_GraphicHandler characterGraphics;


	[SerializeField]
	private Character_AbilityHandler abilityHandler;
	public Character_AbilityHandler getAbilityHandler => abilityHandler;

	bool isPlayer;
	public bool getIsPlayer => isPlayer;
	public void InitCharacter (bool _isPlayer) {
		isPlayer = _isPlayer;
	}

}
