
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour, SpawnObserver
{
    public enum Powerup { strongPU, projectilePU, smashPU };

    private int coroutineDelay = 7;
    private float projectileRepeatRate = 1.0f;
    private GameObject player;
    private PlayerController playerController;
    private GameManager gameManager;
    private List<Powerup> activePowerups = new List<Powerup>();

    void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.Register(this);
    }

    void Update() { }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
        playerController = player.GetComponent<PlayerController>();
        InvokeRepeating("LaunchProjectiles", projectileRepeatRate, projectileRepeatRate);
    }

    public bool hasPowerup(Powerup pu)
    {
        return activePowerups.Contains(pu);
    }

    public void HandlePowerUpTrigger(Collider other)
    {
        playerController.powerupIndicator.gameObject.SetActive(true);
        GameObject powerupInCollision = other.gameObject;

        Powerup thePowerup = Powerup.strongPU;
        if (powerupInCollision.name.Contains("Powerup Projectile"))
        {
            thePowerup = Powerup.projectilePU;
        }
        else if (powerupInCollision.name.Contains("Powerup Smash"))
        {
            thePowerup = Powerup.smashPU;
        }
        activePowerups.Add(thePowerup);
        StartCoroutine(PowerupCountdownRoutine(thePowerup));
    }

    IEnumerator PowerupCountdownRoutine(Powerup powerup)
    {
        yield return new WaitForSeconds(coroutineDelay);
        activePowerups.Remove(powerup);
        if (0 == activePowerups.Count)
        {
            playerController.powerupIndicator.gameObject.SetActive(false);
        }
    }

    private void LaunchProjectiles()
    {
        if (hasPowerup(Powerup.projectilePU))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                HomingRocketController homingRocketController = HomingRocketController.GetHomingRocketController(
                    playerController.transform.position,
                    enemy.transform.position,
                    playerController.projectile);
                homingRocketController.enemy = enemy;
                homingRocketController.activated = true;
            }
        }
    }

    public void Register()
    {
    }

    public void Spawn()
    {
    }

    public void Game()
    {
        foreach (Powerup thePowerup in activePowerups)
        {
            StopCoroutine(PowerupCountdownRoutine(thePowerup));
        }
        activePowerups.Clear();
        playerController.powerupIndicator.gameObject.SetActive(false);
    }
}
