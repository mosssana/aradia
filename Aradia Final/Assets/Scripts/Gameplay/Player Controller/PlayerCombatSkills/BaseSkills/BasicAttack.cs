using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicAttack : EventInvoker
{
    float damage = 1;
    int manaCost = -1;

    public float Damage
    {
        get { return damage; }
    }

    public int ManaCost
    {
        get { return manaCost; }
    }

    [SerializeField] GameObject basicAttackSource;
    [SerializeField] GameObject basicAttackMissile;
    public GameObject BasicAttackMissile
    {
        get { return basicAttackMissile; }
        set { basicAttackMissile = value; }
    }

    StateControl stateControl;
    Animator animator;
    PlayerDamageIndex pDI;

    // state control variable
    bool performing;

    public bool Performing
    {
        get { return performing; }
    }

    void Start()
    {
        stateControl = GetComponent<StateControl>();
        animator = GetComponent<Animator>();
        pDI = GetComponent<PlayerDamageIndex>();

        unityEvents.Add(EventName.BasicAttackPerformed, new BasicAttackPerformed());
        EventManager.AddInvoker(EventName.BasicAttackPerformed, this);
    }

    void OnDisable()
    {
        EventManager.RemoveInvoker(EventName.BasicAttackPerformed, this);
    }

    void Update()
    {
        animator.SetBool("BasicAttack", performing);
    }

    /// called by input system
    public void BasicAttackPerform(InputAction.CallbackContext ctx)
    {
        if (!PauseMenuManager.IsPaused)
        {
            if (ctx.started && pDI.CheckMana(manaCost)) performing = true;
            else if (ctx.canceled) performing = false;
        }
    }

    /// <summary>
    /// Instantiates a Magic Missile as part of the Basic Attack when method is called by an animation event
    /// </summary>
    public void BasicAttackThrowMissile()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                AudioManager.Play(AudioClipName.basicAttack);
                break;
            case 1:
                AudioManager.Play(AudioClipName.basicAttack2);
                break;
            case 2:
                AudioManager.Play(AudioClipName.basicAttack3);
                break;
        }

        unityEvents[EventName.BasicAttackPerformed].Invoke();
        GameObject basicAttackMissileTheMagicianObj = Instantiate(basicAttackMissile, new Vector3(basicAttackSource.transform.position.x, basicAttackSource.transform.position.y, 0), Quaternion.identity);
    }

}
