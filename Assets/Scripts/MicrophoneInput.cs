using System.Collections;
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
    public float[] noiseThresholds = { 0.1F, 0.2F, 0.3F, 0.4F, 0.5F };
    public GameObject gui;
    private float timeAtNoiseLevel = 0F;
    private int lastNoiseLevel = 0;
    public readonly float timeAtNoiseLevelRequired = 3F;
    public readonly float cheatVolumeLevel = 1F;
    public readonly float cooldownTime = 0.2F;
    private float openTime = 1F;
    public float maxOpenTime = 1F;
	private float deadZone = 0.2F;

    void StartMic() {
        if (device == null) {
            device = Microphone.devices[0];
            clip = Microphone.Start(device, true, 999, 44100);
        }
    }

    void StopMic() {
        if (device != null) {
            Microphone.End(device);
        }
    }

    float CalculateMaximumVolume() {
        float maximumVolume = 0F;
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

	// Use this for initialization
	void Start () {
        StartMic();
	}

    // Update is called once per frame
    void Update() {
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

			if (noiseLevel < GameStateManager.Instance.WaveHeight) {
				GameStateManager.Instance.WaveHeight -= Time.deltaTime * 1.1F;
				if (timeAtNoiseLevel >= timeAtNoiseLevelRequired) {
					if (noiseLevel > 0) {
						godCalm.GetComponent<PlayRandom> ().PlaySound ();
					}
					timeAtNoiseLevel = 0F;
				}
			} else {
				if (GameStateManager.Instance.microphoneCooldown <= 0) {
					if (openTime > 0F) {
						GameStateManager.Instance.WaveHeight = Mathf.Lerp (GameStateManager.Instance.WaveHeight, volume * 5F, 0.1F);
						godAngry.GetComponent<PlayRandom> ().PlaySound ();
						openTime -= Time.deltaTime;
					} else {
						if (volume > deadZone) {
							GameStateManager.Instance.microphoneCooldown = cooldownTime;
						}
					}
				}
			}
	        
			if (GameStateManager.Instance.WaveHeight < 1)
				GameStateManager.Instance.WaveHeight = 1;

			if (GameStateManager.Instance.WaveHeight > 5)
				GameStateManager.Instance.WaveHeight = 5;
			
			gui.GetComponentInChildren<ProgressBar> ().barProgress = (GameStateManager.Instance.WaveHeight - 1) / 4F;
		}
    }
}
