using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript_Utility_Cutter : MachineScript
{
	//need to move to machine script.
	[SerializeField]
	Animator animator;

	[SerializeField]
	Transform cutterRotateTransform;

	[SerializeField]
	ParticleSystem cutterHitPSE;

	[SerializeField]
	ParticleSystem addResourcePSE;

	[SerializeField]
	LayerMask mineableLayer;

	[SerializeField]
	Collider cutterCollider;

	[PropertyRange (1, 3)]
	[SerializeField]
	int resourceGatherStrength = 1;

	UtilityPart connectedUtilityPart;

	Mineable_Resource mineableResource;
	// Start is called before the first frame update
	void Start () {
		connectedUtilityPart = (UtilityPart)ownerMachine;
		connectedUtilityPart.onPerformUtility += Perform;
		connectedUtilityPart.onActivation += OnActivation;
		connectedUtilityPart.onShutDown += OnShutDown;

		//cutterCollider.enabled = false;
	}

	void Perform () {
		
		cutterRotateTransform.transform.Rotate(Vector3.up,300 * Time.deltaTime,Space.World);
		
		//MineResource ();

		//find with collider
		//mineableResource

		
			//if(mineableResource != null && (miningRaycastHit.transform == null || miningRaycastHit.transform != mineableResource.transform)) {
			//	removeTargetLock (mineableResource);
			//	return;
			//}

			//if (miningRaycastHit.transform.TryGetComponent<Mineable_Resource> (out Mineable_Resource mineable_Resource)) {
			//	lockOnTarget (mineable_Resource);
			//}
		//}else if (mineableResource) {
		//	removeTargetLock (mineableResource);
		//}
	}

	void OnActivation () {
		//cutterCollider.enabled = true;
	
	}

	void OnShutDown () {
		//cutterCollider.enabled = false;
		

		//removeTargetLock (mineableResource);
	}

	void lockOnTarget (Mineable_Resource lockedMineableResource) {
		miningTimer = 0;

		mineableResource = lockedMineableResource;

	}

	void removeTargetLock (Mineable_Resource removedMineableResource) {
		mineableResource = null;
		miningTimer = -1;

		if(resourceGraphics)
		resourceGraphics.SetActive (false);

	}


	float miningTimer = 0;
	float timePerResourceUnit;
	Data_Resource recievedResouce;
	GameObject resourceGraphics;
	void MineResource () {
	//	if (miningTimer == -1)
	//		return;

	//	timePerResourceUnit = miningRate * mineableResource.getResourceGatherDifficulty - (resourceGatherStrength-1);
	//	float t = miningTimer / timePerResourceUnit;
		
	//	if(t == 0) {
	//		resourceGraphics = mineableResource.GetContainedResourceGraphics;
	//		resourceGraphics.SetActive (true);
	//	}

	//	if(resourceGraphics !=null) {
	//		resourceGraphics.transform.position = Vector3.Lerp (laserLineRenderer.GetPosition (1), laserLineRenderer.GetPosition (0), t);
	//		resourceGraphics.transform.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, 1 - Mathf.Pow (Mathf.Max (0, Mathf.Abs (t) * 2 - 1), 3f));

	//	}

	//	if(t >= 1) {
	//		miningTimer= 0;
	//		Data_Resource recievedResouce = mineableResource.ExtractResource ();
	//		ownerMachine.getOwnerMachineBase.AddResourceToMachine(recievedResouce);
	//		resourceGraphics.SetActive (false);
	//		animator.Play ("AddResource");
	//		addResourcePSE.Play ();
	//		return;
	//	}

	//	miningTimer += Time.deltaTime;
	}
}
