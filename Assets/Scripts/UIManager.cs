using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Slider healthSlider;
    public TextMeshProUGUI pauseButtonText;

    public void UpdatePauseText(bool isPaused)
    {
        if (pauseButtonText != null)
        {
            pauseButtonText.text = isPaused ? "Continuar" : "Pausar";
        }
    }

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
}
