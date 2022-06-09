using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
///  Revela todos los objetos ocultos, coleccionables, puertas secretas, etc. de la run
/// </summary>
public class TheHierophant : TarotCard
{
    new void Awake()
    {
        cardName = CardName.TheHierophant;
        difficultyScore = 10;
        luckScore = 100;
        base.Awake();
    }

    #region Support Variables

    #endregion

    void OnEnable()
    {
        SceneManager.sceneLoaded += UpdateParticles;
    }

    void UpdateParticles(Scene scene, LoadSceneMode mode)
    {
        foreach (GameObject resource in GameObject.FindGameObjectsWithTag("Resource").ToList())
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/TheHierophantParticleIndicator"), transform.position, Quaternion.identity, gameObject.transform);
            obj.GetComponent<TheHierophantParticleSystem>().Target = resource;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= UpdateParticles;
    }
}
