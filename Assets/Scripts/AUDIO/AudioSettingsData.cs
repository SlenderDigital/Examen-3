using UnityEngine;

[CreateAssetMenu(
    fileName = "AudioSettingsData",
    menuName = "Settings/Audio Settings Data")]
public class AudioSettingsData : ScriptableObject
{
    [Header("Volume Settings")]

    [Range(0f, 1f)]
    public float masterVolume = 1f;

    [Range(0f, 1f)]
    public float musicVolume = 1f;

    [Range(0f, 1f)]
    public float sfxVolume = 1f;
}