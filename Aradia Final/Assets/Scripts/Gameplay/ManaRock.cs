using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRock : MonoBehaviour
{
    PlayerDamageIndex pDI;
    LifeSystem lifeSystem;

    void Start()
    {
        pDI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageIndex>();
    }

    void OnDestroy()
    {
        if (GetComponent<LifeSystem>().Dead) pDI.ConsumeMana(-PlayerDamageIndex.TotalMana);
    }
}
