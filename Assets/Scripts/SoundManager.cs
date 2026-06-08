using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
            return;
        }

        if (effectsSource == null)
        {
            effectsSource = GetComponent<AudioSource>();
        }
    }

    public void PlayCoinSound()
    {
        if (effectsSource == null) effectsSource = GetComponent<AudioSource>();
        
        if (effectsSource != null && coinSound != null)
        {
            effectsSource.PlayOneShot(coinSound);
        }
    }

    public void PlayDamageSound()
    {
        if (effectsSource == null) effectsSource = GetComponent<AudioSource>();

        if (effectsSource != null && damageSound != null)
        {
            effectsSource.PlayOneShot(damageSound);
        }
    }
}
