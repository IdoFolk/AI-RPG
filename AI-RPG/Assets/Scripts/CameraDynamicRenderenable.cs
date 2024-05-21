using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamicRenderenable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera> ().allowHDR = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
