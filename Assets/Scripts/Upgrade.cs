using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string displayName;
    [TextArea] public string description;
    public string internalName;
    public int maxLevel;
    public int[] costs = {};
    public float[] effects = {};
    public int userLevel = 0;
}
