using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TarotReading : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    static Dictionary<CardName, CardType> cardNameTypeEquivalency = new Dictionary<CardName, CardType>()
    {
        {CardName.Judgement, CardType.Passive},
        {CardName.TheDevil, CardType.Active},
        {CardName.TheEmperor, CardType.Active},
        {CardName.TheEmpress, CardType.Ultimate},
        {CardName.TheFool, CardType.Passive},
        {CardName.TheHierophant, CardType.Passive},
        {CardName.TheMagician, CardType.Passive},
        {CardName.TheSun, CardType.Ultimate},
        {CardName.WheelOfFortune, CardType.Other}
    };

    public static Dictionary<CardName, CardType> CardNameTypeEquivalency
    {
        get { return cardNameTypeEquivalency; }
    }

    /// <summary>
    /// 
    /// </summary>
    static List<CardName> tarotCardsHand = new List<CardName>();
    public static List<CardName> TarotCardsHandList
    {
        get { return tarotCardsHand; }
    }

    [SerializeField] GameObject card1;
    [SerializeField] GameObject card2;
    [SerializeField] GameObject card3;
    [SerializeField] GameObject card4;

    List<GameObject> tarotCardsObjects = new List<GameObject>();

    bool cardsDealt = false;

    [SerializeField] GameObject instructionalText;

    FadeUIGroup globalFader;

    void Start()
    {
        globalFader = GameObject.FindGameObjectWithTag("GlobalFader").GetComponent<FadeUIGroup>();

        tarotCardsObjects.Add(card1);
        tarotCardsObjects.Add(card2);
        tarotCardsObjects.Add(card3);
        tarotCardsObjects.Add(card4);

        //cleans list
        tarotCardsHand.Clear();

        SelectCard(CardType.Passive, 2);
        SelectCard(CardType.Active, 1);
        SelectCard(CardType.Ultimate, 1);

        for (int i = 0; i < tarotCardsHand.Count; i++)
        {
            tarotCardsObjects[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI/Cards/" + tarotCardsHand[i] + "_gold");
        }
    }

    // adds x amount of card names given specified type
    void SelectCard(CardType cardType, int amount)
    {
        for (int i = 0; i < amount;)
        {
            Array values = Enum.GetValues(typeof(CardName));
            System.Random random = new System.Random();
            CardName cardName = (CardName)values.GetValue(random.Next(values.Length));

            if (cardNameTypeEquivalency[cardName] == cardType)
            {
                if (!GetCard(cardName))
                {
                    tarotCardsHand.Add(cardName);
                    i++;
                }
            }
        }
    }

    /// <summary>
    /// Returns true if cardName is in hand
    /// </summary>
    public static bool GetCard(CardName cardName)
    {
        foreach (CardName card in tarotCardsHand) if (card == cardName) return true;
        return false;
    }

    /// called by input system
    public void Continue(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (!cardsDealt)
            {
                AudioManager.Play(AudioClipName.menuClick);
                instructionalText.GetComponent<FadeUIGroup>().Fade();
                foreach (GameObject card in tarotCardsObjects) card.GetComponent<TarotReadingCards>().move = true;
                cardsDealt = true;
                AudioManager.Play(AudioClipName.tarotCardsReadingDeal);
            }
            else
            {
                AudioManager.Play(AudioClipName.menuClick);
                globalFader.Fade(GoToGameplay);
            }
        }
    }

    void GoToGameplay()
    {
        AudioManager.StopLoop(AudioClipName.menuMusic);
        PlayerManager.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("level1Room1");
    }

    public void QuitToTitle()
    {
        AudioManager.Play(AudioClipName.menuClick);
        globalFader.Fade(SceneManager.LoadScene, "title");
    }
}
