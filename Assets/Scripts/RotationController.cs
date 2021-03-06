﻿using UnityEngine;
using System.Collections;

public class RotationController : MonoBehaviour {

    public bool isRotationDirty = false;

    private SFS2XConnectInput networkingManager;

    Vector3 previousPos;

    private void Start()
    {
        networkingManager = GameObject.Find("DoesNotGetDestroyed").GetComponent<SFS2XConnectInput>();
    }

    private void LateUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward * Time.deltaTime, mousePos - transform.position);
        isRotationDirty = true;
    }

    private void FixedUpdate()
    {
        if (isRotationDirty)
        {
            networkingManager.sendRotationData((double)transform.rotation.eulerAngles.z);
            isRotationDirty = false;
        }
    }

}
