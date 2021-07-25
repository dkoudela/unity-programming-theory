using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllInOnePowerupSpawnStrategy : PowerupSpawnStrategyBase
{
    private List<GameObject> gameObjects = new List<GameObject>();
    private float spawnRepeat = 15.0f;

    public override void Register(GameObject gameObject)
    {
        base.Register(gameObject);

        gameObjects.Add(spawnManager.powerupStrongerPrefab);
        gameObjects.Add(spawnManager.powerupProjectilePrefab);
        gameObjects.Add(spawnManager.powerupSmashPrefab);

        spawnManager.InvokeRepeating("SpawnPowerups", spawnRepeat, spawnRepeat);
    }

    public override void SpawnPowerups(int gameLevel)
    {
        SpawnPowerup();
    }

    private void SpawnPowerup()
    {
        int gameObjectIndex = Random.Range(0, gameObjects.Count);
        GameObject objectToSpawn = gameObjects.ElementAt(gameObjectIndex);
        GameObject.Instantiate(objectToSpawn, Utilities.GenerateSpawnPosition(spawnManager.spawnRange), objectToSpawn.transform.rotation);
    }
}
