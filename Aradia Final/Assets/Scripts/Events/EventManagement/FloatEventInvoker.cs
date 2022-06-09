using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatEventInvoker : MonoBehaviour
{
    protected Dictionary<EventName, UnityEvent<float>> unityEvents = new Dictionary<EventName, UnityEvent<float>>();

    public void AddListener(EventName eventName, UnityAction<float> listener)
    {
        if (unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(listener);
        }
    }
}
