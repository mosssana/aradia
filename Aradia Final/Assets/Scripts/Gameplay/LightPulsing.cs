using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulsing : MonoBehaviour
{
    float maxValue = 2.2f;
    float minValue = 1.6f;
    float speed;

    UnityEngine.Experimental.Rendering.Universal.Light2D light2D;

    bool decrease;


    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        switch (transform.position.z)
        {
            case 5:
                // BackgroundLayer1
                maxValue = 3f;
                minValue = 1.6f;
                speed = Random.Range(0.4f, 0.7f);
                break;
            case 10:
                // BackgroundLayer2
                maxValue = 0.77f;
                minValue = 0.4f;
                speed = Random.Range(0.2f, 0.4f);
                break;
            case 20:
                // BackgroundLayer3
                maxValue = 0.48f;
                minValue = 0.2f;
                speed = Random.Range(0.05f, 0.2f);
                break;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (decrease)
        {
            if (light2D.intensity > minValue) light2D.intensity -= speed * Time.deltaTime;
            else decrease = false;
        }
        else
        {
            if (light2D.intensity < maxValue) light2D.intensity += speed * Time.deltaTime;
            else decrease = true;
        }
    }
}
