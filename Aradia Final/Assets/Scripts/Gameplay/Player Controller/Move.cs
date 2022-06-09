using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;

    float moveInput;

    bool facingRight = true;

    bool canMove = true;

    [SerializeField] float movementSpeed; // = 20;
    [SerializeField] float runAccel; // = 22
    [SerializeField] float runDeccel; // = 7
    [Range(.5f, 2f)][SerializeField] float accelPower; // = 1.2
    [Range(.5f, 2f)][SerializeField] float stopPower; // = 1.2
    [Range(.5f, 2f)][SerializeField] float turnPower; // = 1

    float movementSpeedMultiplier = 1;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public float MovementSpeedMultiplier
    {
        set { movementSpeedMultiplier = value; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (canMove) Run(1);
    }

    private void Run(float lerpAmount)
    {
        //calculate the direction we want to move in and our desired velocity
        float targetSpeed = moveInput * (movementSpeed * movementSpeedMultiplier);
        //calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.velocity.x;
        //change acceleration rate depending on situation
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccel : runDeccel;

        //if we want to run but are already going faster than max run speed
        if (PlayerGrounded.Grounded && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f) accelRate = 0; //prevent any deceleration from happening, or in other words conserve are current momentum

        #region Velocity Power
        float velPower;
        if (Mathf.Abs(targetSpeed) < 0.01f)
        {
            velPower = stopPower;
        }
        else if (Mathf.Abs(rb.velocity.x) > 0 && (Mathf.Sign(targetSpeed) != Mathf.Sign(rb.velocity.x)))
        {
            velPower = turnPower;
        }
        else
        {
            velPower = accelPower;
        }
        #endregion

        // applies acceleration to speed difference, then raises to set power so acceleration increases with higher speed, finally multiplies by sign to reapply direction
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        movement = Mathf.Lerp(rb.velocity.x, movement, lerpAmount); // lerp so that we can prevent the Run from immediately slowing the player down, in some situations eg wall jump, dash 

        // applies force to rigidbody
        rb.AddForce(movement * Vector2.right);

        // flips player depending on movement direction
        if (moveInput != 0)
            CheckDirectionToFace(moveInput > 0);
    }

    public void CheckDirectionToFace(bool isMovingLeft)
    {
        if (isMovingLeft != facingRight)
            Turn();
    }

    private void Turn()
    {
        Vector3 scale = transform.localScale; //stores scale and flips x axis, "flipping" the entire gameObject around. (could rotate the player instead)
        scale.x *= -1;
        transform.localScale = scale;

        facingRight = !facingRight; //flip bool
        StateControl.FacingRight = facingRight;
    }

    ///<summary>
    /// Updates moveInput value according to recieved input
    ///</summary>
    public void HorizontalMovement(InputAction.CallbackContext ctx)
    {
        if (!PauseMenuManager.IsPaused) moveInput = ctx.ReadValue<float>();
    }
}
