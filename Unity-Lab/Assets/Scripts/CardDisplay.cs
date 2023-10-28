using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class representing a card displayed on the screen. It visualizes player's card in the deck.
/// </summary>
public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Reference to the card.
    /// </summary>
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
    /// Method triggered when object was clicked. It swaps the card with the one selected on the board.
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
