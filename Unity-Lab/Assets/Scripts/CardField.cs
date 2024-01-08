using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class representing a card field on the board. It is a space on the board where a card can be placed.
/// </summary>
public class CardField : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Card card;
    public int multiplier = 1;

    /// <summary>
    /// Method setting the card multiplier to 2.
    /// </summary>
    public void SetMultiplier2()
    {
        multiplier = 2;
        GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MUL_X2");
        // set sprite size
    }

    /// <summary>
    /// Method setting the card multiplier to 3.
    /// </summary>
    public void SetMultiplier3()
    {
        multiplier = 3;
        GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("MUL_X3");
    }

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
        Building buildingComponent = buildingObject.AddComponent<Building>();
        buildingComponent.SetCard(card);

        transform.GetChild(1).localScale = new Vector3(.5f, .5f, .5f);
        buildingObject.transform.localPosition = new Vector3(0, 0, 0);
        // createa a copy of the list of possible positions
        List<Vector2> tempList = new List<Vector2>(card.positions);
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
            int positionIndex = Random.Range(0, tempList.Count);
            Vector2 position = tempList[positionIndex];
            treeObject.transform.localScale = new Vector3(.07f, .07f, .07f);
            treeObject.transform.localPosition = new Vector3(position.x, 0, position.y);
            // Remove the position from the list of possible positions
            tempList.RemoveAt(positionIndex);
        }
        if (fountainChance < 101)
        {
            if (tempList.Count > 0)
            {
                GameObject fountainPrefab = Game.instance.fountainPrefab;
                GameObject fountainObject = Instantiate(fountainPrefab, transform);
                // select a random position for the fountain from the list of possible positions
                int positionIndex = Random.Range(0, tempList.Count);
                Vector2 position = tempList[positionIndex];
                fountainObject.transform.localScale = new Vector3(.015f, .015f, .015f);
                fountainObject.transform.localPosition = new Vector3(position.x, 0.012f, position.y);
            }
        }

        Game.instance.cardsLeft--;

        // add points for card parameters to player
        foreach (var param in card.cardParamsValues)
        {
            var param1 = param;
            param1.paramValue *= multiplier;
            Game.instance.AddPointsForParameter(param1);
        }

        // rebuild the navmesh
        Game.instance.RebuildNavMesh();
    }
}
