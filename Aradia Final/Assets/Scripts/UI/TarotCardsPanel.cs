using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TarotCardsPanel : MonoBehaviour
{
    List<GameObject> tarotCardsUIList = new List<GameObject>();

    List<Color> colorList = new List<Color>();

    [SerializeField] GameObject TarotCardUI;

    float margin = 10;
    float marginCumulative;

    bool setup = false;

    // public static bool passiveIconSet = false;


    void Start()
    {
        // colorList.Add(new Color(255f/255f, 207f/255f, 0f/255f, 255f/255f));
        colorList.Add(new Color(255f/255f, 114f/255f, 0f/255f, 255f/255f));
        colorList.Add(new Color(255f/255f, 114f/255f, 0f/255f, 255f/255f));
        colorList.Add(new Color(255f/255f, 0f/255f, 156f/255f, 255f/255f));
        colorList.Add(new Color(184f/255f, 0f/255f, 255f/255f, 255f/255f));

        if (!setup)
        {
            marginCumulative = margin;

            // creates all tarot cards if not created already
            for (int i = 0 + tarotCardsUIList.Count; i < TarotCardsHand.TarotCardsHandList.Count; i++)
            {
                tarotCardsUIList.Add(Instantiate(TarotCardUI));
            }

            // positions and chooses sprite
            for (int i = 0; i < tarotCardsUIList.Count; i++)
            {
                GameObject cardUI = tarotCardsUIList[i];
                TarotCard cardSkill = TarotCardsHand.TarotCardsHandList[i];

                // position
                cardUI.transform.SetParent(this.transform, false);
                RectTransform rectTransform = cardUI.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, 0);
                rectTransform.anchoredPosition += new Vector2(marginCumulative, 0);
                marginCumulative += margin + (rectTransform.sizeDelta.x / 2);

                // Debug.Log(cardUI.GetComponent<Image>());
                cardUI.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("UI/Cards/" + cardSkill.Name);
                // cardUI.GetComponent<Image>().overrideSprite = Resources.Load("UI/Cards/TheEmperor") as Sprite;


                cardUI.GetComponent<Image>().color = colorList[i];
            }

            setup = true;
        }
    }
}
