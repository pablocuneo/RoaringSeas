using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleExplosionSelfDestruct : MonoBehaviour {

	public float TimeElapsed = 0;
	public float LifeExpectancy = 2.0f;


	// Use this for initialization
	void Start () {
		TimeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		TimeElapsed += Time.deltaTime;
		if (TimeElapsed > LifeExpectancy) {
			Destroy (this.gameObject);
		}
	}
}
