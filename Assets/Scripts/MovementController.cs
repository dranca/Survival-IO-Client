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

    private void Update()
    {
        Vector3 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        rigidBody.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }
}
