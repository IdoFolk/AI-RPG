using UnityEngine;

public static class Interfaces 
{
    public interface Interactable {
		public void Interact ();
		public void CancelInteract ();
		
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
