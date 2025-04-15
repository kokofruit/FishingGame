using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotebookListingController : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;

    Button button;
    Bug storedBug;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenEntry);
    }

    public void SetBug(Bug bug)
    {
        storedBug = bug;
        nameText.SetText(bug.commonName);
    }

    void OpenEntry()
    {
        EncyclopediaScreenManager.instance.OpenEntry(storedBug);
    }
    
}
