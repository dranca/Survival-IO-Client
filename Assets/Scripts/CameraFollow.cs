using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    public Vector3 offset;

    void Start () {
	    if (target == null)
        {
            Debug.LogError("No target. Consider adding one.");
        }
	}

    private void LateUpdate()
    {
        gameObject.transform.position = target.transform.position + offset;
    }
}
