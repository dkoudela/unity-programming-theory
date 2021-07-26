using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, GameDifficultyObserver
{
    // ENCAPSULATION
    public PlayerControllerStrategy PlayerControllsStrategy { get; private set; }
    public EnemySpawnStrategy EnemySpawnStrategy { get; private set; }
    public PowerupSpawnStrategy PowerupSpawnStrategy { get; private set; }

    public bool GameOver { get; private set; }
    public int LevelNumber { get; private set; } = 1;
    public int Score { get; private set; } = 0;
    public int MaxScore { get; private set; } = 0;

    private int strategyIndex = 1;
    private string maxScoreTextPrefix = "Max Score: ";
    private List<EnemySpawnStrategy> enemySpawnStrategies = new List<EnemySpawnStrategy>();
    private List<SpawnObserver> spawnObservers = new List<SpawnObserver>();
    private GameDifficulty gameDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnStrategies.Add(new BasicEnemySpawnStrategy());
        enemySpawnStrategies.Add(new HarderEnemySpawnStrategy());
        enemySpawnStrategies.Add(new BossEnemySpawnStrategy());

        LevelNumber = 1;
        strategyIndex = 0;
        Score = 0;
        MaxScore = 0;
        gameDifficulty = FindObjectOfType<GameDifficulty>();
        SetupStrategies();
        GameStart();
        gameDifficulty.Attach(this);
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
            Utilities.ChangeText("Max Score Global", maxScoreTextPrefix + MaxScore);
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
            LevelNumber = (strategyIndex / enemySpawnStrategies.Count) + 1;
            EnemySpawnStrategy = enemySpawnStrategies.ElementAt(strategyIndex % enemySpawnStrategies.Count);
        }
        else 
        {
            LevelNumber++;
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
        this.Score += score;
    }

    public void GameFinished()
    {
        GameOver = true;
        LevelNumber = 1;
        strategyIndex = 0;
        if (Score > MaxScore)
            MaxScore = Score;
        Score = 0;
        spawnObservers.Clear();
    }

    private void GameStart()
    {
        GameOver = false;

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
