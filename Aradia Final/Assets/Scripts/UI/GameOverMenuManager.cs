using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuManager : EventInvoker
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameOverButtons;
    static bool isOpen;

    [SerializeField] GameObject firstSelected;

    public static bool IsOpen
    {
        get { return isOpen; }
    }

    void Start()
    {
        unityEvents.Add(EventName.QuitGameplayEvent, new QuitGameplayEvent());
        EventManager.AddInvoker(EventName.QuitGameplayEvent, this);

        EventManager.AddListener(EventName.PlayerLifeChanged, CheckIfDead);
    }

    void CheckIfDead(float life, float totalLife)
    {
        if (life <= 0) ActivateMenu();
    }

    void ActivateMenu()
    {
        gameOverUI.SetActive(true);        
        isOpen = true;
        StartCoroutine(ActivateButtons());
        AudioManager.StopLoop(AudioClipName.music);
    }

    public void DeactivateMenu()
    {
        gameOverButtons.SetActive(false);
        gameOverUI.SetActive(false);
        isOpen = false;
    }

    IEnumerator ActivateButtons()
    {
        yield return new WaitForSeconds(2);
        gameOverButtons.SetActive(true);
        firstSelected.GetComponent<Button>().Select();
        yield return null;
    }

    public void PlayAgain()
    {
        unityEvents[EventName.QuitGameplayEvent].Invoke();
        DeactivateMenu();

        FadeUIGroup globalFader = GameObject.FindGameObjectWithTag("GlobalFader").GetComponent<FadeUIGroup>();
        globalFader.Fade(PlayAgainMethod);
    }

    void PlayAgainMethod()
    {
        AudioManager.PlayLoop(AudioClipName.menuMusic);
        SceneManager.LoadScene("tarotReading");
    }

    public void QuitToTitle()
    {
        unityEvents[EventName.QuitGameplayEvent].Invoke();
        DeactivateMenu();

        FadeUIGroup globalFader = GameObject.FindGameObjectWithTag("GlobalFader").GetComponent<FadeUIGroup>();
        globalFader.Fade(QuitToTitleMethod);
    }

    void QuitToTitleMethod()
    {
        AudioManager.PlayLoop(AudioClipName.menuMusic);
        SceneManager.LoadScene("title");
    }
}
