using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manager for keeping track of upgrades
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

    void Start()
    {
        GameManager.instance.OnReset += OnReset;
    }

    public Upgrade GetUpgrade(string upgradeName)
    {
        // return an upgrade based on name
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.name.ToUpper() == upgradeName.ToUpper())
            {
                return upgrade;
            }
        }

        return null;
    }

    public float GetUpgradeEffect(string upgradeName)
    {
        // return the effect of an upgrade at its current level
        Upgrade upgrade = GetUpgrade(upgradeName);
        return upgrade.effects[upgrade.userLevel];
    }

    public int GetUpgradeCost(Upgrade upgrade)
    {
        // return the cost of an upgrade at its current level
        return upgrade.costs[upgrade.userLevel];
    }

    public bool IsUpgradeMax(Upgrade upgrade)
    {
        // return if an upgrade is at max level
        return upgrade.userLevel == upgrade.effects.Length - 1;
    }

    void OnReset()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.userLevel = 0;
        }
    }

}
