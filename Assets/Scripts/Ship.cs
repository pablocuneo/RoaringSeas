﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	public int ShipHitpoints = 1;
	public bool isSinking = false;

	// Use this for initialization
	void Start () {
		GameStateManager.Instance.ShipsFloatingAround++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
