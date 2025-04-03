using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyListingController : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descText;
    [SerializeField] TMP_Text costText;

    Button listing;
    Upgrade storedUpgrade;

    void Awake()
    {
        listing = GetComponent<Button>();
        listing.onClick.AddListener(BuyUpgrade);
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        storedUpgrade = upgrade;
        
        nameText.SetText(upgrade.displayName + ": Lvl " + upgrade.userLevel);
        descText.SetText(upgrade.description);

        if (UpgradeManager.instance.IsUpgradeMax(upgrade))
        {
            costText.SetText("max");
            listing.interactable = false;
        }
        else costText.SetText("$" + UpgradeManager.instance.GetUpgradeCost(upgrade));

    }

    void BuyUpgrade(){

        // if upgrade is max level
        // if (storedUpgrade.userLevel == storedUpgrade.maxLevel){
        //     print("max level, can't upgrade");
        //     return;
        // }  

        // cost of the upgrade at the current level
        int upgradeCost = UpgradeManager.instance.GetUpgradeCost(storedUpgrade);

        // if player cannot afford it
        if (GameManager.instance.money < upgradeCost){
            print("not enough doll hairs :(");
            return;
        }
    
        // subtract money
        GameManager.instance.money -= upgradeCost;
        // increase level
        storedUpgrade.userLevel += 1;
        // update listing
        SetUpgrade(storedUpgrade);
    
    }
}
