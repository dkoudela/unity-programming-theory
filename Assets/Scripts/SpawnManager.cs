using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, SpawnObserver
{
    private int enemyCount;
    private string levelTextPrefix = "Level: ";
    private GameManager gameManager;
    public GameObject enemyPrefab;
    public GameObject harderEnemyPrefab;
    public GameObject bossEnemyPrefab;
    public GameObject powerupStrongerPrefab;
    public GameObject powerupProjectilePrefab;
    public GameObject powerupSmashPrefab;
    public GameObject projectile;
    public const float spawnRange = 9;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Register();
        Spawn();
        gameManager.Register(this); // ABSTRACTION
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            gameManager.LevelComplete();
        }
    }

    public void Register()
    {
        gameManager.EnemySpawnStrategy.Register(gameObject); // ABSTRACTION
        gameManager.PowerupSpawnStrategy.Register(gameObject); // ABSTRACTION
    }

    public void Spawn()
    {
        Utilities.ChangeText("Level", levelTextPrefix + gameManager.LevelNumber);

        gameManager.EnemySpawnStrategy.SpawnEnemies(gameManager.LevelNumber); // ABSTRACTION
        SpawnPowerups();
    }

    public void Game()
    {
    }

    private void SpawnPowerups()
    {
        gameManager.PowerupSpawnStrategy.SpawnPowerups(gameManager.LevelNumber); // ABSTRACTION
    }

    private void Attack()
    {
        gameManager.EnemySpawnStrategy.Attack(); // ABSTRACTION
    }
}
