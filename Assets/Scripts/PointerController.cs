using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class PointerController : MonoBehaviour
{
    public static PointerController instance;
    [SerializeField] float pointerMoveSpeed;

    void Awake()
    {
        instance = this;
    }

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("hitting! slaying! serving cunt!");
        } 
    }
}
