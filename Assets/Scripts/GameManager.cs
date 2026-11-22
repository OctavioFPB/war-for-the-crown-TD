using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    [Header("Menu")]
    public string menuSceneName = "MainMenu";

    private bool gameEnded = false;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        Enemy.AliveCount = 0;
    }

    void Start()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    public void Victory()
    {
        if (gameEnded) return;
        gameEnded = true;
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Defeat()
    {
        if (gameEnded) return;
        gameEnded = true;
        defeatPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}