// INHERITANCE
public class ProjectilePowerupSpawnStrategy : PowerupSpawnStrategyBase
{
    // POLYMORPHISM
    public override void SpawnPowerups(int gameLevel)
    {
        SpawnPowerupWave(gameLevel, spawnManager.powerupProjectilePrefab);
    }
}
