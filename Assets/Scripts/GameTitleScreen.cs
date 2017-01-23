using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitleScreen : MonoBehaviour {

	public GameObject GameLostTitle;
	public bool GameLostScreenShown = false;
	public GameObject GameWonTitle;
	public bool GameWonScreenShown = false;
	public float ShowLoseScreenDelay = 2.0f;
	public float ShowWinScreenDelay = 0.5f;

	void SetNewGameState ()
	{
		GameLostScreenShown = false;
		GameWonScreenShown = false;
	}

	// Use this for initialization
	void Start () {
		SetNewGameState ();
	}

	void ShowGameLostScreen ()
	{
		Instantiate (GameLostTitle, transform.position, transform.rotation);
	}

	void ShowGameWonScreen ()
	{
		Instantiate (GameWonTitle, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStateManager.Instance.GameWon) {
			if (!GameWonScreenShown) {
				GameWonScreenShown = true;
				Invoke("ShowGameWonScreen", ShowWinScreenDelay);
			}
		}

		if (GameStateManager.Instance.GameLost) {
			if (!GameLostScreenShown) {
				GameLostScreenShown = true;
				Invoke("ShowGameLostScreen", ShowLoseScreenDelay);
			}
		}
	}
}
