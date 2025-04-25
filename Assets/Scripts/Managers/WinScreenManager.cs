using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    public static WinScreenManager instance;
    [SerializeField] Transform spinContainer;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text catchPhraseText;
    [SerializeField] GameObject newStar;

    // Set the singleton instance
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void OnDisable()
    {
        // remove model if there is one already
        foreach (Transform child in spinContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void UnpackBug(Bug bug)
    {
        // Create the spinning model
        Instantiate(bug.model, spinContainer);

        // Set texts
        nameText.SetText(bug.commonName);
        catchPhraseText.SetText(bug.catchPhrase);

        // Show or hide new star
        newStar.SetActive(!bug.isDiscovered);
    }

}
