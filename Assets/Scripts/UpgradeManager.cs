using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    
    public List<Upgrade> upgrades = new();

    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public Upgrade GetUpgrade(string upgradeName)
    {
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.internalName == upgradeName)
            {
                return upgrade;
            }
        }

        return null;
    }

    public float GetUpgradeEffect(string upgradeName)
    {
        Upgrade upgrade = GetUpgrade(upgradeName);
        return upgrade.effects[upgrade.userLevel];
    }

    public int GetUpgradeCost(string upgradeName)
    {
        Upgrade upgrade = GetUpgrade(upgradeName);
        return upgrade.costs[upgrade.userLevel];
    }
    public int GetUpgradeCost(Upgrade upgrade)
    {
        return upgrade.costs[upgrade.userLevel];
    }

    public bool IsUpgradeMax(Upgrade upgrade)
    {
        return upgrade.userLevel == upgrade.maxLevel;
    }

}
