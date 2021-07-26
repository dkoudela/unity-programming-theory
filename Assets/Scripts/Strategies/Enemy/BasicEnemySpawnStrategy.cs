// INHERITANCE
public class BasicEnemySpawnStrategy : EnemySpawnStrategyBase
{
    // POLYMORPHISM
    public override void SpawnEnemies(int gameLevel)
    {
        SpawnEnemyWave(gameLevel, spawnManager.enemyPrefab);
    }
}
