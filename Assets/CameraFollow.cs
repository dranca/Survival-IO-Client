using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    public Vector3 offset;
    private Vector2 velocity = Vector2.zero;
    void Start () {
	    if (target == null)
        {
            Debug.LogError("No target. Consider adding one.");
        }
	}

    private void LateUpdate()
    {
        gameObject.transform.position = Vector2.SmoothDamp(gameObject.transform.position, target.transform.position + offset, ref velocity, Time.deltaTime);
    }
}
