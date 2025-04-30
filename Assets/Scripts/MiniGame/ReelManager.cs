using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// manager for the reel of the minigame
public class ReelManager : MonoBehaviour
{
    // public instance
    public static ReelManager instance;

        // Serialized variables //
    // The maximum output turning speed
    [SerializeField] float maxTurnSpeed;

        // Private variables //
    // The main camera of the scene
    Camera cam;
    // the audio source that plays the reeling sound
    AudioSource audioSource;
    // whether the reeling is paused or not
    bool isPaused;

    //unity actions
    // will listen for the tutorial resuming in order to pause the reel
    UnityAction enterTutorialListener;
    // will listen for the tutorial exiting in order to resume the reel
    UnityAction exitTutorialListener;

    // Set the singleton instance
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        // cache
        cam = Camera.main;
        audioSource = GetComponent<AudioSource>();

        // unity actions
        enterTutorialListener = new UnityAction(PauseReel);
        exitTutorialListener = new UnityAction(ResumeReel);
    }

    // start listeners
    void OnEnable()
    {
        if (!GameManager.tutorialCompleted)
        {
            EventManager.StartListening("TutorialReel", enterTutorialListener);
            EventManager.StartListening("TutorialCompletion", enterTutorialListener);
            EventManager.StartListening("ExitTutorial", exitTutorialListener);
        }
    }

    // stop listeners
    void OnDisable()
    {
        if (!GameManager.tutorialCompleted)
        {
            EventManager.StopListening("TutorialReel", enterTutorialListener);
            EventManager.StopListening("TutorialCompletion", enterTutorialListener);
            EventManager.StopListening("ExitTutorial", exitTutorialListener);
        }
    }

    void PauseReel()
    {
        audioSource.volume = 0;
        isPaused = true;
    }

    void ResumeReel()
    {
        isPaused = false;
    }

    // Call helper functions
    private void FixedUpdate()
    {
        if (isPaused) return;

        // Rotate the reel to the mouse and calculate the rotation speed
        float speed = RotateReel();
        float turnSpeed = Mathf.Clamp01(speed / maxTurnSpeed);

        // adjust the reeling sound effect
        AdjustSound(turnSpeed);
        // send data to pointer
        PointerManager.instance.MovePointer(turnSpeed);
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

    // adjust sound based on reel's rotation speed
    void AdjustSound(float turnSpeed)
    {
        float volume = turnSpeed * 0.6f;
        float pitch = 1.3f * turnSpeed + 0.7f;

        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }

}
