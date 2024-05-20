using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireTransitionVFXSquence : MonoBehaviour
{
	[SerializeField]
    Animator manaqueenAnimator;

	[SerializeField]
    Animator WarrockAnimator;

	[SerializeField]
    VisualEffect vfxGraph;

	[SerializeField]
	float transitionDuration;

	[SerializeField]
	float transitionBackDuration;

	[SerializeField]
	AnimationCurve transitionAnimationCurve;

	[SerializeField]
	[Range(0,1)]
	float MonsterAnimationOnT;
	

	private void Start () {
	

	}

	[ContextMenu("PlaySequence")]
	public void PlaySequence () {
		StartCoroutine (PlaySequenceRoutine ());
	}

	public IEnumerator PlaySequenceRoutine () {
		yield return new WaitForSeconds (2);

		manaqueenAnimator.SetTrigger ("CastSpell");
		yield return new WaitForSeconds (1.1f);
		vfxGraph.SetFloat ("CharacterTransition", 0.002f);
		yield return new WaitForSeconds (4.6f);

		float startTime = Time.time;
		float t = 0;

		bool flag = false;
		while(Time.time - startTime < transitionDuration) {
			Debug.Log (t);
			t = (Time.time - startTime) / transitionDuration;

			float curveSample = transitionAnimationCurve.Evaluate (t);

		
			Debug.Log (curveSample);
			vfxGraph.SetFloat ("CharacterTransition", curveSample);

			if(!flag && t > MonsterAnimationOnT) {
				WarrockAnimator.SetTrigger ("Roar");
				flag = true;
			}

			yield return null;
		}
		vfxGraph.SetFloat ("CharacterTransition", 1);

		yield return new WaitForSeconds (5);

		startTime = Time.time;
		t = 0;

		
		while (Time.time - startTime < transitionBackDuration) {
			Debug.Log (t);
			t = 1 - (Time.time - startTime) / transitionBackDuration;

			float curveSample = transitionAnimationCurve.Evaluate (t);

			vfxGraph.SetFloat ("CharacterTransition", curveSample);

			yield return null;
		}

		vfxGraph.SetFloat ("CharacterTransition", 0);
		yield return null;
	}
}
