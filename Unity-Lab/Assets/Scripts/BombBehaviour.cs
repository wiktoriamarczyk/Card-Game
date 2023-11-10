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
    [SerializeField] int bombCounter = 0;

    void Awake()
    {
        button.onClick.AddListener(DeleteSelectedCard);
        bombCounterDisplay.SetText(bombCounter.ToString());
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(DeleteSelectedCard);
    }

    void DeleteSelectedCard()
    {
        if (bombCounter <= 0 || Game.instance.GetCardsCount() <= 0)
        {
            return;
        }
        Game.instance.SetNewSelectedCardRandomly();
        bombCounter--;
        bombCounterDisplay.SetText(bombCounter.ToString());
    }
}
