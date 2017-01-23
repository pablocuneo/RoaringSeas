using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour {

    float stateTime;
    public float gradient = 1F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        stateTime += Time.deltaTime;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        float velocityY = Mathf.Cos(stateTime);
        body.velocity = new Vector2(0.2F, 0.2F * gradient * velocityY);
	}
}
