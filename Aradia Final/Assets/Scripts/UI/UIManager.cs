using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    static bool initialized = false;

    /// <summary>
    /// Gets whether or not the player has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    public static void Initialize()
    {
        initialized = true;
    }

    public static void ResetUI()
    {
        Debug.Log("UI DESTROYED");
        initialized = false;
    }
}
