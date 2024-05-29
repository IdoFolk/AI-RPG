using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

	public static InputHandler instance;


	public delegate void Delegate_FrameInput (FrameInput frameInput);

	public static Delegate_FrameInput delegate_FrameInput;


	GameControls gameControls;
	public GameControls getGameControles => gameControls;

	InputActionMap inputMap;

	PlayerInput playerInput;

	FrameInput FrameInput;




	private void Awake () {
		instance = this;
	}

	private void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		gameControls = new GameControls ();
		gameControls.Player.Enable ();
		playerInput = GetComponent<PlayerInput> ();

		inputMap = playerInput.currentActionMap;
		

		//RegisterInputs ();
	}
	private void Update () {
		DelegateFrameInputs ();	
	}

	//private void RegisterInputs () {
	//	gameControls.Player.Interract.performed += OnInputInterract;
	//}

	public void DelegateFrameInputs () {
		delegate_FrameInput?.Invoke (GetConstantInputs ());
	}


	private FrameInput GetConstantInputs () {
		float actionButton1 = gameControls.Player.ActionButton1.ReadValue<float> ();
		float actionButton2 = gameControls.Player.ActionButton2.ReadValue<float> ();
		float actionButton3 = gameControls.Player.ActionButton3.ReadValue<float> ();

		float leftShift = gameControls.Player.LeftShift.ReadValue<float> ();

		bool flightMode = gameControls.Player.FlightMode.WasPerformedThisFrame();
		bool jump = gameControls.Player.Jump.WasPerformedThisFrame ();
		bool interract = gameControls.Player.Interract.WasPerformedThisFrame ();
		bool buttonQ = gameControls.Player.Q.WasPerformedThisFrame ();
		bool buttonE = gameControls.Player.E.WasPerformedThisFrame ();
		bool inventory = gameControls.Player.Inventory.WasPerformedThisFrame ();
		bool crafting = gameControls.Player.Crafting.WasPerformedThisFrame ();
		bool map = gameControls.Player.Map.WasPerformedThisFrame ();
		bool stats = gameControls.Player.Stats.WasPerformedThisFrame ();
		bool escape = gameControls.Player.Escape.WasPerformedThisFrame ();



		Vector2 movementInput = gameControls.Player.Walk.ReadValue<Vector2> ();
		Vector2 mouseDeltaInput = gameControls.Player.MouseMovement.ReadValue<Vector2> ();
		Vector2 scrollWheel = gameControls.Player.ScrollWheel.ReadValue<Vector2> ();
		
		return new FrameInput (actionButton1, actionButton2, actionButton3, leftShift, movementInput,mouseDeltaInput, scrollWheel, flightMode,jump, interract, buttonQ, buttonE,inventory,crafting,map,stats,escape);
	}

	//	private void OnInputInterract (InputAction.CallbackContext context) {

	//animator.CrossFade ("Interract", .3f);

	//	}



	[ContextMenu ("test")]
	public List<string> GetAllInputNames () {
		GameControls inputActions = new GameControls ();
		List<string> inputStrings = new List<string>();

		foreach (var action in inputActions) {
			inputStrings.Add(action.name);
		}

		return inputStrings;
	}
}

public class FrameInput {

	public FrameInput (
		float ActionButton1, float ActionButton2, float ActionButton3, float LeftShift,
		Vector2 MovementInput, Vector2 MouseDeltaInput,Vector2 ScrollWheel,
		bool FlightMode,bool Jump,bool Interract,bool ButtonQ, bool ButtonE,bool Inventory,bool Crafting, bool Map, bool Stats,bool Escape
		) {

		actionButton1 = ActionButton1;
		actionButton2 = ActionButton2;
		actionButton3 = ActionButton3;

		leftShift = LeftShift;
		movementInput = MovementInput;
		mouseDeltaInput = MouseDeltaInput;
		scrollWheelInput = ScrollWheel;
		interract = Interract;
		flightMode = FlightMode;
		jump = Jump;
		buttonE = ButtonE;
		buttonQ = ButtonQ;
		inventory = Inventory;
		crafting = Crafting;
		map = Map;
		stats = Stats;
		escape = Escape;

	}

	float actionButton1, actionButton2, actionButton3, leftShift;

	public float getActionButton1 => actionButton1;
	public float getActionButton2 => actionButton2;
	public float getActionButton3 => actionButton3;
	public float getLeftShift => leftShift;


	bool flightMode, jump, interract, buttonQ, buttonE,inventory,crafting, map, stats, escape;
	public bool getFlightMode => flightMode;
	public bool getJump => jump;
	public bool getInterract => interract;
	public bool getButtonQ => buttonQ;
	public bool getButtonE => buttonE;
	public bool getInventory => inventory;
	public bool getCrafting => crafting;
	public bool getMap => map;
	public bool getStats => stats;
	public bool getEscape => escape;
	


	Vector2 movementInput;
	public Vector2 getMovementInput => movementInput;

	Vector2 mouseDeltaInput;
	public Vector2 getMouseDeltaInput => mouseDeltaInput;

	Vector2 scrollWheelInput;
	public Vector2 getScrollWheelInput => scrollWheelInput;
}
