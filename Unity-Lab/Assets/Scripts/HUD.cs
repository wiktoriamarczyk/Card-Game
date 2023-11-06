using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static DifficultySettings;

/// <summary>
/// Class representing the HUD of the game displaying current game information.
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nickname;
    [SerializeField] Image lvlIcon;
    [SerializeField] TextMeshProUGUI bombsLeft;
    [SerializeField] GameObject parameterPrefab;
    [SerializeField] GameObject parametersContener;

    List<LevelParameter> lvlParameters = new List<LevelParameter>();

    public int bombsLeftValue
    {
        get => int.Parse(bombsLeft.text);
        set => bombsLeft.text = value.ToString();
    }

    public void Initialize(string nick, Sprite icon, int bombsCount, List<LevelParameter> parameters)
    {
        nickname.text = nick;
        lvlIcon.sprite = icon;
        bombsLeftValue = bombsCount;
        lvlParameters = parameters;

        foreach (var parameter in lvlParameters)
        {
            var parameterDisplay = Instantiate(parameterPrefab, parametersContener.transform).GetComponent<ParameterDisplay>();
            parameterDisplay.parameterName = parameter.name;
            parameterDisplay.minPointsValue = parameter.minValue;
            parameterDisplay.maxPointsValue = parameter.maxValue;
            parameterDisplay.parameterIcon.sprite = parameter.icon;
            parameterDisplay.playerPoints = 0;
        }
    }
}
