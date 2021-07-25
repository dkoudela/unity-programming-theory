using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameDifficultyObserver
{
    void notify();
}

public class GameDifficulty : MonoBehaviour
{
    public enum GameDifficultyEnum { Basic, Easy, Medium, Hard, Expert, AllAround };

    public GameDifficultyEnum CurrentGameDifficulty { get; private set; }
    private List<GameDifficultyObserver> observers = new List<GameDifficultyObserver>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameDifficulty(GameDifficultyEnum gameDifficulty)
    {
        CurrentGameDifficulty = gameDifficulty;
        foreach (GameDifficultyObserver observer in observers)
        {
            observer.notify();
        }
    }

    public void Attach (GameDifficultyObserver gameDifficultyObserver)
    {
        observers.Add(gameDifficultyObserver);
    }
}
