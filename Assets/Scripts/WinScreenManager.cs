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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //Instantiate(model, spinContainer.transform);
    }

    public void UnpackBug(Bug bug)
    {
        GameObject model = bug.model;
        string cName = bug.commonName;
        string desc = bug.description;

        if (spinContainer.transform.childCount != 0) Destroy(spinContainer.transform.GetChild(0));
        Instantiate(model, spinContainer.transform);
    }
}
