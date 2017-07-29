using UnityEngine;

public class AttackController : MonoBehaviour {

    public Transform weaponSlot;

    public float attackDuration;

    private bool isAttacking = false;

	// Use this for initialization
	void Start () {
	    if (weaponSlot == null)
        {
            Debug.LogError("Please bind the weapons slot.");
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
