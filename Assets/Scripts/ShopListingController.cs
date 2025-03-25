using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopListingController : MonoBehaviour
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
        print("dolla dolla bills");
        ShopScreenManager.instance.RemoveListing(button);
    }
}
