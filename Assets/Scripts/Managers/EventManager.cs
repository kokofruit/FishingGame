using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    //These are the things we are going to listen to
    //Revised so that functions handling the event will accept an int
    private Dictionary<string, UnityAction> eventDictionary;

    //allows us to access the instance from other classes!
    private static EventManager eventManager;

    //using our C# setter and getter technique to setup our instance
    public static EventManager instance;
    
    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        // init dictionary
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityAction>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        if (instance.eventDictionary.ContainsKey(eventName))
        {
            instance.eventDictionary[eventName] += listener;
        }
        else
        {
            instance.eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (instance.eventDictionary.ContainsKey(eventName))
        {
            instance.eventDictionary[eventName] -= listener;
        }
    }

    public static void TriggerEvent(string eventName)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out UnityAction thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
