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
        // set the associated upgrade
        storedUpgrade = upgrade;
        
        // set the visual components
        nameText.SetText(upgrade.name + ": Lvl " + upgrade.userLevel);
        descText.SetText(upgrade.description);

        // disable maxxed out upgrades
        if (UpgradeManager.instance.IsUpgradeMax(upgrade))
        {
            costText.SetText("max");
            listing.interactable = false;
        }
        else costText.SetText("$" + UpgradeManager.instance.GetUpgradeCost(upgrade));

    }

    void BuyUpgrade(){

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

        // Update money display
        ShopScreenManager.instance.UpdateMoney();
    
    }
}
