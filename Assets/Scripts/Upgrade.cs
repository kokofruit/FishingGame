using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    public int level = 0;
    public int[] costs = {};

}
