using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotebookListingController : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;

    Button button;
    Bug storedBug;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PullUpBug);
    }

    public void SetBug(Bug bug)
    {
        storedBug = bug;
        nameText.SetText(bug.commonName);
    }

    void PullUpBug()
    {
        print(storedBug.commonName);
    }
    
}
