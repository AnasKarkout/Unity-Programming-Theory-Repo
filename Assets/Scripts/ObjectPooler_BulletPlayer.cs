using UnityEngine;

public class ObjectPooler_BulletPlayer : ObjectPooler
{

    protected override void Awake()
    {
        base.Awake();
        PoolObjectsFromSinglePrefab();
    }

    protected override void AddObjectToPool(GameObject obj)
    {
        pooledPlayerBullets.Add(obj);
    }

    protected override GameObject GetPooledPlayerBullet()
    {
        for (int i = 0; i < pooledPlayerBullets.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledPlayerBullets[i].activeInHierarchy)
            {
                return pooledPlayerBullets[i];
            }
        }
        return null;
    }

}
