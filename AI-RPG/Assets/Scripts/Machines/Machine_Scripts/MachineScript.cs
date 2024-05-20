using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
	[SerializeField]
	[ReadOnly]
	public Machine ownerMachine;


	public virtual void OnSync () {

	}



#if UNITY_EDITOR
	[OnInspectorInit]
	void SetupMachineScript () {
		if (!ownerMachine)
			ownerMachine = transform.GetComponent<Machine> ();
	}
#endif
}
