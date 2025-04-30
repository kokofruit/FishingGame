using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Objects/Bug")]
public class Bug : ScriptableObject
{
    [Header("Display Data")]
    public string commonName;
    public string scientficName;
    [TextArea]
    public string description;
    [Space(15f)]
    public string catchPhrase;
    public GameObject model;

    [Header("Internal Data")]
    public int difficulty;
    public int price;
    [Space(15f)]
    public bool isDiscovered;
    public int amountCaught;
}
