using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Audio Clips")]
    public AudioClip ButtonClickSFX;
    public AudioClip CoinSFX;
    public AudioClip MinigameFinishSFX;
    public AudioClip StallClickSFX;
    public AudioClip UpgradeSFX;


    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }



    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void OnCloseSettings()
    {
        PlayerPrefs.Save();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sfxSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
        PlaySFX(ButtonClickSFX);
    }

    public void PlayCoin()
    {
        PlaySFX(CoinSFX);
    }

    public void PlayMinigameFinish()
    {
        PlaySFX(MinigameFinishSFX);
    }

    public void PlayStallClick()
    {
        PlaySFX(StallClickSFX);
    }

    public void PlayUpgrade()
    {
        PlaySFX(UpgradeSFX);
    }
    
}
