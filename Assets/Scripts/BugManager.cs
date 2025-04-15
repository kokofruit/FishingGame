using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void Start()
    {
        GameManager.instance.OnReset += OnReset;
    }

    public Bug GetBug(string bugName)
    {
        foreach (Bug bug in allBugs)
        {
            if (bug.commonName == bugName)
            {
                return bug;
            }
        }

        return null;
    }

    public void CatchBug(Bug bug)
    {
        bug.isDiscovered = true;
        bug.amountCaught += 1;

        inventory.Add(bug);
    }

    public void SellBug(Bug bug)
    {
        inventory.Remove(bug);
    }

    public Bug RandomBug()
    {
        int bIndex = Random.Range(0, allBugs.Count);
        return allBugs[bIndex];
    }

    void OnReset()
    {
        foreach (Bug bug in allBugs)
        {
            bug.isDiscovered = false;
            bug.amountCaught = 0;
        }
    }

}
