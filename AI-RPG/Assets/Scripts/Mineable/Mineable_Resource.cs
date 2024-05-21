using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Mineable_Resource : MonoBehaviour
{
    [OnValueChanged("PreloadContainedResourceGraphics")]
    [SerializeField]
    Data_Resource containedResource;
    public Data_Resource getContainedResource => containedResource;

	[SerializeField]
    int containedResourceUnits = 100;
    public int getContainedResourceUnits => containedResourceUnits;

    [PropertyRange(1,3)]
    [SerializeField]
    int resourceGatherDifficulty = 1;
    public int getResourceGatherDifficulty => resourceGatherDifficulty;

    [ReadOnly]
    [SerializeField]
    GameObject preloadedResourceGraphics;
	public GameObject GetContainedResourceGraphics => preloadedResourceGraphics;

	private void Start () {
      
	}
    public Data_Resource ExtractResource () { containedResourceUnits-- ; return containedResource; }


    [OnInspectorInit]
    void PreloadContainedResourceGraphics () {
        if(preloadedResourceGraphics == null || preloadedResourceGraphics.name != containedResource.getResourceGraphics.name) {
            if(preloadedResourceGraphics!= null) {
                Editor.DestroyImmediate (preloadedResourceGraphics);
            }
			preloadedResourceGraphics = Editor.Instantiate(containedResource.getResourceGraphics, transform.position, quaternion.identity, transform);
            preloadedResourceGraphics.name = containedResource.getResourceGraphics.name;
			preloadedResourceGraphics.SetActive (false);
		}
    }

    
}
