using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    FadeUIGroup globalFader;

    void Awake()
    {
        if (!EventManager.Initialized) EventManager.Initialize();
    }

    void Start()
    {
        AudioManager.PlayLoop(AudioClipName.menuMusic);
        globalFader = GameObject.FindGameObjectWithTag("GlobalFader").GetComponent<FadeUIGroup>();
    }

    public void Play()
    {
        AudioManager.Play(AudioClipName.menuClickPlay);
        globalFader.Fade(SceneManager.LoadScene, "tarotReading");
    }

    public void LoadCredits()
    {
        AudioManager.Play(AudioClipName.menuClick);
        globalFader.Fade(SceneManager.LoadScene, "credits");
    }

    public void Quit()
    {
        AudioManager.Play(AudioClipName.menuClick);
        globalFader.Fade(Application.Quit);
    }
}
