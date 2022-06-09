using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUIGroup : MonoBehaviour
{
    public delegate void MethodDelegate();
    public delegate void MethodDelegateString(string scene);

    [SerializeField] bool fadeOnEnable = false;
    bool isFaded;
    [SerializeField] float duration = 1;

    CanvasGroup canvGroup;

    void OnEnable()
    {
        if (fadeOnEnable) Fade();
    }

    public void Fade()
    {
        Fade(null);
    }

    public void Fade(MethodDelegate method)
    {
        try
        {
            canvGroup = GetComponent<CanvasGroup>();
            if (canvGroup.alpha == 0) isFaded = true;
            else isFaded = false;

            //Toggle the end value depending on the faded state ( from 1 to 0)
            StartCoroutine(DoFade(canvGroup, canvGroup.alpha, isFaded ? 1 : 0, method));
        }
        catch
        {

        }
    }

    public void Fade(MethodDelegateString method, string scene)
    {
        try
        {
            canvGroup = GetComponent<CanvasGroup>();
            if (canvGroup.alpha == 0) isFaded = true;
            else isFaded = false;

            //Toggle the end value depending on the faded state ( from 1 to 0)
            StartCoroutine(DoFade(canvGroup, canvGroup.alpha, isFaded ? 1 : 0, method, scene));
        }
        catch
        {

        }
    }

    IEnumerator DoFade(CanvasGroup canvGroup, float start, float end, MethodDelegateString method, string scene)//Runto complition beforex
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null; //Because we don't need a return value.
        }

        if (method != null) method(scene);
    }

    IEnumerator DoFade(CanvasGroup canvGroup, float start, float end, MethodDelegate method)//Runto complition beforex
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null; //Because we don't need a return value.
        }

        if (method != null) method();
    }
}
