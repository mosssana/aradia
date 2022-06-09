using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A LifeSystem gives LifePoints to a Game Object and the ability to deduct them 
/// It takes into account Armor and Multipliers in respect to a Base Damage provided by the offender
/// </summary>
public class LifeSystem : FloatFloatEventInvoker
{
    [SerializeField] GameObject onAttackedSFX;
    [SerializeField] GameObject onDeathSFX;
    [SerializeField] AudioClipName onAttackedAudio;
    [SerializeField] AudioClipName onDeathAudio;

    /// Life points of this LifeSystem
    [SerializeField] float totalLife;

    float life;

    /// Armor points of this LifeSystem
    /// Acts as a a percentage, meaning 10 armor reduces damage received by 10%
    [Range(0, 100)][SerializeField] float armor;

    /// Multiplies the effect of Armor points
    [SerializeField] float armorMultiplier = 1;

    bool invulnerable = false;
    Timer attackedInvulnerableTimer;
    float attackedInvulnerableTimerDuration = 2;

    bool player; // true if life system parent is player
    bool dead = false;
    bool ignoringCollisions = false;

    Timer spriteColorTimer;
    float spriteColorTimerDuration = 0.25f;
    SpriteRenderer spriteRenderer;
    Color attacked = new Color(1, 1, 1, 0.4f);
    Color normal = new Color(1, 1, 1);

    public bool Invulnerable
    {
        get { return invulnerable; }
        set { invulnerable = value; }

    }

    public bool Dead
    {
        get { return dead; }
    }

    public float TotalLife
    {
        get { return totalLife; }
    }

    /// <summary>
    /// Gets Life points of this LifeSystem
    /// </summary>
    public float CurrentLife
    {
        get { return life; }
    }

    /// <summary>
    /// Gets and Sets Armor of this LifeSystem
    /// </summary>
    public float Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    void Awake()
    {
        if (gameObject.tag == "Player") player = true;
        else player = false;
    }

    void Start()
    {
        life = totalLife;
        unityEvents.Add(EventName.PlayerAttacked, new PlayerAttackedEvent());
        EventManager.AddInvoker(EventName.PlayerAttacked, this);

        unityEvents.Add(EventName.PlayerLifeChanged, new PlayerLifeChangedEvent());
        EventManager.AddInvoker(EventName.PlayerLifeChanged, this);

        unityEvents.Add(EventName.PlayerAttackedForShields, new PlayerLifeChangedEvent());
        EventManager.AddInvoker(EventName.PlayerAttackedForShields, this);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteColorTimer = gameObject.AddComponent<Timer>();
        spriteColorTimer.Duration = spriteColorTimerDuration;

        attackedInvulnerableTimer = gameObject.AddComponent<Timer>();
        attackedInvulnerableTimer.Duration = attackedInvulnerableTimerDuration;
    }

    /// <summary>
    /// Deducts life points to this LifeSystem
    /// </summary>
    public float DealDamage(float baseDamage, bool ignoreShields)
    {
        return DealDamage(baseDamage, 1, 1, 1, ignoreShields);
    }

    /// <summary>
    /// Deducts life points to this LifeSystem
    /// </summary>
    public float DealDamage(float baseDamage)
    {
        return DealDamage(baseDamage, 1, 1, 1, false);
    }

    /// <summary>
    /// Deducts life points to this LifeSystem, simpler method with less paramaters for simple situations
    /// </summary>
    public float DealDamage(float baseDamage, float skillDamage)
    {
        return DealDamage(baseDamage, skillDamage, 1, 1, false);
    }

    public float DealDamage(float baseDamage, float skillDamage, float baseDamageMultiplier, float skillDamageMultiplier)
    {
        return DealDamage(baseDamage, skillDamage, baseDamageMultiplier, skillDamageMultiplier, false);
    }

