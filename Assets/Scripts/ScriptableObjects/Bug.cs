using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Bug")]
public class Bug : ScriptableObject
{
    [Header("Display Data")]
    public string commonName;
    public string catchPhrase;
    [TextArea]
    public string description;
    public GameObject model;

    [Header("Internal Data")]
    public int difficulty;
    public bool isDiscovered;
    public int price;
}
