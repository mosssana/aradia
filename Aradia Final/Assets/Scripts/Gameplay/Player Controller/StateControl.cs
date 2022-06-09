using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Keeps track of all states and updates movement related ones an their animations
/// </summary>
public class StateControl : MonoBehaviour
{
    #region Fields

    #region Base and Components
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    BasicAttack basicAttackScript;
    SpecialAttack specialAttackScript;
    LifeSystem playerLifeSystem;

    #endregion

    #region States
    static bool idle;
    static bool running;
    static bool jumping;
    static bool jumbEnabled;
    static bool falling;

    // timer that keeps track of how much time has elapsed (in seconds) since 'falling' state was true
    float lastFallingTime;
    bool landing;

    bool basicAttack;
    bool specialAttack;
    bool activeSkill;
    bool ultimateSkill;

    #endregion

    #region Other
    static bool receivingMoveInput;
    static bool facingRight = true;


    #endregion

    #endregion

    #region Properties
    public static bool Running
    {
        get { return running; }
        set { running = value; }
    }

    public static bool FacingRight
    {
        get { return facingRight; }
        set { facingRight = value; }
    }

    public static bool JumpEnabled
    {
        get { return jumbEnabled; }
    }

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerLifeSystem = GetComponent<LifeSystem>();
        basicAttackScript = GetComponent<BasicAttack>();
        specialAttackScript = GetComponent<SpecialAttack>();
    }

    void Start()
    {
        EventManager.AddListener(EventName.NotEnoughMana, NotEnoughMana);
    }

    private void FixedUpdate()
    {
        //Debug.Log("Grounded " + PlayerGrounded.Grounded + "/// " + "Running " + running + "/// " + "Idle " + idle + "/// " + "Landing " + landing);
        //Debug.Log("Life: " + playerLifeSystem.Life + " || Mana: " + PlayerDamageIndex.CurrentMana);

        basicAttack = basicAttackScript.Performing;
        specialAttack = specialAttackScript.Performing;
        activeSkill = TarotCardsHand.GetCard(CardType.Active).Performing;
        ultimateSkill = TarotCardsHand.GetCard(CardType.Ultimate).Performing;

        if (specialAttack) jumbEnabled = false;
        else jumbEnabled = true;

        #region LastFallingTime State Control

        if (falling) lastFallingTime = 0;
        else lastFallingTime += Time.deltaTime;

        #endregion


        if (PlayerGrounded.Grounded)
        {
            #region Landing State Control

            if (lastFallingTime < 0.5 && !running)
            {
                landing = true;
            }
            else landing = false;

            #endregion

            #region Running State Control

            if (receivingMoveInput)
            {

                if (rb.velocity.x > 1 || rb.velocity.x < 1)
                {
                    running = true;
                }
            }
            else
            {
                running = false;
            }

            #endregion

            #region  Jumping and Falling State Control

            jumping = false;
            falling = false;
        }
        else
        {
            if (rb.velocity.y > 1)
            {
                jumping = true;
                falling = false;
            }
            else if (rb.velocity.y < -0.5)
            {
                jumping = false;
                falling = true;
            }

            #endregion

            //used to prevent the 'landing' state to remain active if player jumps back into air too fast
            landing = false;
        }

        //idle 
        if (!running && !jumping && !falling && !landing && !basicAttack && !specialAttack && !activeSkill && !ultimateSkill && PlayerGrounded.Grounded) idle = true;
        else idle = false;

        Sound();
        Animation();
    }

    // takes advantage of the fact that it is called before updating aniamtion bools so we can compare previous frame with current frame
    // and catch when change is made
    void Sound()
    {
        // if (jumping && !animator.GetBool("Jumping")) AudioManager.Play(AudioClipName.playerJump);
        // if (landing && !animator.GetBool("Landing")) AudioManager.Play(AudioClipName.playerLand);
    }

    void Animation()
    {
        animator.SetBool("Idle", idle);
        animator.SetBool("Running", running);
        animator.SetBool("Jumping", jumping);
        animator.SetBool("Falling", falling);
        animator.SetBool("Landing", landing);
    }

    public void HorizontalMovementUpdateState(InputAction.CallbackContext ctx)
    {
        if (ctx.started) receivingMoveInput = true;
        else if (ctx.canceled) receivingMoveInput = false;
    }

    void NotEnoughMana()
    {
        Debug.Log("NOT ENOUGH MANA!");
        Debug.Log("NOT ENOUGH MANA!");
        Debug.Log("NOT ENOUGH MANA!");
        Debug.Log("NOT ENOUGH MANA!");
        Debug.Log("NOT ENOUGH MANA!");
    }

    public static void Reset()
    {
        // idle;
        // running;
        // jumping;
        // jumbEnabled;
        // falling;
        receivingMoveInput = false;
        facingRight = true;
    }
}
