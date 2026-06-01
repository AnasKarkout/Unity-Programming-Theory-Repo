using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // A Unit is an entity that has health and can move and attack another Unit

    protected Unit target = null;
    protected float speed = 4;
    protected float maxHealth;
    protected float currentHealth;
    protected float damageStrength = 3;

    // Awake is called once when GameObject is loaded regardless if the script is enabled
    void Awake()
    {
        // initialize components and internal variables
        InitializeHealth();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void InitializeHealth();

    protected abstract void AcquireTarget();

    protected abstract void AttackTarget();

    protected abstract void Move();

    protected void DecreaseHealth(float damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            EndUnit();
        }
    }

    protected virtual void EndUnit()
    {

    }
}
