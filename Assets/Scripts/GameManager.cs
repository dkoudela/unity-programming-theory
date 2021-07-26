using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, GameDifficultyObserver
{
    public PlayerControllerStrategy PlayerControllsStrategy { get; private set; }
    public EnemySpawnStrategy EnemySpawnStrategy { get; private set; }
    public PowerupSpawnStrategy PowerupSpawnStrategy { get; private set; }

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
        if (gameDifficulty.CurrentGameDifficulty == GameDifficulty.GameDifficultyEnum.AllAround)
        {
            strategyIndex++;
            levelNumber = (strategyIndex / enemySpawnStrategies.Count) + 1;
            EnemySpawnStrategy = enemySpawnStrategies.ElementAt(strategyIndex % enemySpawnStrategies.Count);
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
        switch(gameDifficulty.CurrentGameDifficulty)
        {
            case GameDifficulty.GameDifficultyEnum.Basic:
                PlayerControllsStrategy = new BasicPlayerControllerStrategy();
                EnemySpawnStrategy = new BasicEnemySpawnStrategy();
                PowerupSpawnStrategy = new BasicPowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.Easy:
                PlayerControllsStrategy = new BasicPlayerControllerStrategy();
                EnemySpawnStrategy = new HarderEnemySpawnStrategy();
                PowerupSpawnStrategy = new BasicPowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.Medium:
                PlayerControllsStrategy = new BasicPlayerControllerStrategy();
                EnemySpawnStrategy = new BasicEnemySpawnStrategy();
                PowerupSpawnStrategy = new ProjectilePowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.Hard:
                PlayerControllsStrategy = new BasicPlayerControllerStrategy();
                EnemySpawnStrategy = new BasicEnemySpawnStrategy();
                PowerupSpawnStrategy = new SmashPowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.Expert:
                PlayerControllsStrategy = new BasicPlayerControllerStrategy();
                EnemySpawnStrategy = new BossEnemySpawnStrategy();
                PowerupSpawnStrategy = new AllInOnePowerupSpawnStrategy();
                break;
            case GameDifficulty.GameDifficultyEnum.AllAround:
                PlayerControllsStrategy = new BasicPlayerControllerStrategy();
                EnemySpawnStrategy = enemySpawnStrategies.ElementAt(strategyIndex % enemySpawnStrategies.Count);
                PowerupSpawnStrategy = new AllInOnePowerupSpawnStrategy();
                break;
        }
    }

    public void notify()
    {
        SetupStrategies();
        GameStart();
    }
}
