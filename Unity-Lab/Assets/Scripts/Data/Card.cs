using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Class representing a single card in the game.
/// </summary>
///
public class Card : MonoBehaviour
{
    /// <summary>
    /// Defines the color of the card.
    /// </summary>
    public enum CardColor { SPADES, HEARTS, DIAMONDS, CLUBS, NONE };
    /// <summary>
    /// Defines the type of the card.
    /// </summary>
    public enum CardType { ACE, N2, N3, N4, N5, N6, N7, N8, N9, N10, JACK, QUEEN, KING, JOKER };

    /// <summary>
    /// Name of the card.
    /// </summary>
    public new string name; 
    /// <summary>
    /// Color of the card.
    /// </summary>
    public CardColor color;
    /// <summary>
    /// Type of the card.
    /// </summary>
    public CardType type;
    /// <summary>
    /// Skin of the card.
    /// </summary>
    public Sprite skin;
    /// <summary>
    /// Sprite renderer of the card.
    /// </summary>
    public SpriteRenderer spriteRenderer;
    /// <summary>
    /// Path to the sprite.
    /// </summary>
    public string skinPath;
    /// <summary>
    /// List of parameters
    /// </summary>
    public List<CardParamValue> cardParamsValues;
    /// <summary>
    /// 3D model of the building associated with the card.
    /// </summary>
    public GameObject building;
    /// <summary>
    /// List of possible tree and fountain positions.
    /// </summary>
    public List<Vector2> positions;

    /// <summary>
    /// Initializes a new instance of the Card class with the specified parameters.
    /// </summary>
    public Card(CardColor color, CardType type, Sprite skin, List<CardParamValue> cardParams, GameObject building)
    {
        this.color = color;
        this.type = type;
        this.skin = skin;
        this.cardParamsValues = cardParams;
        this.building = building;
    }

    /// <summary>
    /// Constructor for copying a card.
    /// </summary>
    /// <param name="card">card to be copied</param>
    public Card(Card card) : this(card.color, card.type, card.skin, card.cardParamsValues, card.building) { }

    public Card() { }
}

/// <summary>
/// Struct representing a single parameter - its category and value
/// </summary>
[Serializable]
public struct CardParamValue
{
    public string paramName;
    public int paramValue;
}
