using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void TogglePause()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MENU");
    }
}
