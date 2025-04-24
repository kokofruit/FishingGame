using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
            // PUBLIC //
    // The singleton instance of the controller
    public static GameManager instance;
    public float money;
    public delegate void reset();
    public event reset OnReset;

            // SERIALIZED //
    // Screens to turn on and off
    [Header("Screens")]
    [SerializeField] CanvasGroup miniGameScreen;
    [SerializeField] CanvasGroup loseScreen;
    [SerializeField] CanvasGroup winScreen;
    [SerializeField] CanvasGroup castScreen;
    [SerializeField] CanvasGroup waitScreen;
    [SerializeField] CanvasGroup shopScreen;
    [SerializeField] CanvasGroup encyclopediaScreen;

            // PRIVATE //
    // The current screen, win or lose, null if none
    CanvasGroup currentScreen;
    // The bug being fished for currently
    Bug currentBug;

    #region UNITY BUILT-INS
    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        OnReset?.Invoke();
        SetScreen(castScreen);
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Additive);
    }
    #endregion

    #region SET SCREENS
    void SetScreen(CanvasGroup screen)
    {
        // deactivate old screen
        if (currentScreen != null) currentScreen.gameObject.SetActive(false);
        // set new screen
        currentScreen = screen;
        // activate new screen if not null
        if (currentScreen != null) currentScreen.gameObject.SetActive(true);
    }

    public void SetCastScreen()
    {
        SetScreen(castScreen);
    }

    public void SetShopScreen()
    {
        SetScreen(shopScreen);
    }

    public void SetEncyclopediaScreen()
    {
        SetScreen(encyclopediaScreen);
    }

    #endregion

    #region CASTING
    public void Cast()
    {
        SetScreen(waitScreen);
        StartCoroutine("WaitForBite");
    }

    IEnumerator WaitForBite()
    {
        float waitDuration = Random.Range(2f, 5f) * UpgradeManager.instance.GetUpgradeEffect("lure");
        yield return new WaitForSeconds(waitDuration);
        StartMiniGame();
    }

    #endregion

    #region MINIGAME

    // Start a new fishing minigame
    void StartMiniGame()
    {
        SetScreen(miniGameScreen);
        // choose a bug
        currentBug = BugManager.instance.RandomBug();
        TargetManager.instance.SetDifficulty(currentBug.difficulty);
    }

    // End a fishing minigame
    public void WinMiniGame()
    {
        SetScreen(winScreen);
        WinScreenManager.instance.UnpackBug(currentBug);
        BugManager.instance.CatchBug(currentBug);
    }
    
    public void LoseMiniGame()
    {
        SetScreen(loseScreen);
    }

    #endregion

}
