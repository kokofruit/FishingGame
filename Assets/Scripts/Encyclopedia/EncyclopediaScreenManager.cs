using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaScreenManager : MonoBehaviour
{
    public static EncyclopediaScreenManager instance;

    [SerializeField] Button notebookListingPrefab;

    [Header("Entry Variables")]
    [SerializeField] GameObject indexScreen;
    [SerializeField] RectTransform indexScrollViewContent;
    [SerializeField] GameObject entryScreen;
    [SerializeField] TMP_Text entryNameText;
    [SerializeField] TMP_Text entryDescText;
    [SerializeField] Transform entryModelContainer;

    List<Bug> discoveredBugs;
    List<Button> notebookListings = new();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        // populate bugs list
        GetDiscoveredBugList();

        // make sure correct screen is shown
        CloseEntry();
        OpenIndex();
    }

    void OnDisable()
    {
        CloseEntry();
    }

    void GetDiscoveredBugList()
    {
        discoveredBugs = new();
        foreach (Bug bug in BugManager.instance.allBugs)
        {
            if (bug.isDiscovered) discoveredBugs.Add(bug);
        }
    }

    void OpenIndex()
    {
        int yVal = 0;
        foreach (Bug bug in discoveredBugs)
        {
            // Instantiate
            Button listing = Instantiate(notebookListingPrefab, indexScrollViewContent);
            listing.transform.localPosition = new Vector2(listing.transform.localPosition.x, yVal);
            // Change content
            listing.GetComponent<NotebookListingController>().SetBug(bug);

            // Add to list
            notebookListings.Add(listing);

            // Change y value for next listing's position
            yVal -= 60;
            // Increase size of scollview 
            indexScrollViewContent.sizeDelta = new Vector2(indexScrollViewContent.sizeDelta.x, indexScrollViewContent.sizeDelta.y + 175);
        }
    }

    void CloseEntry()
    {
        // remove model if there is one already
        foreach (Transform child in entryModelContainer)
        {
            Destroy(child.gameObject);
        }
        // hide entry screen and show index screen
        entryScreen.SetActive(false);
        indexScreen.SetActive(true);
    }

    public void OpenEntry(Bug bug)
    {
        indexScreen.SetActive(false);
        entryScreen.SetActive(true);
        
        // set entry texts
        entryNameText.SetText(bug.commonName);
        entryDescText.SetText(bug.description);
        // Create the spinning model
        Instantiate(bug.model, entryModelContainer);
    }

}
