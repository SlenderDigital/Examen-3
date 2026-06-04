using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public string gameSceneName = "GAME in Play";

    [Header("Score Display")]
    public TextMeshProUGUI lastScoreText;   // drag the "Puntaje" TMP text here
    public TextMeshProUGUI historyText;     // drag the history TMP text here

    void Start()
    {
        // Trim any existing history down to 5 entries
        string raw = PlayerPrefs.GetString("ScoreHistory", "");
        if (raw.Length > 0)
        {
            string[] all = raw.Split(',');
            if (all.Length > 5)
            {
                raw = string.Join(",", all, 0, 5);
                PlayerPrefs.SetString("ScoreHistory", raw);
                PlayerPrefs.Save();
            }
        }

        // Show best score
        if (lastScoreText != null)
        {
            if (raw.Length > 0)
                lastScoreText.text = "Mejor Puntaje: " + raw.Split(',')[0];
            else
                lastScoreText.text = "Mejor Puntaje: -";
        }

        // Show history list (no header, just numbered scores)
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

    public void ClearHistory()
    {
        PlayerPrefs.DeleteKey("ScoreHistory");
        PlayerPrefs.Save();
        if (lastScoreText != null) lastScoreText.text = "Mejor Puntaje: -";
        if (historyText != null) historyText.text = "-";
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
