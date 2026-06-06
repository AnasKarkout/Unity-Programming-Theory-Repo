using UnityEngine;

public class ObjectPooler_Powerup : ObjectPooler
{

    protected override void Awake()
    {
        base.Awake();
        PoolObjectsFromArray();
    }

    protected override void AddObjectToPool(GameObject obj)
    {
        pooledPowerups.Add(obj);
    }

    protected override GameObject GetPooledPowerup()
    {
        // For as many objects as are in the pooledObjects list
        // retrieve one of the pooled enemies of the random type
        // ex: random index = 1 means enemy prefab at enemyObjects[1]
        // cycle through the pooled objects of the same type which are pooledEnemies[1 + enemyObjects.Length * EnemiesToPool]
        int randomTypeIndex = Random.Range(0, arrayOfPrefabsToPool.Length);
        for (int i = randomTypeIndex; i < pooledPowerups.Count; i += arrayOfPrefabsToPool.Length)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledPowerups[i].activeInHierarchy)
            {
                return pooledPowerups[i];
            }
        }
        return null;
    }
}
