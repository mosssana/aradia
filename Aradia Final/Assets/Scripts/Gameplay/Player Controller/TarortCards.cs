using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 
/// </summary>
public class TarotCard : MonoBehaviour
{
    #region Fields to Modify by child classes
    protected CardName cardName;
    protected CardType type;

    /// <summary>
    /// Influences the difficulty of the level that is going to be played, the number and difficulty of enemies...
    /// </summary>
    protected float difficultyScore;

    /// <summary>
    /// Influences the length of the level, the amount of resources received during a level gameplay...
    /// </summary>
    protected float luckScore;

    protected float cooldown = 0;

    protected int manaCost;

    protected bool canMove = true;

    #endregion

    #region Support Fields

    Timer cooldownTimer;
    bool performing;
    protected bool fired;
    PlayerDamageIndex pDI;
    Animator animator;

    Move moveScript;

    #endregion

    #region Properties

    public CardName Name
    {
        get { return cardName; }
    }

    public CardType Type
    {
        get { return type; }
    }

    public float DifficultyScore
    {
        get { return difficultyScore; }
    }

    public float LuckScore
    {
        get { return luckScore; }
    }

    public bool Performing
    {
        get { return performing; }
    }

    protected bool Available
    {
        get { return (!cooldownTimer.Running && pDI.CheckMana(manaCost)); }
    }

    // returns true if skill is on cooldown
    public bool OnCooldown
    {
        get { return cooldownTimer.Running; }
    }

    // returns cooldown left
    public float Cooldown
    {
        get { return cooldownTimer.Remaining; }
    }

    // returns true if player has enough mana to cast skill
    public bool EnoughMana
    {
        get { return pDI.CheckMana(manaCost); }
    }

    protected PlayerDamageIndex PDI
    {
        get { return pDI; }
    }

    #endregion

    /// <summary>
    /// Adds itself to the tarotCardsHand List
    /// </summary>
    public void AddCardToHand()
    {
        TarotCardsHand.TarotCardsHandList.Add(this);
    }

    /// <summary>
    /// ALWAYS add "base.Awake();" at the end of the method
    /// </summary>
    protected void Awake()
    {
        type = TarotReading.CardNameTypeEquivalency[cardName];
        pDI = GetComponent<PlayerDamageIndex>();
        cooldownTimer = gameObject.AddComponent<Timer>();
        cooldownTimer.Duration = cooldown;
        animator = GetComponent<Animator>();
        if (!canMove) moveScript = GetComponent<Move>();

        // EventManager.AddListener(EventName.PlayerManaChanged, UpdateBar);
    }

    /// <sumamry>
    /// Called by TarotCardsHand when player presses key to use the skill.
    /// Checks for mana and cooldown and makes the skill available for use or fires events for "unavailable" respectively.
    /// It also updates animation state variable
    /// </summary>    
    public virtual void SkillPerform(InputAction.CallbackContext ctx)
    {
        if (!PauseMenuManager.IsPaused)
        {
            if (ctx.started)
            {
                if (Available)
                {
                    if (!canMove) moveScript.CanMove = false;
                    UpdateAnimation(performing = true);
                    if (fired) fired = false;
                    Debug.Log(cardName + " performed");
                }
                else if (cooldownTimer.Running)
                {
                    AudioManager.Play(AudioClipName.skillUnavailable);
                    //fire SKILL NOT AVAILABLE EVENT!!!!!!!!!!!!!
                    Debug.Log(cardName + " is unavailable as cooldown hasn't refreshed yet");
                }
                else if (!pDI.CheckMana(manaCost))
                {
                    AudioManager.Play(AudioClipName.skillUnavailable);
                    //fire SKILL NOT AVAILABLE EVENT!!!!!!!!!!!!!
                    // do the same for SpecialAttack
                    Debug.Log(cardName + " is unavailable as you don't have enough mana");
                }
            }
            else if (ctx.canceled)
            {
                if (!canMove) moveScript.CanMove = true;
                if (type != CardType.Active) UpdateAnimation(performing = false);
            }
        }
    }

    /// called by active skills when animation is over
    public void SkillPerformCancel()
    {
        UpdateAnimation(performing = false);
    }


    /// <sumamry>
    /// Called by TarotCardsHand when the skill animation has reached the "fire frame".
    /// A skill has a particular animation, with different casting times, so each aniamtion has an event to "fire" the actual skill effects.
    /// Consumes the mana and starts cooldown timer.
    /// </summary>  
    public virtual void SkillFire()
    {
        pDI.ConsumeMana(manaCost);
        cooldownTimer.Run();
        if (!canMove) moveScript.CanMove = true;
        if (type != CardType.Active) UpdateAnimation(performing = false);
        fired = true;
        Debug.Log(cardName + " fired");
    }

    ///
    /// Updates animation state variable of corresponding skill
    ///
    void UpdateAnimation(bool state)
    {
        if (type != CardType.Passive) animator.SetBool(cardName.ToString(), state);
    }
}


/// <summary>
/// 
/// </summary>
public enum CardName
{
    TheFool,
    TheMagician,
    TheHierophant,
    Judgement,
    TheDevil,
    TheEmperor,
    TheEmpress,
    TheSun,
    WheelOfFortune
}

/// <summary>
/// 
/// </summary>
public enum CardType
{
    Passive,
    Active,
    Ultimate,
    Other
}