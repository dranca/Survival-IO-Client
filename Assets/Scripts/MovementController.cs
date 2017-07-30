using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    private Rigidbody2D rigidBody;

    public float speed = 3;
    private SFS2XConnectInput networkingManager;

    private bool isMovementDirty = false;

	// Use this for initialization
	void Start () {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

        if (rigidBody == null)
        {
            Debug.LogError("Missing rigidbody please add.");
        }

        networkingManager = GameObject.Find("DoesNotGetDestroyed").GetComponent<SFS2XConnectInput>();
    }

    private void Update()
    {
        Vector3 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidBody.MovePosition(transform.position + direction * speed * Time.deltaTime);
        if (direction != Vector3.zero)
        {
            isMovementDirty = true;
        }
    }

    private void FixedUpdate()
    {
        if (isMovementDirty)
        {
            networkingManager.sendTransformData(transform);
            isMovementDirty = false;
        }
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 100), "X: "+transform.position.x + " Y: " + transform.position.y);
    }
}
