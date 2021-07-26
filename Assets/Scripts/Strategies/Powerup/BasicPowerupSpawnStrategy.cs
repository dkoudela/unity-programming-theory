// INHERITANCE
public class BasicPowerupSpawnStrategy : PowerupSpawnStrategyBase
{
    // POLYMORPHISM
    public override void SpawnPowerups(int gameLevel)
    {
        SpawnPowerupWave(gameLevel, spawnManager.powerupStrongerPrefab);
    }
}
