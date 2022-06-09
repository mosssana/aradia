using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Diana obtiene Invulnerabilidad al primer golpe que reciba. Este efecto solo puede ocurrir una vez por sala y por Forma
/// </summary>
public class TheFool : TarotCard
{
    new void Awake()
    {
        cardName = CardName.TheFool;
        difficultyScore = 50;
        luckScore = 50;
        base.Awake();
    }

    #region Support Variables
    LifeSystem lifeSystem;

    bool used = true;

    float originalArmorValue;

    Object shieldPrefab;
    GameObject shield;

    #endregion

    public bool Used
    {
        get { return used; }
    }

    void Start()
    {
        lifeSystem = GetComponent<LifeSystem>();
        shieldPrefab = Resources.Load("Prefabs/TheFoolShield");

        EventManager.AddListener(EventName.PlayerAttackedForShields, DestroyShield);
        EventManager.AddListener(EventName.RoomChanged, Reset);

        Reset();
    }

    void DestroyShield(float damage, float life)
    {
        // if not used and first shield on the list
        if (!used)
        {
            used = true;
            if (TarotCardsHand.GetCard(CardName.TheSun) != null)
            {
                if (!GetComponent<TheSun>().ShieldActive) lifeSystem.Invulnerable = false;
            }
            else lifeSystem.Invulnerable = false;
            AudioManager.StopLoop(AudioClipName.theFool);
            Destroy(shield);

        }
    }

    void Reset()
    {
        if (used)
        {
            used = false;
            lifeSystem.Invulnerable = true;
            shield = Instantiate((GameObject)shieldPrefab, gameObject.transform);
            AudioManager.PlayLoop(AudioClipName.theFool);
        }
    }

    // void OnDisable()
    // {
    //     AudioManager.StopLoop(AudioClipName.theFool);
    //     if (lifeSystem != null) lifeSystem.Invulnerable = false;
    //     Destroy(shield);
    // }
}
