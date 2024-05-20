using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CarryableObject : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;

    public Vector3 getCurrentVelocity => rigidbody == null ? Vector3.zero : rigidbody.linearVelocity;
    

    bool isBeingCarried;
    public bool getIsBeingCarried => isBeingCarried;

	[HideInInspector]
    public Delegates.Delegate_Void onCarriedStart;

	[HideInInspector]
	public Delegates.Delegate_Void onCarriedEnd;

	Vector2 hoverRotationSpeedRange;

	// Update is called once per frame
	void Update()
    {
    //    if(isBeingCarried)
    //    transform.Rotate (Random.Range (1, 2) * Time.deltaTime, Random.Range (1, 2) * Time.deltaTime, Random.Range (1, 2) * Time.deltaTime);
    }

	public void OnInterract () {
        isBeingCarried = true;
        rigidbody.isKinematic = true;
        onCarriedStart?.Invoke ();

	}

	public void onDetach () {
        isBeingCarried = false;
        rigidbody.isKinematic = false;

		onCarriedEnd?.Invoke();
	}
}
