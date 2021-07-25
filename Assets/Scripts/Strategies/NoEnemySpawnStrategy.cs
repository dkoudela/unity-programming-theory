using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEnemySpawnStrategy : EnemySpawnStrategy
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
    }
}
