using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance;
    
    private float _musicVolume = 5;
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            setMusicVolume(_musicVolume);
        }
    }
    
    private float _sfxVolume = 5; 
    public float SfxVolume
    {
        get => _sfxVolume;
        set
        {
            _sfxVolume = value;
            setSfxVolume(_sfxVolume);
        }
    }

    private void Awake()
    {
        CreateSingleton();
    }
    
    private void setMusicVolume(float value)
    {
        //todo add Functionality
    }
    private void setSfxVolume(float value)
    {
        //todo add Functionality
    }
    
    private void CreateSingleton()
    {
        // if there's already an instance, and it's not THIS object, destroy.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
