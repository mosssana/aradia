using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInitializer : MonoBehaviour
{
    void Awake()
    {
        // make sure we only have one of this game object
        // in the game
        if (!UIManager.Initialized)
        {
            UIManager.Initialize();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // duplicate game object, so destroy
            Destroy(gameObject);
        }
    }

    void Start()
    {
        EventManager.AddListener(EventName.QuitGameplayEvent, DestroyUI);
    }

    // void Update()
    // {
    //     if (SceneManager.GetActiveScene().name == "title" ||
    //         SceneManager.GetActiveScene().name == "tarotReading" ||
    //         SceneManager.GetActiveScene().name == "level1Completed"||
    //         SceneManager.GetActiveScene().name == "credits") DestroyUI();
    // }

    void DestroyUI()
    {
        UIManager.ResetUI();
        Destroy(gameObject);
    }
}
