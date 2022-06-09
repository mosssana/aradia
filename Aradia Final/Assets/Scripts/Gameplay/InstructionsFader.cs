using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsFader : MonoBehaviour
{
    bool done = false;

    float duration = 0.5f;

    [SerializeField] GameObject nextInstruciton;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!done)
        {
            if (col.tag == "Player")
            {
                StartCoroutine(FadeOut(duration));
                Destroy(GetComponent<CircleCollider2D>());
            }
        }
    }

    IEnumerator FadeOut(float duration)
    {
        float counter = 0f;
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            spriteRenderer.color = new Color(1, 1, 1, alpha);

            yield return null; //Because we don't need a return value.
        }

        if (nextInstruciton != null) StartCoroutine(nextInstruciton.GetComponent<InstructionsFader>().FadeIn(duration));

        done = true;
        // Destroy(gameObject);
    }


    public IEnumerator FadeIn(float duration)
    {
        float counter = 0f;
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, counter / duration);
            spriteRenderer.color = new Color(1, 1, 1, alpha);

            yield return null; //Because we don't need a return value.
        }
    }
}
