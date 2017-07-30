using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    private NetworkingAttackAnimationInput animationController;
    private SFS2XConnectInput sfsConnect;

    private void Start()
    {
        animationController = GetComponent<NetworkingAttackAnimationInput>();
        if (animationController == null)
        {
            Debug.LogError("Add animation controller");
        }

        sfsConnect = GameObject.Find("DoesNotGetDestroyed").GetComponent<SFS2XConnectInput>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animationController.Attack();
            sfsConnect.sendUserStartedAttacking();
        } else if (Input.GetMouseButtonUp(0)) 
        {
            sfsConnect.sentUserStoppedAttacking();
            animationController.TryStopAttack();
        }
            
    }
}
