using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using static Delegates;
using static Interfaces;
using UnityEngine.InputSystem;

public class Ui_InventorySlot : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler , IPointerMoveHandler, IPointerUpHandler , IPointerDownHandler
{
	public Delegate_OnInventorySlotSelected onSlotSelected;


	float quantity;
	public float getQuantity => quantity;

	UISlotData slotData;
	public UISlotData getSlotData => slotData;

    [SerializeField]
    TextMeshProUGUI quantityText;

	[SerializeField]
    Image dataImage;
	// Start is called before the first frame update

	bool isPressed;
	bool isHovered;
	bool isSelected;

	Transform prevDataImageParent;
	Canvas ownerMenuCanvas;
	GraphicRaycaster ownerMenuGraphicRaycaster;

	//public Delegate onSelected;

	public void Init()
    {
		ownerMenuCanvas = dataImage.canvas;

		if(!ownerMenuGraphicRaycaster || ownerMenuGraphicRaycaster.gameObject != ownerMenuCanvas.gameObject)
		ownerMenuGraphicRaycaster = ownerMenuCanvas.GetComponent<GraphicRaycaster>();

		if (slotData == null) {
            quantityText.gameObject.SetActive (false);
            dataImage.color = new Color (dataImage.color.r, dataImage.color.g, dataImage.color.b, 0);
        } else {
            dataImage.color = new Color (dataImage.color.r, dataImage.color.g, dataImage.color.b, 1);
			quantityText.gameObject.SetActive (true);
		}

        
	}

	private void Update () {
		if (isPressed) {
			dataImage.rectTransform.position = new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, 0);
		} else {

		}
	}

	void OnSlotSelected () {
		if (slotData == null)
			return;
		prevDataImageParent = dataImage.transform.parent;
		dataImage.transform.parent = transform.parent;

		isPressed = true;
		isSelected = true;
		onSlotSelected.Invoke (this);
	}

	public void OnPointerDown (PointerEventData eventData) {
		if (slotData == null)
			return;
		
		OnSlotSelected ();
	}

	public void OnPointerEnter (PointerEventData eventData) {
		if (slotData == null)
			return;

		dataImage.color = new Color (dataImage.color.r, dataImage.color.b, dataImage.color.b, 0.5f);
		isHovered = true;
	}

	public void OnPointerExit (PointerEventData eventData) {
		if (slotData == null)
			return;

		isHovered = false;
		dataImage.color = new Color (dataImage.color.r, dataImage.color.b, dataImage.color.b, 1);
	}

	public void OnPointerMove (PointerEventData eventData) {
		if (slotData == null)
			return;

	}

	List<RaycastResult> pointerUpRaycastResolts = new List<RaycastResult> ();
	
	public void OnPointerUp (PointerEventData eventData) {
		if (slotData == null)
			return;

		dataImage.transform.parent = prevDataImageParent;
		dataImage.rectTransform.localPosition = Vector3.zero;

		pointerUpRaycastResolts.Clear ();
		ownerMenuGraphicRaycaster.Raycast (eventData, pointerUpRaycastResolts);

		foreach(RaycastResult raycastResult in pointerUpRaycastResolts) {
			if (raycastResult.gameObject.TryGetComponent<Ui_InventorySlot> (out Ui_InventorySlot pointedSlotOnPointerUp)){
				if(pointedSlotOnPointerUp != this) {
					if (pointedSlotOnPointerUp.getSlotData == null) {
						pointedSlotOnPointerUp.SyncSlot (slotData, quantity);
						SyncSlot (null, 0);
						break;
					}

					if(pointedSlotOnPointerUp.getSlotData != null) {
						UISlotData prevSlotData = slotData;
						float prevQuantity = quantity;

						SyncSlot (pointedSlotOnPointerUp.slotData, pointedSlotOnPointerUp.quantity);
						pointedSlotOnPointerUp.SyncSlot (prevSlotData, prevQuantity);
						break;

					}


				}
				
				
			}
		}
		
		isPressed = false;
	}

	public void SyncSlot (Interfaces.UISlotData newSlotData, float newQuantity) {
        if (newSlotData == null) {
			slotData = null;
			quantity = 0;
			dataImage.color = new Color (dataImage.color.r, dataImage.color.g, dataImage.color.b, 0);
			quantityText.gameObject.SetActive (false);
			return;
		}
        Debug.Log ("SlotSynced");
		quantityText.gameObject.SetActive (true);

		if (isHovered) {
			dataImage.color = new Color (dataImage.color.r, dataImage.color.b, dataImage.color.b, 0.5f);
		} else {
			dataImage.color = new Color (dataImage.color.r, dataImage.color.g, dataImage.color.b, 1);
		}

		quantity = newQuantity;
        slotData = newSlotData;

		quantityText.text = quantity.ToString();
        //dataImage.sprite.texture.Apply ();
		dataImage.sprite = slotData.UISlotDataGetSprite();
	}
}
