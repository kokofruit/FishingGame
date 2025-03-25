using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetManager : MonoBehaviour
{
    // Public
    // The singleton instance of the controller
    public static TargetManager instance;

    // Private
    
    enum TargetStates
    {
        idling,
        deciding,
        moving,
        nothing,
    }
    [SerializeField] TargetStates targetState = TargetStates.deciding;

    [SerializeField] float idleTimer;
    [SerializeField] float idleDuration;

    Vector2 destination;

    [SerializeField] float targetMoveSpeed;

    private int bugDifficulty;

    // Set the singleton instance
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);
        targetState = TargetStates.deciding;
    }

    void OnDisable()
    {
        targetState = TargetStates.nothing;
    }

    public void SetDifficulty(int difficulty)
    {
        bugDifficulty = difficulty;
        targetMoveSpeed = 0.125f * difficulty + 0.25f;
        idleTimer = idleDuration = -0.175f * difficulty + 3f;
        float localX = -0.015f * difficulty + 0.3f;
        transform.localScale = new Vector3(localX, transform.localScale.y, transform.localScale.z);
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

    bool GetInstantMove()
    {
        // lower level bugs cannot instantly move
        if (bugDifficulty < 5) return false;

        // use a random number to decide
        // lvl 5: 10% chance
        // lvl 10: 30% chance
        float randomChance = Random.Range(0f, 100f);
        float decidingFactor = 4 * bugDifficulty - 10;
        return (randomChance <= bugDifficulty);
    }

    void DoIdle()
    {
        // if the target has idled long enough, decide a new location to move to
        if (idleTimer <= 0f)
        {
            idleTimer = GetInstantMove() ? 0f : (idleDuration * Random.Range(0.5f, 1.5f));
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
