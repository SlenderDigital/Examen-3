using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private AudioSettingsManager audioManager;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = AudioSettingsManager.EnsureInstance();
        }
    }

    private void Start()
    {
        if (slider != null)
        {
            slider.value = audioManager.Volume;
            slider.onValueChanged.RemoveListener(OnVolumeChanged);
            slider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    private void OnVolumeChanged(float value)
    {
        if (audioManager != null)
        {
            audioManager.SetVolume(value);
        }
    }

    private void OnDestroy()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }
}