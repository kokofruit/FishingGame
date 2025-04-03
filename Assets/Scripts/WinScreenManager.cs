using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    public static WinScreenManager instance;
    [SerializeField] GameObject spinContainer;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descText;

    // Set the singleton instance
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void UnpackBug(Bug bug)
    {
        // gather values
        GameObject model = bug.model;
        string cName = bug.commonName;
        string desc = bug.description;

        // remove model if there is one already
        if (spinContainer.transform.childCount != 0) Destroy(spinContainer.transform.GetChild(0).gameObject);
        // Create the spinning model
        Instantiate(model, spinContainer.transform);

        // Set texts
        nameText.SetText(cName);
        descText.SetText(desc);
    }
}
