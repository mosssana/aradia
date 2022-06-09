using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    Timer timer;

    int whatIsGround;

    bool isJumping;

    [SerializeField] int extraJumps; // = 1;
    int remainingJumps;
    [SerializeField] float jumpForce; // = 70;
    [SerializeField] float jumpTime; // = 0.6f;
    float maxFallingSpeed = -75;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = jumpTime;
        remainingJumps = extraJumps;
    }

    private void Update()
    {
        if (PlayerGrounded.Grounded) remainingJumps = extraJumps;

        // prevents player from surpassing max falling speed
        if (rb.velocity.y < maxFallingSpeed) rb.velocity = new Vector2(rb.velocity.x, maxFallingSpeed);
    }

    public void JumpMethod(InputAction.CallbackContext ctx)
    {
        if (!PauseMenuManager.IsPaused && !TarotCardsHand.GetCard(CardType.Ultimate).Performing)
        {
            if (ctx.started && StateControl.JumpEnabled)
            {
                if (PlayerGrounded.Grounded)
                {
                    timer.Run();
                    isJumping = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                else if (remainingJumps > 0)
                {
                    timer.Run();
                    --remainingJumps;
                    isJumping = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
            }
            else if (ctx.canceled) isJumping = false;
        }
    }

    // resets 'remainingJumps'
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == whatIsGround) remainingJumps = extraJumps;
    }

    private void FixedUpdate()
    {
        if (isJumping && timer.Running)
        {
            float dynamicJumpForce = jumpForce * timer.Remaining;
            rb.velocity = new Vector2(rb.velocity.x, dynamicJumpForce);
        }
        else if (timer.Finished) isJumping = false;
    }
}

