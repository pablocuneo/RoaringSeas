﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {

    [SerializeField]
    AudioSource godAngry;
    [SerializeField]
    AudioSource godCalm;
    public float volume;
    private string device;
    AudioClip clip = new AudioClip();
    int sampleWindow = 128;
    bool isInitialized;
    public int noiseLevel = 0;
	public float[] noiseThresholds;
    public GameObject gui;
	public GameObject MainMenuUI;
    private float timeAtNoiseLevel = 0F;
    private int lastNoiseLevel = 0;
    public readonly float timeAtNoiseLevelRequired = 3F;
    public readonly float cheatVolumeLevel = 1F;
    public readonly float cooldownTime = 0.2F;
    private float openTime = 1F;
    public float maxOpenTime = 1F;
	private float deadZone;
	public float newWaveHeight;
	public float maximumVolume;

	public GameObject MicrophoneTestText;


	void Start () {
		//#if !UNITY_EDITOR && ( UNITY_ANDROID || UNITY_IOS )
		//deadZone = 0.05F;
		//noiseThresholds = new float[] { 0.025f, 0.05f, 0.075f, 0.1f, 0.125f };
		//#else
		deadZone = 0.2F;
		noiseThresholds = new float[] { 0.1F, 0.2F, 0.3F, 0.4F, 0.5F };
		//#endif

		StartMic();
	}

    void StartMic() {
        if (device == null) {
			/*
			int min = 0;
			int max = 0;
			Microphone.GetDeviceCaps(Microphone.devices[0], out min, out max);
			Debug.Log (string.Format("MinFreq: {0} - MaxFreq: {1}", min, max));
			*/

			#if !UNITY_EDITOR && ( UNITY_ANDROID || UNITY_IOS )
			int min = 0;
			int max = 0;
			Microphone.GetDeviceCaps(Microphone.devices[0], out min, out max);
			//clip = Microphone.Start(Microphone.devices[0], true, 2, max);
			//clip = Microphone.Start(Microphone.devices[0], true, 2, 44100);
			clip = Microphone.Start(Microphone.devices[0], true, 999, min);
			device = Microphone.devices[0];
			#else
			device = Microphone.devices[0];
			clip = Microphone.Start(device, true, 999, 44100);
			#endif
        }
    }

    void StopMic() {
        if (device != null) {
            Microphone.End(device);
        }
    }

    float CalculateMaximumVolume() {
		maximumVolume = 0F;
        if (Input.GetKey(KeyCode.R)) {
            maximumVolume = cheatVolumeLevel;
        } else {
            float[] waveData = new float[sampleWindow];
            int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1);
            if (micPosition < 0) return 0;
            clip.GetData(waveData, micPosition);
            for (int i = 0; i < sampleWindow; i++) {
                float peak = waveData[i] * waveData[i];
                if (maximumVolume < peak) {
                    maximumVolume = peak;
                }
            }
        }
        
        return maximumVolume;
    }
		
    void Update() {
		newWaveHeight = GameStateManager.Instance.WaveHeight;

		if (!GameStateManager.Instance.GameWon && !GameStateManager.Instance.GameLost) {

			if (GameStateManager.Instance.microphoneCooldown > 0F) {
				GameStateManager.Instance.microphoneCooldown -= Time.deltaTime;
				if (GameStateManager.Instance.microphoneCooldown < 0F) {
					GameStateManager.Instance.microphoneCooldown = 0F;
					openTime = maxOpenTime;
				}
			}
			if (openTime < maxOpenTime) {
				openTime -= Time.deltaTime;
				if (openTime < 0F) {
					openTime = 0F;
				}
			}
			volume = CalculateMaximumVolume ();
			int newNoiseLevel = 0;
			while (noiseThresholds.Length > newNoiseLevel && volume >= noiseThresholds [newNoiseLevel]) {
				newNoiseLevel++;
			}
			lastNoiseLevel = noiseLevel;
			noiseLevel = newNoiseLevel;

			if (noiseLevel == lastNoiseLevel) {
				timeAtNoiseLevel += Time.deltaTime;
			} else {
				timeAtNoiseLevel = 0F;
			}

			if (noiseLevel < newWaveHeight) {
				newWaveHeight -= Time.deltaTime * 1.1F;
				if (timeAtNoiseLevel >= timeAtNoiseLevelRequired) {
					if (noiseLevel > 0) {
						godCalm.GetComponent<PlayRandom> ().PlaySound ();
					}
					timeAtNoiseLevel = 0F;
				}
			} else {
				if (GameStateManager.Instance.microphoneCooldown <= 0) {
					if (openTime > 0F) {
						newWaveHeight = Mathf.Lerp (newWaveHeight, volume * 5F, 0.1F);
						godAngry.GetComponent<PlayRandom> ().PlaySound ();
						openTime -= Time.deltaTime;
					} else {
						if (volume > deadZone) {
							GameStateManager.Instance.microphoneCooldown = cooldownTime;
						}
					}
				}
			}
	        
			if (newWaveHeight < 1)
				newWaveHeight = 1;

			if (newWaveHeight > 5)
				newWaveHeight = 5;

			GameStateManager.Instance.WaveHeight = newWaveHeight;
			if (GameStateManager.Instance.IsGamePaused) {
				if (MainMenuUI != null) {
					ProgressBar progressBar = MainMenuUI.GetComponentInChildren<ProgressBar> ();
					if (progressBar != null) progressBar.barProgress = (newWaveHeight - 1) / 4F;
				}

				if (newWaveHeight >= 3) {
					MainMenuUI.GetComponent<StartOptions> ().StartGameInScene();
					GameStateManager.Instance.WaveHeight = 1;
				}
			} else {
				gui.GetComponentInChildren<ProgressBar> ().barProgress = (newWaveHeight - 1) / 4F;
			}
		}

		//string showTestText = string.Format("volume: {0}, NoiseLvl: {1}, maxVol: {2}", volume, noiseLevel, maximumVolume);
		//if (MicrophoneTestText != null) MicrophoneTestText.GetComponent<Text> ().text = showTestText;
    }
}
