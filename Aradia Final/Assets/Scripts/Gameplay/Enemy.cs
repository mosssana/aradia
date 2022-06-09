using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    GameObject player;

    bool playerCollision;
    Timer playerCollisionTimer;

    // Timer spriteColorTimer;
    // SpriteRenderer spriteRenderer;
    // Color attacked = new Color(255, 255, 255, 170);
    // Color normal = new Color(255, 255, 255);

    protected AIPath aIPath;
    LifeSystem lifeSystem;

    protected float baseDamage;

    protected Animator animator;

    protected Rigidbody2D rb;

    protected float xSize;
    protected float ySize;

    void Awake()
    {
        aIPath = GetComponent<AIPath>();

        lifeSystem = GetComponent<LifeSystem>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteColorTimer = gameObject.AddComponent<Timer>();
        // spriteColorTimer.Duration = 0.3f;
        playerCollisionTimer = gameObject.AddComponent<Timer>();
        playerCollisionTimer.Duration = 1;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        xSize = transform.localScale.x;
        ySize = transform.localScale.y;

        // GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Start()
    {
        baseDamage *= ((TarotCardsHand.CardsDifficultyScore + 100) / 100) / 2;
        lifeSystem.IncreaseTotalLife(lifeSystem.TotalLife * (((TarotCardsHand.CardsDifficultyScore + 100) / 1000)), true);
    }

    public virtual void Update()
    {
        // if (spriteColorTimer.Finished) spriteRenderer.color = normal;

        // flips according to direction
        if (aIPath.desiredVelocity.x >= 0.01f) transform.localScale = new Vector3(-xSize, ySize, 1f);
        else if (aIPath.desiredVelocity.x <= -0.01f) transform.localScale = new Vector3(xSize, ySize, 1f);

        // checks if it's close enoguh to target, if there's still a target and wether or not they r stunned
        if (aIPath.reachedEndOfPath && GetComponent<AIDestinationSetter>().target != null && GetComponent<AIPath>().canMove)
        {
            animator.SetBool("running", false);
            OnReachedEndOfPath();
        }
        else if (GetComponent<AIDestinationSetter>().target != null)
        {
            animator.SetBool("running", true);
        }

        if (aIPath.desiredVelocity.x != 0f) animator.SetBool("running", true);
        else animator.SetBool("running", false);

        if (playerCollision && !playerCollisionTimer.Running)
        {
            player.GetComponent<LifeSystem>().DealDamage(baseDamage, 1);
            playerCollisionTimer.Run();
        }

        if (aIPath.canMove)
        {
            animator.SetBool("stunned", false);
            gameObject.transform.Find("StunnedEffect").gameObject.SetActive(false);

        }
        else
        {
            animator.SetBool("stunned", true);
            gameObject.transform.Find("StunnedEffect").gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                playerCollision = true;
                player = col.gameObject;
                break;
            case "PlayerAttack":
                // spriteRenderer.color = attacked;
                // spriteColorTimer.Run();
                break;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                playerCollision = false;
                break;
        }
    }

    public virtual void OnReachedEndOfPath()
    {

    }
}
