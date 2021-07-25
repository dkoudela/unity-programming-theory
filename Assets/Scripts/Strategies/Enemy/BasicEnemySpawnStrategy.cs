using UnityEngine;

public class BasicEnemySpawnStrategy : EnemySpawnStrategyBase
{
    public override void SpawnEnemies(int gameLevel)
    {
        SpawnEnemyWave(gameLevel, spawnManager.enemyPrefab);
    }
}
