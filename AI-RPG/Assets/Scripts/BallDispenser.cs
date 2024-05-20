using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDispenser : MonoBehaviour
{
    [SerializeField]
    List<LightBall> lightBalls;

	[SerializeField]
	float watermarksHight;

	[SerializeField]
	float inWaterHight;

	[SerializeField]
	float minHight;
	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		foreach(LightBall ball in lightBalls) {
			if (ball.transform.localPosition.y < watermarksHight && !ball.isWaterMarking && !ball.isInWater)
				StartWaterMarks (ball);

			if (ball.transform.localPosition.y < inWaterHight && !ball.isInWater)
				HitWaterBall (ball);

			if (ball.transform.localPosition.y < minHight)
				ThrowBall (ball);

		}
    }


	void HitWaterBall (LightBall ball) {
		ball.isInWater = true;
		ball.ballRb.linearVelocity = ball.ballRb.linearVelocity * 0.2f;
		ball.waterMarks.Stop (true,ParticleSystemStopBehavior.StopEmitting);
	}
    void ThrowBall (LightBall ball) {
		if (ball.isWaterMarking)
			StopWaterMarks (ball);

		ball.transform.position = transform.position;
		ball.ballRb.linearVelocity= Vector3.zero;
		ball.ballRb.angularVelocity = Vector3.zero;
		ball.ballRb.AddForce (Vector3.up * 10 + new Vector3(Random.Range(-50,50),0,Random.Range (-50, 50)), ForceMode.Impulse);

		ball.isInWater = false;


	}

    void StartWaterMarks (LightBall ball) {
		ball.waterMarks.Play (true);
		ball.isWaterMarking = true;
	}

	void StopWaterMarks (LightBall ball) {
		ball.waterMarks.Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);
		ball.isWaterMarking = false;
	}
}
