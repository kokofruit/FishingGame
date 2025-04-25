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

    // The display text
    [SerializeField] TMP_Text tutText;

    // Canvas group of the tutorial
    CanvasGroup tutCanvasGroup;
    // UnityActions
    UnityAction tutorialReelListener;

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
    }

    void OnEnable()
    {
        HideTutorial();
        EventManager.StartListening("TutorialReel", tutorialReelListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("TutorialReel", tutorialReelListener);
    }

    #endregion

    #region VISIBILITY
    void ShowTutorial()
    {
        // Show tutorial and enable clicking on it
        tutCanvasGroup.alpha = 1;
        tutCanvasGroup.blocksRaycasts = true;

        // Pause other interactions
        GameManager.instance.PauseScreenRaycasts();
    }

    public void HideTutorial()
    {
        // Hide tutorial and disable clicking on it
        tutCanvasGroup.alpha = 0;
        tutCanvasGroup.blocksRaycasts = false;

        // Resume other interactions
        GameManager.instance.ResumeScreenRaycasts();
    }

    #endregion

    #region EVENT ACTORS

    void TutorialReelActor()
    {
        ShowTutorial();

        tutText.SetText("You hooked a bug!\nTo reel in a bug, rotate your mouse around the reel.");
    }
    
    #endregion
}
