using UnityEngine;

public class Enemy : Unit
{
    // An Enemy is a Unit that targets the Player

    private Vector3 startingPosition;
    private Vector3 newPosition;
    private float localXBoundary = 1.5f;
    private float localZBoundary = 2.0f;
    private float moveTimeInterval = 5.0f;
    private bool isMovingAround = false;
    //private bool facingPlayer = false;

    protected override void Awake()
    {
        base.Awake();

        moveTimeInterval = Random.Range(4.5f, 7f);
        startingPosition = transform.position;
        newPosition = startingPosition;

        InvokeRepeating("SetNewPosition", moveTimeInterval, moveTimeInterval);
        AcquireTarget();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void AcquireTarget()
    {
        target = GameObject.Find("Player").GetComponent<Unit>();
    }

    private void SetNewPosition()
    {
        float newX = startingPosition.x + Random.Range(-localXBoundary, localXBoundary);
        float newZ = startingPosition.z + Random.Range(-localZBoundary, localZBoundary);

        newPosition = new Vector3(newX, startingPosition.y, newZ);
        isMovingAround = true;
    }
    
    protected override void DoMove()
    {
        if (isMovingAround)
        {
            Move(newPosition);
            bool currPosNearNewPos = Vector3.Distance(transform.position, newPosition) <= 0.001f;
            if (currPosNearNewPos)
            {
                isMovingAround = false;
            }
        }
    }

    protected override void EndUnit()
    {
        base.EndUnit();
    }
}
