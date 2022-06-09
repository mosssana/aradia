using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    float upForce = 75;
    float damage = 15;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            AudioManager.Play(AudioClipName.spikesImpact);
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
            col.gameObject.GetComponent<LifeSystem>().DealDamage(damage);
        }
    }
}
