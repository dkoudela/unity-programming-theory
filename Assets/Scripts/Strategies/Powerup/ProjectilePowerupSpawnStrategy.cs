using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePowerupSpawnStrategy : PowerupSpawnStrategyBase
{
    public override void SpawnPowerups(int gameLevel)
    {
        SpawnPowerupWave(gameLevel, spawnManager.powerupProjectilePrefab);
    }
}
