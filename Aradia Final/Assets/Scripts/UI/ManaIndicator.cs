using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaIndicator : MonoBehaviour
{
    TarotCard card;
    Image image;
    [SerializeField] Sprite manaSprite;
    bool passive = false;
    Color cardColor;

    void Start()
    {
        image = GetComponent<Image>();
        card = TarotCardsHand.GetCard(transform.parent.GetComponent<Image>().overrideSprite.name);
        cardColor = transform.parent.GetComponent<Image>().color;
        if (card.Type == CardType.Passive) passive = true;
    }


    void Update()
    {
        if (!passive)
        {
            if (!card.EnoughMana && !card.OnCooldown)
            {
                Color tempColor = new Color(0, 0, 0);
                // tempColor.a = 0.5f;
                transform.parent.GetComponent<Image>().color = tempColor;

                image.color = new Color(255, 255, 255);
                image.overrideSprite = manaSprite;
            }
            else if (!card.OnCooldown)
            {
                transform.parent.GetComponent<Image>().color = cardColor;
                
                Color tempColor = new Color(0, 0, 0);
                tempColor.a = 0;
                image.color = tempColor;
                image.overrideSprite = null;
            }


        }
    }
}
