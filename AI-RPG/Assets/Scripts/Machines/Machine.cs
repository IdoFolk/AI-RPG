using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using Unity.Mathematics;

public class Machine : SerializedMonoBehaviour, Interfaces.OnPointed , Interfaces.Interactable
{

    [Header("MachineSetup")]

    [OnValueChanged("SetUpPorts")]
    [SerializeField]
	private List<MachinePort> machinePorts;

	[Space (5)]

	[SerializeField]
    private MachineActivationType activationType;

    enum MachineActivationType { Toggle }

	[SerializeField]
	Rigidbody rigidbody;
	public Rigidbody getRigidbody => rigidbody;


	[SerializeField]
	CarryableObject carryableObject;
	public CarryableObject getCarryableObject => carryableObject;

	[SerializeField]
	InventoryHandler inventoryHandler;
	public InventoryHandler getMachineInventoryHandler => inventoryHandler;

	[SerializeField]
	GameObject machineUi;

	[HideInInspector]
    [SerializeField]
    bool isMachineBase = false; //machinePorts.OfType<PowerPort> ().Any ();

	[SerializeField]
	OutlineEffect outlineEffect;

	[Tooltip("The Highest Machine in the machine complex hirarchy")]
	[HideIf ("isMachineBase")]
	[SerializeField]
	Machine ownerMachineBase;
	public Machine getOwnerMachineBase => isMachineBase || ownerMachineBase==null ? this : ownerMachineBase;

	[GUIColor("getMachineBaseButtonColor")]
	[Button]
	void IsMachineBase (){
		isMachineBase = !isMachineBase;
	}

	Color getMachineBaseButtonColor => isMachineBase ? Color.green : Color.red;


    [Space(10)]

	[ShowIf("isMachineBase")]
	[SerializeField]
	private float containedPower;

	[ShowIf ("isMachineBase")]
	[SerializeField]
	private float maxContainedPower;

	[ShowIf("isMachineBase")]
	[ReadOnly]
	[SerializeField]
	Dictionary<Data_Resource, int> machineInventory;

	bool isOn;


	// Start is called before the first frame update
	void Start()
    {
		Init ();
		SetUpPorts ();   
    }

	void Init () {
		if(isMachineBase)
			machineInventory = new Dictionary<Data_Resource, int> ();

		if (carryableObject) {
			carryableObject.onCarriedEnd += onCarriedEnd;
			carryableObject.onCarriedStart += onCarriedStart;
		}

	}
	// Update is called once per frame
	bool doSyncPortsThisFrame = false;
	public bool isMachineMoving => carryableObject ? (carryableObject.getCurrentVelocity != Vector3.zero || carryableObject.getIsBeingCarried) : false;

	void Update()
    {
		if (!isMachineBase)
			return;

	
		SyncPorts ();
	}

	void SyncPorts () {
		foreach (MachinePort port in machinePorts) {
			if (!port.getIsPortInited) {
				port.InitPort ();
				//port.onPortConnectedDelegate
			}

			if (!port.getIsPortInUse)
				continue;

			port.SyncPort ();


		}
	}

	#region Ports

#if UNITY_EDITOR

	[OnInspectorInit]
	[Button(Icon = SdfIconType.Recycle)]
    void SetUpPorts () {
        bool hasPowerPort = false;

		foreach (MachinePort port in machinePorts) {
			port.getSetOwnerMachine = this;

			port.SetPortDirection (isMachineBase && !(port is OutputPort) && !(port is InputPort) ? MachinePort.PortDirection.In : MachinePort.PortDirection.Out);

			if (port is PowerPort) {
				PowerPort powerPort = (PowerPort)port;

				if (!hasPowerPort) {
						hasPowerPort = true;
				}
				 
			}
		}
	}

#endif

	public void OnPortSnapped (MachinePort myPort, MachinePort otherPort) {
		if (!isMachineBase) {
			ownerMachineBase = otherPort.getSetOwnerMachine;
		}
	}

	#endregion
	public void AddResourceToMachine (Data_Resource data_Resource) {
		if (machineInventory.ContainsKey (data_Resource)) {
			machineInventory[data_Resource]++;
		} else {
			machineInventory.Add (data_Resource, 1);
		}

		if (inventoryHandler) {
			inventoryHandler.AddItemToInventory (data_Resource, 1);
		}
	}

	public bool RequestPower(float powerNeeded) {
		if(containedPower < powerNeeded) 
			return false;

		containedPower -= powerNeeded;
		return true;
	}

	public float ProvidePower (float providedPower) {
		if (containedPower == maxContainedPower)
			return 0;

		float powerNotUsed = Mathf.Clamp (containedPower + providedPower - maxContainedPower, 0,providedPower); 

		containedPower = Mathf.Clamp (containedPower + providedPower, 0, maxContainedPower);
		return powerNotUsed;
	}


	#region Interfaces
	public void onPointed () {
		if (machineUi)
			machineUi.SetActive (true);

		outlineEffect.EnableOutlines ();
	}

	public void onPointRemove () {
		if (machineUi)
			machineUi.SetActive (false);

		outlineEffect.DisableOutlines ();
	}

	public void Interact () {
		UiManager.instance.OpenMachineMenu (this);
	}

	public void CancelInteract () {
		//throw new System.NotImplementedException ();
	}

	public void OnInterractRTS (Group group) {
		//throw new System.NotImplementedException ();
	}

	public void OnInterractPerson () {
		//throw new System.NotImplementedException ();
	}

	public bool isInterractable () {
		return true;
	}

	#endregion

	void onCarriedStart () {
		DetachOutPorts ();
	}

	void DetachOutPorts () {
		foreach (MachinePort port in machinePorts) {
			if (port.getIsPortInUse && port.getPortDirection == MachinePort.PortDirection.Out) {
				port.DetachConnectedPort ();
			}
		}
	}

	void onCarriedEnd () {

	}


}

class MachineInventory {
    


}
