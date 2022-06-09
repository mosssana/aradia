using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownIndicator : MonoBehaviour
{
    TarotCard card;
    Text text;
    bool passive = false;
    Color cardColor;

    void Start()
    {
        card = TarotCardsHand.GetCard(transform.parent.GetComponent<Image>().overrideSprite.name);
        cardColor = transform.parent.GetComponent<Image>().color;

        text = GetComponent<Text>();
        text.color = cardColor;

        if (card.Type == CardType.Passive) passive = true;
        text.text = "";
    }


    void Update()
    {
        if (!passive)
        {
            if (card.OnCooldown)
            {
                if (card.Cooldown > 0)
                {
                    // tint gris + counter
                    Color tempColor = new Color(0, 0, 0);
                    // tempColor.a = 0.5f;
                    transform.parent.GetComponent<Image>().color = tempColor; ;

                    text.text = ((int)card.Cooldown).ToString();
                }
            }
            else
            {
                transform.parent.GetComponent<Image>().color = cardColor;
                text.text = "";
            }


        }
    }
}
