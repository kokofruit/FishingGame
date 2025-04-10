using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaScreenManager : MonoBehaviour
{
    public static EncyclopediaScreenManager instance;

    [SerializeField] Button listingPrefab;

    [Header("Entry Variables")]
    [SerializeField] GameObject entryScreen;
    [SerializeField] TMP_Text entryNameText;
    [SerializeField] TMP_Text entryDescText;
    [SerializeField] Transform entryModelContainer;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        entryScreen.SetActive(false);
    }

    void OnDisable()
    {
        CloseEntry();
    }

    void CloseEntry()
    {
        // remove model if there is one already
        foreach (Transform child in entryModelContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void OpenEntry(Bug bug)
    {
        entryScreen.SetActive(true);
        
        // set entry texts
        entryNameText.SetText(bug.commonName);
        entryDescText.SetText(bug.description);
        // Create the spinning model
        Instantiate(bug.model, entryModelContainer);
    }

}
