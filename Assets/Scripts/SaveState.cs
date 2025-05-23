using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public bool tutorialCompleted;

    public float money;
    public List<string> inventory;

    public List<string[]> bugStatuses;
    public List<string[]> upgradeStatuses;
}
