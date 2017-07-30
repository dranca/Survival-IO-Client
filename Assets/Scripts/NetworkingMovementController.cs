using UnityEngine;
using System.Collections;
using System;

public interface NetworkingMovementControllerInput
{
    void moveToPosition(Vector2 targetPosition);
    void rotateToEuler(float euler);
}

public class NetworkingMovementController : MonoBehaviour, NetworkingMovementControllerInput {

    private Vector2 targetPosition;
    private float targetRotation;

    public void moveToPosition(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public void rotateToEuler(float euler)
    {
        this.targetRotation = euler;
    }

    // Update is called once per frame
    void Update () {
	    if (targetPosition != null)
        {
            // TODO: Add smooth movement
            gameObject.transform.position = targetPosition;
        }
        if (targetRotation > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        }
	}
}
