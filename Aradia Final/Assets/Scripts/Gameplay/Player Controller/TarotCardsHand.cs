using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TarotCardsHand : MonoBehaviour
{
    #region Fields and Properties

    /// <summary>
    /// 
    /// </summary>
    static List<TarotCard> tarotCardsHand = new List<TarotCard>();
    public static List<TarotCard> TarotCardsHandList
    {
        get { return tarotCardsHand; }
        set { tarotCardsHand = value; }
    }

    static float cardsTotalDifficultyScore = 0;
    static float cardsTotalLuckScore = 0;

    public static float CardsDifficultyScore
    {
        get { return cardsTotalDifficultyScore; }
    }

    public static float CardsTotalLuckScore
    {
        get { return cardsTotalLuckScore; }
    }

    #endregion

    void Awake()
    {
        // Adds all cards based on list provided by TarotReading
        if (tarotCardsHand.Count == 0)
        {
            if (TarotReading.TarotCardsHandList.Count > 0)
                foreach (CardName cardName in TarotReading.TarotCardsHandList) AddCard(cardName);
            else
            {
                // AddCard(CardName.TheMagician);
                // AddCard(CardName.Judgement);
                // AddCard(CardName.TheEmperor);
                // AddCard(CardName.TheSun);

                AddCard(CardName.TheHierophant);
                AddCard(CardName.TheFool);
                AddCard(CardName.TheDevil);
                AddCard(CardName.TheEmpress);
            }
            // determines cards combined difficulty score if it hasn't already been calculated
            if (cardsTotalDifficultyScore == 0)
            {
                foreach (TarotCard card in tarotCardsHand) cardsTotalDifficultyScore += card.DifficultyScore;
            }

            // determines cards combined luck score if it hasn't already been calculated
            if (cardsTotalLuckScore == 0)
            {
                foreach (TarotCard card in tarotCardsHand) cardsTotalLuckScore += card.LuckScore;
            }

            Debug.Log("Difficulty Score: " + cardsTotalDifficultyScore);
            Debug.Log("Luck Score: " + cardsTotalLuckScore);
        }
    }

    #region Private Methods

    /// <summary>
    /// Attaches card's script to player gameObject and adds card to tarotCardsHand list
    /// </summary>
    void AddCard(CardName cardName)
    {
        switch (cardName)
        {
            case CardName.TheFool:
                gameObject.AddComponent<TheFool>().AddCardToHand();
                break;
            case CardName.TheMagician:
                gameObject.AddComponent<TheMagician>().AddCardToHand();
                break;
            case CardName.Judgement:
                gameObject.AddComponent<Judgement>().AddCardToHand();
                break;
            case CardName.TheHierophant:
                gameObject.AddComponent<TheHierophant>().AddCardToHand();
                break;
            case CardName.TheDevil:
                gameObject.AddComponent<TheDevil>().AddCardToHand();
                break;
            case CardName.TheEmperor:
                gameObject.AddComponent<TheEmperor>().AddCardToHand();
                break;
            case CardName.TheEmpress:
                gameObject.AddComponent<TheEmpress>().AddCardToHand();
                break;
            case CardName.TheSun:
                gameObject.AddComponent<TheSun>().AddCardToHand();
                break;
        }
    }

    #endregion

    #region Public Methods

    public static void ClearHand()
    {
        tarotCardsHand.Clear();
        cardsTotalDifficultyScore = 0;
        cardsTotalLuckScore = 0;
    }


    /// <summary>
    /// Returns card by Type from hand
    /// </summary>
    public static TarotCard GetCard(CardType cardType)
    {
        foreach (TarotCard card in tarotCardsHand) if (card.Type == cardType) return card;
        return null;
    }

    /// <summary>
    /// Returns card by Name from hand
    /// </summary>
    public static TarotCard GetCard(CardName cardName)
    {
        foreach (TarotCard card in tarotCardsHand) if (card.Name == cardName) return card;
        return null;
    }

    /// <summary>
    /// Returns card by Name in String format from hand
    /// </summary>
    public static TarotCard GetCard(string stringName)
    {
        foreach (TarotCard card in tarotCardsHand) if (card.Name.ToString() == stringName) return card;
        return null;
    }

    void RemoveCard(CardName cardName)
    {
        foreach (TarotCard card in tarotCardsHand) if (card.Name == cardName) tarotCardsHand.Remove(card);
    }

    #endregion

    #region  Input Methods

    /// called by input system
    public void ActiveSkillPerform(InputAction.CallbackContext ctx)
    {
        GetCard(CardType.Active).SkillPerform(ctx);
    }

    // called by animation event
    public void ActiveSkillFire()
    {
        GetCard(CardType.Active).SkillFire();
    }

    /// called by input system
    public void UltimateSkillPerform(InputAction.CallbackContext ctx)
    {
        GetCard(CardType.Ultimate).SkillPerform(ctx);
    }

    // called by animation event
    public void UltimateSkillFire()
    {
        GetCard(CardType.Ultimate).SkillFire();
    }

    public void ActiveSkillPerformCancel()
    {
        GetCard(CardType.Active).SkillPerformCancel();
    }

    #endregion

}
