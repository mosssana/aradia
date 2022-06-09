using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPlayerSearchRadius : MonoBehaviour
{
    AIDestinationSetter aIDestinationSetter;

    void Start()
    {
        aIDestinationSetter = transform.parent.GetComponent<AIDestinationSetter>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (aIDestinationSetter != null) aIDestinationSetter.target = col.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (aIDestinationSetter != null) aIDestinationSetter.target = null;
        }
    }
}
