using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class BasicUnit : MonoBehaviour
{

    #region Private Varubles

    bool pressed;



    Material _matSave;
    NavMeshAgent _agent;
    Coroutine rotationRoutine;
    Coroutine arrivalCheckRoutine;


	#endregion




	#region SerializeFields

	[SerializeField]
	MeshRenderer _Mat;
	[SerializeField] Material ClickedMat;
    [SerializeField] float rotationSpeed;

    #endregion



   
    #region Unit_Inner_Processes
    private void Start()
    {
        GetComponents();
        _matSave = _Mat.material;
    }
    public void GetComponents()
    {
        _agent = GetComponent<NavMeshAgent>();
  
    }
 
    /// <summary>
    /// Set unit Orientation Towards Certain pos
    /// </summary>
    /// <param name="lookDir"></param>
    /// <returns></returns>
    private IEnumerator RotateTowards(Vector3 lookDir)
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        Quaternion initialRotation = transform.rotation;
		float t = 0;
		while (transform.rotation != Quaternion.Euler(-lookRotation.x, -lookRotation.y, -lookRotation.z) && t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            t = math.clamp(t, 0, 1);

			Debug.Log ("ROTATING");
			Debug.Log (transform.rotation);
			Debug.Log (lookRotation);
			transform.rotation = Quaternion.Lerp(initialRotation, lookRotation, t);
            yield return null;
        }
        Debug.Log ("DoneRotating");

    }
    IEnumerator ArrivalCheck(Vector3 Destination, Vector3 orientation)
    {
		if (rotationRoutine != null) {
			StopCoroutine (rotationRoutine);
		}

		yield return null;
        Debug.Log (_agent.pathStatus == NavMeshPathStatus.PathComplete);
		Debug.Log (_agent.remainingDistance);
		yield return new WaitUntil(() => _agent.pathStatus == NavMeshPathStatus.PathComplete && _agent.remainingDistance == 0);

		
		Debug.Log ("Arrived");
		Debug.Log (_agent.pathStatus == NavMeshPathStatus.PathComplete);
		Debug.Log (_agent.remainingDistance);
		

		rotationRoutine = StartCoroutine (RotateTowards(orientation));
    }
    #endregion



    //All activity related to Player commands 
    #region PlayerCommands


    /// <summary>
    /// Unit reaction when Marked by player
    /// </summary>
    public void Clicked()
    {
        _Mat.material = ClickedMat;
        pressed = true;
    }

    /// <summary>
    /// Unit reaction when Mark released by player
    /// </summary>
    public void ReleaseCilck()
    {
        _Mat.material = _matSave;
        pressed = false;
    }

    /// <summary>
    /// Sends Unit towards Certain Location and set orientation after arrival
    /// </summary>
    /// <param name="Destination"></param>
    /// <param name="orientation"></param>
    public void GoTo(Vector3 Destination,Vector3 orientation)
    {
        _agent.SetDestination(Destination);

        if(arrivalCheckRoutine != null) {
            StopCoroutine(arrivalCheckRoutine);
        }

		arrivalCheckRoutine = StartCoroutine (ArrivalCheck(Destination, orientation));
        
    }

   

    #endregion


}