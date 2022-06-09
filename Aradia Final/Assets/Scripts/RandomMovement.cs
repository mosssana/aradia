using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    Rigidbody2D rb;
    // Light2D light;
    public float accelerationTime;
    public float maxSpeed;
    private Vector2 movement;
    private float timeLeft;

    // float prevLightIntensity;
    // float newLightIntensity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // light =GetComponent<Light>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            // prevLightIntensity = light.intensity;
            // newLightIntensity = Random.Range(1f, 2f);
            timeLeft += Random.Range(-0.01f, 1f);
        }
    }

    void FixedUpdate()
    {
        // rb.AddForce(movement * maxSpeed);
        rb.position += movement * maxSpeed;
        // light.intensity = Mathf.Lerp(prevLightIntensity, newLightIntensity, 0.1f);
    }
}
