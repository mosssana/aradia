using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The names of the events in the game
/// </summary>
public enum EventName
{
    PlayerAttacked,
    PlayerLifeChanged,
    PlayerManaChanged,
    BasicAttackPerformed,
    NotEnoughMana,
    RoomChanged,
    QuitGameplayEvent,
    PlayerAttackedForShields
}
