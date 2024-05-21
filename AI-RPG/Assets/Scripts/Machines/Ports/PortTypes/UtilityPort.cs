using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityPort : MachinePort {

    UtilityPart ownerUtilityPart;
	public override void RecivePortSync () {
		base.RecivePortSync ();
		if (!ownerUtilityPart) {

			if (getSetOwnerMachine) {
				ownerUtilityPart = (UtilityPart)getSetOwnerMachine;
			} else {
				return;
			}
		}

		if (!ownerUtilityPart.isActivated) {
			if (!ownerUtilityPart.Activate ())
				return;
		}

		ownerUtilityPart.PerformUtility ();
        //getConnectedPort.getSetOwnerMachine
	}
}
