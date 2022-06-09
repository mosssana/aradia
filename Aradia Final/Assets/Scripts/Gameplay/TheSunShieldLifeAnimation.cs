using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSunShieldLifeAnimation : MonoBehaviour
{
    [Range(1,3)][SerializeField] int threshold;

    float actualThreshold;
    TheSun theSun;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        theSun = GameObject.FindGameObjectWithTag("Player").GetComponent<TheSun>();
        float part = theSun.ShieldLife / 4;
        actualThreshold = threshold*part;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theSun.ShieldLife <= actualThreshold) spriteRenderer.enabled = false;
        else spriteRenderer.enabled = true;
    }
}
