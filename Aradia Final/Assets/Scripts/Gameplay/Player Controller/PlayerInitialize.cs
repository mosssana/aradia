using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInitialize : MonoBehaviour
{

    void Awake()
    {
        // make sure we only have one of this game object
        // in the game
        if (!PlayerManager.Initialized)
        {
            PlayerManager.Initialize();
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
        EventManager.AddListener(EventName.QuitGameplayEvent, DestroyPlayer);
    }

    // void Update()
    // {
    //     if (SceneManager.GetActiveScene().name == "title"||
    //         SceneManager.GetActiveScene().name == "tarotReading" ||
    //         SceneManager.GetActiveScene().name == "level1Completed" ||
    //         SceneManager.GetActiveScene().name == "credits") DestroyPlayer();
    // }

    void DestroyPlayer()
    {
        PlayerManager.ResetPlayer();
        Destroy(gameObject);
    }
}
