using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public static TutorialManager instance;

    [SerializeField] TMP_Text tutText;
    [SerializeField] RectTransform tutShadow;

    int stepIndex = 0;

    public struct TutorialValue
    {
        public string DisplayString;
        public Vector3 ShadowPos;

        public TutorialValue(string displayString, Vector3 shadowPos)
        {
            DisplayString = displayString;
            ShadowPos = shadowPos;
        }
    }

    List<TutorialValue> tutValues = new();

    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        tutValues.Add(new TutorialValue("Test string 1", Vector3.one));
        tutValues.Add(new TutorialValue("test string 2!!!", Vector3.zero));

    }

    void OnEnable()
    {
        ProgressTutorial();
    }

    public void ProgressTutorial()
    {
        TutorialValue currentStep = tutValues[stepIndex];
        tutText.SetText(currentStep.DisplayString);
        tutShadow.position = currentStep.ShadowPos;

        stepIndex++;
    }

}
