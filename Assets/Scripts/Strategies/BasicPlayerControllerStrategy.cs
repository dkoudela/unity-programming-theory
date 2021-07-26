using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class BasicPlayerControllerStrategy : PlayerControllerStrategy
{
    public GameObject gameObject;
    public PlayerController playerController;
    public Rigidbody playerRb;
    private bool isJump = false;
    private float smashCoroutineDelay = 1.0f;
    private float smashForce = 1000.0f;

    public void Register(GameObject gameObject)
    {
        this.gameObject = gameObject;
        playerController = gameObject.GetComponent<PlayerController>();
        playerRb = gameObject.GetComponent<Rigidbody>();
    }

    public void HandleControlls()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(playerController.FocalPoint.transform.forward * forwardInput * PlayerController.speed);
        playerController.powerupIndicator.transform.position = playerController.transform.position + new Vector3(0, -0.5f, 0);

        if (playerController.PowerupManager.hasPowerup(PowerupManager.Powerup.smashPU))
        {
            SmashEnemiesOnlyOnFallDown();

            if (Input.GetKeyDown(KeyCode.Space)
                && gameObject.transform.position.y < PlayerController.jumpUpLimit)
            {
                playerRb.AddForce(Vector3.up * PlayerController.jumpForce);
                playerController.StartCoroutine(SmashEnableCountdownRoutine());
            }
        }
        else
        {
            isJump = false;
        }
    }

    public void HandleEnemyCollision(Collision collision)
    {
        if (playerController.PowerupManager.hasPowerup(PowerupManager.Powerup.strongPU))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - playerController.transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * PlayerController.powerupStrength, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Smashes only on fall-down and not on jump-up
    /// </summary>
    private void SmashEnemiesOnlyOnFallDown()
    {
        if (isJump
            && Utilities.IsBetweenRange(gameObject.transform.position.y, PlayerController.jumpDownLimit, PlayerController.jumpUpLimit)
            && Utilities.IsBetweenRange(gameObject.transform.position.x, -PlayerController.islandBorder, PlayerController.islandBorder)
            && Utilities.IsBetweenRange(gameObject.transform.position.z, -PlayerController.islandBorder, PlayerController.islandBorder))
        {
            isJump = false;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Vector3 direction = enemy.transform.position - gameObject.transform.position;
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                enemyRb.AddForce(direction * smashForce);
            }
        }
    }

    /// <summary>
    /// Enables Smash enemies only after the defined time when player has jumped up 
    /// </summary>
    /// <returns></returns>
    IEnumerator SmashEnableCountdownRoutine()
    {
        yield return new WaitForSeconds(smashCoroutineDelay);
        isJump = true;
    }
}
