using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class PointerController : MonoBehaviour
{
    // Public
    // The singleton instance of the controller
    public static PointerController instance;
    
    // Private
    // The moving speed of the pointer
    [SerializeField] float pointerMoveSpeed;

    // Set the instance
    void Awake()
    {
        instance = this;
    }

    // Move the pointer towards a position
    public void MovePointer(float position)
    {
        // Clamp the position
        //float cPosition = Mathf.Clamp(position, 0, maxTurnSpeed);
        float cPosition = Mathf.Clamp01(position);
        // Find the placement along the range bar
        float rPosition = cPosition - 0.5f;
        // Limit the movement by pointer speed 
        float xPosition = Mathf.MoveTowards(transform.localPosition.x, rPosition, pointerMoveSpeed * Time.deltaTime);
        // Set the pointer's position
        transform.localPosition = new Vector2(xPosition, 0);
    }

    // If the pointer is in the target, set the progress bar to increase
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Target"))
        {
            print("hitting! slaying! serving cunt!");
            CompletionController.instance.SetGaining(true);
        } 
    }

    // If the pointer is in the target, set the progress bar to decrease
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Target"))
        {
            print("losing! flopping! giving nothing!");
            CompletionController.instance.SetGaining(false);
        }
    }
}
