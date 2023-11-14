using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Card;
using static LevelSettings;

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
    [SerializeField] NavMeshSurface navMeshSurface;

    [SerializeField] Toggle changePerspective;
    [SerializeField] FirstPersonMovement fps;

    [SerializeField] GameOverScreen gameOverScreen;
    [SerializeField] HUD hud;

    List<CardDisplay> cardsDisplay = new List<CardDisplay>();

    /* public variables */
    public int cardsLeft;

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
                UnityEngine.Debug.Log("#Singleton# New instance: " + typeof(Game).Name, _instance);
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

    /* private variables */
    private bool isAlive = true;

    private void Awake()
    {
        if (instanceExists && instance != this)
        {
            Destroy(gameObject);
        }
        _instance = this;

        cardsLeft = cards.Count;

        EqualChanceRandom equalChanceRandom = new EqualChanceRandom();
        OnlyBlacks onlyBlacks = new OnlyBlacks();
        OnlyReds onlyReds = new OnlyReds();
        this.randomStrategy = equalChanceRandom;

        currentCard = GetRandomCard();
        InitializeDeck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && fps.enabled)
        {
            changePerspective.isOn = false;
        }

       if(isAlive) CheckIfAlive();
    }

    /// <summary>
    /// Delegates the gameover screen function with according parameter, depending on whether the player has won
    /// </summary>
    /// <param name="win">True if the player won the game, false otherwise.</param>

    private void GameOver(bool win)
    {
        if (win)
        {
            gameOverScreen.Setup(true);
            return;
        }
        else
        {
            gameOverScreen.Setup(false);
            return;
        }
    }

    /// <summary>
    /// Checks if the player is still alive in the game, possibly involving more complex logic.
    /// </summary>
    private void CheckIfAlive()
    {
        //maybe some more complicated logic here

        if (cardsLeft <= 0 && currentCard == null)
        {
            GameOver(hud.AreParametersSatisfied());
            isAlive = false;

        }
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
    /// <param name="cardParamsValues">parameters of the card</param>
    /// <param name="building">building associated with the card</param>
    public void AddCardToDeck(CardColor color, CardType type, Sprite skin, List<CardParamValue> cardParamsValues, GameObject building)
    {
        Card card = new Card(color, type, skin, cardParamsValues, building);
        cards.Add(card);
    }

    /// <summary>
    /// Updates an existing card in the deck with new data.
    /// </summary>
    public void UpdateCardInDeck(CardColor color, CardType type, Sprite skin, List<CardParamValue> cardParamsValues, GameObject building)
    {
        Card cardToUpdate = GetCardFromDeck(color, type);
        if (cardToUpdate == null)
        {
            return;
        }
        cardToUpdate.skin = skin;
        cardToUpdate.cardParamsValues = cardParamsValues;
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
    /// Swaps a random card from the deck with the current selected card.
    /// </summary>
    public void SwapRandomCardFromDeckWithCurrentSelected()
    {
        List<Card> cardsFromDeck = new List<Card>();
        foreach (var cardDisplay in cardsDisplay)
        {
            cardsFromDeck.Add(cardDisplay.card);
        }

        Card randomCard = randomStrategy.GetRandomCard(cardsFromDeck);

        var deckIndex = cards.IndexOf(randomCard);
        var displayIndex = cardsDisplay.FindLastIndex(c => c.card == randomCard);
        if (deckIndex >= 0 && displayIndex >= 0 && currentCard != null)
        {
            if (cards[deckIndex] == cardsDisplay[displayIndex].card)
            {
                cards[deckIndex] = currentCard;
                cardsDisplay[displayIndex].card = currentCard;

                SetNewSelectedCard(randomCard);
            }
        }
    }

    /// <summary>
    /// Returns the number of cards
    /// </summary>
    /// <returns>cards count</returns>
    public int GetCardsCount()
    {
        return cards.Count;
    }

    /// <summary>
    /// Adds points to the HUD for a given parameter.
    /// </summary>
    /// <param name="cardParameters">specific parameter</param>
    public void AddPointsForParameter(CardParamValue cardParameters)
    {
        hud.AddPointsForParameter(cardParameters);
    }

    /// <summary>
    /// Rebuilds the navmesh.
    /// </summary>
    public void RebuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
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
