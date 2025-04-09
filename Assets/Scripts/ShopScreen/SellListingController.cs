using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellListingController : MonoBehaviour
{
    // Serialized
    // the text that displays the name of the bug
    [SerializeField] TMP_Text nameText;
    // the text that displays the price of the bug
    [SerializeField] TMP_Text priceText;

    // Private
    // the button component of the entire listing
    Button button;
    // the bug associated with the listing
    Bug storedBug;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SellBug);
    }

    public void SetBug(Bug bug)
    {
        // Set the associated bug
        storedBug = bug;
        // Set visual components to match bug
        nameText.SetText(bug.commonName);
        priceText.SetText("$" + bug.price);
    }

    void SellBug()
    {
        // Get money
        GameManager.instance.money += storedBug.price;
        print(GameManager.instance.money + " dolla dolla bills");
        
        // Remove from inventory
        GameManager.instance.inventory.Remove(storedBug);
        // Remove listing
        ShopScreenManager.instance.RemoveListing(button);

        // Update money display
        ShopScreenManager.instance.UpdateMoney();
    }
}
