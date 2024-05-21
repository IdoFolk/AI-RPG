using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassFan : MonoBehaviour
{
    [SerializeField]
    BoxCollider windCollider;
    public BoxCollider getWindCollider => windCollider;

    [SerializeField]
    GameObject fanBlades;

    [SerializeField]
    bool rotate;

	// Start is called before the first frame update
	void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(rotate)
        transform.Rotate (Vector3.up, Mathf.Sin(Time.time* 0.8f) * 1f);

        fanBlades.transform.Rotate (Vector3.up, 20);

	}

    
}
