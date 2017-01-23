using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawnerScript : MonoBehaviour {

	public bool GoingRight = true;
	public GameObject EnemyShipSmall;
	public GameObject EnemyShipMedium;
	public GameObject EnemyShipLarge;
	public GameObject HealthShip;

	public GameObject EnemiesParentGroup;

	public bool IsWaveInProgress = false;

	public string[][] waves;

    private float waveTime = 0F;
    private float lastLaunch = -0.5F;
    public float shipDelay = 2F;

    private bool win = false;
    public GameObject winText;
    public AnimationClip winAnimation;

    private float timeSinceLastWave = 4F;
    public float timeBetweenWaves = 6F;

	int GetEnemyWaveNumber(){
		if (GoingRight) {
			return GameStateManager.Instance.EnemyWaveNumberLeft;
		} else {
			return GameStateManager.Instance.EnemyWaveNumberRight;
		}
	}

	int IncreaseEnemyWaveNumber(){
		if (GoingRight) {
			return GameStateManager.Instance.EnemyWaveNumberLeft++;
		} else {
			return GameStateManager.Instance.EnemyWaveNumberRight++;
		}
	}

	// Use this for initialization
	void Start () {
		if (GoingRight)
			waves = new string[][] {
				//new string[] { "EnemyShipSmall"}
				new string[] { "EnemyShipSmall", "EnemyShipSmall", "EnemyShipSmall", "EnemyShipSmall" },
	            new string[] { "EnemyShipSmall", "EnemyShipSmall", "HealthShip", "EnemyShipSmall" },
	            new string[] { "EnemyShipSmall", "EnemyShipSmall", "EnemyShipMedium", "HealthShip", "EnemyShipMedium" },
	            new string[] { "HealthShip", "HealthShip", "EnemyShipLarge", "HealthShip", "EnemyShipSmall" },
	            new string[] { "EnemyShipMedium", "EnemyShipMedium", "EnemyShipMedium", "EnemyShipLarge", "EnemyShipMedium" },
	            new string[] { "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge" },
	            new string[] { "EnemyShipLarge", "HealthShip", "EnemyShipLarge", "EnemyShipLarge", "HealthShip", "EnemyShipLarge" },
	            new string[] { "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "HealthShip", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge" }
			};
		else 
			waves = new string[][] {
				//new string[] { "EnemyShipSmall"}
				new string[] { "EnemyShipSmall", "EnemyShipSmall", "EnemyShipSmall", "EnemyShipSmall" },
				new string[] { "EnemyShipSmall", "EnemyShipSmall", "HealthShip", "EnemyShipSmall" },
				new string[] { "EnemyShipSmall", "EnemyShipSmall", "EnemyShipMedium", "HealthShip", "EnemyShipMedium" },
				new string[] { "HealthShip", "HealthShip", "EnemyShipLarge", "HealthShip", "EnemyShipSmall" },
				new string[] { "EnemyShipMedium", "EnemyShipMedium", "EnemyShipMedium", "EnemyShipLarge", "EnemyShipMedium" },
				new string[] { "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge" },
				new string[] { "EnemyShipLarge", "HealthShip", "EnemyShipLarge", "EnemyShipLarge", "HealthShip", "EnemyShipLarge" },
				new string[] { "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "HealthShip", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge", "EnemyShipLarge" }
			};
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStateManager.Instance.GameWon || GameStateManager.Instance.GameLost) {
			win = true;
		}

        if (!IsWaveInProgress && !win) {
            if (timeSinceLastWave < timeBetweenWaves) {
                timeSinceLastWave += Time.deltaTime;
            } else {
				Debug.Log("Launching wave number: " + GetEnemyWaveNumber());
                IsWaveInProgress = true;
                waveTime = 0F;
                lastLaunch = -0.5F;
            }
        }

        if (IsWaveInProgress) {
            waveTime += Time.deltaTime;


			for (int i = 0; i < waves[GetEnemyWaveNumber()].Length; i++) {
                if (lastLaunch < i * shipDelay && waveTime >= i * shipDelay) {
					string shipName = waves[GetEnemyWaveNumber()][i];
                    switch (shipName) {
                        case "EnemyShipSmall":
                            Instantiate(EnemyShipSmall, transform.position, transform.rotation);
                            break;
                        case "EnemyShipMedium":
                            Instantiate(EnemyShipMedium, transform.position, transform.rotation);
                            break;
                        case "EnemyShipLarge":
                            Instantiate(EnemyShipLarge, transform.position, transform.rotation);
                            break;
                        case "HealthShip":
                            Instantiate(HealthShip, transform.position, transform.rotation);
                            break;
                    }
                    lastLaunch = waveTime;
                }
            }

			if (waveTime >= waves[GetEnemyWaveNumber()].Length * shipDelay) {
                IsWaveInProgress = false;
                timeSinceLastWave = 0F;
				if (GetEnemyWaveNumber() < waves.Length - 1) {
					IncreaseEnemyWaveNumber ();
				} else {
                    win = true;
                    //winText.GetComponent<Animator>().Play(winAnimation.name);

					if (GoingRight) {
						GameStateManager.Instance.NoMoreShipsToSpawnLeft = true;
					} else {
						GameStateManager.Instance.NoMoreShipsToSpawnRight = true;
					}
                }
            }
        }
	}
}
