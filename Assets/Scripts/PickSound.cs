using System.Collections;
using UnityEngine;

public class PickSound : MonoBehaviour {

    public AudioClip[] clips;

    public float PlayMe(int clipIndex) {
        GetComponent<AudioSource>().clip = clips[clipIndex];
        GetComponent<AudioSource>().Play();

		return GetComponent<AudioSource>().clip.length;
    }

    void Update() {
        // testing code, commented out!
        //if (Input.GetKeyDown("space")) {
        //    PlayMe(0);
        //}
    }
}
