// INHERITANCE
public class HarderEnemySpawnStrategy : EnemySpawnStrategyBase
{
    // POLYMORPHISM
    public override void SpawnEnemies(int gameLevel)
    {
        SpawnEnemyWave(gameLevel, spawnManager.harderEnemyPrefab);
    }
}
