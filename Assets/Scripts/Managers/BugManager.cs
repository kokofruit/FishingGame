using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manager for keeping track of bugs
public class BugManager : MonoBehaviour
{
    public static BugManager instance;
    
    public List<Bug> allBugs = new();
    public List<Bug> inventory = new();

    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        GameManager.instance.OnReset += OnReset;
    }

    void Start()
    {
        GameManager.instance.OnReset += OnReset;
    }

    // return a bug based on its name
    public Bug GetBug(string bugName)
    {
        foreach (Bug bug in allBugs)
        {
            if (bug.name == bugName)
            {
                return bug;
            }
        }

        return null;
    }

    // function for when a bug is caught from the minigame
    public void CatchBug(Bug bug)
    {
        // mark it as caught
        bug.isDiscovered = true;
        // add to the amount caught
        bug.amountCaught += 1;
        // add to the inventory
        inventory.Add(bug);
    }

    // remove the bug from the inventory when sold
    public void SellBug(Bug bug)
    {
        inventory.Remove(bug);
    }

    // get a random bug in the dictionary based on the hook level
    public Bug RandomBug()
    {
        float hookLevel = UpgradeManager.instance.GetUpgradeEffect("hook");
        int bIndex = Random.Range(0, ((int)hookLevel) + 1);
        return allBugs[bIndex];
    }

    // reset the bug values
    void OnReset()
    {
        foreach (Bug bug in allBugs)
        {
            bug.isDiscovered = false;
            bug.amountCaught = 0;
        }
    }

}
