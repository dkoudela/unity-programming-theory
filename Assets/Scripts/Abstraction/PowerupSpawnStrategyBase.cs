using UnityEngine;

public class PowerupSpawnStrategyBase : PowerupSpawnStrategy
{
    public GameObject gameObject;
    public SpawnManager spawnManager;

    public virtual void Register(GameObject gameObject)
    {
        this.gameObject = gameObject;
        spawnManager = gameObject.GetComponent<SpawnManager>();
    }

    public virtual void SpawnPowerups(int gameLevel)
    {
    }

    protected void SpawnPowerupWave(int gameLevel, GameObject powerupPrefab)
    {
        GameObject.Instantiate(powerupPrefab, Utilities.GenerateSpawnPosition(SpawnManager.spawnRange), powerupPrefab.transform.rotation);
    }
}
