using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour {

	public bool GoingRight = true;
	public float horizontalSpeed = 1.0f;
	private float baseVerticalSpeed = 1.0f;
	private float baseMaxVerticalDelta = 0.30f;
	private float currentVerticalDelta = 0f;
	private bool bobbingUp = true;
	private Vector3 horizontalDirection;

	// Use this for initialization
	void Start () {
		if (GoingRight) {
			horizontalDirection = Vector3.right;
		} else {
			horizontalDirection = Vector3.left;
		}
	}

	void Update() {

		float waveHeight = GameStateManager.Instance.WaveHeight;
		float verticalSpeed = baseVerticalSpeed * waveHeight;
		float maxVerticalDelta = baseMaxVerticalDelta * waveHeight;

		transform.position += horizontalDirection * horizontalSpeed * Time.deltaTime;

		float distanceToMove = verticalSpeed * Time.deltaTime;
		if (bobbingUp) {
			transform.position += Vector3.up * distanceToMove; //TODO: check to see if it doesn't go over
			currentVerticalDelta += distanceToMove;

			if (currentVerticalDelta > maxVerticalDelta) {
				bobbingUp = false;
			}
		} else {
			transform.position += Vector3.down * distanceToMove; //TODO: check to see if it doesn't go over
			currentVerticalDelta -= distanceToMove;

			if (currentVerticalDelta < -maxVerticalDelta) {
				bobbingUp = true;
			}
		}
	}
}
