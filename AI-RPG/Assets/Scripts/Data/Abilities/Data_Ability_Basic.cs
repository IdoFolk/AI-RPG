using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Shilderang")]
public class Data_Ability_Basic : Data_Ability
{
	public override void UseAbility () {
        base.getOwnerPlayerController.TryUseAbility (this);


		//ability logic going here?? not sure, like maybe effects? or extra things that wont be streamlined?
	}
	

}
