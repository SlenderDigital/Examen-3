using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip damageSound;
    public AudioClip musicClip;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        AudioSettingsManager.EnsureInstance();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = true;
        }

        ApplyVolume();
    }

    private void Start()
    {
        PlayMusic();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusic();
        ApplyVolume();
    }

    public void PlayMusic()
    {
        if (audioSource == null || musicClip == null)
        {
            return;
        }

        if (audioSource.clip != musicClip)
        {
            audioSource.clip = musicClip;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PlayCoinSound()
    {
        if (audioSource == null)
        {
            return;
        }

        if (coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }
    }

    public void PlayDamageSound()
    {
        if (audioSource == null)
        {
            return;
        }

        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    private void ApplyVolume()
    {
        if (audioSource != null && AudioSettingsManager.Instance != null)
        {
            audioSource.volume = AudioSettingsManager.Instance.Volume;
        }
    }
}
