using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] GameObject respawnPosition;

    float healthDeductPercentage = 25;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            LifeSystem lifeSystem = col.GetComponent<LifeSystem>();
            lifeSystem.DealDamage(lifeSystem.TotalLife * (healthDeductPercentage / 100));

            if (lifeSystem.CurrentLife > 0) col.transform.position = respawnPosition.transform.position;
        }
    }
}
