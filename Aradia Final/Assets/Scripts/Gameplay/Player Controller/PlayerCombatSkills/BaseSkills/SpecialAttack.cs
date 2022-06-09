using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialAttack : MonoBehaviour
{
    float damage = 4;
    int manaCost = 4;
    int numberOfPorjectiles = 5;

    public float Damage
    {
        get { return damage; }
    }

    public int ManaCost
    {
        get { return manaCost; }
    }

    public int NumberOfPorjectiles
    {
        get { return numberOfPorjectiles; }
    }

    [SerializeField] GameObject specialAttackSource;
    [SerializeField] GameObject specialAttackMissile;
    Animator animator;
    PlayerDamageIndex pDI;

    // state control variable
    bool performing;
    public bool Performing
    {
        get { return performing; }
    }

    Move moveScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveScript = GetComponent<Move>();
        pDI = GetComponent<PlayerDamageIndex>();
    }

    void Update()
    {
        if (performing) if (!pDI.CheckMana(manaCost)) performing = false;
        animator.SetBool("SpecialAttack", performing);
    }

    /// called by input system
    public void SpecialAttackPerform(InputAction.CallbackContext ctx)
    {
        if (!PauseMenuManager.IsPaused)
        {
            if (ctx.started && pDI.CheckMana(manaCost))
            {
                performing = true;
                moveScript.MovementSpeedMultiplier = 0.5f;
                AudioManager.PlayUnique(AudioClipName.specialAttackCharge);
            }
            else if (ctx.started)
            {
                AudioManager.Play(AudioClipName.skillUnavailable);
            }
            else if (ctx.canceled)
            {
                performing = false;
                moveScript.MovementSpeedMultiplier = 1f;
                AudioManager.StopUnique(AudioClipName.specialAttackCharge);
            }
        }
    }

    /// <summary>
    /// Instantiates 5 missiles in the shape of a cone as part of the Special Attack when method is called by an animation event
    /// </summary>
    void SpecialAttackThrowMissileCone()
    {
        AudioManager.Play(AudioClipName.specialAttackFire);
        pDI.ConsumeMana(manaCost);

        float angle;
        if (StateControl.FacingRight) angle = 80;
        else angle = 260;


        for (int i = 0; i < numberOfPorjectiles; i++)
        {
            GameObject specialAttackMissileObj = Instantiate(specialAttackMissile, new Vector3(specialAttackSource.transform.position.x, specialAttackSource.transform.position.y, 0), Quaternion.Euler(0f, 0f, angle));

            if ((angle + 5) <= 360) angle += 5;
            else angle += 5 - 360;
        }
    }
}
