using UnityEngine;

public class BasicPowerupSpawnStrategy : PowerupSpawnStrategy
{
    public GameObject gameObject;
    public SpawnManager spawnManager;

    public void Register(GameObject gameObject)
    {
        this.gameObject = gameObject;
        spawnManager = gameObject.GetComponent<SpawnManager>();
    }

    public void SpawnPowerups(int gameLevel)
    {
        GameObject.Instantiate(spawnManager.powerupStrongerPrefab, Utilities.GenerateSpawnPosition(spawnManager.spawnRange), spawnManager.powerupStrongerPrefab.transform.rotation);
    }
}
