using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region Fields
    static bool initialized = false;

    /// <summary>
    /// Gets whether or not the player has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    static Dictionary<EventName, List<EventInvoker>> eventInvokers = new Dictionary<EventName, List<EventInvoker>>();
    static Dictionary<EventName, List<UnityAction>> eventListeners = new Dictionary<EventName, List<UnityAction>>();


    static Dictionary<EventName, List<FloatEventInvoker>> floatEventInvokers = new Dictionary<EventName, List<FloatEventInvoker>>();
    static Dictionary<EventName, List<UnityAction<float>>> floatEventListeners = new Dictionary<EventName, List<UnityAction<float>>>();

    static Dictionary<EventName, List<FloatFloatEventInvoker>> floatFloatEventInvokers = new Dictionary<EventName, List<FloatFloatEventInvoker>>();
    static Dictionary<EventName, List<UnityAction<float, float>>> floatFloatEventListeners = new Dictionary<EventName, List<UnityAction<float, float>>>();

//    static Dictionary<EventName, List<BoolEventInvoker>> boolEventInvokers = new Dictionary<EventName, List<BoolEventInvoker>>();
//     static Dictionary<EventName, List<UnityAction<bool>>> boolEventListeners = new Dictionary<EventName, List<UnityAction<bool>>>();


    #endregion

    #region Public methods

    /// Initializes the event manager
    public static void Initialize()
    {
        // create empty lists for all the dictionary entries
        foreach (EventName name in Enum.GetValues(typeof(EventName)))
        {
            if (!eventInvokers.ContainsKey(name))
            {
                eventInvokers.Add(name, new List<EventInvoker>());
                eventListeners.Add(name, new List<UnityAction>());
            }
            else
            {
                eventInvokers[name].Clear();
                eventListeners[name].Clear();
            }



            if (!floatEventInvokers.ContainsKey(name))
            {
                floatEventInvokers.Add(name, new List<FloatEventInvoker>());
                floatEventListeners.Add(name, new List<UnityAction<float>>());
            }
            else
            {
                floatEventInvokers[name].Clear();
                floatEventListeners[name].Clear();
            }



            if (!floatFloatEventInvokers.ContainsKey(name))
            {
                floatFloatEventInvokers.Add(name, new List<FloatFloatEventInvoker>());
                floatFloatEventListeners.Add(name, new List<UnityAction<float, float>>());
            }
            else
            {
                floatFloatEventInvokers[name].Clear();
                floatFloatEventListeners[name].Clear();
            }



            // if (!intIntEventInvokers.ContainsKey(name))
            // {
            //     intIntEventInvokers.Add(name, new List<IntIntEventInvoker>());
            //     intIntEventListeners.Add(name, new List<UnityAction<int, int>>());
            // }
            // else
            // {
            //     intIntEventInvokers[name].Clear();
            //     intIntEventListeners[name].Clear();
            // }
        }

        initialized = true;
    }

    // public static void Reset()
    // {
    //     eventInvokers.Clear();
    //     eventListeners.Clear();
    //     floatEventInvokers.Clear();
    //     floatEventListeners.Clear();
    //     floatFloatEventInvokers.Clear();
    //     floatFloatEventListeners.Clear();
    // }

    public static void AddInvoker(EventName eventName, EventInvoker invoker)
    {
        // add listeners to new invoker and add new invoker to dictonary
        eventInvokers[eventName].Add(invoker);
        foreach (UnityAction listener in eventListeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
    }

    public static void AddInvoker(EventName eventName, FloatEventInvoker invoker)
    {
        // add listeners to new invoker and add new invoker to dictonary
        floatEventInvokers[eventName].Add(invoker);
        foreach (UnityAction<float> listener in floatEventListeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
    }

    public static void AddInvoker(EventName eventName, FloatFloatEventInvoker invoker)
    {
        // add listeners to new invoker and add new invoker to dictonary
        floatFloatEventInvokers[eventName].Add(invoker);
        foreach (UnityAction<float, float> listener in floatFloatEventListeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
    }


    // public static void AddInvoker(EventName eventName, IntIntEventInvoker invoker)
    // {
    //     // add listeners to new invoker and add new invoker to dictonary
    //     intIntEventInvokers[eventName].Add(invoker);
    //     foreach (UnityAction<int, int> listener in intIntEventListeners[eventName])
    //     {
    //         invoker.AddListener(eventName, listener);
    //     }
    // }




    public static void RemoveInvoker(EventName eventName, EventInvoker invoker)
    {
        // remove invoker from dictionary
        eventInvokers[eventName].Remove(invoker);
    }

    public static void RemoveInvoker(EventName eventName, FloatEventInvoker invoker)
    {
        // remove invoker from dictionary
        floatEventInvokers[eventName].Remove(invoker);
    }

    public static void RemoveInvoker(EventName eventName, FloatFloatEventInvoker invoker)
    {
        // remove invoker from dictionary
        floatFloatEventInvokers[eventName].Remove(invoker);
    }

    //  public static void RemoveInvoker(EventName eventName, IntIntEventInvoker invoker)
    // {
    //     // remove invoker from dictionary
    //     intIntEventInvokers[eventName].Remove(invoker);
    // }





    public static void AddListener(EventName eventName, UnityAction listener)
    {
        eventListeners[eventName].Add(listener);
        foreach (EventInvoker invoker in eventInvokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }

    }

    public static void AddListener(EventName eventName, UnityAction<float> listener)
    {
        floatEventListeners[eventName].Add(listener);
        foreach (FloatEventInvoker invoker in floatEventInvokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }

    }

    public static void AddListener(EventName eventName, UnityAction<float, float> listener)
    {
        floatFloatEventListeners[eventName].Add(listener);
        foreach (FloatFloatEventInvoker invoker in floatFloatEventInvokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }

    }

    // public static void AddListener(EventName eventName, UnityAction<int, int> listener)
    // {
    //     intIntEventListeners[eventName].Add(listener);
    //     foreach (IntIntEventInvoker invoker in intIntEventInvokers[eventName])
    //     {
    //         invoker.AddListener(eventName, listener);
    //     }

    // }


    #endregion
}
