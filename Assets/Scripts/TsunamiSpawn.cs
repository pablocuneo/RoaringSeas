using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiSpawn : MonoBehaviour {

	public bool RightSideSpawner = true;
	public bool IsWaveInProgress = false;
	public GameObject TsunamiWave;

    // audio cue
    public AudioSource tsunamiCue;

	void Start () {
		
	}

	void Update () {
		if (RightSideSpawner && GameStateManager.Instance.LaunchTsunamiRight) {
			GameStateManager.Instance.LaunchTsunamiRight = false;
			Instantiate(TsunamiWave, transform.position, transform.rotation, this.transform);
			Debug.Log("Spawning Tsunami");
            tsunamiCue.Play();
		}

		if (!RightSideSpawner && GameStateManager.Instance.LaunchTsunamiLeft) {
			GameStateManager.Instance.LaunchTsunamiLeft = false;
			Instantiate(TsunamiWave, transform.position, transform.rotation, this.transform);
			Debug.Log("Spawning Tsunami");
		}
	}
}
