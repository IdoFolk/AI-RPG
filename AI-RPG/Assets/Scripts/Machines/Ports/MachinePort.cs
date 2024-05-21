using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PowerPort;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;
using UnityEditor.Presets;

[RequireComponent (typeof (BoxCollider))]
public class MachinePort : MonoBehaviour {
    [ReadOnly]
    [SerializeField]
    Machine ownerMachine;
    public Machine getSetOwnerMachine { get { return ownerMachine; } set { ownerMachine = value; } }

    [SerializeField]
    Collider portCollider;
    public Collider getPortCollider => portCollider;


    [SerializeField]
    LineRenderer portConnectionLineRenderer;
    public LineRenderer getPortConnectionLineRenderer => portConnectionLineRenderer;

    [ReadOnly]
    [SerializeField]
    bool portInUse;
    public bool getIsPortInUse => portInUse;

    bool portInited;
    public bool getIsPortInited => portInited;


    MachinePort connectedPort;
    public MachinePort getConnectedPort => connectedPort;


	public enum PortDirection { In, Out }

	[ReadOnly]
	[SerializeField]
	PortDirection portDirection;

	public PortDirection getPortDirection => portDirection;

	[ReadOnly]
	[SerializeField]
	Rigidbody rigidbody;

    public void InitPort () {
        portInited = true;

        if (!portInUse)
            SetConnectionVisuals (false);
            
    }

	//this command is applied only on ports that are connected to machine bases.
	public virtual void SyncPort () {
        if (!connectedPort)
            return;

		if (ownerMachine.isMachineMoving) {
			portConnectionLineRenderer.SetPosition (0, transform.position);
			portConnectionLineRenderer.SetPosition (1, connectedPort.transform.position);
		}

        connectedPort.RecivePortSync ();

	}

	//this command is applied only on ports that are NOT connected to machine bases.
	public virtual void RecivePortSync () {

    }

	public void SetPortDirection (PortDirection newPortDirection) {
		portDirection = newPortDirection;

		portCollider.isTrigger = newPortDirection == PortDirection.In || this is OutputPort || this is InputPort ? true : false;

		if (newPortDirection == PortDirection.Out) {
			if (rigidbody == null) {
				rigidbody = gameObject.GetComponent<Rigidbody> ();
				if (rigidbody == null) {
					rigidbody = gameObject.AddComponent<Rigidbody> ();
				}
				rigidbody.mass = 0;
				rigidbody.linearDamping = 0;
				rigidbody.angularDamping = 0;
				rigidbody.useGravity = false;
				rigidbody.isKinematic = true;
				rigidbody.constraints = RigidbodyConstraints.FreezeAll;
				
				
			}
		} else {
			if (rigidbody != null) {
				Component.DestroyImmediate (rigidbody);
			}
		}

		EditorUtility.SetDirty (this);
	}


	//The "IN" machine port recognizes the out and making him to snap on the in.
	private void OnTriggerEnter (Collider other) {
        Debug.Log ("hit");
		if (!portInUse) {
			if (other.TryGetComponent<MachinePort>(out MachinePort collisionPort)) {
                if(!collisionPort.getIsPortInUse
                    &&( 
                    collisionPort is PowerPort && this is PowerPort || 
                    collisionPort is UtilityPort && this is UtilityPort ||
					collisionPort is FunctionalityPort && this is FunctionalityPort ||
					collisionPort is InputPort && this is InputPort ||
					collisionPort is OutputPort && this is OutputPort
					)) {

                    SnapPortIn (collisionPort);
                }
            }
		}
	}

	//private void OnTriggerExit (Collider other) {
	//	Debug.Log ("hit");
	//	if (portInUse) {
	//		if (other.TryGetComponent<MachinePort> (out MachinePort collisionPort)) {
	//			if (collisionPort.getIsPortInUse
	//				&& (
	//				collisionPort == connectedPort
	//				)) {

	//				DetachConnectedPort ();
	//			}
	//		}
	//	}
	//}

	void SetConnectionVisuals (bool value) {
        if(value == false) {
			portConnectionLineRenderer.gameObject.SetActive (false);
		} else {
            if (connectedPort) {

                portConnectionLineRenderer.gameObject.SetActive (true);

                portConnectionLineRenderer.SetPosition (0, transform.position);
                portConnectionLineRenderer.SetPosition (1, connectedPort.transform.position);
            }
		}


    }

	///The commaned used by the Detacher
	public virtual void DetachConnectedPort () {
		connectedPort.OnConnectedPortDetached ();

		SetConnectionVisuals (false);

		this.portInUse = false;
		connectedPort = null;
		portCollider.enabled = true;
		ownerMachine.transform.parent = null;

	}

	//The commaned used by the Detached
	public virtual void OnConnectedPortDetached () {
		SetConnectionVisuals (false);

		this.portInUse = false;
		connectedPort = null;
		ownerMachine.transform.parent = null;
	}

    //The commaned used by the snapper.
    public virtual void SnapPortIn (MachinePort machinePortToSnapIn) {
		connectedPort = machinePortToSnapIn;
		
        Debug.Log ("Snap");
        machinePortToSnapIn.SnapedByOtherPort (this);
		SetConnectionVisuals (true);
		//onPortConnectedDelegate.Invoke (machinePortToSnapIn);
		this.portInUse= true;
		this.getSetOwnerMachine.OnPortSnapped (this, machinePortToSnapIn);

    }

	//The commaned used by the snapped.
	public virtual void SnapedByOtherPort(MachinePort machineSnapperPort) {
		connectedPort = machineSnapperPort;

		Debug.Log ("Snaped");
        Vector3 directionFromMachinToPort = (transform.position - ownerMachine.transform.position).normalized;

		Vector3 directionFromSnapperMachinToSnapperPort = (machineSnapperPort.transform.position - machineSnapperPort.getSetOwnerMachine.transform.position).normalized;

		ownerMachine.transform.position = machineSnapperPort.transform.position + directionFromSnapperMachinToSnapperPort * 0.75f;// + (directionFromMachinToPort * Vector3.Distance(transform.position ,ownerMachine.transform.position));

        ownerMachine.transform.LookAt (transform.position - machineSnapperPort.transform.forward * 10000);

        ownerMachine.transform.Rotate (directionFromMachinToPort * 90);

        ownerMachine.transform.parent = machineSnapperPort.ownerMachine.transform;

        CarryableObject currentPlayerCarriedObject = PlayerController.instance.getCarriedObject;

		if (currentPlayerCarriedObject == machineSnapperPort.ownerMachine.getCarryableObject || currentPlayerCarriedObject == this.ownerMachine.getCarryableObject) {
            Debug.Log ("Detach");
            PlayerController.instance.DetachCarriedObject ();
        }

		ownerMachine.getRigidbody.isKinematic = true;
        portCollider.enabled = false;
		this.portInUse = true;
		this.getSetOwnerMachine.OnPortSnapped (this, machineSnapperPort);
	}
    
}