using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    Object projectile;
    Timer projectileTimer;

    override public void Start()
    {
        baseDamage = 2;

        projectile = Resources.Load("Prefabs/Enemies/EnemyProjectile");
        projectileTimer = gameObject.AddComponent<Timer>();
        projectileTimer.Duration = 2;

        base.Start();
    }

    override public void OnReachedEndOfPath()
    {
        if (!projectileTimer.Running)
        {
            
            animator.SetBool("attacking", true);
            projectileTimer.Run();
        }
    }

    public void FireProjectile()
    {
        GameObject proj = (GameObject)Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        proj.GetComponent<EnemyProjectile>().baseDamage = baseDamage;
        animator.SetBool("attacking", false);
    }
}
