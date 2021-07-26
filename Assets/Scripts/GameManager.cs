using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, GameDifficultyObserver
{
    public PlayerControllerStrategy playerControllsStrategy;
    public EnemySpawnStrategy enemySpawnStrategy;
    public PowerupSpawnStrategy powerupSpawnStrategy;

    public bool gameOver;
    public GameDifficulty gameDifficulty;
    public int levelNumber = 1;
    public int score = 0;
    public int maxScore = 0;

    private int strategyIndex = 1;
    private string maxScoreTextPrefix = "Max Score: ";
    private List<EnemySpawnStrategy> enemySpawnStrategies = new List<EnemySpawnStrategy>();
    private List<SpawnObserver> spawnObservers = new List<SpawnObserver>();

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnStrategies.Add(new BasicEnemySpawnStrategy());
        enemySpawnStrategies.Add(new HarderEnemySpawnStrategy());
        enemySpawnStrategies.Add(new BossEnemySpawnStrategy());

        levelNumber = 1;
        strategyIndex = 0;
        score = 0;
        maxScore = 0;
        gameDifficulty = FindObjectOfType<GameDifficulty>();
        SetupStrategies();
        GameStart();
        gameDifficulty.Attach(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Sumo Battle Menu"))
        {
            Utilities.ChangeText("Max Score Global", maxScoreTextPrefix + maxScore);
        }
    }

    public void Register(SpawnObserver spawnObserver)
    {
        spawnObservers.Add(spawnObserver);
    }

    public void LevelComplete()
    {
        if (gameDifficulty.gameDifficultyEnum == GameDifficulty.GameDifficultyEnum.AllAround)
        {
            strategyIndex++;
            levelNumber = (strategyIndex / enemySpawnStrategies.Count) + 1;
            enemySpawnStrategy = enemySpawnStrategies.ElementAt(strategyIndex % enemySpawnStrategies.Count);
        }
        else 
        {
            levelNumber++;
            strategyIndex++;
        }

        foreach (SpawnObserver spawnObserver in spawnObservers)
        {
            spawnObserver.Register();
            spawnObserver.Spawn();
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void GameOver()
    {
        gameOver = true;
        levelNumber = 1;
        strategyIndex = 0;
        if (score > maxScore)
            maxScore = score;
        score = 0;
        spawnObservers.Clear();
    }

    private void GameStart()
    {
        gameOver = false;

        foreach (SpawnObserver spawnObserver in spawnObservers)
        {
            spawnObserver.Game();
        }
    }

    private void SetupStrategies()
    {
        switch(gameDifficulty.gameDifficultyEnum)
        {
            case GameDifficulty.GameDifficultyEnum.Basic:
                playerControllsStrategy = new BasicPlayerControllerStrategy();
                enemySpawnStrategy = new BasicEnemySpawnStrategy();
                powerupSpawnStrategy = new BasicPowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.Easy:
                playerControllsStrategy = new BasicPlayerControllerStrategy();
                enemySpawnStrategy = new HarderEnemySpawnStrategy();
                powerupSpawnStrategy = new BasicPowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.Medium:
                playerControllsStrategy = new BasicPlayerControllerStrategy();
                enemySpawnStrategy = new BasicEnemySpawnStrategy();
                powerupSpawnStrategy = new ProjectilePowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.Hard:
                playerControllsStrategy = new BasicPlayerControllerStrategy();
                enemySpawnStrategy = new BasicEnemySpawnStrategy();
                powerupSpawnStrategy = new SmashPowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.Expert:
                playerControllsStrategy = new BasicPlayerControllerStrategy();
                enemySpawnStrategy = new BossEnemySpawnStrategy();
                powerupSpawnStrategy = new AllInOnePowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.AllAround:
                playerControllsStrategy = new BasicPlayerControllerStrategy();
                enemySpawnStrategy = enemySpawnStrategies.ElementAt(strategyIndex % enemySpawnStrategies.Count);
                powerupSpawnStrategy = new AllInOnePowerupSpawnStrategy();
                break;
        }
    }

    public void notify()
    {
        SetupStrategies();
        GameStart();
    }
}
