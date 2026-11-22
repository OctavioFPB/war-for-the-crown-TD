using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    private bool gameEnded = false;
    private int score = 0;

    [Header("Menu")]
    public string menuSceneName = "MainMenu";

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

    public void AddScore(int amount)
    {
        if (!gameEnded) score += amount;
    }

    void SubmitScore(bool won)
    {
        if (won)
        {
            int hp = HealthManager.Instance != null ? HealthManager.Instance.health : 0;
            int coins = CoinManager.instance != null ? CoinManager.instance.coins : 0;
            score += 1000 + hp * 20 + coins;
        }
        RankingManager.Submit(SceneManager.GetActiveScene().name, "Player", score);
    }

    public void Victory()
    {
        if (gameEnded) return;
        gameEnded = true;
        SubmitScore(true);
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Defeat()
    {
        if (gameEnded) return;
        gameEnded = true;
        SubmitScore(false);
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