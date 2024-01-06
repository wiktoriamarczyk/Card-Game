using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Card card;

    public void OnPointerClick(PointerEventData eventData)
    {
        var infoPanel = Game.instance.GetInfoPanelObject();
        infoPanel.Display(card.buildingSprite, card.name, card.description);
    }

    public void SetCard(Card card)
    {
        this.card = card;
    }
}
