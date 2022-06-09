using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackMissile : MonoBehaviour
{
    protected GameObject player;
    protected BasicAttack basicAttackScript;
    protected Rigidbody2D rb;
    protected PlayerDamageIndex pDI;

    protected float speedForce = 5000;
    bool directionLeft;

    protected float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        basicAttackScript = player.GetComponent<BasicAttack>();
        rb = GetComponent<Rigidbody2D>();
        pDI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageIndex>();
    }

    void Start()
    {
        if (StateControl.FacingRight) directionLeft = false;
        else directionLeft = true;

        if (directionLeft)
        {
            speedForce *= -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        rb.AddForce(speedForce * Vector2.right);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            pDI.ConsumeMana(basicAttackScript.ManaCost);
            PlayerDamageIndex.DealDamage(col.gameObject, basicAttackScript.Damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5) Destroy(gameObject);
    }


}
