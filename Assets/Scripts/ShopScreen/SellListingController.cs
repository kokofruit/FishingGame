using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellListingController : MonoBehaviour
{
    Button button;
    Bug storedBug;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SellBug);
    }

    public void SetBug(Bug bug)
    {
        storedBug = bug;
        GetComponentInChildren<TMP_Text>().SetText(bug.commonName);
        // TODO
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
    }
}
