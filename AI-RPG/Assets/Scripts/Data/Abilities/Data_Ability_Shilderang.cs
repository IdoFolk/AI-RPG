using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Shilderang")]
public class Data_Ability_Shilderang : Data_Ability
{
	public override void UseAbility () {
        base.getOwnerPlayerController.Chop ();
	}
	

}
