using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {
	public float moveSpeed = 5f;
	public float changeDirectionInterval = 2f;
	public float rotationSpeed = 5f;

	private Vector3 moveDirection;
	private float timeSinceLastDirectionChange;

	Vector3 startPos;
	void Start () {
		// Initialize movement direction
		startPos = transform.position;

		ChangeDirection ();
	}

	void Update () {
		// Move the object
		

		// Update time since last direction change
		timeSinceLastDirectionChange += Time.deltaTime;

		// Check if it's time to change direction
		if (timeSinceLastDirectionChange >= changeDirectionInterval) {
			// Change direction
			ChangeDirection ();
			// Reset the timer
			timeSinceLastDirectionChange = 0f;
		}

		// Rotate towards movement direction
		if (moveDirection != Vector3.zero) {
			Quaternion targetRotation = Quaternion.LookRotation (moveDirection);
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			transform.position += (transform.forward * moveSpeed * Time.deltaTime) ;
		}
	}

	void ChangeDirection () {

		if(Vector3.Distance(startPos,transform.position)>10){
			moveDirection = startPos - transform.position + new Vector3(Random.Range(-2,2),0, Random.Range (-2, 2));
			moveDirection.Normalize ();
			return;
		}
		// Generate a random direction
		moveDirection = Random.insideUnitSphere;
		moveDirection.y = 0; // Ensure the object moves only on the X-Z plane
		moveDirection.Normalize ();
	}
}