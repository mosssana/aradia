using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTypeIconIndicator : MonoBehaviour
{
    TarotCard card;
    // Start is called before the first frame update
    void Start()
    {
        card = TarotCardsHand.GetCard(transform.parent.GetComponent<Image>().overrideSprite.name);

        switch (card.Type)
        {
            case CardType.Passive:
                // if (!TarotCardsPanel.passiveIconSet)
                // {
                //     GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("UI/passive1");
                //     TarotCardsPanel.passiveIconSet = true;
                // }
                // else GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("UI/passive2");
                GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("UI/passive2");
                break;
            case CardType.Active:
                GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("UI/active");
                break;
            case CardType.Ultimate:
                GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("UI/ultimate");
                break;
        }
    }
}
