using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInvoker : MonoBehaviour
{
    protected Dictionary<EventName, UnityEvent> unityEvents = new Dictionary<EventName, UnityEvent>();

    public void AddListener(EventName eventName, UnityAction listener)
    {
        if (unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(listener);
        }
    }
}
