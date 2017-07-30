using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface NetworkingAttackAnimationInput {
    void Attack();
    void TryStopAttack();
}


public class NetworkingAttackAnimation : MonoBehaviour, NetworkingAttackAnimationInput {

    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Please connect animator.");
        }
    }

    public void Attack()
    {
        animator.SetBool("isAttacking", true);
    }

    public void TryStopAttack()
    {
        animator.SetBool("isAttacking", false);
    }
}
