using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaDropsPanel : MonoBehaviour
{
    List<GameObject> manaDropsList = new List<GameObject>();

    [SerializeField] GameObject manaDropUI;
    [SerializeField] Sprite manaDropEmpty;
    [SerializeField] Sprite manaDropFull;

    float margin = 2;
    float marginCumulative;

    void Start()
    {
        EventManager.AddListener(EventName.PlayerManaChanged, UpdateBar);
        UpdateBar(PlayerDamageIndex.CurrentMana, PlayerDamageIndex.TotalMana);
    }

    void UpdateBar(float mana, float totalMana)
    {
        marginCumulative = margin;

        // creates all mana drops
        // adds mana drops if increased total mana
        for (int i = 0 + manaDropsList.Count; i < totalMana; i++)
        {
            manaDropsList.Add(Instantiate(manaDropUI));
        }

        // positions and refills drops
        for (int i = 0; i < manaDropsList.Count; i++)
        {
            GameObject drop = manaDropsList[i];

            // position
            drop.transform.SetParent(this.transform, false);
            RectTransform rectTransform = drop.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-805, 465);
            rectTransform.anchoredPosition += new Vector2(marginCumulative, 0);
            marginCumulative += margin + rectTransform.sizeDelta.x;

            drop.GetComponent<Image>().overrideSprite = DetermineSprite(mana, i);
        }
    }

    Sprite DetermineSprite(float mana, int index)
    {   
        if (mana > index) return manaDropFull;
        else return manaDropEmpty;
    }
}
