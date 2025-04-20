using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    [TextArea] public string description;
    public int[] costs = {};
    public float[] effects = {};
    public int userLevel = 0;
}
