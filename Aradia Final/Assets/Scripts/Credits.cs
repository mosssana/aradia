using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Credits : MonoBehaviour
{
    FadeUIGroup globalFader;

    void Start()
    {
        globalFader = GameObject.FindGameObjectWithTag("GlobalFader").GetComponent<FadeUIGroup>();
    }

    public void Escape(InputAction.CallbackContext ctx)
    {
        if (ctx.started) globalFader.Fade(SceneManager.LoadScene, "title");
    }

    public void OnCreditsEnd()
    {
        globalFader.Fade(SceneManager.LoadScene, "title");
    }
}
