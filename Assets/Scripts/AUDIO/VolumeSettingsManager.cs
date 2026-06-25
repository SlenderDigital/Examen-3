using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager Instance { get; private set; }

    [SerializeField]
    private AudioSettingsData settingsData;

    private const string VolumeKey = "MasterVolume";
    private const string SettingsAssetPath = "Assets/Resources/AudioSettingsData.asset";

    public float Volume => settingsData != null ? settingsData.masterVolume : 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        EnsureSettingsData();
        LoadSettings();
        ApplyVolume();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static AudioSettingsManager EnsureInstance()
    {
        if (Instance != null)
        {
            return Instance;
        }

        var managerObject = new GameObject("AudioSettingsManager");
        return managerObject.AddComponent<AudioSettingsManager>();
    }

    public void SetVolume(float value)
    {
        EnsureSettingsData();
        settingsData.masterVolume = Mathf.Clamp01(value);

        ApplyVolume();

        PlayerPrefs.SetFloat(VolumeKey, settingsData.masterVolume);
        PlayerPrefs.Save();
    }

    private void ApplyVolume()
    {
        EnsureSettingsData();
        AudioListener.volume = settingsData.masterVolume;
    }

    private void LoadSettings()
    {
        EnsureSettingsData();

        if (PlayerPrefs.HasKey(VolumeKey))
        {
            settingsData.masterVolume = PlayerPrefs.GetFloat(VolumeKey);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyVolume();
    }

    private void EnsureSettingsData()
    {
        if (settingsData != null)
        {
            return;
        }

        settingsData = Resources.Load<AudioSettingsData>("AudioSettingsData");

        if (settingsData == null)
        {
#if UNITY_EDITOR
            var directory = Path.GetDirectoryName(SettingsAssetPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            settingsData = ScriptableObject.CreateInstance<AudioSettingsData>();
            AssetDatabase.CreateAsset(settingsData, SettingsAssetPath);
            AssetDatabase.SaveAssets();
#else
            settingsData = ScriptableObject.CreateInstance<AudioSettingsData>();
#endif
        }
    }
}