using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawnStrategy : EnemySpawnStrategy
{
    public GameObject spawnManagerGameObject;
    public SpawnManager spawnManager;

    private float spawnRepeat = 15.0f;

    public void Attack()
    {
        GameObject player = GameObject.Find("Player");
        GameObject[] bossEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject bossEnemy = bossEnemies[Random.Range(0, bossEnemies.Length)];
        HomingRocketController homingRocketController = HomingRocketController.GetHomingRocketController(
            bossEnemy.transform.position,
            player.transform.position,
            spawnManager.projectile);
        homingRocketController.enemy = player;
        homingRocketController.activated = true;
    }

    public void Register(GameObject gameObject)
    {
        this.spawnManagerGameObject = gameObject;
        spawnManager = spawnManagerGameObject.GetComponent<SpawnManager>();

        spawnManager.InvokeRepeating("Attack", spawnRepeat, spawnRepeat);
    }

    public void SpawnEnemies(int gameLevel)
    {
        SpawnEnemyWave(gameLevel);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject.Instantiate(spawnManager.bossEnemyPrefab, Utilities.GenerateSpawnPosition(spawnManager.spawnRange),
                spawnManager.bossEnemyPrefab.transform.rotation);
        }
    }
}
