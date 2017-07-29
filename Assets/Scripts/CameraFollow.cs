using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public float dampTime = 0.15f;

    void Start () {
	    if (target == null)
        {
            Debug.LogError("No target. Consider adding one.");
        }
	}

    private void LateUpdate()
    {
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, target.transform.position + offset, dampTime);
    }

    void Update()
    {
        if (!target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.transform.position);
            Vector3 delta = target.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}
