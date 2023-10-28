using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Class representing a single card in the game.
/// </summary>
///
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
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
    /// List of parameters
    /// </summary>
    public List<CardParam> cardParams;
    /// <summary>
    /// 3D model of the building associated with the card.
    /// </summary>
    public GameObject building;

    /// <summary>
    /// Initializes a new instance of the Card class with the specified parameters.
    /// </summary>
    public Card(CardColor color, CardType type, Sprite skin, List<CardParam> cardParams, GameObject building)
    {
        this.color = color;
        this.type = type;
        this.skin = skin;
        this.cardParams = cardParams;
        this.building = building;
    }

    /// <summary>
    /// Constructor for copying a card.
    /// </summary>
    /// <param name="card">card to be copied</param>
    public Card(Card card) : this(card.color, card.type, card.skin, card.cardParams, card.building) { }
}

/// <summary>
/// Struct representing a single parameter - its category and value
/// </summary>
[Serializable]
public struct CardParam
{
    public string category;
    public int value;
}
