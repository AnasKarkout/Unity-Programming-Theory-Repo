using UnityEngine;

public class Player : Unit
{
    // A Player is a Unit that targets an Enemy and can collect Powerups

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void InitializeHealth()
    {
        //throw new System.NotImplementedException();
    }

    protected override void AcquireTarget()
    {
        //throw new System.NotImplementedException();
    }

    protected override void AttackTarget()
    {
        //throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        //throw new System.NotImplementedException();
    }

    protected override void EndUnit()
    {
        base.EndUnit();
    }
}
