using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float fallBorder = -10;
    private Rigidbody enemyRb;
    private GameObject player;
    private GameManager gameManager;
    private string scoreTextPrefix = "Score: ";
    private string maxScoreTextPrefix = "Max Score: ";
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        Utilities.ChangeText("Max Score", maxScoreTextPrefix + gameManager.maxScore);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < fallBorder)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!gameManager.gameOver)
        {
            AddScore();
        }
    }

    private void AddScore()
    {
        int achievedScore = 10;
        if (gameObject.name.Contains("Harder Enemy"))
        {
            achievedScore = 100;
        }
        else if (gameObject.name.Contains("Boss Enemy"))
        {
            achievedScore = 1000;
        }

        gameManager.AddScore(achievedScore);
        Utilities.ChangeText("Score", scoreTextPrefix + gameManager.score);
    }
}
