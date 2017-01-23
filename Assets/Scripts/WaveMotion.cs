using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMotion : MonoBehaviour {

    private float stateTime = 0F;
    public float offsetY;
    public float timeOffset;
    private float waveHeight = 1F;

	// Use this for initialization
	void Start () {
        offsetY = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
        stateTime += Time.deltaTime;
        waveHeight = Mathf.Lerp(waveHeight, GameStateManager.Instance.WaveHeight, 0.01F);
        transform.position = new Vector3(
            transform.position.x,
            ((waveHeight * 0.2F) * Mathf.Sin((stateTime + timeOffset) * waveHeight) + offsetY),
            transform.position.z
        );
	}
}
