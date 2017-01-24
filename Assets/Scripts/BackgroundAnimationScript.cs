using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimationScript : MonoBehaviour {

	private float currentAnimationSpeed;

	void Start () {
		SetAnimationSpeed (0);
	}
		
	void Update () {
		if (GameStateManager.Instance.AnimateDayNight) {
			if (currentAnimationSpeed <= 0) {
				SetAnimationSpeed (0.8f);
			}
		} else {
			SetAnimationSpeed (0);
		}
	}

	void SetAnimationSpeed (float animationSpeed)
	{
		currentAnimationSpeed = animationSpeed;
		Animator animator = this.gameObject.GetComponent<Animator> ();
		int defaultLayerIndex = 0;
		if (animator.GetCurrentAnimatorStateInfo (defaultLayerIndex).IsName ("Day-night cycle")) {
			animator.speed = animationSpeed;
		}
	}
}
