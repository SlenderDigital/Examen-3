using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public string gameSceneName = "GAME in Play";

    [Header("Score Display")]
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI historyText;

    void Start()
    {
        string raw = PlayerPrefs.GetString("ScoreHistory", "");
        if (raw.Length > 0)
        {
            string[] all = raw.Split(',');
            if (all.Length > 3)
            {
                raw = string.Join(",", all, 0, 3);
                PlayerPrefs.SetString("ScoreHistory", raw);
                PlayerPrefs.Save();
            }
        }

        if (lastScoreText != null)
        {
            if (raw.Length > 0)
                lastScoreText.text = "Mejor Puntaje: " + raw.Split(',')[0];
            else
                lastScoreText.text = "Mejor Puntaje: -";
        }

        if (historyText != null)
        {
            if (raw.Length > 0)
            {
                string[] entries = raw.Split(',');
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < entries.Length; i++)
                    sb.AppendLine((i + 1) + ". " + entries[i] + " pts");
                historyText.text = sb.ToString().TrimEnd();
            }
            else
            {
                historyText.text = "-";
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
