using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    private Dictionary <string, UnityEvent<EventData>> eventDictionary;

    private static EventManager eventManager;

    private static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init (); 
                }
            }

            return eventManager;
        }
    }

    void Init ()
    {
        eventDictionary ??= new Dictionary<string, UnityEvent<EventData>>();
    }

    public static void StartListening (string eventName, UnityAction<EventData> listener)
    {
        if (instance.eventDictionary.TryGetValue (eventName, out var thisEvent))
        {
            thisEvent.AddListener (listener);
        } 
        else
        {
            thisEvent = new UnityEvent<EventData> ();
            thisEvent.AddListener (listener);
            instance.eventDictionary.Add (eventName, thisEvent);
        }
    }

    public static void StopListening (string eventName, UnityAction<EventData> listener)
    {
        if (eventManager == null) return;
        if (instance.eventDictionary.TryGetValue (eventName, out var thisEvent))
        {
            thisEvent.RemoveListener (listener);
        }
    }

    public static void TriggerEvent (string eventName, EventData eventData)
    {
        if (instance.eventDictionary.TryGetValue (eventName, out var thisEvent))
        {
            thisEvent.Invoke(eventData);
        }
    }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
}
