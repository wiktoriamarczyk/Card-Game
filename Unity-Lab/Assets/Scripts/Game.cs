using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

/// <summary>
/// Singleton class responsible for managing the game and its objects (board, cards etc.).
/// </summary>
public class Game : MonoBehaviour
{
    [SerializeField] List<Card> cards;
    [SerializeField] Board board;
    
    public static Game instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Game>();
            }

            if (_instance == null && Application.isPlaying)
            {
                GameObject newGO = new(typeof(Game).Name);
                _instance = newGO.AddComponent<Game>();
                Debug.Log("#Singleton# New instance: " + typeof(Game).Name, _instance);
            }

            return _instance;
        }
    }

    static Game _instance;

    public static bool instanceExists
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Game>();
            }

            return _instance != null;
        }
    }

    void Awake()
    {
        if (instanceExists && instance != this)
        {
            Destroy(gameObject);
        }
        _instance = this;


    }
    
    /// <summary>
    /// Returns a random card from the list
    /// </summary>
    /// <returns></returns>
    public Card GetRandomCard()
    {
        int rand = Random.Range(0, cards.Count);
        var card = cards[rand];
        cards.Remove(card);
        return card;
    }

    public void AddCardToDeck(CardColor color, CardType type, Sprite skin, List<CardParam> cardParams, GameObject building)
    {
        Card card = new Card(color, type, skin, cardParams, building);
        cards.Add(card);
    }

    public void UpdateCardInDeck(CardColor color, CardType type)
    {
        Card cardToUpdate = GetCardFromDeck(color, type);
        Card cardFoundInDeck = cards.Find(card => card == cardToUpdate);
        if (cardFoundInDeck != null)
        {
            cardFoundInDeck = cardToUpdate;
        }
    }

    public void DeleteCardFromDeck(CardColor color, CardType type)
    {
        Card cardToDelete = GetCardFromDeck(color, type);
        cards.Remove(cardToDelete);
    }

    Card GetCardFromDeck(CardColor color, CardType type)
    {
        return cards.Find(card => card.color == color && card.type == type);
    }
}
