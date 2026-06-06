using System.Collections.Generic;
using UnityEngine;

public enum PoolType { Enemy, Powerup, BulletPlayer, BulletEnemy }

public abstract class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    private static bool SpawnStarted = false;

    [SerializeField] protected GameObject[] arrayOfPrefabsToPool;
    [SerializeField] private GameObject singlePrefabToPool;
    [SerializeField] protected int amountToPool; // number of enemies active = the number of enemy platforms

    protected List<GameObject> pooledEnemies;
    protected List<GameObject> pooledPowerups;
    protected List<GameObject> pooledPlayerBullets;
    protected List<GameObject> pooledEnemyBullets;

    protected virtual void Awake()
    {
        if (SharedInstance == null)
        {
            // If not, set instance to this
            SharedInstance = this;
        }
        /*else if (SharedInstance != this)
        {
            // If instance already exists and it's not this, then destroy this to enforce the singleton. Singleton is a design pattern that restricts a class to a single instance and provides a global point of access to it, typically via a static property like ClassName.Instance.
            // This pattern is commonly used for manager scripts (e.g., GameManager, AudioManager, UIManager) to ensure that global game state or services are accessible from any other script without needing to manually attach references.
            Destroy(gameObject);
        }*/
        pooledEnemies = new List<GameObject>();
        pooledPowerups = new List<GameObject>();
        pooledPlayerBullets = new List<GameObject>();
        pooledEnemyBullets = new List<GameObject>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!SpawnStarted)
        {
            StartSpawnManagers();
        }
    }

    protected void PoolObjectsFromArray()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            for (int j = 0; j < arrayOfPrefabsToPool.Length; j++)
            {
                GameObject obj = (GameObject)Instantiate(arrayOfPrefabsToPool[j]);
                PoolObject(obj);
            }
        }
    }

    protected void PoolObjectsFromSinglePrefab()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(singlePrefabToPool);
            PoolObject(obj);
        }
    }

    private void PoolObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.transform);
        AddObjectToPool(obj);
    }

    protected abstract void AddObjectToPool(GameObject obj);

    public GameObject GetPooledObject(PoolType poolType)
    {
        switch (poolType)
        {
            // Since using a shared instance and this is in the base class, the base class method will be called.
            // So in each base class method, call the specific child method that needs to run to force that override
            // version to be called
            case PoolType.Enemy:
                return GetPooledEnemy();
            case PoolType.Powerup:
                return GetPooledPowerup();
            case PoolType.BulletPlayer:
                return GetPooledPlayerBullet();
            case PoolType.BulletEnemy:
                return GetPooledEnemyBullet();
            default:
                return null;
        }
    }

    protected virtual GameObject GetPooledEnemy()
    {
        return GetComponent<ObjectPooler_Enemy>().GetPooledEnemy();
    }

    protected virtual GameObject GetPooledPowerup()
    {
        return GetComponent<ObjectPooler_Powerup>().GetPooledPowerup();
    }

    protected virtual GameObject GetPooledPlayerBullet()
    {
        return GetComponent<ObjectPooler_BulletPlayer>().GetPooledPlayerBullet();
    }

    protected virtual GameObject GetPooledEnemyBullet()
    {
        return GetComponent<ObjectPooler_BulletEnemy>().GetPooledEnemyBullet(); ;
    }

    private void StartSpawnManagers()
    {
        SpawnStarted = true;
        SpawnManager[] spawnManagers = GameObject.FindObjectsByType<SpawnManager>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (SpawnManager spawnManager in spawnManagers)
        {
            spawnManager.StartSpawning();
        }
    }
}
