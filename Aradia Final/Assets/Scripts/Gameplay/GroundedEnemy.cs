using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemy : Enemy
{
    // Object projectile;
    // Timer projectileTimer;
    GameObject target;
    Vector2 targetInitialPosition;
    // Timer dashDuration;
    Timer dashCooldown;
    // Rigidbody2D rb;
    // float ogSpeed;

    bool doDash = false;

    override public void Start()
    {
        baseDamage = 10;

        target = GameObject.FindGameObjectWithTag("Player");
        targetInitialPosition = target.transform.position;

        dashCooldown = gameObject.AddComponent<Timer>();
        dashCooldown.Duration = 1;

        base.Start();
    }

    override public void Update()
    {
        base.Update();

        if (!aIPath.canMove && doDash) FinishDash();

        if (target != null)
        {
            if (target.transform.position.x < transform.position.x) transform.localScale = new Vector3(xSize, ySize, 1f);
            else transform.localScale = new Vector3(-xSize, ySize, 1f);
        }

        if (doDash) transform.position = Vector2.Lerp(transform.position, new Vector2(targetInitialPosition.x, transform.position.y), 0.2f);
    }

    override public void OnReachedEndOfPath()
    {
        if (target != null && target.transform.position.y <= transform.position.y + 3 && target.transform.position.y >= transform.position.y - 3)
        {
            if (!dashCooldown.Running) animator.SetBool("attacking", true);
        }
    }

    public void Dash()
    {
        doDash = true;
        if (target != null)
        {
            AudioManager.Play(AudioClipName.groundedEnemyAttack);
            targetInitialPosition = target.transform.position;
        }
        target = null;
    }

    public void FinishDash()
    {
        animator.SetBool("attacking", false);
        doDash = false;
        target = GameObject.FindGameObjectWithTag("Player");
        dashCooldown.Run();
    }
}
