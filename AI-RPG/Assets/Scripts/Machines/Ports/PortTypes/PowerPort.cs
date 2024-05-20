using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPort : MachinePort {
	// Start is called before the first frame update

	PowerSource ownerPowerSource;
	public override void SnapPortIn (MachinePort machinePortToSnapIn) {
		base.SnapPortIn (machinePortToSnapIn);
	}

	public override void SyncPort () {
		base.SyncPort ();
	}

	public override void RecivePortSync () {
		base.RecivePortSync ();
		if (!ownerPowerSource) {

			if (getSetOwnerMachine) {
				ownerPowerSource = (PowerSource)getSetOwnerMachine;
			} else {
				return;
			}
		}


		ownerPowerSource.ProvidePower ();
		//getConnectedPort.getSetOwnerMachine
	}
}
