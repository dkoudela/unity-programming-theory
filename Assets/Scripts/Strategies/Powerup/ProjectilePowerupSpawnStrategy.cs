public class ProjectilePowerupSpawnStrategy : PowerupSpawnStrategyBase
{
    public override void SpawnPowerups(int gameLevel)
    {
        SpawnPowerupWave(gameLevel, spawnManager.powerupProjectilePrefab);
    }
}
