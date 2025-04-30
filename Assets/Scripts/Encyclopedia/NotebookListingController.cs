using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// controller for listings within the notebook
public class NotebookListingController : MonoBehaviour
{
    // text for displaying the bug's name
    [SerializeField] TMP_Text nameText;

    // the button component of the entire listing
    Button button;
    // the bug related to the listing
    Bug storedBug;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenEntry);
    }

    // set the bug for future reference and visual component
    public void SetBug(Bug bug)
    {
        storedBug = bug;
        nameText.SetText(bug.commonName);
    }

    // ask the encyclopedia manager to 'open' the entry for the stored bug
    void OpenEntry()
    {
        EncyclopediaScreenManager.instance.OpenEntry(storedBug);
    }
    
}
