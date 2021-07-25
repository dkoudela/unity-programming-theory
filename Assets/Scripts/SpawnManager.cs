using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, SpawnObserver
{
    public int enemyCount;
    public GameManager gameManager;
    public GameObject enemyPrefab;
    public GameObject harderEnemyPrefab;
    public GameObject bossEnemyPrefab;
    public GameObject powerupStrongerPrefab;
    public GameObject powerupProjectilePrefab;
    public GameObject powerupSmashPrefab;
    public GameObject projectile;
    public float spawnRange = 9;

    private string levelTextPrefix = "Level: ";

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Register();
        Spawn();
        gameManager.Register(this);
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
        gameManager.enemySpawnStrategy.Register(gameObject);
        gameManager.powerupSpawnStrategy.Register(gameObject);
    }

    public void Spawn()
    {
        Utilities.ChangeText("Level", levelTextPrefix + gameManager.levelNumber);

        gameManager.enemySpawnStrategy.SpawnEnemies(gameManager.levelNumber);
        SpawnPowerups();
    }

    public void Game()
    {
    }

    private void SpawnPowerups()
    {
        gameManager.powerupSpawnStrategy.SpawnPowerups(gameManager.levelNumber);
    }

    private void Attack()
    {
        gameManager.enemySpawnStrategy.Attack();
    }
}
