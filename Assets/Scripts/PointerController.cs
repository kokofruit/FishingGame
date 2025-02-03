using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class PointerController : MonoBehaviour
{
    public static PointerController instance;

    // The range bar that limits where the target can move in
    [SerializeField] Image rangeBar;
    // The width of the rangeBar, which is the range of the pointer
    float range;

    // The target that moves around
    [SerializeField] Image target;

    // The RectTransform of the pointer
    RectTransform rectTransform;
    // The y position of the pointer
    float yPos;
    // The left-most and right-most positions of the pointer
    float minPos;
    float maxPos;
    // The speed of the pointer
    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //// Store the rect transform of the pointer
        rectTransform = GetComponent<RectTransform>();

        //// Store the range of the fish bar
        //range = rangeBar.GetComponent<RectTransform>().rect.width;

        //// Store the left-most position of the pointer
        //minPos = rectTransform.TransformPoint(rectTransform.position).x;


        //// Calculate the right-most position of the fish bar
        //maxPos = minPos + range;

        RectTransform rangeRectTransform = rangeBar.GetComponent<RectTransform>();

        range = rangeRectTransform.rect.width;

        minPos = rangeRectTransform.position.x - (range / 2);

        print(range + ", " + minPos + ", " + maxPos);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // do something
        
    }

    public void MovePointer(float position)
    {
        float newX = Mathf.MoveTowards(rectTransform.position.x, position * range + minPos, moveSpeed * Time.deltaTime);
        rectTransform.position = new Vector2(newX, rectTransform.position.y);
    }
    
}
