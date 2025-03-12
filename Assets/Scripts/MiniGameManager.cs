using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
            // PUBLIC //
    // The singleton instance of the controller
    public static MiniGameManager instance;

    // If the player is making progress towards the catch (if pointer in target)
    public static bool isGainingCompletion = false;

    // Delegate used to move the pointer
    public delegate void MovePointer(float position);
    public event MovePointer movePointer;

            // SERIALIZED //
    // The prefab used to make a minigame instance
    [SerializeField] GameObject MiniGamePrefab;
    [SerializeField] CanvasGroup LoseScreen;
    [SerializeField] CanvasGroup WinScreen;

            // PRIVATE //
    // The current instance of a minigame, null if there is none
    GameObject mgInstance = null;
    // The current screen, win or lose, null if none
    CanvasGroup currentScreen = null;

    
    private void Awake()
    {
        // Set the singleton instance

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartMiniGame();
    }

    // Start a new fishing minigame
    public void StartMiniGame()
    {
        mgInstance = Instantiate(MiniGamePrefab);
    }

    // End a fishing minigame
    public void EndMiniGame(bool bugGained)
    {
        Destroy(mgInstance);
        mgInstance = null;

        if (bugGained)
        {
            currentScreen = WinScreen;
        }
        else
        {
            currentScreen = LoseScreen;
        }
        currentScreen.gameObject.SetActive(true);
    }

    // Close win or lose screen
    public void CloseScreen()
    {
        currentScreen.gameObject.SetActive(false);
        currentScreen = null;
    }

    public void UpdatePointer(float position)
    {
        if (movePointer != null) movePointer(position);
    }
}
