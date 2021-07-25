using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashPowerupSpawnStrategy : PowerupSpawnStrategy
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
        GameObject.Instantiate(spawnManager.powerupSmashPrefab, Utilities.GenerateSpawnPosition(spawnManager.spawnRange), spawnManager.powerupSmashPrefab.transform.rotation);
    }
}
