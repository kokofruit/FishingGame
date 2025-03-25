using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreenManager : MonoBehaviour
{
    public static ShopScreenManager instance;
    [SerializeField] Button listingPrefab;
    [SerializeField] RectTransform scrollViewContent;
    List<Button> allListings = new();

    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        PropagateListings();
    }

    void OnDisable()
    {
        ClearListings();
    }

    void PropagateListings()
    {   
        // Starting y value
        int yVal = -25;

        // Create button listings based off bugs in inventory
        foreach (Bug bug in GameManager.instance.inventory)
        // for (int i = 0; i < 10; i++)
        {
            // Instantiate
            Button listing = Instantiate(listingPrefab, scrollViewContent);
            listing.transform.localPosition = new Vector2(listing.transform.localPosition.x, yVal);
            // Change content
            listing.GetComponent<ShopListingController>().SetBug(bug);

            // Add to list
            allListings.Add(listing);

            // Change y value for next listing's position
            yVal -= 175;
            // Increase size of scollview 
            scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, scrollViewContent.sizeDelta.y + 175);
        }
    }

    void ClearListings(){
        // Remove gameobjects
        foreach (Button listing in allListings)
        {
            Destroy(listing.gameObject);
        }
        // empty list
        allListings.Clear();
    }

    public void RemoveListing(Button listing)
    {
        // Find the listing in the list and remove it
        int listingIndex = allListings.IndexOf(listing);
        allListings.RemoveAt(listingIndex);

        // Move other listings up
        for (int i = listingIndex; i < allListings.Count; i++)
        {
            Button moveListing = allListings[i];
            moveListing.transform.localPosition = new Vector2(moveListing.transform.localPosition.x, moveListing.transform.localPosition.y + 175);
        }
        // Decrease size of scrollview
        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, scrollViewContent.sizeDelta.y - 175);

        // Destroy the listing
        Destroy(listing.gameObject);
    }

}
