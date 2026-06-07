using System.Collections;
using UnityEngine;

public class Enemy_Ranged : Enemy
{
    [SerializeField] private float attackDelay = 1.0f; // seconds between attacks
    private bool isAimingAtTarget = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isLookingAtTarget && !isAimingAtTarget)
        {
            StartCoroutine(FireAtPlayer());
        }
    }

    IEnumerator FireAtPlayer()
    {
        isAimingAtTarget = true;
        while (isLookingAtTarget)
        {
            AttackTarget();
            yield return new WaitForSeconds(attackDelay);
        }
        isAimingAtTarget = false;
    }

    protected override void ResetBehavior()
    {
        base.ResetBehavior();
        isAimingAtTarget = false;
    }
}
