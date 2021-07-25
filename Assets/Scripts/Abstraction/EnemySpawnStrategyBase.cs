using UnityEngine;

public abstract class EnemySpawnStrategyBase : EnemySpawnStrategy
{
    public GameObject gameObject;
    public SpawnManager spawnManager;

    public virtual void Attack()
    {
    }

    public virtual void Register(GameObject gameObject)
    {
        this.gameObject = gameObject;
        spawnManager = gameObject.GetComponent<SpawnManager>();
    }

    public virtual void SpawnEnemies(int gameLevel)
    {
    }

    protected void SpawnEnemyWave(int enemiesToSpawn, GameObject enemyPrefab)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject.Instantiate(enemyPrefab, Utilities.GenerateSpawnPosition(spawnManager.spawnRange), enemyPrefab.transform.rotation);
        }
    }
}
