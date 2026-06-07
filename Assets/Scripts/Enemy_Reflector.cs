using UnityEngine;

public class Enemy_Reflector : Enemy
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void TakeHit(float damageTaken)
    {
        ReflectDamage();
        base.TakeHit(damageTaken);
    }

    private void ReflectDamage()
    {
        AttackTarget();
    }
}
