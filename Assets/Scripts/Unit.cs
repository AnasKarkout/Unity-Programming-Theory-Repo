using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // A Unit is an entity that has health and can move and attack another Unit

    [SerializeField] protected float moveSpeed = 4;
    [SerializeField] protected float rotationSpeed = 10.0f;
    [SerializeField] protected float maxHealth = 10;
    [SerializeField] protected float damageStrength = 3;

    protected Unit target = null;
    protected Rigidbody unitsRigidbody;
    protected Quaternion targetRotation;

    protected float currentHealth;
    protected float rotationThresholdToTarget = 5.0f;

    protected bool isLookingAtTarget = false;
    protected bool overrideRotation = false;

    [SerializeField] private GameObject bulletPrefab;
    protected PoolType bulletType;
    protected bool debug = false;


    // Awake is called once when GameObject is loaded regardless if the script is enabled
    protected virtual void Awake()
    {
        unitsRigidbody = GetComponent<Rigidbody>();
        // initialize components and internal variables
        InitializeHealth();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (TargetExists() && !overrideRotation)
        {
            CalculateAngleToTarget();
        }
    }

    protected virtual void FixedUpdate()
    {
        DoMove();
        DoRotation();
    }

    protected abstract void AcquireTarget();

    protected virtual void AttackTarget()
    {

        Bullet bullet = CreateBullet();
        bullet.damage = damageStrength;
        bullet.AimAtTarget(target);
    }

    private Bullet CreateBullet()
    {
        Vector3 currentPosition = transform.position;
        GameObject bulletObject = ObjectPooler.SharedInstance.GetPooledObject(bulletType);
        bulletObject.SetActive(true);
        bulletObject.transform.position = currentPosition + transform.forward;
        return bulletObject.GetComponent<Bullet>();
    }

    protected abstract void DoMove();

    protected virtual void Move(Vector3 targetDirection)
    {
        Vector3 moveDirection = Vector3.MoveTowards(transform.position, targetDirection, moveSpeed * Time.deltaTime);
        unitsRigidbody.MovePosition(moveDirection);
    }

    protected virtual void DoRotation()
    {
        if (TargetExists())
        {
            LookAtTarget();
        }
        else if (isLookingAtTarget)
        {
            isLookingAtTarget = false;
        }
    }

    private void LookAtTarget()
    {
        bool currRotWithinThreshold = Quaternion.Angle(transform.rotation, targetRotation) < rotationThresholdToTarget;
        if (!currRotWithinThreshold)
        {
            isLookingAtTarget = false;
        }
        if (!isLookingAtTarget)
        {
            Rotate();
            if (currRotWithinThreshold)
            {
                isLookingAtTarget = true;
            }
        }
    }

    protected virtual void Rotate()
    {
        float rotationStep = rotationSpeed * Time.fixedDeltaTime;
        Quaternion rotateTowards = Quaternion.RotateTowards(unitsRigidbody.rotation, targetRotation, rotationStep);
        unitsRigidbody.MoveRotation(rotateTowards);
    }

    private void CalculateAngleToTarget()
    {
        if (!overrideRotation)
        {
            Vector3 directionToTarget = target.transform.position - transform.position;
            directionToTarget.y = 0;
            targetRotation = Quaternion.LookRotation(directionToTarget.normalized);
        }
    }

    protected virtual void InitializeHealth()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeHit(float damageTaken)
    {
        currentHealth -= damageTaken;
        Debug.Log(gameObject.name + " took " + damageTaken + " hit and now has " + currentHealth + " hp.");
        if (currentHealth <= 0)
        {
            EndUnit();
        }
    }

    protected virtual void EndUnit()
    {
        gameObject.SetActive(false);
    }

    protected bool TargetExists()
    {
        return target != null && target.gameObject.activeInHierarchy;
    }
}
