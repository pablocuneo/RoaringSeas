using UnityEngine;
using System.Collections;

public class GameStateManager : UnitySingletonPersistent<GameStateManager> {

	public float WaveHeight = 1;
	public int EnemyWaveNumberRight = 0;
	public int EnemyWaveNumberLeft = 0;
	public int PlayerHealth = 10;
	public bool GameLost = false;
	public bool LaunchTsunamiRight = false;
	public bool LaunchTsunamiLeft = false;
    public bool ExplodeCastle = false;
    public int ShipsFloatingAround = 0;
    public bool GameWon = false;
    public bool NoMoreShipsToSpawnLeft = false;
    public bool NoMoreShipsToSpawnRight = false;
    public float microphoneCooldown = 0F;
	public bool IsGamePaused = false;
	public bool AnimateDayNight = false;

	void Start () {
	}

	// Update is called once per frame
	void Update () {

    }
}
