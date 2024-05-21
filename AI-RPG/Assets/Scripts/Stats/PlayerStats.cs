using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats 
{

	[SerializeField]
	float walkSpeed;
	public float getWalkSpeed => walkSpeed;

	[SerializeField]
	float sprintSpeed;
	public float getSprintSpeed => sprintSpeed;

	[SerializeField]
	float accelerationTime;
	public float getAccelerationTime => accelerationTime;

	[SerializeField]
	AnimationCurve accelerationCurve;
	public AnimationCurve getAccelerationCurve => accelerationCurve;


	[SerializeField]
	float rotationSpeed;
	public float getRotationSpeed => rotationSpeed;

	[SerializeField]
	float jumpForce;
	public float getJumpForce => jumpForce;
}
