using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackMissile : MonoBehaviour
{
    protected GameObject player;
    protected SpecialAttack specialAttackScript;
    protected Rigidbody2D rb;

    protected float speedForce = 5000;
    bool directionLeft;

    protected float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        specialAttackScript = player.GetComponent<SpecialAttack>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AddForceAtAngle(speedForce, transform.eulerAngles.z);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            AudioManager.PlayUnique(AudioClipName.specialAttackImpact);
            PlayerDamageIndex.DealDamage(col.gameObject, specialAttackScript.Damage);
        }
        if (col.gameObject.tag != "Ground") Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5) Destroy(gameObject);
    }

    void AddForceAtAngle(float force, float angle)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;

        rb.AddForce(new Vector2(ycomponent, xcomponent));
    }
}
