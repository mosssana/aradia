using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float baseDamage;
    float skillDamage = 4;

    GameObject target;
    Vector2 targetInitialPosition;
    float timer;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        if (target != null) targetInitialPosition = target.transform.position;

        if (target != null) AddForceAtAngle(3000, CalculateAngle());
        AudioManager.Play(AudioClipName.flyingEnemyMissile);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == target)
        {
            target.GetComponent<LifeSystem>().DealDamage(baseDamage, skillDamage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.6f) Destroy(gameObject);
    }


    float CalculateAngle()
    {
        float deg = Mathf.Rad2Deg * Mathf.Atan2((target.transform.position.y - transform.position.y), (target.transform.position.x - transform.position.x));
        return deg;
    }

    void AddForceAtAngle(float force, float angle)
    {
        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        rb.AddForce(dir * force);
    }
}
