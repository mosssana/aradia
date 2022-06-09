using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ¿¿¿ El maná de Diana se restaura al completo ???
/// ¿¿¿ Aumenta el maná máximo de Diana ???
/// Diana obtiene Regeneración de Maná mientras esté en Forma Terrenal
/// </summary>
public class Judgement : TarotCard
{
    new void Awake()
    {
        cardName = CardName.Judgement;
        difficultyScore = 100;
        luckScore = 10;
        manaCost = -1;
        cooldown = 2;
        base.Awake();
    }

    int extraMana = 4;

    void Start()
    {
        PlayerDamageIndex.TotalMana += extraMana;
    }


    void Update()
    {
        if (Available) SkillFire();
    }

    // void OnDisable()
    // {
    //     PlayerDamageIndex.TotalMana -= extraMana;
    // }
}
