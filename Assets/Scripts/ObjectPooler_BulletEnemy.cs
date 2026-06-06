using UnityEngine;

public class ObjectPooler_BulletEnemy : ObjectPooler
{

    protected override void Awake()
    {
        base.Awake();
        PoolObjectsFromSinglePrefab();
    }

    protected override void AddObjectToPool(GameObject obj)
    {
        pooledEnemyBullets.Add(obj);
    }

    protected override GameObject GetPooledEnemyBullet()
    {
        for (int i = 0; i < pooledEnemyBullets.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledEnemyBullets[i].activeInHierarchy)
            {
                return pooledEnemyBullets[i];
            }
        }
        return null;
    }
}
