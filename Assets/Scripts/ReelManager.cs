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

    // The fish bar manager
    FishBarManager fishBarManager;

    // Initialize variables
    void Start()
    {
        mainRect = GetComponent<RectTransform>();
        reelRB = reel.GetComponent<Rigidbody2D>();
        fishBarManager = FindFirstObjectByType<FishBarManager>();
    }
    
    // Call helper functions every frame
    void FixedUpdate()
    {
        RotateReel();
        float speed = MeasureSpeed();
        //fishBarManager.MoveFishBar(speed / turnSpeed);
        PointerController.instance.MovePointer(speed / turnSpeed);
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
        float speed = diff / Time.deltaTime;

        // Update the last-rotation variable
        lastRot = reelRB.rotation;

        // If the difference is positive, return that
        if (speed >= 0)
        {
            if (DEBUG) print("Rotation per frame: " + speed);

            return speed;
        }
        // If not, return zero
        return 0f;
    }
}
