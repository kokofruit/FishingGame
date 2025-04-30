using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// the manager for the shop screen
// handles sell and buy screen
public class ShopScreenManager : MonoBehaviour
{
    // public instance
    public static ShopScreenManager instance;

    // scroll view for buy and sell screen
    [SerializeField] RectTransform scrollViewContent;

    // prefabs for populating buy and sell screens
    [SerializeField] Button buyListingPrefab;
    [SerializeField] Button sellListingPrefab;

    // buy and sell tab buttons
    [SerializeField] Button buyButton;
    [SerializeField] Button sellButton;

    // money display
    [SerializeField] TMP_Text moneyText;
    
    List<Button> listings = new();

    // the image for the buy button
    Image buyButtonImage;
    // the image for the sell button
    Image sellButtonImage;
    // the color of the inactive overlay for the button
    Color buttonOverlay = new(0.8f, 0.8f, 0.8f);

    #region UNITY BUILT-INS
    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // Cache components
        buyButtonImage = buyButton.GetComponent<Image>();
        sellButtonImage = sellButton.GetComponent<Image>();
    }

    void OnEnable()
    {
        SetBuyScreen();
        UpdateMoney();
    }

    void OnDisable()
    {
        ClearListings();
    }
    #endregion

    #region MENU MANAGEMENT

    public void SetBuyScreen()
    {
        // set tab colors
        buyButtonImage.color = Color.white;
        sellButtonImage.color = buttonOverlay;

        // clear the listings and replace
        ClearListings();
        PropagateBuyListings();
    }

    public void SetSellScreen()
    {
        // set tab colors
        buyButtonImage.color = buttonOverlay;
        sellButtonImage.color = Color.white;

        // clear the listings and replace
        ClearListings();
        PropagateSellListings();
    }

    #endregion

    #region LISTING MANAGEMENT

    #region PROPAGATE
    void PropagateBuyListings()
    {   
        // Starting y value
        int yVal = 0;

        // Create button listings based off bugs in inventory
        foreach (Upgrade upgrade in UpgradeManager.instance.upgrades)
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
            yVal -= 150;
            // Increase size of scollview 
            scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, scrollViewContent.sizeDelta.y + 150);

        }
    }

    void PropagateSellListings()
    {   
        // Starting y value
        int yVal = 0;

        // Create button listings based off bugs in inventory
        foreach (Bug bug in BugManager.instance.inventory)
        {
            // Instantiate
            Button listing = Instantiate(sellListingPrefab, scrollViewContent);
            listing.transform.localPosition = new Vector2(listing.transform.localPosition.x, yVal);
            // Change content
            listing.GetComponent<SellListingController>().SetBug(bug);

            // Add to list
            listings.Add(listing);

            // Change y value for next listing's position
            yVal -= 150;
            // Increase size of scollview 
            scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, scrollViewContent.sizeDelta.y + 150);
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
        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, 0);
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
            moveListing.transform.localPosition = new Vector2(moveListing.transform.localPosition.x, moveListing.transform.localPosition.y + 150);
        }
        // Decrease size of scrollview
        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, scrollViewContent.sizeDelta.y - 155);

        // Destroy the listing
        Destroy(listing.gameObject);
    }
    #endregion

    public void UpdateMoney()
    {
        moneyText.SetText("$ " + GameManager.instance.money);
    }
}
