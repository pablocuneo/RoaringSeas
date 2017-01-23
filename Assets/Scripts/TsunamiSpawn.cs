using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiSpawn : MonoBehaviour {

	public bool RightSideSpawner = true;
	public bool IsWaveInProgress = false;
	//public bool GoingRight = true;
	public GameObject TsunamiWave;

    // audio cue
    public AudioSource tsunamiCue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.T)) {
			Instantiate(TsunamiWave, transform.position, transform.rotation);
			Debug.Log("Spawning Tsunami");
		}

		if (RightSideSpawner && GameStateManager.Instance.LaunchTsunamiRight) {
			GameStateManager.Instance.LaunchTsunamiRight = false;
			Instantiate(TsunamiWave, transform.position, transform.rotation);
			Debug.Log("Spawning Tsunami");
            tsunamiCue.Play();
		}

		if (!RightSideSpawner && GameStateManager.Instance.LaunchTsunamiLeft) {
			GameStateManager.Instance.LaunchTsunamiLeft = false;
			Instantiate(TsunamiWave, transform.position, transform.rotation);
			Debug.Log("Spawning Tsunami");
		}
	}
}
