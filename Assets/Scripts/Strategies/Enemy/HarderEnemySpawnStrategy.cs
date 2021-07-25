public class HarderEnemySpawnStrategy : EnemySpawnStrategyBase
{
    public override void SpawnEnemies(int gameLevel)
    {
        SpawnEnemyWave(gameLevel, spawnManager.harderEnemyPrefab);
    }
}
