using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// An interface to implement different strategies of choosing a random card
/// </summary>
public interface RandomStrategy
{
    /// <summary>
    /// Returns a random card from the list
    /// </summary>
    /// <param name="list">list of cards</param>
    /// <returns></returns>
    public Card GetRandomCard(List<Card> list);
}

/// <summary>
/// Strategy in which all cards have an equal chance to be chosen 
/// </summary>
class EqualChanceRandom : RandomStrategy
{
    /// <summary>
    /// Returns a random card from the list
    /// </summary>
    /// <param name="list">list of cards</param>
    /// <returns></returns>
    public Card GetRandomCard(List<Card> list)
    {
        int rand = Random.Range(0, list.Count);
        var card = list[rand];
        list.Remove(card);
        return card;
    }
}

/// <summary>
/// Strategy in which only black cards have a chance to be chosen
/// </summary>
class OnlyBlacks : RandomStrategy
{
    /// <summary>
    /// Returns a random black card from the list
    /// </summary>
    /// <param name="list">list of cards</param>
    /// <returns></returns>
    public Card GetRandomCard(List<Card> list)
    {
        // create a temporary list of black cards
        var blackList = new List<Card>();
        foreach (var element in list)
        {
            if (element.color == Card.CardColor.SPADES || element.color == Card.CardColor.CLUBS)
            {
                blackList.Add(element);
            }
        }
        // choose a random card from the temporary list
        int rand = Random.Range(0, blackList.Count);
        var card = blackList[rand];
        list.Remove(card);
        return card;
    }
}

/// <summary>
/// Strategy in which only red cards have a chance to be chosen
/// </summary>
class OnlyReds : RandomStrategy
{
    /// <summary>
    /// Returns a random red card from the list
    /// </summary>
    /// <param name="list">list of cards</param>
    /// <returns></returns>
    public Card GetRandomCard(List<Card> list)
    {
        // create a temporary list of red cards
        var redList = new List<Card>();
        foreach (var element in list)
        {
            if (element.color == Card.CardColor.DIAMONDS || element.color == Card.CardColor.HEARTS)
            {
                redList.Add(element);
            }
        }
        // choose a random card from the temporary list
        int rand = Random.Range(0, redList.Count);
        var card = redList[rand];
        list.Remove(card);
        return card;
    }
}