using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiMove : MonoBehaviour {

	public bool GoingRight = true;
	public float horizontalSpeed = 8.0f;
	private Vector3 horizontalDirection;

	// Use this for initialization
	void Start () {
		if (GoingRight) {
			horizontalDirection = Vector3.right;
		} else {
			horizontalDirection = Vector3.left;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += horizontalDirection * horizontalSpeed * Time.deltaTime;
	}
}
