using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePowerupSpawnStrategy : PowerupSpawnStrategy
{
    public GameObject gameObject;
    public SpawnManager spawnManager;

    public void Register(GameObject gameObject)
    {
        this.gameObject = gameObject;
        spawnManager = gameObject.GetComponent<SpawnManager>();
    }

    public void SpawnPowerups(int gameLevel)
    {
        GameObject.Instantiate(spawnManager.powerupProjectilePrefab, Utilities.GenerateSpawnPosition(spawnManager.spawnRange), spawnManager.powerupProjectilePrefab.transform.rotation);
    }
}
