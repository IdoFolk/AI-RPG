using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class MarketFruit : MonoBehaviour
{
	[System.Flags]
	public enum Fruits {
		Apple = 1 << 1,
		Cucumber = 1 << 2,
		Carrot = 1 << 3,
		Beet = 1 << 4,
		Tomato = 1 << 5,
		Onion = 1 << 6,
		WaterMelon = 1 << 7,
		Banana = 1 << 8,
		Eggplant = 1 << 9,
		Potato = 1 << 10,

		all = Apple| Cucumber | Carrot| Beet| Onion | WaterMelon |Tomato| Banana| Eggplant | Potato
	}

	[SerializeField]
	public Fruits fruitType;
}
