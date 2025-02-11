using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    // Serialized variables
    // DEBUG
    [SerializeField] bool DEBUG = false;
    // The maximum turning speed
    [SerializeField] float turnSpeed;

    // Private variables
    // The main camera of the scene
    Camera cam;
    // The last rotation of the reel
    float lastRot = 0f;
    // The rigidbody2d of the reel
    Rigidbody2D rb;
    // The fish bar manager
    FishBarManager fishBarManager;


    // Initialize variables
    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        fishBarManager = FindFirstObjectByType<FishBarManager>();
    }

    // Call helper functions
    private void FixedUpdate()
    {
        float speed = RotateReel();
        //float speed = MeasureSpeed();
        PointerController.instance.MovePointer( speed / turnSpeed);
    }

    float RotateReel()
    {
        // Find the mouse's position in the world
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the angle between the top of the reel and the mouse position
        //float angle = Vector2.SignedAngle(Vector2.up, mousePos);
        //float rot = Mathf.MoveTowardsAngle(rb.rotation, angle, turnSpeed * Time.deltaTime);

        float angle = Vector2.SignedAngle(transform.up, mousePos);
        //float rot = Mathf.Clamp(angle, -1f * turnSpeed * Time.deltaTime, 0f);

        // Rotate the reel
        transform.Rotate(new Vector3(0, 0, angle));
        //transform.Rotate(new Vector3(0,0,rot));
        //rb.rotation = rot;

        return -1f * angle;
    }

    void MeasureSpeed()
    {
        // Find the mouse's position in the world
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Vector2.SignedAngle(transform.up, mousePos);

    }
}
