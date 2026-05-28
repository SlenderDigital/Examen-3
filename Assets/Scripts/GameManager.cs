using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int health = 5;
    public int targetScore = 5;
    public bool isGameOver = false;

    private UIManager uiManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateUI(score, health);
        }
    }

    public void AddScore(int value)
    {
        if (isGameOver) return;

        score += value;
        if (uiManager != null) uiManager.UpdateUI(score, health);

        if (score >= targetScore)
        {
            WinGame();
        }
    }

    public void TakeDamage(int value)
    {
        if (isGameOver) return;

        health -= value;
        if (uiManager != null) uiManager.UpdateUI(score, health);

        if (health <= 0)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        uiManager.ShowGameOver(true);
    }

    void LoseGame()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        uiManager.ShowGameOver(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MENU");
    }

    public void TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }
}
