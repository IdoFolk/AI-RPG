using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class Character_AbilityHandler : MonoBehaviour
{
    [SerializeField]
    List<Data_Ability> abilities;
   
	PlayerController playerController;
	InputHandler inputHandler;

	private void Awake () {
        playerController = PlayerController.instance;
        inputHandler = InputHandler.instance;
		Debug.Log (playerController);
		foreach (Data_Ability ability in abilities) {
            ability.init (playerController);
            
        }

		
		

	}
	FrameInput frameInput;

	public void CheckAbilityInputs () {
		frameInput = playerController.getFrameInput;

		if (frameInput == null)
			return;

		foreach (Data_Ability ability in abilities) {
			if (ability.getInputConfig.getActionName == "ActionButton1" && frameInput.getActionButton1 == 1) {

				ability.UseAbility ();

			}



			if (frameInput.getJump) {

			}

		}
	}
	
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
