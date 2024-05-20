using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Sirenix.OdinInspector;
using static Interfaces;
using static Delegates;

public class InventoryHandler : SerializedMonoBehaviour
{
    float maxInventorySpace = 30;
    public float getMaxInventorySpace;

    [ReadOnly]
    [SerializeField]
    Dictionary<Interfaces.InventoryHeldObject ,float> inventoryHeldObjects = new();
	public Dictionary<Interfaces.InventoryHeldObject, float> getInventoryHeldObjects => inventoryHeldObjects;

    [HideInInspector]
    public Delegate_Void delegate_OnInventoryChange;

	public void AddItemToInventory (InventoryHeldObject newInventoryHeldObject, float quantity) {
        if (inventoryHeldObjects.Count == maxInventorySpace)
            return;

        if (inventoryHeldObjects.ContainsKey (newInventoryHeldObject)) {
            inventoryHeldObjects[newInventoryHeldObject] += quantity;
        } else {
            inventoryHeldObjects.Add (newInventoryHeldObject,quantity);
        }

        delegate_OnInventoryChange?.Invoke ();

	}


}
