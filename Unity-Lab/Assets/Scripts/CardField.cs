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

        // place a card on the field
        card = Game.instance.currentCard;
        Game.instance.SetNewSelectedCardRandomly();

        GetComponentInChildren<SpriteRenderer>().sprite = card.skin;
        GameObject buildingPrefab = card.building;
        GameObject buildingObject = Instantiate(buildingPrefab, transform);

        transform.GetChild(1).localScale = new Vector3(.5f, .5f, .5f);
        buildingObject.transform.localPosition = new Vector3(0, 0, 0);

        // decide whether to place a tree and/or a fountain
        int treeChance = Random.Range(0, 100);
        int fountainChance = Random.Range(0, 100);
        if (treeChance < 101)
        {
            // select a random tree prefab
            int treeIndex = Random.Range(0, Game.instance.treePrefabs.Count);
            GameObject treePrefab = Game.instance.treePrefabs[treeIndex];
            GameObject treeObject = Instantiate(treePrefab, transform);
            // select a random position for the tree from the list of possible positions
            int positionIndex = Random.Range(0, card.positions.Count);
            Vector2 position = card.positions[positionIndex];
            treeObject.transform.localScale = new Vector3(.07f, .07f, .07f);
            treeObject.transform.localPosition = new Vector3(position.x, 0, position.y);
            // Remove the position from the list of possible positions
            card.positions.RemoveAt(positionIndex);
        }
        if (fountainChance < 60)
        {
            if (card.positions.Count > 0)
            {
                // TODO Place a fountain
            }
        }

        Game.instance.cardsLeft--;

        // add points for card parameters to player
        foreach (var param in card.cardParamsValues)
        {
            Game.instance.AddPointsForParameter(param);
        }

        // rebuild the navmesh
        Game.instance.RebuildNavMesh();
    }
}
