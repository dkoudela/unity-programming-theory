using UnityEngine;

// INHERITANCE
public class BossEnemySpawnStrategy : EnemySpawnStrategyBase
{
    private float spawnRepeat = 15.0f;

    // POLYMORPHISM
    public override void Attack()
    {
        GameObject player = GameObject.Find("Player");
        GameObject[] bossEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject bossEnemy = bossEnemies[Random.Range(0, bossEnemies.Length)];
        HomingRocketController homingRocketController = HomingRocketController.GetHomingRocketController(
            bossEnemy.transform.position,
            player.transform.position,
            spawnManager.projectile);
        homingRocketController.Enemy = player;
        homingRocketController.Activated = true;
    }

    // POLYMORPHISM
    public override void Register(GameObject gameObject)
    {
        base.Register(gameObject);

        spawnManager.InvokeRepeating("Attack", spawnRepeat, spawnRepeat);
    }

    // POLYMORPHISM
    public override void SpawnEnemies(int gameLevel)
    {
        SpawnEnemyWave(gameLevel, spawnManager.bossEnemyPrefab);
    }
}
