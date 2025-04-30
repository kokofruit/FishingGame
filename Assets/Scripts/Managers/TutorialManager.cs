using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// manager for the tutorial scene
public class TutorialManager : MonoBehaviour
{
    // Public instance
    public static TutorialManager instance;
    public bool completionTutorialCompleted = false;

    // The display groups
    [SerializeField] List<CanvasGroup> tutorialGroups;

    // Canvas group of the tutorial
    CanvasGroup tutCanvasGroup;
    // UnityActions
    UnityAction tutorialCastingListener;
    UnityAction tutorialReelListener;
    UnityAction tutorialCompletionListener;
    UnityAction tutorialPostCatchListener;

    #region UNITY BUILT-INS
    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        // Cache components
        tutCanvasGroup = GetComponent<CanvasGroup>();

        // Set unityactions
        tutorialCastingListener = new UnityAction(TutorialCastingActor);
        tutorialReelListener = new UnityAction(TutorialReelActor);
        tutorialCompletionListener = new UnityAction(TutorialCompletionActor);
        tutorialPostCatchListener = new UnityAction(TutorialPostCatchActor);
    }

    void OnEnable()
    {
        // start listening
        EventManager.StartListening("TutorialCasting", tutorialCastingListener);
        EventManager.StartListening("TutorialReel", tutorialReelListener);
        EventManager.StartListening("TutorialCompletion", tutorialCompletionListener);
        EventManager.StartListening("TutorialPostCatch", tutorialPostCatchListener);
    }

    void Start()
    {
        EventManager.TriggerEvent("TutorialCasting");
    }

    void OnDisable()
    {
        // stop listening
        EventManager.StopListening("TutorialCasting", tutorialCastingListener);
        EventManager.StopListening("TutorialReel", tutorialReelListener);
        EventManager.StopListening("TutorialCompletion", tutorialCompletionListener);
        EventManager.StopListening("TutorialPostCatch", tutorialPostCatchListener);
    }

    #endregion

    #region VISIBILITY

    public void OnClick()
    {
        HideTutorial();
        if (!GameManager.tutorialCompleted) EventManager.TriggerEvent("ExitTutorial");
        else EndTutorial();
    }

    void ShowTutorial()
    {
        // Show tutorial and enable clicking on it
        tutCanvasGroup.alpha = 1;
        tutCanvasGroup.blocksRaycasts = true;

        // Pause other interactions
        GameManager.instance.PauseScreenRaycasts();
    }

    void HideTutorial()
    {
        // Hide tutorial and disable clicking on it
        tutCanvasGroup.alpha = 0;
        tutCanvasGroup.blocksRaycasts = false;

        foreach (CanvasGroup imageGroup in tutorialGroups)
        {
            imageGroup.alpha = 0;
        }

        // Resume other interactions
        GameManager.instance.ResumeScreenRaycasts();
    }

    void EndTutorial()
    {
        GameManager.instance.ResumeScreenRaycasts();
        SceneManager.UnloadSceneAsync("Tutorial");
    }

    #endregion

    // functions that respond to event triggers and show relevant tutorial content
    #region EVENT ACTORS

    // for when you start
    void TutorialCastingActor()
    {
        ShowTutorial();
        tutorialGroups[0].alpha = 1;
    }

    // explains how to reel in
    void TutorialReelActor()
    {
        ShowTutorial();
        tutorialGroups[1].alpha = 1;
    }

    // explains the completion radial
    void TutorialCompletionActor()
    {
        ShowTutorial();
        completionTutorialCompleted = true;
        tutorialGroups[2].alpha = 1;
    }

    // explains the shop and notebook
    void TutorialPostCatchActor()
    {
        ShowTutorial();
        tutorialGroups[3].alpha = 1;
        GameManager.tutorialCompleted = true;
    }

    #endregion
}
