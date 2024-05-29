using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    #region Private Variables

    private float scrollDPIMouse;
    private float scrollDefault = 80f;
    private float rotateSpeed = 100f;
    private float rotateAmount = 10f;
    private float scrollWidth = 50f;


	[SerializeField]
	private float cameraMovementSpeed = 10f;

	[SerializeField]
    private float minHeight = 10f;

	[SerializeField]
	private float maxHeight = 40f;

    private Vector3 defaultCamPos;

    private Camera rtsCam;


    #endregion

    void Start()
    {
        rtsCam = RTSControllManager.instance.getRtsCam;
        Debug.Log (rtsCam);
        defaultCamPos = rtsCam.transform.eulerAngles;
    }

    void Update()
    {
        MoveCameraEdge();
        RotateCamera();
        MoveCameraArrows();
    }

    #region Movement
    
    public void ToggleInTransition () {
		rtsCam = RTSControllManager.instance.getRtsCam;

		Debug.Log (rtsCam);
        Debug.Log (PlayerController.instance);
        rtsCam.transform.position = PlayerController.instance.gameObject.transform.position + Vector3.up;
    }

    private void MoveCameraEdge()
    {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //horizontal camera Logic
        if (xpos >= 0 && xpos < scrollWidth)
        {
            scrollDPIMouse = Mathf.Clamp(xpos - scrollWidth, -50, 0);
            movement.x -= scrollDPIMouse;
        }else if (xpos >= 0 && xpos > Screen.width - scrollWidth)
        {
            scrollDPIMouse = Mathf.Clamp(xpos - (Screen.width - scrollWidth), 0, 50);
            movement.x += scrollDPIMouse;
        }
        //vertical camera Logic
        else if (ypos >= 0 && ypos < scrollWidth)
        {
            scrollDPIMouse = Mathf.Clamp(ypos - scrollWidth, -50, 0);
            movement.z -= scrollDPIMouse;
        }else if (ypos >= 0 && ypos > Screen.height - scrollWidth)
        {
            scrollDPIMouse = Mathf.Clamp(ypos - (Screen.height - scrollWidth), 0, 50);
            movement.z += scrollDPIMouse;
        }

        movement = rtsCam.transform.TransformDirection(movement);
        movement.y = 0;

        //Zoom in and out
        movement.y -= scrollDefault * Input.GetAxis("Mouse ScrollWheel");

        //calculate camera position
        Vector3 origin = rtsCam.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit zoom in and out
        if (destination.y > maxHeight)
        {
            destination.y = maxHeight;
        }
        else if (destination.y < minHeight)
        {
            destination.y = minHeight;
        }

        //Movement execution **Only if camera should move!!!
        if (destination != origin)
        {
			rtsCam.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * scrollDPIMouse);
        }
        //movement execution for *zoom in and out*
        if ((int)destination.y != (int)origin.y)
        {
			rtsCam.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * scrollDefault);
        }
    }

    void MoveCameraArrows()
    {
        //Get Arrow Keys
        float xpos = Input.GetAxis("Horizontal");
        float ypos = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0, 0, 0);

        //Move Logic
        if (xpos < 0)
        {
            movement.x -= scrollDefault;
        }
        if (xpos > 0)
        {
            movement.x += scrollDefault;
        }

        if (ypos < 0)
        {
            movement.z -= scrollDefault;
        }
        if (ypos > 0)
        {
            movement.z += scrollDefault;
        }

        movement = rtsCam.transform.TransformDirection(movement);
        movement.y = 0;

        //calculate camera position
        Vector3 origin = rtsCam.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        if (destination != origin)
        {
            rtsCam.transform.position += movement.normalized * Time.deltaTime * cameraMovementSpeed;
        }
    }

    #endregion

    #region Rotation

    private void RotateCamera()
    {
        Vector3 origin = rtsCam.transform.eulerAngles;
        Vector3 destination = origin;

        //left ALT or Right ALT + right mouse button = rotation
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1))
        {
            destination.y += Input.GetAxis("Mouse X") * rotateAmount;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rtsCam.transform.eulerAngles = defaultCamPos;
        }

        //Rotation execution
        if (destination != origin)
        {
           rtsCam.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * rotateSpeed);
        }
    }
}

#endregion

