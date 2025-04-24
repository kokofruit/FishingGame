using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelManager : MonoBehaviour
{
    public static ReelManager instance;

        // Serialized variables //
    // The maximum output turning speed
    [SerializeField] float maxTurnSpeed;

        // Private variables //
    // The main camera of the scene
    Camera cam;

    // Set the singleton instance
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Initialize variables
    private void Start()
    {
        cam = Camera.main;
    }

    // Call helper functions
    private void FixedUpdate()
    {
        // Rotate the reel to the mouse and calculate the rotation speed
        float speed = RotateReel();
        //float speed = MeasureSpeed();
        PointerManager.instance.MovePointer(speed / maxTurnSpeed);
    }

    // Rotate the reel towards the mouse and calculate the rotation speed
    float RotateReel()
    {
        // Find the mouse's position in the world
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the angle between the top of the reel and the mouse position
        float angle = Vector2.SignedAngle(transform.up, mousePos);

        // Rotate the reel
        transform.Rotate(new Vector3(0, 0, angle));

        // Return the rotation per second. Invert negative to account for counter/clockwise reel rotation.
        return -1f * angle / Time.deltaTime;
    }

}
