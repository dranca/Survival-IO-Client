using System;
using UnityEngine;

public interface AttackControllerInput
{
    void Attack();
    void TryStopAttack();
}

public class AttackController : MonoBehaviour, AttackControllerInput
{

    public bool isAttacking = false;

    public Animator animator;

	// Use this for initialization
	void Start () {
        if (animator == null)
        {
            Debug.LogError("Please connect animator.");
        }
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("isAttacking", isAttacking);
    }

    public void Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);
    }

    public void TryStopAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }
}
