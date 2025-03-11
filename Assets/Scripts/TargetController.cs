using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetController : MonoBehaviour
{
    // Public
    // The singleton instance of the controller
    public static TargetController instance;

    // Private
    
    enum TargetStates
    {
        idling,
        deciding,
        moving
    }
    [SerializeField] TargetStates targetState = TargetStates.deciding;

    [SerializeField] float idleTimer;
    [SerializeField] float idleDuration;

    [SerializeField] Vector2 destination;

    [SerializeField] float targetMoveSpeed;

    // Set the instance
    void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        switch (targetState)
        {
            case TargetStates.idling:
                DoIdle();
                break;
            case TargetStates.deciding:
                DoDecide();
                break;
            case TargetStates.moving:
                DoMove();
                break;
            default:
                break;
        }
    }

    void DoIdle()
    {
        if (idleTimer >= idleDuration)
        {
            targetState = TargetStates.deciding;
            idleTimer = 0f;
            return;
        }
        idleTimer += Time.deltaTime;
    }

    void DoDecide()
    {
        float xPos = Random.Range(0f, 1f);
        print(xPos);
        xPos -= 0.5f;
        // TODO: allow for width 
        destination = new Vector2(xPos, 0f);

        targetState = TargetStates.moving;
    }

    void DoMove()
    {
        if ((Vector2)transform.localPosition == destination)
        {
            targetState = TargetStates.idling;
            return;
        }

        Vector2 pos = Vector2.MoveTowards(transform.localPosition, destination, targetMoveSpeed * Time.deltaTime);
        transform.localPosition = pos;
    }
}
