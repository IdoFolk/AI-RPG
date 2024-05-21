using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class OutlineEffect : MonoBehaviour
{
	[Tooltip("WIP")]
	[SerializeField]
	Color outlineColor;

	MeshRenderer[] renderers;
	Material outlineMat;

	private void Start () {
		renderers = transform.GetComponentsInChildren<MeshRenderer> ();
		outlineMat = GameManager.instance.getOutlineMaterial;
	}

	public void EnableOutlines () {
		for (int i = 0; i < renderers.Length; i++) {
			if (renderers[i].sharedMaterials.Contains (outlineMat))
				continue;

			List<Material> materials = renderers[i].sharedMaterials.ToList ();
			materials.Add (outlineMat);
			renderers[i].SetSharedMaterials (materials);
		}
	}

	public void DisableOutlines () {

		for (int i = 0; i < renderers.Length; i++) {
			if (renderers[i].sharedMaterials.Contains (outlineMat)) {

				List<Material> materials = renderers[i].sharedMaterials.ToList ();
				int index = -1;

				for (int x = 0; x < materials.Count; x++) {
					if(materials[x] == outlineMat) {
						index = x;
						//Destroy (materials[x]);
					}
				}

				if(index != -1) 
				materials.RemoveAt (index);
				
				renderers[i].SetSharedMaterials (materials);
			}


		}
	}
}