    /// <summary>
    /// Deducts life points to this LifeSystem, complete method with all parameters
    /// </summary>
    public float DealDamage(float baseDamage, float skillDamage, float baseDamageMultiplier, float skillDamageMultiplier, bool ignoreShields)
    {
        float damage = CalculateDamage(baseDamage, skillDamage, baseDamageMultiplier, baseDamageMultiplier);

        if (onAttackedAudio != AudioClipName.none) AudioManager.Play(onAttackedAudio);

        if (!invulnerable && !attackedInvulnerableTimer.Running || ignoreShields)
        {
            life -= damage;
            if (life <= 0) Die();
        }

        if (player)
        {
            unityEvents[EventName.PlayerAttacked].Invoke(damage, life);
            unityEvents[EventName.PlayerLifeChanged].Invoke(life, totalLife);
            if (!ignoreShields) unityEvents[EventName.PlayerAttackedForShields].Invoke(damage, life);

            if (!attackedInvulnerableTimer.Running)
            {
                if (!dead)
                {
                    Physics2D.IgnoreLayerCollision(8, 9, true); // enemies
                    Physics2D.IgnoreLayerCollision(8, 11, true); // enemy attacks
                    Physics2D.IgnoreLayerCollision(8, 15, true); //spikes
                    ignoringCollisions = true;
                    spriteRenderer.color = attacked;
                    onAttackedSFX.SetActive(true);
                    attackedInvulnerableTimer.Run();
                }
            }
        }
        else
        {
            spriteRenderer.color = attacked;
            if (onAttackedSFX != null) onAttackedSFX.SetActive(true);
            spriteColorTimer.Run();
        }
        return damage;
    }

    public void Heal(float amount)
    {
        if ((life + amount) <= totalLife) life += amount;
        else life = totalLife;

        if (player)
        {
            unityEvents[EventName.PlayerLifeChanged].Invoke(life, totalLife);
        }
    }

    /// <summary>
    /// Calculates the damage to receive taking into account:
    /// (A) BaseDamage, SkillDamage and Multipliers of the offender;
    /// (B) Armor and Multipliers of the receiver (aka this LifeSystem)
    /// </summary>
    float CalculateDamage(float baseDamage, float skillDamage, float baseDamageMultiplier, float skillDamageMultiplier)
    {
        float calculatedDamage;
        float damageToDeal = (skillDamage * skillDamageMultiplier) * (baseDamage * baseDamageMultiplier);
        float damageToNeglect = damageToDeal * (armor / 100) * armorMultiplier;
        if ((damageToDeal > damageToNeglect)) calculatedDamage = (damageToDeal - damageToNeglect);
        else calculatedDamage = 0;


        return calculatedDamage;
    }

    /// <summary>
    /// Adds (or substracts in case of receiving a negative argument) to armorMiltiplier
    /// </summary>
    public void ArmorMultiplierModify(float modifier)
    {
        armorMultiplier += modifier;
    }

    /// <summary>
    /// What takes place when object dies
    /// </summary>
    void Die()
    {
        if (onDeathAudio != AudioClipName.none) AudioManager.Play(onDeathAudio);
        if (onDeathSFX != null)
        {
            GameObject sfx = Instantiate(onDeathSFX, transform.position, Quaternion.identity);
            sfx.SetActive(true);
        }
        if (player) PlayerManager.ResetPlayer();
        dead = true;
        EventManager.RemoveInvoker(EventName.PlayerAttacked, this);
        EventManager.RemoveInvoker(EventName.PlayerLifeChanged, this);
        Destroy(gameObject);
    }

    void Update()
    {
        if (spriteColorTimer.Finished || attackedInvulnerableTimer.Finished)
        {
            spriteRenderer.color = normal;
            if (onAttackedSFX != null) onAttackedSFX.SetActive(false);
            if (player && ignoringCollisions)
            {
                Physics2D.IgnoreLayerCollision(8, 9, false); // enemies
                Physics2D.IgnoreLayerCollision(8, 11, false); // enemy attacks
                Physics2D.IgnoreLayerCollision(8, 15, false); //spikes
                ignoringCollisions = false;
            }
        }
    }

    public void IncreaseTotalLife(float amountToIncrease, bool healAfter)
    {
        totalLife += amountToIncrease;
        if (healAfter) life += totalLife;
    }
}
