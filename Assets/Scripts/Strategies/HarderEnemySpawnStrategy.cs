using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarderEnemySpawnStrategy : EnemySpawnStrategy
{
    public GameObject gameObject;
    public SpawnManager spawnManager;

    public void Attack()
    {
    }

    public void Register(GameObject gameObject)
    {
        this.gameObject = gameObject;
        spawnManager = gameObject.GetComponent<SpawnManager>();
    }

    public void SpawnEnemies(int gameLevel)
    {
        SpawnEnemyWave(gameLevel);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject.Instantiate(spawnManager.harderEnemyPrefab, Utilities.GenerateSpawnPosition(spawnManager.spawnRange), 
                spawnManager.harderEnemyPrefab.transform.rotation);
        }
    }
}
