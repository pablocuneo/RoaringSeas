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
			//Debug.Log ("An enemy ship hit");
			//Debug.Log ("Reducing health from " + GameStateManager.Instance.PlayerHealth);
			if (!gameEnded) {
				GameStateManager.Instance.PlayerHealth -= 1;
				if (enemies != null) enemies.GetComponent<PlayRandom> ().PlaySound ();
			}
			//Debug.Log ("Current health is " + GameStateManager.Instance.PlayerHealth);
			GameStateManager.Instance.ShipsFloatingAround--;
		} else if (other.gameObject.tag == "EnemyShipMedium") {
			//Debug.Log("An enemy ship hit");
			//Debug.Log("Reducing health from " + GameStateManager.Instance.PlayerHealth);
			if (!gameEnded) {
				GameStateManager.Instance.PlayerHealth -= 2;
				if (enemies != null) enemies.GetComponent<PlayRandom> ().PlaySound ();
			}
			//Debug.Log("Current health is " + GameStateManager.Instance.PlayerHealth);
			GameStateManager.Instance.ShipsFloatingAround--;
		} else if (other.gameObject.tag == "EnemyShipLarge") {
			//Debug.Log("An enemy ship hit");
			//Debug.Log("Reducing health from " + GameStateManager.Instance.PlayerHealth);
			if (!gameEnded) {
				GameStateManager.Instance.PlayerHealth -= 3;
				if (enemies != null) enemies.GetComponent<PlayRandom> ().PlaySound ();
			}
			//Debug.Log("Current health is " + GameStateManager.Instance.PlayerHealth);
			GameStateManager.Instance.ShipsFloatingAround--;
        } else if (other.gameObject.tag == "HealthShip") {
            //Debug.Log("A health ship hit");
            //Debug.Log("Increasing health from " + GameStateManager.Instance.PlayerHealth);
			if (!gameEnded) {
				GameStateManager.Instance.PlayerHealth += 1;
				if (friendlies != null) friendlies.GetComponent<PlayRandom> ().PlaySound ();
			}
            //Debug.Log("Current health is " + GameStateManager.Instance.PlayerHealth);
			GameStateManager.Instance.ShipsFloatingAround--;
        } else {
			Debug.Log ("Castle hit by something else: " + other.gameObject.tag);
		}

		if (other.tag != "Tsunami") {
			Destroy (other.gameObject);
		}
	}
}
