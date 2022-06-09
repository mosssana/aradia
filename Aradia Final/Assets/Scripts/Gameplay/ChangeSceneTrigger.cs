using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ChangeSceneTrigger : EventInvoker
{
    [SerializeField] bool invokeQuitGameplayEvent;
    [Range(0, 400)][SerializeField] float luckThreshold;
    [SerializeField] string sceneToLoadBeforeThreshold;
    [SerializeField] string sceneToLoadAfterThreshold;

    FadeUIGroup globalFader;

    void Start()
    {
        unityEvents.Add(EventName.RoomChanged, new RoomChangedEvent());
        EventManager.AddInvoker(EventName.RoomChanged, this);

        if (invokeQuitGameplayEvent)
        {
            unityEvents.Add(EventName.QuitGameplayEvent, new QuitGameplayEvent());
            EventManager.AddInvoker(EventName.QuitGameplayEvent, this);
        }

        globalFader = GameObject.FindGameObjectWithTag("GlobalFader").GetComponent<FadeUIGroup>();
    }

    void OnDisable()
    {
        EventManager.RemoveInvoker(EventName.RoomChanged, this);
        if (invokeQuitGameplayEvent) EventManager.RemoveInvoker(EventName.QuitGameplayEvent, this);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerManager.PreviousScene = SceneManager.GetActiveScene().name;

            if (TarotCardsHand.CardsTotalLuckScore < luckThreshold)
            {
                globalFader.Fade(SceneManager.LoadScene, sceneToLoadBeforeThreshold);
                // SceneManager.LoadScene(sceneToLoadBeforeThreshold);
            }
            else
            {
                globalFader.Fade(SceneManager.LoadScene, sceneToLoadBeforeThreshold);
                // SceneManager.LoadScene(sceneToLoadAfterThreshold);
            }

            if (invokeQuitGameplayEvent)
            {
                unityEvents[EventName.QuitGameplayEvent].Invoke();

                AudioManager.Play(AudioClipName.levelCompleted);
                AudioManager.PlayLoop(AudioClipName.menuMusic);
                AudioManager.StopLoop(AudioClipName.music);
            }
            else
            {
                unityEvents[EventName.RoomChanged].Invoke();
                AudioManager.Play(AudioClipName.roomChange);
            }
        }
    }
}
