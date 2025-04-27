using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    UnityAction tutorialReelListener;
    UnityAction tutorialCompletionListener;

    #region UNITY BUILT-INS
    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        // Cache components
        tutCanvasGroup = GetComponent<CanvasGroup>();

        // Set unityactions
        tutorialReelListener = new UnityAction(TutorialReelActor);
        tutorialCompletionListener = new UnityAction(TutorialCompletionActor);
    }

    void OnEnable()
    {
        EventManager.StartListening("TutorialReel", tutorialReelListener);
        EventManager.StartListening("TutorialCompletion", tutorialCompletionListener);
    }

    void Start()
    {
        HideTutorial();
    }

    void OnDisable()
    {
        EventManager.StopListening("TutorialReel", tutorialReelListener);
        EventManager.StopListening("TutorialCompletion", tutorialCompletionListener);
    }

    #endregion

    #region VISIBILITY

    public void OnClick()
    {
        HideTutorial();
        EventManager.TriggerEvent("ExitTutorial");
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

    #endregion

    #region EVENT ACTORS

    void TutorialReelActor()
    {
        ShowTutorial();
        tutorialGroups[0].alpha = 1;
    }

    void TutorialCompletionActor()
    {
        ShowTutorial();
        completionTutorialCompleted = true;
        tutorialGroups[1].alpha = 1;
    }
    
    #endregion
}
