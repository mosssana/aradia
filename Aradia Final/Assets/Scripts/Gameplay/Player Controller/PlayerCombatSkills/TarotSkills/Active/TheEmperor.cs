using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Pathfinding;
using System.Linq;

/// <summary>
/// TheDevil:
/// Diana concentra su fuerza y desencadena un terremoto que aturde a todos los enemigos de la sala
/// </summary>
public class TheEmperor : TarotCard
{
    new void Awake()
    {
        cardName = CardName.TheEmperor;
        difficultyScore = 80;
        luckScore = 30;
        cooldown = 20;
        manaCost = 4;
        base.Awake();
    }

    #region Card Stats Variables

    float skillDamage = 5;
    float stunTimerDuration = 2.5f;

    Timer stunTimer;
    bool stunFinishedControlVariable = false;

    List<GameObject> stunnedEnemies;
    SearchRadius searchRadius;

    #endregion

    #region Support Variables
    // things of the earthquake credit to Lumidi Developer https://www.youtube.com/watch?v=O2Pg8e2xwzg
    bool shake = false;
    float shakeDuration = 0.6f;          // Time the Camera Shake effect will last
    float shakeAmplitude = 3f;         // Cinemachine Noise Profile Parameter
    float shakeFrequency = 2.0f;
    float shakeElapsedTime = 0f;
    CinemachineVirtualCamera VirtualCamera;
    CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    #endregion

    void Start()
    {
        stunTimer = gameObject.AddComponent<Timer>();
        stunTimer.Duration = stunTimerDuration;


        stunnedEnemies = new List<GameObject>();
        searchRadius = gameObject.GetComponentInChildren<SearchRadius>();
    }

    void Update()
    {
        if (shake)
        {
            // If Camera Shake effect is still playing
            if (shakeElapsedTime > 0)
            {

                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                // Update Shake Timer
                shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
                shake = false;
            }
        }

        if (stunTimer.Finished && !stunFinishedControlVariable)
        {
            stunnedEnemies.RemoveAll(item => item == null);
            // foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            foreach (GameObject enemy in stunnedEnemies)
            {
                if (enemy != null) if (enemy.GetComponent<AIPath>() != null) enemy.GetComponent<AIPath>().canMove = true;
            }
            stunnedEnemies.Clear();
            stunFinishedControlVariable = true;
        }
    }

    override public void SkillFire()
    {
        base.SkillFire();

        AudioManager.PlayUnique(AudioClipName.theEmperorEarthquake);

        VirtualCamera = GameObject.FindGameObjectWithTag("CinemachineCam").GetComponent<CinemachineVirtualCamera>();
        virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        stunTimer.Run();
        stunFinishedControlVariable = false;
        shakeElapsedTime = shakeDuration;
        shake = true;

        // foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        foreach (GameObject enemy in searchRadius.EnemiesList.ToList())
        {
            if (enemy != null)
            {
                AudioManager.PlayUnique(AudioClipName.theEmperorImpact);
                PlayerDamageIndex.DealDamage(enemy, skillDamage);
                stunnedEnemies.Add(enemy);
                if (enemy.GetComponent<AIPath>() != null) enemy.GetComponent<AIPath>().canMove = false;
            }
        }
    }
}
