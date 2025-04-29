using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScreenManager : MonoBehaviour
{

    [SerializeField] Button saveButton;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider soundVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Button creditsButton;
    [SerializeField] AudioMixer audioMixer;

    float masterVolume;
    float soundVolume;
    float musicVolume;

    void Awake()
    {
        saveButton.onClick.AddListener(SaveButton);
        masterVolumeSlider.onValueChanged.AddListener(MasterVolumeSliderChanged);
        soundVolumeSlider.onValueChanged.AddListener(SoundVolumeSliderChanged);
        musicVolumeSlider.onValueChanged.AddListener(MusicVolumeSliderChanged);
    }

    void OnEnable()
    {
        LoadPrefs();
    }

    void SaveButton()
    {

    }

    #region VOLUME

    #region MASTER

    void MasterVolumeSliderChanged(float value)
    {
        // store the volume preference
        PlayerPrefs.SetFloat("MasterVolume", value);
        // set the actual volume
        SetMasterVolume(value);
    }

    void SetMasterVolume(float volume)
    {
        // set the internal variable
        masterVolume = volume;
        // Set the mixer's volume
        audioMixer.SetFloat("MasterVolume", volume);
    }

    #endregion

    #region SOUND

    void SoundVolumeSliderChanged(float value)
    {
        // store the volume preference
        PlayerPrefs.SetFloat("SoundVolume", value);
        // set the actual volume
        SetSoundVolume(value);
    }

    void SetSoundVolume(float volume)
    {
        // set the internal variable
        soundVolume = volume;
        // Set the mixer's volume
        audioMixer.SetFloat("SoundVolume", volume);
    }

    #endregion

    #region MUSIC

    void MusicVolumeSliderChanged(float value)
    {
        // store the volume preference
        PlayerPrefs.SetFloat("MusicVolume", value);
        // set the actual volume
        SetMusicVolume(value);
    }

    void SetMusicVolume(float volume)
    {
        // set the internal variable
        musicVolume = volume;
        // Set the mixer's volume
        audioMixer.SetFloat("MusicVolume", volume);
    }

    #endregion
    
    #endregion

    public void LoadPrefs()
    {
        // set the sound volume and the sound slider
        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 0f));
        print("master:" + masterVolume);
        masterVolumeSlider.SetValueWithoutNotify(masterVolume);

        // set the sound volume and the sound slider
        SetSoundVolume(PlayerPrefs.GetFloat("SoundVolume", 0f));
        print("sound:" + soundVolume);
        soundVolumeSlider.SetValueWithoutNotify(soundVolume);
        
        // set the music volume and the music slider
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0f));
        print("music:" + musicVolume);
        musicVolumeSlider.SetValueWithoutNotify(musicVolume);
    }

}
