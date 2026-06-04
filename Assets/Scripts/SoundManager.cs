using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource effectsSource;
    public AudioClip coinSound;
    public AudioClip damageSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (effectsSource == null)
        {
            effectsSource = GetComponent<AudioSource>();
        }
    }

    public void PlayCoinSound()
    {
        if (effectsSource != null && coinSound != null)
        {
            effectsSource.PlayOneShot(coinSound);
        }
    }

    public void PlayDamageSound()
    {
        if (effectsSource != null && damageSound != null)
        {
            effectsSource.PlayOneShot(damageSound);
        }
    }
}
