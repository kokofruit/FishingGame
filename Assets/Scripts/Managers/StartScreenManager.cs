using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// manager for the start screen
public class StartScreenManager : MonoBehaviour
{
    // public instance
    public static StartScreenManager instance;

    // visual components
    [SerializeField] Button startButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitButton;

    // canvas group of the entire start screen
    CanvasGroup canvasGroup;

    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // cache
        canvasGroup = GetComponent<CanvasGroup>();

        // add listeners
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("FishingGame");
    }

    void OpenSettings()
    {
        // show settings menu and pause interactions with start screen
        SceneManager.LoadScene("SettingsMenu", mode: LoadSceneMode.Additive);
        canvasGroup.blocksRaycasts = false;
    }

    public void ResumeScreenRaycasts()
    {
        // allow interactions
        canvasGroup.blocksRaycasts = true;
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
