using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    private int health = 5;
    public int targetScore = 5;
    public bool isGameOver = false;
    [Header("Scenes")]
    public string menuSceneName = "MENU";
    public string defeatSceneName = "DEFEAT";

    public float winDelay = 3f;

    private UIManager uiManager;

    void Awake()
    {
        Physics2D.autoSyncTransforms = true;

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

        string raw = PlayerPrefs.GetString("ScoreHistory", "");
        if (raw.Length > 0)
        {
            string[] entries = raw.Split(',');
            if (int.TryParse(entries[0], out int bestScore))
            {
                int[] tiers = { 25, 35, 45, 50 };
                targetScore = 25; 

                if (bestScore >= 50)
                {
                    targetScore = (bestScore / 5 + 1) * 5;
                }
                else
                {
                    foreach (int tier in tiers)
                    {
                        if (bestScore < tier)
                        {
                            targetScore = tier;
                            break;
                        }
                    }
                }
            }
        }

        if (uiManager != null)
            uiManager.UpdateUI(score, health);
    }

    public void AddScore(int value)
    {
        if (isGameOver) return;

        score += value;
        if (uiManager != null) uiManager.UpdateUI(score, health);

        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayCoinSound();
        }

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

        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayDamageSound();
        }

        if (health <= 0)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        isGameOver = true;
        StartCoroutine(LoadEndSceneAfterDelay(menuSceneName, winDelay));
    }

    IEnumerator LoadEndSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadEndScene(sceneName);
    }

    void LoseGame()
    {
        isGameOver = true;
        LoadEndScene(defeatSceneName);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SaveScoreToHistory(score);
        SceneManager.LoadScene(menuSceneName);
    }

    public void LoadEndScene(string sceneName)
    {
        Time.timeScale = 1f;
        SaveScoreToHistory(score);
        SceneManager.LoadScene(sceneName);
    }

    public static void SaveScoreToHistory(int newScore)
    {
        PlayerPrefs.SetInt("JustScored", newScore);
        string raw = PlayerPrefs.GetString("ScoreHistory", "");
        System.Collections.Generic.List<string> entries =
            new System.Collections.Generic.List<string>(
                raw.Length > 0 ? raw.Split(',') : new string[0]);

        entries.Add(newScore.ToString());
        entries.Sort((a, b) => int.Parse(b).CompareTo(int.Parse(a)));
        if (entries.Count > 3) entries.RemoveRange(3, entries.Count - 3);

        PlayerPrefs.SetString("ScoreHistory", string.Join(",", entries));
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (!isGameOver && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            if (uiManager != null) uiManager.UpdatePauseText(false);
        }
        else
        {
            Time.timeScale = 0f;
            if (uiManager != null) uiManager.UpdatePauseText(true);
        }
    }
}
