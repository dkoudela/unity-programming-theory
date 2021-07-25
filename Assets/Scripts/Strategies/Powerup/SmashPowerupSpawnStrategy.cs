﻿public class SmashPowerupSpawnStrategy : PowerupSpawnStrategyBase
{
    public override void SpawnPowerups(int gameLevel)
    {
        SpawnPowerupWave(gameLevel, spawnManager.powerupSmashPrefab);
    }
}
