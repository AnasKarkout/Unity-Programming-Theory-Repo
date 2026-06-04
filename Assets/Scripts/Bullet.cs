using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float damage = 0;

    [SerializeField] private float bulletSpeed = 5.0f;
    private Unit target;
    private Vector3 moveDirection;
    private float timeAlive = 5.0f;

    // Update is called once per frame
    void Update()
    {
        MoveToTargetDirection();
    }

    private void MoveToTargetDirection()
    {
        if (moveDirection != null)
        {
            transform.position += moveDirection * bulletSpeed * Time.deltaTime;
        }
    }

    public void AimAtTarget(Unit targetUnit)
    {
        target = targetUnit;
        Vector3 targetDirection = target.transform.position;
        moveDirection = (targetDirection - transform.position).normalized;
        transform.up = moveDirection;
        Destroy(gameObject, timeAlive);
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit collidedUnit = other.gameObject.GetComponent<Unit>();
        if (collidedUnit != null && collidedUnit == target)
        {
            collidedUnit.TakeHit(damage);
        }
        Destroy(gameObject);
    }
}
