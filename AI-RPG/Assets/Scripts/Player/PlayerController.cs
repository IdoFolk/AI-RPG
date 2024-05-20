using Unity.Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : SerializedMonoBehaviour
{
	public static PlayerController instance;

	[SerializeField]
	Character_Handler characterHandler;
	public Character_Handler getCharacterHandler => characterHandler;

	[SerializeField]
	Character_ShieldHandler characterShieldHandler;

	[SerializeField]
	GameObject graphics;

	[SerializeField]
	Animator animator;

	[SerializeField]
	CinemachineVirtualCamera virtualCamera;

	[SerializeField]
	GameObject camLookAtObject;

	[SerializeField]
	LayerMask interractionLayer;

	[Space (30)]

	[SerializeField]
	Vector2 lookHightMinMax;

	[SerializeField]
	float hightLookSpeed;

	[SerializeField]
	float movementEffectOnRotation = 40f;

	[Space(20)]

	[Header("Flight Settings")]

	[SerializeField]
	float flightYawStrenght= 2f;

	[SerializeField]
	float flightYawContrastStrenght = 0.001f;

	[Space (30)]

	[SerializeField]
	PlayerStats stats;

	[SerializeField]
	float gravity;

	[SerializeField]
	ParticleSystem tempRunPS;

	[SerializeField]
	ParticleSystem selectionPSE;

	[SerializeField]
	ParticleSystem pointerPSE;


	[Space (20)]
	[Header ("Testing")]

	[SerializeField]
	bool debugShowPointerRay;

	[SerializeField]
	Dictionary<Data_Resource,float> giveResources = new Dictionary<Data_Resource, float>();

	

	[Button]
	void GiveResources () {
		foreach(Data_Resource resource in giveResources.Keys) {

			characterHandler.getInventoryHandler.AddItemToInventory (resource, giveResources[resource]);
		}

	}
	

	FrameInput frameInput;
	public FrameInput getFrameInput => frameInput;

	UnityEngine.CharacterController characterController;

	enum MovementMode {Natural, Flight}
	
	MovementMode movementMode = MovementMode.Natural;

	Vector3 previousFramemovementVector;
	Vector3 previousFramerotationVector;
//	Vector2 movementInput;
//	Vector2 mouseDeltaInput;

	float yVelcoity;

	float lookHightAxis =0.5f;

	float initialCamLookObjectYPos;

	float modifiedMovementSpeed;

	bool isInited = false;

	bool isControllerEnabled = false;

	bool isPortBuilding;

	private void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

	private void Update () {
		if (!isControllerEnabled)
			return;

		ModifyStats ();
		InterractionPointer ();
		HandlePortBuilding ();
	}

	private void FixedUpdate () {
		if (!isControllerEnabled)
			return;

		MoveCharacter ();
		RotateCharacter ();
		AimCamera ();
		ApplyGravity ();
	}

	

	private void Start () {
		characterController = GetComponent<UnityEngine.CharacterController> ();

		characterHandler.InitCharacter (true);

		InputHandler.delegate_FrameInput += TakeInputs;

		initialCamLookObjectYPos = camLookAtObject.transform.localPosition.y;

		gradualModifyTime = stats.getAccelerationTime;
		
		Cursor.lockState = CursorLockMode.Locked;
	
		GameManager.instance.getPointerImage.SetActive (debugShowPointerRay);
	}

	public void ToggleController (bool toggle) {
		isControllerEnabled= toggle;
		virtualCamera.gameObject.SetActive(toggle);
	}

	private void TakeInputs (FrameInput newFrameInput) {
		if (!isControllerEnabled) {
			frameInput = null;
			return;
		}

		frameInput = newFrameInput;

		if (frameInput.getFlightMode) {
			bool enableFightMode = movementMode == MovementMode.Natural ? true : false;
			SetFlightMode (enableFightMode);
		}

		if (frameInput.getJump) {
			Jump ();
		}

		if (frameInput.getInterract) {
			if (isPortBuilding) {
				ApplyPortBuilding ();
			} else {
				Interract ();
			}
		}

		if (frameInput.getInventory) {
			Debug.Log ("Inventory");
			UiManager.instance.OpenPlayerMenu (UI_Menu.MenuCategory.Inventory);
		}

		if (frameInput.getCrafting) {
			UiManager.instance.OpenPlayerMenu (UI_Menu.MenuCategory.Crafting);
		}

		if (frameInput.getMap) {
			UiManager.instance.OpenPlayerMenu (UI_Menu.MenuCategory.Map);
		}

		if (frameInput.getStats) {
			UiManager.instance.OpenPlayerMenu (UI_Menu.MenuCategory.Stats);
		}

		characterHandler.getAbilityHandler.CheckAbilityInputs ();
		//if (frameInput.getActionButton1 == 1) {
		//	Chop ();
		//}

	}
	bool psFlag;
	bool blockMovement;
	private void MoveCharacter () {
		if (frameInput == null)
			return;

		Vector3 movementVector = graphics.transform.forward * frameInput.getMovementInput.y + graphics.transform.up * yVelcoity; //+ graphics.transform.right * frameInput.getMovementInput.x;

		if (blockMovement) {
			movementVector = Vector3.zero;
		}

		movementVector = movementVector * modifiedMovementSpeed;

		virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(60,65,sprintModifier);
		
		if(sprintModifier > 0.5f) {
			if (!psFlag) {
				tempRunPS.Play ();
				psFlag= true;
			}
		} else {
			if (psFlag) {
				psFlag = false;
				tempRunPS.Stop();
			}
		}


		movementVector = Vector3.Lerp (movementVector, previousFramemovementVector, 0.2f);

		animator.SetFloat ("MovementBlend", frameInput.getMovementInput.y);

		characterController.Move (movementVector);

		previousFramemovementVector = movementVector;
	}

	float previousFrameSprintInput;
	float sprintInput;
	float sprintModifier;
	float gradualModifyTime = 5;
	float t;
	float startTime;
	float lastT;
	private void ModifyStats () {
		if (frameInput == null)
			return;
		
		sprintInput = frameInput.getLeftShift;

		float currentTime = Time.time + lastT * gradualModifyTime;
		
		if (sprintInput != previousFrameSprintInput) {
			startTime = Time.time;

			if (t > 0 && t < 1) {
				lastT = t;
			} else {
				lastT = previousFrameSprintInput;
			}

			if(sprintInput == 0) {
				
				lastT = 1 - lastT;
			}

		}


		if(gradualModifyTime != -1 ){
			if(gradualModifyTime + startTime > currentTime ) {
				
				if (sprintInput == 1) {
					t = (currentTime - startTime) / gradualModifyTime;
				} else {
					t = 1 - ((currentTime - startTime) / gradualModifyTime);
				}
			}
		}

		//Debug.Log (currentTime < gradualModifyTime + startTime);
		if (t <= 0 || t >= 1 || (currentTime > gradualModifyTime + startTime)) {
			t = sprintInput;
		}

		sprintModifier = stats.getAccelerationCurve.Evaluate (t);

		animator.SetFloat ("RunningBlend", sprintModifier);

		modifiedMovementSpeed = Mathf.Lerp (stats.getWalkSpeed, stats.getSprintSpeed, sprintModifier);

		previousFrameSprintInput = sprintInput;
	}

	
	
	private void RotateCharacter () {
		if (frameInput == null)
			return;
		
		float yInputs = math.remap (0, 2, 0, 1, frameInput.getMouseDeltaInput.x + frameInput.getMovementInput.x * movementEffectOnRotation);
		//float yInputs = math.remap (0, 2, 0, 1, frameInput.getMouseDeltaInput.x );

		float xInputs = movementMode == MovementMode.Flight ? frameInput.getMovementInput.x * flightYawStrenght : 0;

		float yRotation = yInputs;

		float zRotation = -xInputs;

		float animatorCurrentZRotation = animator.transform.rotation.eulerAngles.z;

		float contrastZRotation = frameInput.getMovementInput.x != 0 || movementMode == MovementMode.Natural ? 0 : animatorCurrentZRotation;

		contrastZRotation = contrastZRotation <= 0 ? 0 : contrastZRotation;

		if(contrastZRotation != 0) {
			contrastZRotation = contrastZRotation > 180 ? 360 - contrastZRotation * 1 : (360 - (360 - contrastZRotation)) * - 1;
			
			 
		}



		//if z rotation exeeds on of his boundries set it to 0.
		zRotation = (animatorCurrentZRotation >= 30 && animatorCurrentZRotation < 180 && zRotation >= 0)  || 
					(animatorCurrentZRotation <= 330 && animatorCurrentZRotation > 180 && zRotation <= 0) ? 0 : zRotation;

		//Debug.Log (animatorCurrentZRotation >= 25 && animatorCurrentZRotation < 180 && zRotation <= 0);
		//Debug.Log (animatorCurrentZRotation <= 335 && animatorCurrentZRotation > 180 && zRotation >= 0);
		//Debug.Log (zRotation + "_" + animatorCurrentZRotation + "_" +  contrastZRotation);
		
		zRotation += contrastZRotation * flightYawContrastStrenght;

		Vector3 rotationVector = new Vector3 (0, yRotation, 0) * stats.getRotationSpeed;

		rotationVector = Vector3.Lerp (rotationVector, previousFramerotationVector,0.1f);

		if (rotationVector != Vector3.zero)
		graphics.transform.Rotate (rotationVector,Space.Self);

		if (zRotation != 0)
		animator.transform.Rotate (0, 0, zRotation, Space.Self);

		

		previousFramerotationVector = rotationVector;
	}

	private void AimCamera () {
		if (frameInput == null)
			return;

		lookHightAxis += frameInput.getMouseDeltaInput.y * hightLookSpeed * Time.deltaTime;
		
		lookHightAxis = Mathf.Lerp(lookHightAxis, Mathf.Clamp (lookHightAxis, lookHightMinMax.x, lookHightMinMax.y),0.9f) ;

		Vector3 newCampos = new Vector3(camLookAtObject.transform.localPosition.x, initialCamLookObjectYPos + lookHightAxis, camLookAtObject.transform.localPosition.z);

		camLookAtObject.transform.localPosition = Vector3.Lerp (camLookAtObject.transform.localPosition, newCampos, 0.9f);
	}

	private void ApplyGravity () {
		if (movementMode == MovementMode.Flight)
			return;

		if (isGrounded ()) {
			yVelcoity = 0;
			return;
		}

		yVelcoity -= gravity;
	}

	private void DisableMovement () {
		blockMovement = true;
	}

	private void EnableMovement () {
		blockMovement = false;
	}


	public void Chop () {
		if (!isGrounded ())
			return;

		int anim = Animator.StringToHash ("Chop");

		animator.CrossFade (anim, 0.1f);
		animator.Update (0);
	}

	void Interract () {
		Debug.Log ("Interract");
		RaycastHit raycastHit;

		if(carriedObject!= null) {
			DetachCarriedObject ();
			return;
		}


		if(didInterractionRayHit){
			if (interractionRayHit.transform.TryGetComponent<CarryableObject> (out CarryableObject carryableObject)) {

				Debug.Log (carryableObject);
				carriedObject = carryableObject;
				StartCoroutine (CarryObject());

				return;
			}

			if (interractionRayHit.transform.TryGetComponent<Machine> (out Machine machine)) {
				UiManager.instance.OpenMachineMenu(machine);
			}


			if (interractionRayHit.transform.TryGetComponent<OutputPort> (out OutputPort outputPort)) {
				currentBuiltPort = outputPort;
				isPortBuilding = true;
				currentBuiltPort.getPortConnectionLineRenderer.gameObject.SetActive (true);
			}

			
		}
	}

	MachinePort currentBuiltPort;
	void HandlePortBuilding () {
		if (!isPortBuilding || currentBuiltPort == null) 
			return;

		currentBuiltPort.getPortConnectionLineRenderer.SetPosition (0, currentBuiltPort.transform.position);
		currentBuiltPort.getPortConnectionLineRenderer.SetPosition (1, transform.position);
		


	}

	void ApplyPortBuilding () {
		if (didInterractionRayHit && interractionRayHit.transform.TryGetComponent<InputPort> (out InputPort inputPort)) {
			currentBuiltPort.getPortConnectionLineRenderer.SetPosition(1,inputPort.transform.position);
			isPortBuilding = false;
			currentBuiltPort = null;


		}
	}

	RaycastHit interractionRayHit;
	bool didInterractionRayHit;
	Interfaces.OnPointed currentPointedUi;
	void InterractionPointer () {
		didInterractionRayHit = Physics.Raycast (/*transform.position + Vector3.up * 0.3f*/ Camera.main.transform.position, /*graphics.transform.forward*/ Camera.main.transform.forward, out interractionRayHit, 10, interractionLayer);

		if (didInterractionRayHit) {
			if (interractionRayHit.transform.TryGetComponent<Interfaces.OnPointed> (out Interfaces.OnPointed onPointedUi)) {
				
				if(currentPointedUi != null && currentPointedUi != onPointedUi) {
					currentPointedUi.onPointRemove ();
				}

				if (currentPointedUi != onPointedUi) {

					currentPointedUi = onPointedUi;
					currentPointedUi.onPointed ();
				}
			}

			pointerPSE.transform.position = interractionRayHit.transform.position;

			if (pointerPSE.isStopped) {
				pointerPSE.Play();
			}
			
		} else {
			pointerPSE.Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);

			if (currentPointedUi != null) {

				currentPointedUi.onPointRemove ();
				currentPointedUi = null;
			}
		}
	}

	public void DetachCarriedObject () {
		if (carriedObject == null)
			return;

		selectionPSE.Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);

		carriedObject.onDetach ();
		carriedObject = null;
	}

	bool isCarryingObject = false;
	CarryableObject carriedObject;
	public CarryableObject getCarriedObject => carriedObject;

	Vector3 carryOrientation = Vector3.up;

	float carriedObjectRotationSpeed;
	IEnumerator CarryObject () {
		isCarryingObject = true;
		carriedObject.OnInterract ();

		selectionPSE.Play ();
		while (carriedObject != null) {
			
			carriedObject.transform.position = transform.position + Vector3.up + (lookHightAxis * 0.75f * Vector3.up) + graphics.transform.forward * 2;
			selectionPSE.transform.position = carriedObject.transform.position;
			//Debug.Log (frameInput.getScrollWheelInput);

			if (frameInput.getScrollWheelInput.y != 0) {
				carriedObjectRotationSpeed = 10 * frameInput.getScrollWheelInput.y;
			}

			carriedObject.transform.Rotate (carriedObjectRotationSpeed * Time.deltaTime * carryOrientation, relativeTo: Space.World);

			carriedObjectRotationSpeed *= 0.02f;

			//Debug.Log (carriedObjectRotationSpeed);

			if (frameInput.getButtonE) {

				if (carryOrientation == Vector3.up) {
					carryOrientation = Vector3.right;
				}

				if (carryOrientation == Vector3.forward) {
					carryOrientation = Vector3.up;
				}

				Debug.Log (carryOrientation);
			}


			if (frameInput.getButtonQ) {
				if (carryOrientation == Vector3.up) {
					carryOrientation = Vector3.forward;
				}

				if (carryOrientation == Vector3.right) {
					carryOrientation = Vector3.up;
				}

				
				Debug.Log (carryOrientation);
			}

			yield return null;
		}
	}


	//Requires more work
	private void Jump () {
		if (!isGrounded ())
			return;

		int anim = Animator.StringToHash ("Jump");

		animator.CrossFade (anim, 0.1f);
		animator.Update (0);

		yVelcoity += stats.getJumpForce;
	}

	bool isGrounded () {
		return characterController.isGrounded;
	}

	void SetFlightMode (bool setEnabled) {
		if (setEnabled) {
			StartCoroutine (StartFlightSequence ());
		} else {
			StartCoroutine (landSequence ());
		}

	}

	IEnumerator StartFlightSequence () {
		movementMode = MovementMode.Flight;

		int anim = Animator.StringToHash ("InitializeFlightMode");

		animator.CrossFade(anim,0.1f);
		animator.Update (0);
		yVelcoity = 0.1f;

		yield return new WaitForSeconds (0.2f);


		yVelcoity = 0f;

		yield return null;
	}

	IEnumerator landSequence () {

		int anim = Animator.StringToHash ("Land");

		animator.CrossFade (anim, 0.1f);
		animator.Update (0);

		yield return new WaitForSeconds (0.1f);

		movementMode = MovementMode.Natural;
		yield return null;
	}

	private void OnDrawGizmos () {

		Gizmos.color = Color.blue;
		Gizmos.DrawLine (Camera.main.transform.position, Camera.main.transform.position +  Camera.main.transform.forward * 10);
		//Physics.Raycast (/*transform.position + Vector3.up * 0.3f*/ Camera.main.transform.position, /*graphics.transform.forward*/ Camera.main.transform.forward, out interractionRayHit, 10, interractionLayer);
	}

}