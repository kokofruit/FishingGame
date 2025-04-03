using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreenManager : MonoBehaviour
{
    public static ShopScreenManager instance;

    [SerializeField] RectTransform scrollViewContent;

    [SerializeField] Button buyListingPrefab;
    [SerializeField] Button sellListingPrefab;

    [SerializeField] Button buyButton;
    [SerializeField] Button sellButton;

    bool isBuyScreen = true;

    List<Button> listings = new();

    #region UNITY BUILT-INS
    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        buyButton.onClick.AddListener(SetBuyScreen);
        sellButton.onClick.AddListener(SetSellScreen);

        buyButton.Select();
    }

    void OnEnable()
    {
        if (isBuyScreen) SetBuyScreen();
        else SetSellScreen();
    }

    void OnDisable()
    {
        ClearListings();
    }
    #endregion

    #region MENU MANAGEMENT

    public void SetBuyScreen(){
        isBuyScreen = true;
        ClearListings();
        PropagateBuyListings();
    }

    public void SetSellScreen(){
        isBuyScreen = false;
        ClearListings();
        PropagateSellListings();
    }

    #endregion

    #region LISTING MANAGEMENT

    #region PROPAGATE
    void PropagateBuyListings()
    {   
        // Starting y value
        int yVal = -25;

        // Create button listings based off bugs in inventory
        foreach (Upgrade upgrade in GameManager.instance.upgrades)
        // for (int i = 0; i < 10; i++)
        {
            // Instantiate
            Button listing = Instantiate(buyListingPrefab, scrollViewContent);
            listing.transform.localPosition = new Vector2(listing.transform.localPosition.x, yVal);
            // Change content
            listing.GetComponent<BuyListingController>().SetUpgrade(upgrade);
            
            // Add to list
            listings.Add(listing);

            // Change y value for next listing's position
            yVal -= 175;
            // Increase size of scollview 
            scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, scrollViewContent.sizeDelta.y + 175);

        }
    }

    void PropagateSellListings()
    {   
        // Starting y value
        int yVal = -25;

        // Create button listings based off bugs in inventory
        foreach (Bug bug in GameManager.instance.inventory)
        // for (int i = 0; i < 10; i++)
        {
            // Instantiate
            Button listing = Instantiate(sellListingPrefab, scrollViewContent);
            listing.transform.localPosition = new Vector2(listing.transform.localPosition.x, yVal);
            // Change content
            listing.GetComponent<SellListingController>().SetBug(bug);

            // Add to list
            listings.Add(listing);

            // Change y value for next listing's position
            yVal -= 175;
            // Increase size of scollview 
            scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, scrollViewContent.sizeDelta.y + 175);
        }
    }
    #endregion

    void ClearListings(){
        // Remove gameobjects
        foreach (Button listing in listings)
        {
            Destroy(listing.gameObject);
        }
        // reset scrolll view height
        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, 25);
        // empty list
        listings.Clear();
    }

    public void RemoveListing(Button listing)
    {
        // Find the listing in the list and remove it
        int listingIndex = listings.IndexOf(listing);
        listings.RemoveAt(listingIndex);

        // Move other listings up
        for (int i = listingIndex; i < listings.Count; i++)
        {
            Button moveListing = listings[i];
            moveListing.transform.localPosition = new Vector2(moveListing.transform.localPosition.x, moveListing.transform.localPosition.y + 175);
        }
        // Decrease size of scrollview
        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, scrollViewContent.sizeDelta.y - 175);

        // Destroy the listing
        Destroy(listing.gameObject);
    }
    #endregion
}
