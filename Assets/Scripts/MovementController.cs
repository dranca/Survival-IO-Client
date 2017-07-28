using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    private Rigidbody2D rigidBody;

    public float speed = 3;

	// Use this for initialization
	void Start () {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

        if (rigidBody == null)
        {
            Debug.LogError("Missing rigidbody please add.");
        }
	}

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        Vector3 MVMT = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidBody.MovePosition(MVMT * speed * Time.fixedDeltaTime + transform.position);
    }
}
