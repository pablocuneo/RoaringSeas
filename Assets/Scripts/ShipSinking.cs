using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSinking : MonoBehaviour {

	public Ship ThisShip;
	public Sprite ShipSunkSprite;
	public GameObject ShipSprite;
	public float SinkingTime = 2.0f;
	public float explosionLingeringTime = 0.5f;

	private bool isSinking;
	private float sinkingTimer;
    private AudioSource shipDiesCue; 
    PlayRandom deathSound;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if ((GameStateManager.Instance.WaveHeight > ThisShip.ShipHitpoints) ||
			GameStateManager.Instance.GameWon || GameStateManager.Instance.GameLost){
			this.isSinking = true;

			ShipSprite.GetComponent<SpriteRenderer>().sprite = ShipSunkSprite;
			//this.sinkingTimer = 0f;  //option to reset time

			// while attacking, count up the timer.
			if (isSinking)
			{
				this.sinkingTimer += Time.deltaTime;

				// once the timer is 2 seconds, stop sinking and reset the sprite.
				if (this.sinkingTimer >= SinkingTime)
				{
					this.isSinking = false;
					this.sinkingTimer = 0f;
				}
			}

			//Debug.Log("Ship dies");
			Invoke("PlayDeathSound", 0);
			Invoke("DestroyMe", explosionLingeringTime); // shedules derived call 
		}
	}

	private void PlayDeathSound() {
		if (shipDiesCue != null) {
			deathSound = shipDiesCue.GetComponent<PlayRandom>();
			if (deathSound != null) deathSound.PlaySound ();
		}
	}

	private void DestroyMe() {
		GameStateManager.Instance.ShipsFloatingAround--;
		Destroy(gameObject);
	}
}
