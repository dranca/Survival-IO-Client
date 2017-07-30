using UnityEngine;
using System.Collections;

public class CameraAspectRatio : MonoBehaviour {

    private float previousHeight;
    private float initialSize;
    private float initialHeight;

    // Use this for initialization
    void Start()
    {
        previousHeight = Screen.height;
        initialSize = Camera.main.orthographicSize;
        initialHeight = previousHeight;
    }


    // Update is called once per frame
    void LateUpdate () {
	    if (previousHeight != Screen.height)
        {
            var scale = Screen.height / initialHeight;
            print(scale);
            Camera.main.orthographicSize = scale * initialSize;
            previousHeight = Screen.height;
        }
	}
}
