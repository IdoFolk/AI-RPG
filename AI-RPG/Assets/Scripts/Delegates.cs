using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ui_InventorySlot;

public static class Delegates 
{
	public delegate void Delegate_Void ();

	public delegate void Delegate_OnInventorySlotSelected (Ui_InventorySlot triggerSlot);

}
