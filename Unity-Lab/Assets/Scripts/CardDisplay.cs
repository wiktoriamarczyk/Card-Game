using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public Card card {
        get => _card;
        set
        {
            _card = value;
            if (_card != null)
            {
                currentImage.sprite = _card.skin;
            }
            else
            {
                currentImage.sprite = defaultImage;
            }
        }
    }

    Card _card;

    [SerializeField] Sprite defaultImage;

    [SerializeField] Image currentImage;

    [SerializeField] bool swapable = true;

    /// <summary>
    /// Method triggered when object was clicked. It places a card on the field if no card is already placed.
    /// </summary>
    /// <param name="eventData">pointer data</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (card != null && swapable)
        {
            Game.instance.SwapCardFromDeckWithCurrentSelected(card);
        }
    }
}
