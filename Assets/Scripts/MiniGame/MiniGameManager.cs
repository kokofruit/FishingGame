using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
            // PUBLIC //
    // The singleton instance of the controller
    public static MiniGameManager instance;

            // SERIALIZED //
    // The prefab used to make a minigame instance
    [SerializeField] GameObject miniGamePrefab;
    [SerializeField] CanvasGroup loseScreen;
    [SerializeField] CanvasGroup winScreen;
    [SerializeField] CanvasGroup castScreen;

            // PRIVATE //
    // The current instance of a minigame, null if there is none
    GameObject mgInstance = null;
    // The current screen, win or lose, null if none
    CanvasGroup currentScreen;

    
    private void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetScreen(castScreen);
    }

    void SetScreen(CanvasGroup screen)
    {
        // deactivate old screen
        if (currentScreen != null) currentScreen.gameObject.SetActive(false);
        // set new screen
        currentScreen = screen;
        // activate new screen if not null
        if (currentScreen != null) currentScreen.gameObject.SetActive(true);
    }

    public void Cast()
    {
        SetScreen(null);
        StartMiniGame();
    }

    // Start a new fishing minigame
    void StartMiniGame()
    {
        mgInstance = Instantiate(miniGamePrefab);
    }

    // End a fishing minigame
    public void EndMiniGame(bool bugGained)
    {
        // destroy the minigame
        Destroy(mgInstance);
        mgInstance = null;
        // set the screen to the win screen or lose screen
        SetScreen(bugGained ? winScreen : loseScreen);
    }

    public void ExitWinLoseScreen()
    {
        SetScreen(castScreen);
    }

}
