using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarderEnemySpawnStrategy : EnemySpawnStrategyBase
{
    public override void SpawnEnemies(int gameLevel)
    {
        SpawnEnemyWave(gameLevel, spawnManager.harderEnemyPrefab);
    }
}
