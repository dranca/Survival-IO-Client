using UnityEngine;
using System.Collections;

public class RotationController : MonoBehaviour {
    private void LateUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward * Time.deltaTime, mousePos - transform.position);
    }
}
