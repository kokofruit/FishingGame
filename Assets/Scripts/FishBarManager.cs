using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishBarManager : MonoBehaviour
{
    // The range bar that limits where the target can move in
    [SerializeField] Image rangeBar;

    // The target that moves around
    [SerializeField] Image target;

    [SerializeField] float moveSpeed;

    float minPos;
    float maxPos;
    float range;

    // Start is called before the first frame update
    void Start()
    {
        // Store the range of the fish bar
        RectTransform rangeRectTransform = rangeBar.GetComponent<RectTransform>();
        range = rangeRectTransform.rect.width;

        // Store the left-most position of the target
        float fishBarWidth = target.rectTransform.rect.width;
        minPos = rangeRectTransform.position.x - range / 2 + fishBarWidth / 2;

        // Calculate the right-most position of the fish bar
        maxPos = rangeRectTransform.position.x + range / 2 + fishBarWidth / 2;

        //print("range: " + range + ". minPos " + minPos + ". maxPos " + maxPos);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveFishBar(float amount)
    {

    }
}
