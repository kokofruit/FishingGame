using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
            // PUBLIC //
    // The singleton instance of the controller
    public static GameManager instance;
    public static bool tutorialCompleted = false;
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

        LoadGame();
    }

    void Start()
    {
        OnReset?.Invoke();
        SetScreen(castScreen);

        if (!tutorialCompleted)
        {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Additive);
        }
    }
    #endregion

    #region SCREEN MANAGEMENT
    void SetScreen(CanvasGroup screen)
    {
        // deactivate old screen
        if (currentScreen != null) currentScreen.gameObject.SetActive(false);
        // set new screen
        currentScreen = screen;
        // activate new screen if not null
        if (currentScreen != null) currentScreen.gameObject.SetActive(true);
    }

    public void PauseScreenRaycasts()
    {
        currentScreen.blocksRaycasts = false;
    }

    public void ResumeScreenRaycasts()
    {
        currentScreen.blocksRaycasts = true;
    }

    public void SetCastScreen()
    {
        SetScreen(castScreen);
        if (!tutorialCompleted) EventManager.TriggerEvent("TutorialPostCatch");
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

        // Trigger tutorial
        if (!tutorialCompleted) EventManager.TriggerEvent("TutorialReel");
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

    public void SaveGame()
    {
        // create new save
        SaveState save = new();

        // add info to save 
        // tutorial completion status
        save.tutorialCompleted = tutorialCompleted;
        // money
        save.money = instance.money;
        // inventory
        save.inventory = BugManager.instance.inventory.ToArray();

        // bug states
        save.bugStatuses = new();
        foreach (Bug bug in BugManager.instance.allBugs)
        {
            string[] bugInfo = { bug.name, bug.isDiscovered.ToString(), bug.amountCaught.ToString() };
            save.bugStatuses.Add(bugInfo);
        }

        // upgrade states
        save.upgradeStatuses = new();
        foreach (Upgrade upgrade in UpgradeManager.instance.upgrades)
        {
            string[] upgradeInfo = { upgrade.name, upgrade.userLevel.ToString() };
            save.upgradeStatuses.Add(upgradeInfo);
        }

        // serialize and close
        BinaryFormatter bf = new();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/player.save");
        print(Application.persistentDataPath);
        bf.Serialize(saveFile, save);
        saveFile.Close();
    }

    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/player.save"))
        {
            // open, deserialize, and close file
            BinaryFormatter bf = new();
            FileStream saveFile = File.Open(Application.persistentDataPath + "/player.save", FileMode.Open);
            SaveState save = (SaveState) bf.Deserialize(saveFile);
            saveFile.Close();
            
            // set tutorial completion
            tutorialCompleted = save.tutorialCompleted;

            // set money
            money = save.money;

            // set inventory
            BugManager.instance.inventory = save.inventory.ToList<Bug>();

            // set bug states
            for (int i = 0; i < save.bugStatuses.Count; i++)
            {
                string[] bugSave = save.bugStatuses[i];
                Bug bug = BugManager.instance.GetBug(bugSave[0]);

                bug.isDiscovered = bool.Parse(bugSave[1]);
                bug.amountCaught = int.Parse(bugSave[2]);
            }

            // set upgrade states
            for (int i = 0; i < save.upgradeStatuses.Count; i++)
            {
                string[] upgradeSave = save.upgradeStatuses[i];
                Upgrade upgrade = UpgradeManager.instance.GetUpgrade(upgradeSave[0]);

                upgrade.userLevel = int.Parse(upgradeSave[1]);
            }
        }
    }
}
