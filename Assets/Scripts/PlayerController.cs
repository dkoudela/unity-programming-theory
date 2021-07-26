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
    public GameObject walls;
    public GameObject FocalPoint { get; private set; }
    public PowerupManager PowerupManager { get; private set; }
    private GameManager gameManager;
    private bool doNotFall = false;
    private float fallBorder = -10;
    private Health health;
    
    // Start is called before the first frame update
    void Start()
    {
        FocalPoint = GameObject.Find("Focal Point");
        gameManager = FindObjectOfType<GameManager>();
        gameManager.PlayerControllsStrategy.Register(gameObject); // ABSTRACTION
        PowerupManager = FindObjectOfType<PowerupManager>();
        PowerupManager.SetPlayer(gameObject);
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

        gameManager.PlayerControllsStrategy.HandleControlls(); // ABSTRACTION

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
            PowerupManager.HandlePowerUpTrigger(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.PlayerControllsStrategy.HandleEnemyCollision(collision); // ABSTRACTION
        }
    }

    void OnDestroy()
    {
        GameOver();
    }

    private void GameOver()
    {
        gameManager.GameFinished(); 
        SceneManager.LoadScene("Sumo Battle Menu", LoadSceneMode.Single);
    }
}
