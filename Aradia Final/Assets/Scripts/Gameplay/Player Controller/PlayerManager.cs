using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    static bool initialized = false;

    /// <summary>
    /// Gets whether or not the player has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }


    static string previousScene;
    public static string PreviousScene
    {
        get { return previousScene; }
        set { previousScene = value; }
    }

    public static void Initialize()
    {
        initialized = true;
    }

    public static void ResetPlayer()
    {
        Debug.Log("PLAYER DESTROYED AND RESET FOR GOOOD");
        initialized = false;
        Physics2D.IgnoreLayerCollision(8, 9, false); // enemies
        Physics2D.IgnoreLayerCollision(8, 11, false); // enemy attacks
        Physics2D.IgnoreLayerCollision(8, 15, false); //spikes
        TarotCardsHand.ClearHand();
        PlayerDamageIndex.Restart();
        StateControl.Reset();
    }
}
