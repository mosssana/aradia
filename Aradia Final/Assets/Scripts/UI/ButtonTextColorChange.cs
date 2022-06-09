using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonTextColorChange : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void Highlight()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(231f / 255f, 197f / 255f, 36f / 255f, 255f / 255f);
        gameObject.transform.Find("ArrowLeft").gameObject.SetActive(true);
        gameObject.transform.Find("ArrowRight").gameObject.SetActive(true);
        AudioManager.Play(AudioClipName.menuNavigation);
    }

    public void DeHighight()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        gameObject.transform.Find("ArrowLeft").gameObject.SetActive(false);
        gameObject.transform.Find("ArrowRight").gameObject.SetActive(false);
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        Highlight();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        DeHighight();
    }

    void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GetComponent<Button>().Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EventSystem.current != null) EventSystem.current.SetSelectedGameObject(null);
    }

}