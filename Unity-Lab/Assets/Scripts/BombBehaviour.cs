using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class representing the bomb button behaviour.
/// </summary>
public class BombBehaviour : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TMP_Text bombCounterDisplay;
    int bombCounter;

    void Awake()
    {
        button.onClick.AddListener(DeleteSelectedCard);
        bombCounter = LevelSettings.instance.bombCount;
        bombCounterDisplay.SetText(bombCounter.ToString());
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(DeleteSelectedCard);
    }

    bool AreThereAnyBombLeft()
    {
        if (bombCounter <= 0 || (Game.instance.GetCardsCount() <= 0 && Game.instance.currentCard == null))
        {
            return false;
        }
        return true;
    }

    void DeleteSelectedCard()
    {
        if (!AreThereAnyBombLeft())
        {
            return;
        }
        Game.instance.SetNewSelectedCardRandomly();
        Game.instance.cardsLeft--;
        bombCounter--;
        if (!AreThereAnyBombLeft())
        {
            bombCounterDisplay.color = Color.red;
        }
        bombCounterDisplay.SetText(bombCounter.ToString());
    }
}
