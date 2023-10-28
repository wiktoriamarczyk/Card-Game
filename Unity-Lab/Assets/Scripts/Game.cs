using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Card;

/// <summary>
/// Singleton class responsible for managing the game and its objects (board, cards etc.).
/// </summary>
public class Game : MonoBehaviour
{
    [SerializeField] List<Card> cards;
    [SerializeField] Board board;
    [SerializeField] CardDisplay currentCardDisplay;
    [SerializeField] GameObject cardsDisplayContener;
    [SerializeField] GameObject cardsDisplayPrefab;

    List<CardDisplay> cardsDisplay = new List<CardDisplay>();

    /// <summary>
    /// Currently selected card.
    /// </summary>
    public Card currentCard {
        get => _currentCard;
        set
        {
            _currentCard = value;
            currentCardDisplay.card = _currentCard;
        }
    }

    Card _currentCard;

    public RandomStrategy randomStrategy;

    /// <summary>
    /// Returns instance of the Game class.
    /// </summary>
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

    /// <summary>
    /// Checks if instance of the Game class exists.
    /// </summary>
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

    private void Awake()
    {
        if (instanceExists && instance != this)
        {
            Destroy(gameObject);
        }
        _instance = this;

        EqualChanceRandom equalChanceRandom = new EqualChanceRandom();
        OnlyBlacks onlyBlacks = new OnlyBlacks();
        OnlyReds onlyReds = new OnlyReds();
        this.randomStrategy = equalChanceRandom;
        currentCard = GetRandomCard();
        InitializeDeck();
    }

    /// <summary>
    /// Sets a new currently selected card.
    /// </summary>
    /// <param name="card">new card to be selected</param>
    public void SetNewSelectedCard(Card card)
    {
        currentCard = card;
    }
    /// <summary>
    /// Changes the random strategy of choosing a card.
    /// </summary>
    /// <param name="randomStrategy">random strategy object</param>
    public void ChangeRandomStrategy(RandomStrategy randomStrategy)
    {
        this.randomStrategy = randomStrategy;
    }

    /// Sets a new selected card randomly from the deck.
    /// </summary>
    public void SetNewSelectedCardRandomly()
    {
        SetNewSelectedCard(GetRandomCard());
    }

    /// <summary>
    /// Returns a random card from the list (deck of cards) based on the random strategy.
    /// </summary>
    /// <returns>The chosen card</returns>
    public Card GetRandomCard()
    {
        if (cards.Count <= 0)
        {
            return null;
        }

        var card = randomStrategy.GetRandomCard(cards);

        var cardDisplay = cardsDisplay.Find(cardDisplay => cardDisplay.card == card && cardDisplay.gameObject.activeSelf);
        if (cardDisplay != null)
        {
            cardDisplay.gameObject.SetActive(false);
        }


        return card;
    }

    /// <summary>
    /// Adds a new card to the deck.
    /// </summary>
    /// <param name="color">color of the card</param>
    /// <param name="type">type of the card</param>
    /// <param name="skin">skin of the card</param>
    /// <param name="cardParams">parameters of the card</param>
    /// <param name="building">building associated with the card</param>
    public void AddCardToDeck(CardColor color, CardType type, Sprite skin, List<CardParam> cardParams, GameObject building)
    {
        Card card = new Card(color, type, skin, cardParams, building);
        cards.Add(card);
    }

    /// <summary>
    /// Updates an existing card in the deck with new data.
    /// </summary>
    public void UpdateCardInDeck(CardColor color, CardType type, Sprite skin, List<CardParam> cardParams, GameObject building)
    {
        Card cardToUpdate = GetCardFromDeck(color, type);
        if (cardToUpdate == null)
        {
            return;
        }
        cardToUpdate.skin = skin;
        cardToUpdate.cardParams = cardParams;
        cardToUpdate.building = building;
    }

    /// <summary>
    /// Deletes a card from the deck.
    /// </summary>
    /// <param name="color">color of the card</param>
    /// <param name="type">type of the card</param>
    public void DeleteCardFromDeck(CardColor color, CardType type)
    {
        Card cardToDelete = GetCardFromDeck(color, type);
        cards.Remove(cardToDelete);
    }

    /// <summary>
    /// Swaps a card from the deck with the current selected card.
    /// </summary>
    /// <param name="card">card from deck</param>
    public void SwapCardFromDeckWithCurrentSelected(Card card)
    {
        var deckIndex = cards.IndexOf(card);
        var displayIndex = cardsDisplay.FindLastIndex(c => c.card == card);
        if (deckIndex >= 0 && displayIndex >= 0 && currentCard != null)
        {
            if (cards[deckIndex] == cardsDisplay[displayIndex].card)
            {
                cards[deckIndex] = currentCard;
                cardsDisplay[displayIndex].card = currentCard;

                SetNewSelectedCard(card);
            }
        }
    }

    /// <summary>
    /// Retrieves a card from the deck based on its color and type.
    /// </summary>
    private Card GetCardFromDeck(CardColor color, CardType type)
    {
        return cards.Find(card => card.color == color && card.type == type);
    }

    /// <summary>
    /// Initializes the deck of cards.
    /// </summary>
    private void InitializeDeck()
    {
        for (int i = 0; i < cards.Count; ++i)
        {
            GameObject imageObject = Instantiate(cardsDisplayPrefab, cardsDisplayContener.transform);
            CardDisplay cardDisplay = imageObject.GetComponent<CardDisplay>();
            cardDisplay.card = cards[i];
            cardsDisplay.Add(cardDisplay);
        }
    }
}
