using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Class responsible for displaying current selected card parameters on the screen.
/// </summary>
public class CurrentSelectedCardParametersDisplay : MonoBehaviour
{
    [SerializeField] CardDisplay cardDisplay;
    [SerializeField] GameObject parameterValuesContainer;
    List<TextMeshProUGUI> parameterValues = new List<TextMeshProUGUI>();

    void Awake()
    {
        parameterValues = parameterValuesContainer.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        cardDisplay.onCardChanged += FillParameterValues;
    }

    void OnDestroy()
    {
        cardDisplay.onCardChanged -= FillParameterValues;
    }

    void FillParameterValues(Card card)
    {
        if (card == null)
        {
            parameterValues.ForEach(x => x.text = "0");
            return;
        }
        foreach (var cardParam in card.cardParamsValues)
        {
            foreach (var paramValue in parameterValues)
            {
                if (cardParam.paramName == paramValue.gameObject.name)
                {
                    paramValue.text = cardParam.paramValue.ToString();
                }
            }
        }
    }
}
