public class BasicPowerupSpawnStrategy : PowerupSpawnStrategyBase
{
    public override void SpawnPowerups(int gameLevel)
    {
        SpawnPowerupWave(gameLevel, spawnManager.powerupStrongerPrefab);
    }
}
