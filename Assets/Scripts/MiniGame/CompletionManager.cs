using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletionManager : MonoBehaviour
{
    public static CompletionManager instance;
    // The gaining and losing speeds of the progress bar
    [SerializeField] float gainSpeed;
    [SerializeField] float loseSpeed;
    // The sprite mask
    Image maskImage;
    // If the player is making progress towards the catch (if pointer in target)
    bool isGaining;
    
    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // Cache components
        maskImage = GetComponent<Image>();
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void OnEnable()
    {
        maskImage.fillAmount = 0.25f;
        isGaining = false;

        gainSpeed = UpgradeManager.instance.GetUpgradeEffect("reel");
        loseSpeed = UpgradeManager.instance.GetUpgradeEffect("line");

        StartCoroutine("GracePeriod");
    }

    IEnumerator GracePeriod()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("UpdateCompletion");
    }

    IEnumerator UpdateCompletion()
    {
        while (true)
        {
            // If the status is gaining, remove more of the sprite mask
            if (isGaining)
            {
                maskImage.fillAmount += gainSpeed * Time.deltaTime;

                // If the mask is completely gone, win the fish
                if (maskImage.fillAmount >= 1)
                {
                    GameManager.instance.WinMiniGame();
                    break;
                }
            }
            // Otherwise, add back more of the sprite mask
            else
            {
                maskImage.fillAmount -= loseSpeed * Time.deltaTime;

                // If the mask is completely there, lose the fish
                if (maskImage.fillAmount <= 0)
                {
                    GameManager.instance.LoseMiniGame();
                    break;
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    public void SetGaining(bool value)
    {
        isGaining = value;
    }
}
