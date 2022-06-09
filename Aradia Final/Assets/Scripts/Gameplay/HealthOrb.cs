using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    [SerializeField] float amountToHeal = 50;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player") {
            AudioManager.Play(AudioClipName.healthResource);
            col.gameObject.GetComponent<LifeSystem>().Heal(amountToHeal);
            Destroy(gameObject);
        }
    }
}
