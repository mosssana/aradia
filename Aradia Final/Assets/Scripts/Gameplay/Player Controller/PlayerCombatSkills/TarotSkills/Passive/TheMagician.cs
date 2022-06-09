using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Magician: 
/// Forma Terrenal: ataques básicos persiguen a los enemigos cercanos.
/// Forma Arconte: Aumenta la velocidad de ataque de Diana.
/// </summary>
public class TheMagician : TarotCard
{
    new void Awake()
    {
        cardName = CardName.TheMagician;
        difficultyScore = 150;
        luckScore = 0;
        base.Awake();
    }

    #region Support Variables

    BasicAttack basicAttackScript;

    SearchRadius searchRadius;

    GameObject basicAttackMissileOriginal;

    GameObject basicAttackMissileModified;

    #endregion

    void Start()
    {
        basicAttackScript = GetComponent<BasicAttack>();
        basicAttackMissileOriginal = basicAttackScript.BasicAttackMissile;
        basicAttackMissileModified = (GameObject)Resources.Load("Prefabs/BasicAttackMissileTheMagician");
        searchRadius = gameObject.GetComponentInChildren<SearchRadius>();

        // PlayerDamageIndex.SkillDamageMultiplierModify(skillDamageMultiplier);
        EventManager.AddListener(EventName.BasicAttackPerformed, CheckToChangeMissile);
    }

    // checks if there's an enemy nearby and changes missile projectile accordingly when BasicAttackPerformed event is invoked
    void CheckToChangeMissile()
    {
        if (searchRadius.EnemySpotted) basicAttackScript.BasicAttackMissile = basicAttackMissileModified;
        else basicAttackScript.BasicAttackMissile = basicAttackMissileOriginal;
    }
}
