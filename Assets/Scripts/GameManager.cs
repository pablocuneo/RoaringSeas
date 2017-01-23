using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public GameObject[] UIPlayerHealth;
	private int playedNarration = -1;
	private int nextNarration = 0;

	[SerializeField]
	AudioSource musicAudioSource;
    MusicControl musicControl;
	[SerializeField]
	AudioSource narrator;

	void Awake () {
		// setup reference to game manager
		if (gm == null)
			gm = this.GetComponent<GameManager>();

        // setup all the variables, the UI, and provide errors if things not setup properly.
        //setupDefaults();
        musicControl = musicAudioSource.GetComponent<MusicControl>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//If the game is pause stop making game decisions
		if (GameStateManager.Instance.IsGamePaused) {
			return;
		}

		refreshGUI ();

		if (GameStateManager.Instance.WaveHeight >= 5 && !GameStateManager.Instance.GameLost) {
			GameStateManager.Instance.LaunchTsunamiRight = true;
			GameStateManager.Instance.LaunchTsunamiLeft = true;
			GameStateManager.Instance.GameLost = true;
			musicControl.LoseGameMusic();
		}

		if (GameStateManager.Instance.WaveHeight >= 4.8 && !GameStateManager.Instance.GameLost) {
			GameStateManager.Instance.LaunchTsunamiRight = true; 
			GameStateManager.Instance.LaunchTsunamiLeft = true;
			GameStateManager.Instance.GameLost = true;
			musicControl.LoseGameMusic();
		}

		if (GameStateManager.Instance.PlayerHealth <= 0 && !GameStateManager.Instance.GameLost) {
			GameStateManager.Instance.ExplodeCastle = true;
			GameStateManager.Instance.GameLost = true;
            musicControl.LoseGameMusic();
		}

		if (GameStateManager.Instance.ShipsFloatingAround <= 0 
			&& GameStateManager.Instance.NoMoreShipsToSpawnLeft && GameStateManager.Instance.NoMoreShipsToSpawnRight) {
			GameStateManager.Instance.GameWon = true;
            musicControl.WinGameMusic();
		}

		if (GameStateManager.Instance.EnemyWaveNumberRight == nextNarration &&
		    playedNarration != GameStateManager.Instance.EnemyWaveNumberRight &&
			!GameStateManager.Instance.GameLost && !GameStateManager.Instance.GameWon) {

			playedNarration = nextNarration;
			nextNarration++;
			narrator.GetComponent<PickSound>().PlayMe(GameStateManager.Instance.EnemyWaveNumberRight);
		}
	}

	void refreshGUI() {
		// turn on the appropriate number of life indicators in the UI based on the number of lives left
		for(int i=0;i<UIPlayerHealth.Length;i++) {
			if (i<(GameStateManager.Instance.PlayerHealth)) {
				UIPlayerHealth[i].SetActive(true);
			} else {
				UIPlayerHealth[i].SetActive(false);
			}
		}
	}
}
