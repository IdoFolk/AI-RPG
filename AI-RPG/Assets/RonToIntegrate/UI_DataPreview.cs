using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces;

public class UI_DataPreview : MonoBehaviour
{

	[SerializeField]
	Image slotImage;

	[SerializeField]
	TextMeshProUGUI slotNameText;

	[SerializeField]
	TextMeshProUGUI slotDescriptionText;
	// Start is called before the first frame update
	
	public void SetDataToPresent (UISlotData uISlotData) {
		if(uISlotData != null) {
			if(slotDescriptionText)
			slotDescriptionText.text = uISlotData.UISlotDataGetDescription ();

			if(slotNameText)
			slotNameText.text = uISlotData.UISlotDataGetName ();

			if(slotImage)
			slotImage.sprite = uISlotData.UISlotDataGetSprite ();
		}
	}
}
