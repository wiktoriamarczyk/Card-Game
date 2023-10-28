using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class representing a card field on the board. It is a space on the board where a card can be placed.
/// </summary>
public class CardField : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Card card;

    /// <summary>
    /// Method triggered when object was clicked. It places a card on the field if no card is already placed.
    /// </summary>
    /// <param name="eventData">pointer data</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (card != null)
        {
            return;
        }

        card = Game.instance.currentCard;
        Game.instance.SetNewSelectedCardRandomly();

        GetComponentInChildren<SpriteRenderer>().sprite = card.skin;
        GameObject buildingPrefab = card.building;
        GameObject buildingObject = Instantiate(buildingPrefab, transform);

        transform.GetChild(1).localScale = new Vector3(.5f, .5f, .5f);
        buildingObject.transform.localPosition = new Vector3(0, 0, 0);
    }
}
