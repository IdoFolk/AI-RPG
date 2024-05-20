using UnityEngine;

public static class Interfaces 
{
    public interface Interactable {
		public void Interact (Player player);
		public void CancelInteract ();
		public void ToggleInteractUI (bool state);

		void OnInterractRTS (Group group);

		void OnInterractPerson ();

		bool isInterractable ();

	}

	public interface OnPointed {

		public void onPointed ();


		public void onPointRemove ();
	}
	public interface UISlotData {

		Sprite UISlotDataGetSprite ();
		string UISlotDataGetDescription ();
		string UISlotDataGetName ();
	}

	public interface InventoryHeldObject : UISlotData {


	}

}
