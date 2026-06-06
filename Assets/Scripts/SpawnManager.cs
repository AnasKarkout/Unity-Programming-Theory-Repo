using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] protected float spawnCooldown = 5.0f;
    //protected bool startSpawning = false;

    public virtual void StartSpawning()
    {
        //startSpawning = true;
    }
}
