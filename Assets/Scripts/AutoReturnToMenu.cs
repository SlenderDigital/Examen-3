using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AutoReturnToMenu : MonoBehaviour
{
    [Header("Settings")]
    public float delaySeconds = 2f;
    public string menuSceneName = "MENU";
    
    [Header("UI")]
    public TextMeshProUGUI disappointmentText;

    void Start()
    {
        if (disappointmentText != null)
        {
            int score = PlayerPrefs.GetInt("JustScored", 0);
            disappointmentText.text = score + "...";
        }

        StartCoroutine(WaitAndReturn());
    }

    IEnumerator WaitAndReturn()
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(menuSceneName);
    }
}
