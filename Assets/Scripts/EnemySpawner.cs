using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : SpawnManager
{
    //private GameObject enemy;
    private float minSpawnCooldown = 1.5f;
    private float maxSpawnCooldown = 5.0f;
    private BoxCollider spawnerCollider;
    private Vector3 spawnerColliderSize;

    public override void StartSpawning()
    {
        base.StartSpawning();
        BoxCollider spawnerCollider = GetComponent<BoxCollider>();
        spawnerColliderSize = spawnerCollider.bounds.size;
        maxSpawnCooldown = spawnCooldown;
        InvokeSpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Enemy enemy = CreateEnemyObject();
        enemy.enemySpawner = this;
        enemy.StartEnemyActivity();
    }

    public void InvokeSpawnEnemy()
    {
        spawnCooldown = Random.Range(minSpawnCooldown, maxSpawnCooldown);
        Invoke("SpawnEnemy", spawnCooldown);
    }

    private Enemy CreateEnemyObject()
    {
        GameObject enemyObject = ObjectPooler.SharedInstance.GetPooledObject(PoolType.Enemy);
        enemyObject.SetActive(true);
        BoxCollider enemyCollider = enemyObject.GetComponent<BoxCollider>();
        Vector3 enemyColliderSize = enemyCollider.bounds.size;
        Vector3 spawnPoint = transform.position;
        spawnPoint.y = spawnerColliderSize.y + enemyColliderSize.y / 2 + 0.01f;
        Debug.Log("spawnPoint = " + spawnPoint.ToString());
        enemyObject.transform.position = spawnPoint;
        enemyObject.GetComponent<Rigidbody>().position = spawnPoint;
        Debug.Log("Enemy spawned at " + enemyObject.transform.position);
        enemyObject.transform.rotation = transform.rotation;
        return enemyObject.GetComponent<Enemy>();
    }
}
