using UnityEngine;

public class Enemy : Unit
{
    // An Enemy is a Unit that targets the Player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
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

    protected override void Move(Vector3 targetDestination)
    {
        //throw new System.NotImplementedException();
    }

    protected override void EndUnit()
    {
        base.EndUnit();
    }
}
