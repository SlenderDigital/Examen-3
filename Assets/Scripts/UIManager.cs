using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Slider lifeSlider;
    public GameObject pausePanel;
    public GameObject menuPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    private bool isPaused = false;

    public void UpdateScore(int score)
    {
        scoreText.text = "Puntaje: " + score.ToString();
    }

    public void UpdateLives(int currentLives, int maxLives)
    {
        lifeSlider.maxValue = maxLives;
        lifeSlider.value = currentLives;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowGameOver(bool victory)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = victory ? "¡Victoria!" : "Juego Terminado";
        Time.timeScale = 0;
    }
}
