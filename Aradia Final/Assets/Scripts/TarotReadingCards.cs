using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotReadingCards : MonoBehaviour
{
    Vector3 desiredPosition;

    public bool move;

    void Start()
    {
        desiredPosition = transform.position;
        transform.position = new Vector2(0, 10);
    }


    void Update()
    {
        if (transform.position != desiredPosition && move) transform.position = Vector2.Lerp(transform.position, desiredPosition, 0.03f);
    }
}
