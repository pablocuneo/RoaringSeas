using System.Collections;
using UnityEngine;

public class PlayRandom : MonoBehaviour {

    public AudioClip[] clips;

    // Plays a random sound from an array; drop audio clips into the array in the Inspector.
    public void PlaySound() {
        int clipPick = Random.Range(0, clips.Length);
        AudioSource sfx = GetComponent<AudioSource>();
        if (!sfx.isPlaying) {
            sfx.clip = clips[clipPick];
            sfx.pitch = Random.Range(0.85f, 1.05f);
            sfx.Play();
        }
            
    }
    
	void Update () {
        // Testing purposes only!
        //if (Input.GetKeyDown("space")) {
        //    PlaySound();
	}
}
