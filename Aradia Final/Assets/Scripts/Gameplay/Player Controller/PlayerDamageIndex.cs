using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerDamageIndex : FloatFloatEventInvoker
{
    /// <summary>
    /// GearScore is a number the purpose of which is balance out gameplay levels in accordance to the Player's Power
    /// This makes replayability much muro enticing and interesting, letting you replay levels with new gear but without overpowering the enemies
    /// It takes into account 
    ///     (A) current Tarot Cards' combined score, which is determined by how much [RESOURCE] is invested into them
    ///     (B) baseDamage and multipliers, which is also affected by [RESOURCE]
    /// Each gameplay level has a "minimum" PlayerScore
    ///     - Before the player reaches the score, enemy stats remain untouched
    ///     - After the player reaches the score, enemy stats begin to scale accordingly
    /// </summary>
    public int PlayerGearScore()
    {
        // right now this system is not in effect, it is a work in progress/future project
        // How it would work on a practical case:
        // Say that BY DESIGN the player is expected to deal around 10 points of Damage by using their Basic Attack on a Regular Enemy in this particular Gameplay Level
        // We could establish a threshold and say that once the player deals 50% more Damage than the one expected (15 points in this case) the Enemies' Stats would scale accordingly
        // Expected damage of 10 DamagePoints to enemy of 100 LifePoints; once damage is 15, scaling begins:  15DP to 110LP, 16to120, 17to130, 18to140, 19to150, 20 DP to 150 LP
        return 1;
    }

    #region  Player Stats
    static float baseDamage = 10;
    static float baseDamageMultiplier = 1;
    static float skillDamageMultiplier = 1;

    static int baseMana = 8;
    static int totalMana = 8;
    static int currentMana = totalMana;

    #endregion

    void Start()
    {
        unityEvents.Add(EventName.NotEnoughMana, new NotEnoughManaEvent());
        EventManager.AddInvoker(EventName.NotEnoughMana, this);

        unityEvents.Add(EventName.PlayerManaChanged, new NotEnoughManaEvent());
        EventManager.AddInvoker(EventName.PlayerManaChanged, this);
    }

    void OnDisable()
    {
        EventManager.RemoveInvoker(EventName.NotEnoughMana, this);
        EventManager.RemoveInvoker(EventName.PlayerManaChanged, this);
    }

    #region Properties

    public static int TotalMana
    {
        get { return totalMana; }
        set { totalMana = value; }
    }

    public static float CurrentMana
    {
        get { return currentMana; }
    }

    #endregion

    /// <summary>
    /// Shorter version of full method. Calls target's LifeSystem's Deal Damage Method
    /// </summary>
    static public float DealDamage(GameObject targetGameObject, float damage)
    {
        return DealDamage(targetGameObject, damage, 1);
    }

    /// <summary>
    /// Calls target's LifeSystem's Deal Damage Method taking into account various other variables
    /// </summary>
    static public float DealDamage(GameObject targetGameObject, float damage, int numberOfPorjectiles)
    {
        LifeSystem targetLifeSystem;
        if ((targetLifeSystem = targetGameObject.GetComponent<LifeSystem>()) != null)
        {
            return targetLifeSystem.DealDamage(baseDamage, damage / numberOfPorjectiles, baseDamageMultiplier, skillDamageMultiplier);
        }
        return 0;
    }

    public void ConsumeMana(int manaCost)
    {
        if (manaCost > 0 || (manaCost < 0 && (currentMana - manaCost) <= totalMana))
        {
            currentMana -= manaCost;
            unityEvents[EventName.PlayerManaChanged].Invoke(currentMana, totalMana);
            Debug.Log("current mana: " + currentMana + " || total mana: " + totalMana);
        }
        else if (manaCost < 0)
        {
            currentMana = totalMana;unityEvents[EventName.PlayerManaChanged].Invoke(currentMana, totalMana);
            Debug.Log("current mana: " + currentMana + " || total mana: " + totalMana);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool CheckMana(int manaCost)
    {
        if (manaCost > 0)
        {
            if ((currentMana - (manaCost)) >= 0)
            {
                return true;
            }
            // FIRE NOT ENOUGH MANA EVENT!!!!!!!!!!!!!
            unityEvents[EventName.NotEnoughMana].Invoke(manaCost, currentMana);
            return false;
        }
        return true; ;
    }

    /// <summary>
    /// Adds (or substracts in case of receiving a negative argument) to baseDamageMultiplier
    /// </summary>
    public static void BaseDamageMultiplierModify(float modifier)
    {
        baseDamageMultiplier += modifier;
    }

    /// <summary>
    /// Adds (or substracts in case of receiving a negative argument) to skillDamageMultiplier
    /// </summary>
    public static void SkillDamageMultiplierModify(float modifier)
    {
        skillDamageMultiplier += modifier;
    }

    public static void Restart()
    {
        totalMana = baseMana;
        currentMana = totalMana;
        baseDamageMultiplier = 1;
        skillDamageMultiplier = 1;
    }
}
