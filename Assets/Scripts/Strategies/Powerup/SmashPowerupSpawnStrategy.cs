// INHERITANCE
public class SmashPowerupSpawnStrategy : PowerupSpawnStrategyBase
{
    // POLYMORPHISM
    public override void SpawnPowerups(int gameLevel)
    {
        SpawnPowerupWave(gameLevel, spawnManager.powerupSmashPrefab);
    }
}
