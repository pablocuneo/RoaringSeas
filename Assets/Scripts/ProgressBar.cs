using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    [SerializeField]
    Image progress;

    public float barProgress = 0f;

    // Update is called once per frame
    void Update () {

        // We are revealing the filled bar by removing the 'empty bar' graphic. 
        // Time.deltaTime is a placeholder for the actual value we want to feed the bar.
        progress.fillAmount = barProgress;
    }
}
