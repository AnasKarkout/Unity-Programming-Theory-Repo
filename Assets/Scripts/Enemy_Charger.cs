using UnityEngine;

public class Enemy_Charger : Enemy
{
    private bool isCharging = false;
    private bool isOnCooldown = false;
    private float chargeSpeedMultiplier = 7.0f;
    private float chargeCooldown = 2.0f;
    private Vector3 chargeTarget;

    protected override void FixedUpdate()
    {
        if (isCharging)
        {
            if (transform.position == chargeTarget && isOnCooldown)
            {
                Invoke("SetChargeTarget", chargeCooldown);
                isOnCooldown = false;
            }
        }
        base.FixedUpdate();
    }

    public override void TakeHit(float damageTaken)
    {
        if (!isCharging)
        {
            rotationSpeed *= 3;
            AttackTarget();
        }
        base.TakeHit(damageTaken);
    }

    protected override void DoMove()
    {
        if (isCharging)
        {
            Move(chargeTarget);
        }
        else
        {
            base.DoMove();
        }
    }

    protected override void AttackTarget()
    {
        isCharging = true;
        moveSpeed *= chargeSpeedMultiplier;
        SetChargeTarget();
    }

    private void SetChargeTarget()
    {
        if (TargetExists())
        {
            chargeTarget = target.transform.position;
            BoxCollider collider = GetComponent<BoxCollider>();
            Vector3 colliderSize = collider.bounds.size;
            chargeTarget.y = colliderSize.y / 2;
            isOnCooldown = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Unit collidedUnit = collision.gameObject.GetComponent<Unit>();
        if (collidedUnit != null && collidedUnit == target)
        {
            collidedUnit.TakeHit(damageStrength);
        }
    }

    protected override void ResetBehavior()
    {
        base.ResetBehavior();
        if (isCharging)
        {
            moveSpeed /= chargeSpeedMultiplier;
        }
        isCharging = false;
        isOnCooldown = false;
    }
}
