using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEmpressParticleSystem : MonoBehaviour
{
    GameObject player;
    LifeSystem lifeSystem;
    TheEmpress theEmpress;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lifeSystem = player.GetComponent<LifeSystem>();
        theEmpress = player.GetComponent<TheEmpress>();
        AudioManager.PlayUnique(AudioClipName.theEmpressSmoke);
    }

    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, player.transform.position, 0.05f);
        transform.rotation = Quaternion.Euler(0f, 0f, CalculateAngle());
    }

    float CalculateAngle()
    {
        return Mathf.Rad2Deg * Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            AudioManager.Play(AudioClipName.healthResource);
            lifeSystem.Heal(((theEmpress.percentageOfLifeDrained / 100) * theEmpress.skillDamage));
            Destroy(gameObject);
        }
    }
}
