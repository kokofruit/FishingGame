using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
            // PUBLIC //
    // The singleton instance of the controller
    public static GameManager instance;
    public delegate void ResetMiniGame();
    public event ResetMiniGame resetMiniGame;
    public List<Bug> inventory;

            // SERIALIZED //
    // Screens to turn on and off
    [SerializeField] CanvasGroup miniGameScreen;
    [SerializeField] CanvasGroup loseScreen;
    [SerializeField] CanvasGroup winScreen;
    [SerializeField] CanvasGroup castScreen;
    [SerializeField] CanvasGroup waitScreen;
    [SerializeField] CanvasGroup shopScreen;
    // list of all bugs
    [SerializeField] List<Bug> bugList;

            // PRIVATE //
    // The current screen, win or lose, null if none
    CanvasGroup currentScreen;
    // The bug being fished for currently
    Bug currentBug;

    
    void Awake()
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

    #region CASTING
    public void Cast()
    {
        SetScreen(waitScreen);
        StartCoroutine("WaitForBite");
    }

    IEnumerator WaitForBite()
    {
        float waitDuration = Random.Range(1f, 6f);
        yield return new WaitForSeconds(waitDuration);
        StartMiniGame();
    }

    #endregion

    #region MINIGAME

    // Choose bug
    void ChooseBug()
    {
        int bIndex = Random.Range(0, bugList.Count);
        currentBug = bugList[bIndex];
        TargetManager.instance.SetDifficulty(currentBug.difficulty);
    }

    // Start a new fishing minigame
    void StartMiniGame()
    {
        SetScreen(miniGameScreen);
        ChooseBug();
        resetMiniGame?.Invoke();
    }

    // End a fishing minigame
    public void WinMiniGame()
    {
        SetScreen(winScreen);
        WinScreenManager.instance.UnpackBug(currentBug);
        inventory.Add(currentBug);
    }
    public void LoseMiniGame()
    {
        SetScreen(loseScreen);
    }

    #endregion

    public void SetCastScreen()
    {
        SetScreen(castScreen);
    }

    public void SetShopScreen()
    {
        SetScreen(shopScreen);
    }

}
