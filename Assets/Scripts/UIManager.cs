using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Slider healthSlider;
    public GameObject gameOverPanel;
    public TextMeshProUGUI resultText;

    public void UpdateUI(int score, int health)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    public void ShowGameOver(bool won)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            resultText.text = won ? "You Win!" : "Game Over!";
        }
    }
}
