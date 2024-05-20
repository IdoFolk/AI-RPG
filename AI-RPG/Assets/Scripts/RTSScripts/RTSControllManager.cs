using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RTSControllManager : MonoBehaviour
{
    public static RTSControllManager instance;

    #region Private Fields
    bool isControllerEnabled;
    bool selectMore;
    Image _image;
    Group ControlledGroup;

	#endregion

	#region SearializeFields

	[SerializeField]
	Camera rtsCam;
    public Camera getRtsCam => rtsCam;

	[SerializeField]
	LayerMask interractLayer,GroundLayer;

	[SerializeField]
	List<BasicUnit> CharactersList = new List<BasicUnit>();

	[SerializeField]
	ShapeCollection shapeCollection;

    [SerializeField]
    CameraController rtsCamController;

    [SerializeField]
    GameObject selectionArrow;

	#endregion

	private void Awake () {
		if(instance== null) {
            instance = this;
        }
	}

	private void Start()
    {
        _image = GetComponentInChildren<Image>();
        _image.gameObject.SetActive(false);
        

    }
    private void Update()
    {
        if (!isControllerEnabled)
            return;

        selectMore = Input.GetKey(KeyCode.LeftControl);
        if (Input.GetButtonDown("Fire1"))
        {


            MouseRay();
            
        }
        if (Input.GetButtonDown("Fire2"))
        {

           StartCoroutine(MousePosRay());

        }

    }

	public void ToggleController (bool toggle) {
        isControllerEnabled = toggle;
        rtsCam.gameObject.SetActive (toggle);

        if(toggle == true) {
            rtsCamController.ToggleInTransition ();
        }
	}

	public IEnumerator DragLoop()
    {
        _image.gameObject.SetActive(true);
        Vector3 startPos = Input.mousePosition;
        _image.rectTransform.position = startPos;
        ;

        while (Input.GetButton("Fire1"))
        {

            float width = Input.mousePosition.x - startPos.x;
            float hight = Input.mousePosition.y - startPos.y;

            _image.rectTransform.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(hight));
            _image.rectTransform.anchoredPosition = (Vector2)startPos + new Vector2(width / 2, hight / 2);

            yield return null;
        }

        if (!selectMore&&ControlledGroup!=null)
        {
            ControlledGroup.ClearGroup();
        }

        Vector2 min = _image.rectTransform.anchoredPosition - (_image.rectTransform.sizeDelta / 2);
        Vector2 max = _image.rectTransform.anchoredPosition + (_image.rectTransform.sizeDelta / 2);

        //adding all Units found between min and max

        List<BasicUnit> SelectedCharsList = new List<BasicUnit>();
        foreach (BasicUnit found in CharactersList)
        {

            Vector3 ScreenPos = rtsCam.WorldToScreenPoint(found.transform.position);
            if (ScreenPos.x > min.x && ScreenPos.x < max.x && ScreenPos.y > min.y && ScreenPos.y < max.y)
            {
                SelectedCharsList.Add(found);        
            }

        }
        if (SelectedCharsList.Count > 0)
        {
            ControlledGroup = new Group(SelectedCharsList);
            
        }
        
        _image.gameObject.SetActive(false);
    }
    public void MouseRay()
    {
        RaycastHit Hit;
        Ray ray = rtsCam.ScreenPointToRay(Input.mousePosition);
        //clear group if needed and picking one unit
        if (!selectMore&&ControlledGroup!=null)
        {
            ControlledGroup.ClearGroup();
        }
        if (Physics.Raycast(ray, out Hit, float.MaxValue, interractLayer))
        {
            BasicUnit SelectedChar = Hit.collider.GetComponent<BasicUnit>();

            if (SelectedChar == null)
                return;

            if (ControlledGroup == null)
            {
                ControlledGroup = new Group();
            }
            ControlledGroup.AddUnit(SelectedChar);

        }
        else { StartCoroutine(DragLoop()); }

    }

    //sending location for group to move
    public IEnumerator MousePosRay()
    {
        Vector3 mouseStartPos= Input.mousePosition;
        RaycastHit hit;
        Ray ray = rtsCam.ScreenPointToRay(mouseStartPos);
      
        if (ControlledGroup != null && Physics.Raycast(ray, out hit, float.MaxValue, GroundLayer)&&ControlledGroup.groupSize()>0)
        {
            selectionArrow.SetActive (true);
            
            LineRenderer line = selectionArrow.GetComponent<LineRenderer>();

            line.positionCount = 2;

            RaycastHit hit2=new RaycastHit();
            Vector3[] Anchors = new Vector3[2];
            
            Anchors[0] = hit.point;
            
            line.SetPosition(0, Anchors[0]+ Vector3.up);

            while (Input.GetButton("Fire2"))
            {

                Ray ray2 = rtsCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out hit2, float.MaxValue, GroundLayer) && ControlledGroup.groupSize() > 0)
                {
                    Anchors[1] = hit2.point;
                    line.SetPosition(1, Anchors[1]+Vector3.up);
                }
                    yield return null;
            }
			selectionArrow.SetActive (false);

			//need to change for several options
			Vector3 Dir = (hit2.point - hit.point).normalized;
            ControlledGroup.SendGroupTowards(hit.point, ControlledGroup.GenerateShape(shapeCollection.GetFormation),Dir);

        }

		if (Physics.Raycast (ray, out hit, float.MaxValue, interractLayer)) {

            Debug.Log ("Interract");
			//need to change for several options

            if(ControlledGroup != null && ControlledGroup.groupSize() > 0) {
			    Vector3 Dir = (hit.point - ControlledGroup.GetGroupMiddle).normalized;
			    ControlledGroup.SendGroupTowards (hit.point, ControlledGroup.GenerateShape (shapeCollection.GetFormation), Dir);

				if (hit.collider.TryGetComponent<Interfaces.Interactable> (out Interfaces.Interactable interactable)) {
					interactable.OnInterractRTS (ControlledGroup);
				}
			}

            

		}

	}




}
