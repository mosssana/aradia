using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackMissileTheMagician : BasicAttackMissile
{
    GameObject target;

    void Start()
    {
        target = GetClosestEnemy(transform);

        if (target != null) AddForceAtAngle(5000, CalculateAngle());
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
        if (timer > 0.7) Destroy(gameObject);
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

    GameObject GetClosestEnemy(Transform fromThis)
    {
        GameObject[] potentialTargets;
        potentialTargets = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (GameObject potentialTarget in potentialTargets)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}
