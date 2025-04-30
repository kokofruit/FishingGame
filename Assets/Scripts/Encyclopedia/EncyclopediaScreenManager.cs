using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// manager for the encyclopedia screen
// responsible for notebook index and the resulting entries
public class EncyclopediaScreenManager : MonoBehaviour
{
    // public instance
    public static EncyclopediaScreenManager instance;

    // the prefab used for the index listings
    [SerializeField] Button notebookListingPrefab;

    // the gameobject components of the index and entry screen
    [Header("Visual Components")]
    [SerializeField] GameObject indexScreen;
    [SerializeField] RectTransform indexScrollViewContent;
    [SerializeField] GameObject entryScreen;
    [SerializeField] TMP_Text entryNameText;
    [SerializeField] TMP_Text entryScientificText;
    [SerializeField] TMP_Text entryDescText;
    [SerializeField] TMP_Text entryCaughtText;
    [SerializeField] Transform entryModelContainer;

    // private list of discovered bugs only
    List<Bug> discoveredBugs;
    // private list of listings in the index
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
        // close the entry and clear the index
        CloseEntry();
        ClearListings();
    }

    void GetDiscoveredBugList()
    {
        // get only bugs that have been discovered
        discoveredBugs = new();
        foreach (Bug bug in BugManager.instance.allBugs)
        {
            if (bug.isDiscovered) discoveredBugs.Add(bug);
        }
    }

    void OpenIndex()
    {
        float yVal = 0;
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
            yVal -= 48.5f;
            // Increase size of scollview 
            indexScrollViewContent.sizeDelta = new Vector2(indexScrollViewContent.sizeDelta.x, indexScrollViewContent.sizeDelta.y + 175);
        }
    }

    public void CloseEntry()
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
        // Show correct screen
        indexScreen.SetActive(false);
        entryScreen.SetActive(true);
        
        // set entry texts
        entryNameText.SetText(bug.commonName);
        entryScientificText.SetText(bug.scientficName);
        entryDescText.SetText(bug.description);
        entryCaughtText.SetText("Amount Caught: " + bug.amountCaught);

        // Create the spinning model
        GameObject model = Instantiate(bug.model, entryModelContainer);
        model.layer = 5;
    }

    void ClearListings(){
        // Remove gameobjects
        foreach (Button listing in notebookListings)
        {
            Destroy(listing.gameObject);
        }
        // reset scrolll view height
        indexScrollViewContent.sizeDelta = new Vector2(indexScrollViewContent.sizeDelta.x, 25);
        // empty list
        notebookListings.Clear();
    }

}
