using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionController : MonoBehaviour
{
    // The singleton instance of the controller
    public static CompletionController instance;
    // The gaining and losing speeds of the progress bar
    [SerializeField] float gainSpeed;
    [SerializeField] float loseSpeed;
    // The sprite mask
    SpriteMask mask;
    // The gaining status
    bool isGaining = false;


    // Set the instance
    void Awake()
    {
        instance = this;
    }

    // Initialize variables
    void Start()
    {
        mask = GetComponent<SpriteMask>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the status is gaining, remove more of the sprite mask
        if (isGaining)
        {
            mask.alphaCutoff -= gainSpeed * Time.deltaTime;

            // If the mask is completely gone, win the fish
            if (mask.alphaCutoff <= 0) print("gain fish");
        }
        // Otherwise, add back more of the sprite mask
        else
        {
            mask.alphaCutoff += loseSpeed * Time.deltaTime;

            // If the mask is completely there, lose the fish
            if (mask.alphaCutoff >= 1) print("lose fish");
        }
    }

    // Turn the gaining status on or off
    public void SetGaining(bool gainStatus)
    {
        isGaining = gainStatus;
    }
}
