using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionController : MonoBehaviour
{
    // The gaining and losing speeds of the progress bar
    [SerializeField] float gainSpeed;
    [SerializeField] float loseSpeed;
    // The sprite mask
    SpriteMask mask;
    // If the player is making progress towards the catch (if pointer in target)
    bool isGaining;

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
            if (mask.alphaCutoff <= 0) MiniGameManager.instance.EndMiniGame(true);
        }
        // Otherwise, add back more of the sprite mask
        else
        {
            mask.alphaCutoff += loseSpeed * Time.deltaTime;

            // If the mask is completely there, lose the fish
            if (mask.alphaCutoff >= 1) MiniGameManager.instance.EndMiniGame(false);
        }
    }

    public void SetGaining(bool value)
    {
        isGaining = value;
    }
}
