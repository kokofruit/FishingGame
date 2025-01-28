using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelManager : MonoBehaviour
{
    // DEBUG
    [SerializeField] bool DEBUG = false;

    // The transform of the group
    RectTransform mainRect;

    // The reel object and it's rigidbody2D
    [SerializeField] GameObject reel;
    Rigidbody2D reelRB;

    // The maximum turning speed
    [SerializeField] float turnSpeed;

    // The last rotation of the reel
    float lastRot = 0f;

    // Initialize variables
    void Start()
    {
        mainRect = GetComponent<RectTransform>();
        reelRB = reel.GetComponent<Rigidbody2D>();
    }
    
    // Call helper functions every frame
    void FixedUpdate()
    {
        RotateReel();
        MeasureSpeed();
    }

    void RotateReel()
    {
        // Calculate the angle to the mouse
        float angle = Vector2.SignedAngle(mainRect.up, mainRect.InverseTransformPoint(Input.mousePosition));

        // Rotate reel to mouse
        float rot = Mathf.MoveTowardsAngle(reelRB.rotation, angle, turnSpeed * Time.deltaTime);
        reelRB.rotation = rot;
    }

    float MeasureSpeed()
    {
        // Calculate the speed of rotation by the difference from the last frame
        float diff = -1f * (reelRB.rotation - lastRot);
        // Update the last-rotation variable
        lastRot = reelRB.rotation;

        // If the difference is positive, return that
        if (diff >= 0)
        {
            if (DEBUG) print("Rotation since last frame: " + diff);

            return diff;
        }
        // If not, return zero
        return 0f;
    }
}
