using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleCollision : MonoBehaviour {

    public AudioSource enemies;
    public AudioSource friendlies;

	public GameObject CastleExplosion;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (GameStateManager.Instance.ExplodeCastle){
			GameStateManager.Instance.ExplodeCastle = false;

			Instantiate(CastleExplosion, transform.position, transform.rotation);
			Debug.Log("Castle Explosion Spawn");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		bool gameEnded = false;
		if (GameStateManager.Instance.GameLost || GameStateManager.Instance.GameWon) {
			gameEnded = true;
		}

		if (other.gameObject.tag == "EnemyShipSmall") {
			Ship ship = other.gameObject.GetComponent<Ship> ();
			//Debug.Log ("An enemy ship hit");
			if (!gameEnded && !ship.isSinking) {
				GameStateManager.Instance.PlayerHealth -= 1;
				if (enemies != null) enemies.GetComponent<PlayRandom> ().PlaySound ();
			}
			//Debug.Log ("Current health is " + GameStateManager.Instance.PlayerHealth);
			GameStateManager.Instance.ShipsFloatingAround--;
		} else if (other.gameObject.tag == "EnemyShipMedium") {
			Ship ship = other.gameObject.GetComponent<Ship> ();
			//Debug.Log("An enemy ship hit");
			if (!gameEnded && !ship.isSinking) {
				GameStateManager.Instance.PlayerHealth -= 2;
				if (enemies != null) enemies.GetComponent<PlayRandom> ().PlaySound ();
			}
			//Debug.Log("Current health is " + GameStateManager.Instance.PlayerHealth);
			GameStateManager.Instance.ShipsFloatingAround--;
		} else if (other.gameObject.tag == "EnemyShipLarge") {
			Ship ship = other.gameObject.GetComponent<Ship> ();
			//Debug.Log("An enemy ship hit");
			if (!gameEnded && !ship.isSinking) {
				GameStateManager.Instance.PlayerHealth -= 3;
				if (enemies != null) enemies.GetComponent<PlayRandom> ().PlaySound ();
			}
			//Debug.Log("Current health is " + GameStateManager.Instance.PlayerHealth);
			GameStateManager.Instance.ShipsFloatingAround--;
        } else if (other.gameObject.tag == "HealthShip") {
			Ship ship = other.gameObject.GetComponent<Ship> ();
            //Debug.Log("A health ship hit");
			if (!gameEnded && !ship.isSinking) {
				if (GameStateManager.Instance.PlayerHealth < 10) {
					GameStateManager.Instance.PlayerHealth += 1;
				}
				if (friendlies != null) friendlies.GetComponent<PlayRandom> ().PlaySound ();
			}
            //Debug.Log("Current health is " + GameStateManager.Instance.PlayerHealth);
			GameStateManager.Instance.ShipsFloatingAround--;
        } else {
			Debug.Log ("Castle hit by something else: " + other.gameObject.tag);
		}

		//Let the tsunami wash trought the castle
		if (other.tag != "Tsunami") {
			Destroy (other.gameObject);
		}
	}
}
