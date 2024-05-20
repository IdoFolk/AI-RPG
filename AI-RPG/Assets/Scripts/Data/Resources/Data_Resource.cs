using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class Data_Resource : DataCreator_Data, Interfaces.InventoryHeldObject{
	[SerializeField]
	string resourceName;
	public string getResourceName => resourceName;

	[Space (20)]

	[SerializeField]
	string description;
	public string getDescription => description;

	[Space (20)]

	[SerializeField]
	Sprite uiSprite;

	[Title("Graphics",Bold = true,TitleAlignment = TitleAlignments.Centered)]
	[LabelText("")]
	[PreviewField(Alignment = ObjectFieldAlignment.Center,Height = 150)]
	[SerializeField]
	GameObject resourceGraphics;
	public GameObject getResourceGraphics => resourceGraphics;

	[InlineProperty]
	[Title ("Physical Properties", Bold = true, TitleAlignment = TitleAlignments.Centered)]
	[LabelText ("")]
	[SerializeField]
	ResourcePhysicalProperties physicalProperties;

	[System.Serializable]
	public class ResourcePhysicalProperties {
		[Range(0,10)]
		public int heatPropery = 0;
		[Range(0,10)]
		public int electricPropery = 0;
		[Range(0,10)]
		public int burnProperty = 0;
		[Range (0, 10)]
		public int strengthProperty = 0;

	}

	public Sprite UISlotDataGetSprite () {
		return uiSprite;
	}

	public string UISlotDataGetDescription () {
		return description;
	}

	public string UISlotDataGetName () {
		return resourceName;
	}
}
