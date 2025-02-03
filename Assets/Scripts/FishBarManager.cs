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

    float minPos;
    float maxPos;
    float range;

    // Start is called before the first frame update
    void Start()
    {
        // Store the range of the fish bar
        range = rangeBar.GetComponent<RectTransform>().rect.width;

        // Store the left-most position of the target
        minPos = target.transform.position.x;

        // Calculate the right-most position of the fish bar
        float fishBarWidth = target.rectTransform.rect.width;
        maxPos = minPos + range - fishBarWidth;
        
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
