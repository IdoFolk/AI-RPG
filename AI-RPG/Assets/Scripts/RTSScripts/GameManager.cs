using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    Material outlineMaterial;
    public Material getOutlineMaterial => outlineMaterial;

    [SerializeField]
    GameObject pointerImage;
    public GameObject getPointerImage => pointerImage;

    bool isInRTSView;
	// Start is called before the first frame update
	private void Awake () {

		if (GameManager.instance == null)
			instance = this;

		EnablePersonView ();
	}
	

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            if(isInRTSView) {
                EnablePersonView();
            } else {
                EnableRtsView();
            }
        }

    }

    void EnableRtsView () {
		RTSControllManager.instance.ToggleController(true);
		PlayerController.instance.ToggleController(false);
        isInRTSView = true;
	}

	void EnablePersonView () {
		RTSControllManager.instance.ToggleController (false);
		PlayerController.instance.ToggleController (true);
		isInRTSView = false;
	}
}
