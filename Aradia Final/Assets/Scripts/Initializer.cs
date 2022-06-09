using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    GameObject player;

    void Awake()
    {
        if (!EventManager.Initialized) EventManager.Initialize();
        if (!PlayerManager.Initialized) Instantiate(Resources.Load("Prefabs/Player"));
        if (!UIManager.Initialized) Instantiate(Resources.Load("Prefabs/UI/UI"));
    }

    void Start()
    {
        AudioManager.PlayLoop(AudioClipName.caveAmbiance);
        AudioManager.PlayLoop(AudioClipName.music);

        player = GameObject.FindGameObjectWithTag("Player");

        switch (SceneManager.GetActiveScene().name)
        {
            case "level1Room1":
                if (PlayerManager.PreviousScene == "tarotReading")
                {
                    player.transform.position = new Vector2(-14, 4);
                }
                else if (PlayerManager.PreviousScene == "level1Room3" || PlayerManager.PreviousScene == "level1Bridge")
                {
                    player.transform.position = new Vector2(208, 94);
                }
                break;
            case "level1Room2":
                if (PlayerManager.PreviousScene != null) player.transform.position = new Vector2(-5, 0);
                break;
            case "level1Room3":
                if (PlayerManager.PreviousScene == "level1Room1")
                {
                    player.transform.position = new Vector2(6, 12);
                }
                else if (PlayerManager.PreviousScene == "level1Room4")
                {
                    player.transform.position = new Vector2(163, 131);
                }
                break;
            case "level1Room4":
                if (PlayerManager.PreviousScene == "level1Room3" || PlayerManager.PreviousScene == "level1Bridge")
                {
                    player.transform.position = new Vector2(10, 0);
                }
                else if (PlayerManager.PreviousScene == "level1Room2")
                {
                    player.transform.position = new Vector2(145, -107);
                }
                break;
            case "level1Bridge":
                if (PlayerManager.PreviousScene == "level1Room1")
                {
                    player.transform.position = new Vector2(-14, 2);
                }
                else if (PlayerManager.PreviousScene == "level1Room2" || PlayerManager.PreviousScene == "level1Room4")
                {
                    player.transform.position = new Vector2(182, 2);
                }
                break;
        }
    }
}
