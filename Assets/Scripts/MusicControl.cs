using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour {

    private AudioSource music;
    [SerializeField]
    private AudioClip[] musicClips;

	// Use this for initialization
	void Start () {
        music = GetComponent<AudioSource>();
        StartGameMusic();
	}

    public void StartGameMusic() {
        music.clip = musicClips[0];
        music.loop = true;
        music.volume = 0.8f;
        music.Play();
    }

    public void LoseGameMusic() {
        music.clip = musicClips[1];
        music.loop = false;
        music.volume = 1f;
        music.PlayOneShot(musicClips[2]);
    }

    public void WinGameMusic() {
        music.clip = musicClips[2];
        music.loop = false;
        music.volume = 1f;
        music.PlayOneShot(musicClips[1]);
    }
}
