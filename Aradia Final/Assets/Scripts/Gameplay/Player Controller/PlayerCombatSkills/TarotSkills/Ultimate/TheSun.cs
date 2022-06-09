using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Linq;

/// <summary>
/// The Empress: 
/// Tras un breve periodo de tiempo, Diana convoca una explosión de llamas sagradas (/fuego fatuo?).
// Las llamas restauran la vida de Diana al completo, ?????????????????????????
// le otorgan un escudo equivalente a su vida máxima,
// y hacen daño masivo a todos los enemigos de la sala
/// </summary>
public class TheSun : TarotCard
{
    new void Awake()
    {
        cardName = CardName.TheSun;
        difficultyScore = 100;
        luckScore = 10;
        cooldown = 60;
        manaCost = 8;
        canMove = false;
        base.Awake();
    }

    #region Card Stats Variables

    float skillDamage = 20;

    #endregion

    #region Support Variables

    LifeSystem lifeSystem;

    bool shieldActive = false;

    float shieldLife;

    Object shieldPrefab;

    GameObject shield;

    public float ShieldLife
    {
        get { return shieldLife; }
    }

    public bool ShieldActive
    {
        get { return shieldActive; }
    }



    // things of the earthquake credit to Lumidi Developer https://www.youtube.com/watch?v=O2Pg8e2xwzg
    bool shake = false;
    float shakeDuration = 1.2f;          // Time the Camera Shake effect will last
    float shakeAmplitude = 6f;         // Cinemachine Noise Profile Parameter
    float shakeFrequency = 1.0f;
    float shakeElapsedTime = 0f;
    CinemachineVirtualCamera VirtualCamera;
    CinemachineBasicMultiChannelPerlin virtualCameraNoise;


    Object particles;

    SearchRadius searchRadius;


    #endregion

    void Start()
    {
        lifeSystem = GetComponent<LifeSystem>();

        EventManager.AddListener(EventName.PlayerAttackedForShields, DamageShield);

        shieldPrefab = Resources.Load("Prefabs/TheSunShield");

        particles = Resources.Load("Prefabs/TheSunParticleSystem");

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
    }

    public override void SkillPerform(InputAction.CallbackContext ctx)
    {
        base.SkillPerform(ctx);

        if (ctx.started) { if (Available) AudioManager.PlayUnique(AudioClipName.theSunFire); }
        else if (ctx.canceled && !fired) AudioManager.StopUnique(AudioClipName.theSunFire);
    }

    override public void SkillFire()
    {
        base.SkillFire();
        
        foreach (GameObject enemy in searchRadius.EnemiesList.ToList())
        {
            if (enemy != null)
            {
                AudioManager.PlayUnique(AudioClipName.theSunImpact);
                PlayerDamageIndex.DealDamage(enemy, skillDamage);
                Instantiate(particles, enemy.transform.position, Quaternion.identity, enemy.transform);
            }
        }

        VirtualCamera = GameObject.FindGameObjectWithTag("CinemachineCam").GetComponent<CinemachineVirtualCamera>();
        virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        ActivateShield();
        Instantiate(particles, transform.position, Quaternion.identity, gameObject.transform);
        shakeElapsedTime = shakeDuration;
        shake = true;
    }

    void ActivateShield()
    {
        if (!shieldActive)
        {
            shieldActive = true;
            lifeSystem.Invulnerable = true;
            shieldLife = lifeSystem.TotalLife;
            shield = Instantiate((GameObject)shieldPrefab, gameObject.transform);
        }
        else
        {
            shieldLife = lifeSystem.TotalLife;
        }
    }

    void DamageShield(float damage, float life)
    {
        if (shieldActive)
        {
            shieldLife -= damage;
            Debug.Log("ShieldLife " + shieldLife);


            if (shieldLife <= 0)
            {
                shieldActive = false;
                Destroy(shield);
                lifeSystem.Invulnerable = false;
            }

        }
    }
}
