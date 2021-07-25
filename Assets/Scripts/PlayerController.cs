using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public const float speed = 5.0f;
    public const float jumpForce = 500.0f;
    public const float powerupStrength = 15.0f;
    public const float jumpUpLimit = 0.5f;
    public const float jumpDownLimit = 0.0f;
    public const float islandBorder = 13.0f;
    public GameObject powerupIndicator;
    public GameObject projectile;
    public GameObject focalPoint;
    public GameObject walls;
    public GameManager gameManager;
    public PowerupManager powerupManager;
    private bool doNotFall = false;
    private float fallBorder = -10;
    private Health health;
    
    // Start is called before the first frame update
    void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
        gameManager = FindObjectOfType<GameManager>();
        gameManager.playerControllsStrategy.Register(gameObject);
        powerupManager = FindObjectOfType<PowerupManager>();
        powerupManager.SetPlayer(gameObject);
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] wallsInScene = GameObject.FindGameObjectsWithTag("Walls");
        if (0 == wallsInScene.Length)
        {
            if (doNotFall)
            {
                Debug.Log("Enable walls");
                Instantiate(walls, walls.transform.position, walls.transform.rotation);
            }
        }
        else
        {
            if (!doNotFall)
            {
                Debug.Log("Disable walls");
                foreach (GameObject w in wallsInScene)
                {
                    Destroy(w);
                }
            }
        }

        gameManager.playerControllsStrategy.HandleControlls();

        if (transform.position.y < fallBorder)
        {
            GameOver();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            doNotFall = (doNotFall) ? false : true;
            health.IncreaseHealth(1000);
            Debug.Log("doNotFall: " + doNotFall);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            powerupManager.HandlePowerUpTrigger(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.playerControllsStrategy.HandleEnemyCollision(collision);
        }
    }

    void OnDestroy()
    {
        GameOver();
    }

    private void GameOver()
    {
        gameManager.GameOver();
        SceneManager.LoadScene("Sumo Battle Menu", LoadSceneMode.Single);
    }
}
