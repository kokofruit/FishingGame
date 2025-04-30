using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

// manager for the settings screen
public class SettingsScreenManager : MonoBehaviour
{
    // visual components
    [SerializeField] Button saveButton;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider soundVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Button closeButton;
    [SerializeField] AudioMixer audioMixer;

    // internal variables for keeping track of volume and setting sliders
    float masterVolume;
    float soundVolume;
    float musicVolume;

    void Awake()
    {
        // add listeners
        saveButton.onClick.AddListener(SaveButton);
        masterVolumeSlider.onValueChanged.AddListener(MasterVolumeSliderChanged);
        soundVolumeSlider.onValueChanged.AddListener(SoundVolumeSliderChanged);
        musicVolumeSlider.onValueChanged.AddListener(MusicVolumeSliderChanged);
        closeButton.onClick.AddListener(CloseButton);
    }

    void OnEnable()
    {
        // load volume settings to set sliders
        LoadPrefs();

        // disable save button if cannot be saved
        if (GameManager.instance == null)
        {
            saveButton.interactable = false;
        }
        else
        {
            saveButton.interactable = true;
        }
    }

    void SaveButton()
    {
        GameManager.instance.SaveGame();
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

    void CloseButton()
    {
        // reactivate other scene
        if (StartScreenManager.instance != null) StartScreenManager.instance.ResumeScreenRaycasts();
        else if (GameManager.instance != null)
        {
            GameManager.instance.SetCastScreen();
        }

        // close scene
        SceneManager.UnloadSceneAsync("SettingsMenu");
    }

}
