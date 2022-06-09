using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntIntEventInvoker : MonoBehaviour
{
    protected Dictionary<EventName, UnityEvent<int, int>> unityEvents = new Dictionary<EventName, UnityEvent<int, int>>();

    public void AddListener(EventName eventName, UnityAction<int, int> listener)
    {
        if (unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(listener);
        }
    }
}
