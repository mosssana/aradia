using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatFloatEventInvoker : MonoBehaviour
{
    protected Dictionary<EventName, UnityEvent<float,float>> unityEvents = new Dictionary<EventName, UnityEvent<float,float>>();

    public void AddListener(EventName eventName, UnityAction<float,float> listener)
    {
        if (unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(listener);
        }
    }
}
