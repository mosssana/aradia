using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// TheDevil:
/// Diana puede sacrificar una cantidad de vida a cambio de subir sus estadísticas.
/// Todos los efectos de esta carta se restablecerán al salir de la sala, pero Diana no se curará la cantidad de vida sacrificada.
/// Esta carta no se puede utilizar en Forma Arconte
/// </summary>
public class TheDevil : TarotCard
{
    new void Awake()
    {
        cardName = CardName.TheDevil;
        difficultyScore = 10;
        luckScore = 80;
        cooldown = 10;
        manaCost = 0;
        base.Awake();
    }

    #region Card Stats Variables
    /// Amount of life to substract from player
    float lifeToSacrifice = 40;
    // float lifeToSacrifice = 2000;

    /// Amount added to player's armorMultiplier
    float armorMultiplier = 0.3f;

    /// Amount added to player's baseDamageMultiplier
    float baseDamageMultiplier = 0.3f;

    /// Amount added to player's skillDamageMultiplier
    float skillDamageMultiplier = 0.3f;


    #endregion

    #region Support Variables
    LifeSystem playerLifeSystem;

    int timesPerformed = 0;

    #endregion

    void Start()
    {
        playerLifeSystem = GetComponent<LifeSystem>();
    }
    
    override public void SkillFire()
    {
        base.SkillFire();

        AudioManager.PlayUnique(AudioClipName.theDevil);
        playerLifeSystem.DealDamage(lifeToSacrifice, true);
        playerLifeSystem.ArmorMultiplierModify(armorMultiplier);
        PlayerDamageIndex.BaseDamageMultiplierModify(baseDamageMultiplier);
        PlayerDamageIndex.SkillDamageMultiplierModify(skillDamageMultiplier);
        timesPerformed++;
    }
}
