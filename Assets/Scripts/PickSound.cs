using System.Collections;
using UnityEngine;

public class PickSound : MonoBehaviour {

    public AudioClip[] clips;

    public void PlayMe(int clipIndex) {
        GetComponent<AudioSource>().clip = clips[clipIndex];
        GetComponent<AudioSource>().Play();
    }

    void Update() {
        // testing code, commented out!
        //if (Input.GetKeyDown("space")) {
        //    PlayMe(0);
        //}
    }
}
