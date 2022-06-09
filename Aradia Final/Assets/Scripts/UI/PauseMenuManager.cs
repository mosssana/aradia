using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : EventInvoker
{
    private InputSystem inputSystem;
    InputAction menu;

    [SerializeField] GameObject pauseUI;
    static bool isPaused;

    [SerializeField] GameObject firstSelected;

    public static bool IsPaused
    {
        get { return isPaused; }
    }

    void Awake()
    {
        inputSystem = new InputSystem();
    }

    void Start()
    {
        unityEvents.Add(EventName.QuitGameplayEvent, new QuitGameplayEvent());
        EventManager.AddInvoker(EventName.QuitGameplayEvent, this);
    }

    void OnEnable()
    {
        menu = inputSystem.UI.Escape;
        menu.Enable();

        menu.performed += Pause;
    }

    void OnDisable()
    {
        menu.Disable();
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (!GameOverMenuManager.IsOpen)
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                ActivateMenu();
            }
            else DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        firstSelected.GetComponent<Button>().Select();
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        isPaused = false;
    }


    public void Continue()
    {
        AudioManager.Play(AudioClipName.menuClick);
        DeactivateMenu();
    }

    public void Tarot()
    {
        AudioManager.Play(AudioClipName.menuClick);
    }

    public void Settings()
    {
        AudioManager.Play(AudioClipName.menuClick);
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
        AudioManager.StopLoop(AudioClipName.music);
        AudioManager.PlayLoop(AudioClipName.menuMusic);
        SceneManager.LoadScene("title");
    }
}
