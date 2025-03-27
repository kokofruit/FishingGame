using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyListingController : MonoBehaviour
{
    Button listing;
    Upgrade storedUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        listing = GetComponent<Button>();
        listing.onClick.AddListener(BuyUpgrade);
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        storedUpgrade = upgrade;
        GetComponentInChildren<TMP_Text>().SetText("Upgrade:" + upgrade.upgradeName + ", Lvl: " + upgrade.level);
        // TODO
    }

    void BuyUpgrade(){

        // if upgrade is max level
        if (storedUpgrade.level == storedUpgrade.costs.Length){
            print("max level, can't upgrade");
            return;
        }  

        // cost of the upgrade at the current level
        int upgradeCost = storedUpgrade.costs[storedUpgrade.level];

        // if player cannot afford it
        if (GameManager.instance.money < upgradeCost){
            print("not enough doll hairs :(");
            return;
        }
    
        // subtract money
        GameManager.instance.money -= upgradeCost;
        // increase level
        storedUpgrade.level += 1;
        // update listing
        SetUpgrade(storedUpgrade);
    
    }
}
