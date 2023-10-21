using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class representing a card field on the board.
/// </summary>
public class CardField : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Card card;

    /// <summary>
    /// Method triggered when object was clicked.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (card != null)
        {
            return;
        }
        
        card = Game.instance.GetRandomCard();
        GetComponentInChildren<SpriteRenderer>().sprite = card.skin;

        GameObject buildingPrefab = card.building; 
        GameObject buildingObject = Instantiate(buildingPrefab, transform);

        transform.GetChild(1).localScale = new Vector3(.5f, .5f, .5f);
        buildingObject.transform.localPosition = new Vector3(0, 0, 0);
    }
}
