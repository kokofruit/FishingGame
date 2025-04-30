using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// manager for the win screen
public class WinScreenManager : MonoBehaviour
{
    // public instance
    public static WinScreenManager instance;

    // visual components
    [SerializeField] Transform spinContainer;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text catchPhraseText;
    [SerializeField] GameObject newStar;
    [SerializeField] AudioClip tadaSound;

    // Set the singleton instance
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        // play victory sound
        SoundManager.instance.PlaySoundOnce(tadaSound, volume: 0.4f);
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
        GameObject bugModel = Instantiate(bug.model, spinContainer);
        bugModel.layer = 5;

        // Set texts
        nameText.SetText(bug.commonName);
        catchPhraseText.SetText(bug.catchPhrase);

        // Show or hide new star
        newStar.SetActive(!bug.isDiscovered);
    }

}
