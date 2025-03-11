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
        // state machine
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
        // if the target has idled long enough, decide a new location to move to
        if (idleTimer <= 0f)
        {
            idleTimer = idleDuration;
            targetState = TargetStates.deciding;
        }
        // otherwise decrease timer
        else idleTimer -= Time.deltaTime;
    }

    void DoDecide()
    {
        // find half of the width to subtract from movement range
        float halfWidth = transform.localScale.x * 0.5f;
        // generate a random number to set to local X
        float xPos = Random.Range(0f + halfWidth, 1f - halfWidth);
        // center x position in rangebar
        xPos -= 0.5f;
        // set the destination to a vector2 using xpos
        destination = new Vector2(xPos, 0f);

        // change state
        targetState = TargetStates.moving;
    }

    void DoMove()
    {
        // if the target has arrived at the destination, return to idling
        if ((Vector2)transform.localPosition == destination)
        {
            targetState = TargetStates.idling;
        }
        // otherwise, move towards the destination
        else
        {
            Vector2 pos = Vector2.MoveTowards(transform.localPosition, destination, targetMoveSpeed * Time.deltaTime);
            transform.localPosition = pos;
        }
    }
}
