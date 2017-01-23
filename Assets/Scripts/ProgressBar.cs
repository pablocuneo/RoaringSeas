using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    [SerializeField]
    Image progress;

    public float barProgress = 0f;

    // Update is called once per frame
    void Update () {

        // We are revealing the filled bar by removing the 'empty bar' graphic. 
        progress.fillAmount = barProgress;
    }
}
