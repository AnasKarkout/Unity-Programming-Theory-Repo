using System.Collections;
using UnityEngine;

public class PowerupSpawner : SpawnManager
{
    private float xPosBound = 7.0f;
    private float zPosBound = 2.0f;

    // TODO make it spawn based on enemies killed instead of a timer

    public override void StartSpawning()
    {
        base.StartSpawning();
        InvokeRepeating("SpawnPowerup", spawnCooldown, spawnCooldown);
    }

    private void SpawnPowerup()
    {
        GameObject powerupObject = ObjectPooler.SharedInstance.GetPooledObject(PoolType.Powerup);
        powerupObject.SetActive(true);
        float randomXPos = Random.Range(-xPosBound, xPosBound);
        float randomZPos = Random.Range(-zPosBound, zPosBound);
        Vector3 randomPosition = new Vector3(randomXPos, transform.position.y, randomZPos);
        powerupObject.transform.position = randomPosition;
    }
}
