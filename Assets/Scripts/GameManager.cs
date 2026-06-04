using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int health = 5;
    public int targetScore = 5;
    public bool isGameOver = false;
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

        // Set target score to beat the current best
        string raw = PlayerPrefs.GetString("ScoreHistory", "");
        if (raw.Length > 0)
        {
            string[] entries = raw.Split(',');
            if (int.TryParse(entries[0], out int bestScore))
            {
                // Fixed progression: 25 → 35 → 45 → 50 → 55 → 60 → ...
                int[] tiers = { 25, 35, 45, 50 };
                targetScore = 25; // fallback

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
        // If no history exists, targetScore keeps its Inspector default

        if (uiManager != null)
            uiManager.UpdateUI(score, health);
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
        StartCoroutine(ReturnToMenuAfterDelay(winDelay));
    }

    IEnumerator ReturnToMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToMenu();
    }

    void LoseGame()
    {
        isGameOver = true;
        ReturnToMenu();
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
        SceneManager.LoadScene("MENU");
    }

    // Stores up to 10 scores as a comma-separated string in PlayerPrefs
    public static void SaveScoreToHistory(int newScore)
    {
        string raw = PlayerPrefs.GetString("ScoreHistory", "");
        System.Collections.Generic.List<string> entries =
            new System.Collections.Generic.List<string>(
                raw.Length > 0 ? raw.Split(',') : new string[0]);

        entries.Add(newScore.ToString());
        entries.Sort((a, b) => int.Parse(b).CompareTo(int.Parse(a)));
        if (entries.Count > 5) entries.RemoveRange(5, entries.Count - 5);

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
