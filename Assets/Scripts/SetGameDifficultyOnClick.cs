using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetGameDifficultyOnClick : MonoBehaviour
{
    private GameDifficulty gameDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        gameDifficulty = FindObjectOfType<GameDifficulty>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    { 
        if (gameObject.CompareTag("Basic"))
        {
            gameDifficulty.SetGameDifficulty(GameDifficulty.GameDifficultyEnum.Basic);
        } 
        else if (gameObject.CompareTag("Easy"))
        {
            gameDifficulty.SetGameDifficulty(GameDifficulty.GameDifficultyEnum.Easy);
        }
        else if (gameObject.CompareTag("Medium"))
        {
            gameDifficulty.SetGameDifficulty(GameDifficulty.GameDifficultyEnum.Medium);
        }
        else if (gameObject.CompareTag("Hard"))
        {
            gameDifficulty.SetGameDifficulty(GameDifficulty.GameDifficultyEnum.Hard);
        }
        else if (gameObject.CompareTag("Expert"))
        {
            gameDifficulty.SetGameDifficulty(GameDifficulty.GameDifficultyEnum.Expert);
        }
        else if (gameObject.CompareTag("AllAround"))
        {
            gameDifficulty.SetGameDifficulty(GameDifficulty.GameDifficultyEnum.AllAround);
        }

        SceneManager.LoadScene("Sumo Battle Game", LoadSceneMode.Single);
    }
}
