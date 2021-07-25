using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllInOnePowerupSpawnStrategy : PowerupSpawnStrategy
{
    public GameObject gameObject;
    public SpawnManager spawnManager;

    private List<GameObject> gameObjects = new List<GameObject>();
    private float spawnRepeat = 15.0f;

    public void Register(GameObject gameObject)
    {
        this.gameObject = gameObject;
        spawnManager = gameObject.GetComponent<SpawnManager>();
        gameObjects.Add(spawnManager.powerupStrongerPrefab);
        gameObjects.Add(spawnManager.powerupProjectilePrefab);
        gameObjects.Add(spawnManager.powerupSmashPrefab);

        spawnManager.InvokeRepeating("SpawnPowerups", spawnRepeat, spawnRepeat);
    }

    public void SpawnPowerups(int gameLevel)
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
