using UnityEngine;

public static class Interfaces 
{
    public interface Interactable {
		public void Interact (Player player);
		public void CancelInteract ();
		public void ToggleInteractUI (bool state);

	}
}
