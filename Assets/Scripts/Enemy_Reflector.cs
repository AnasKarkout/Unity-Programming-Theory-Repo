using UnityEngine;

public class Enemy_Reflector : Enemy
{
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
