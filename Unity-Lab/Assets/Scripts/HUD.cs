using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static LevelSettings;

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

    private void Awake()
    {
        LevelSettings lvlSettings = LevelSettings.instance;
        Initialize(lvlSettings.nickname, lvlSettings.lvlIcon, lvlSettings.bombCount, lvlSettings.levelParameters);
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
            parameterDisplay.playerPoints = 0;
        }
    }

    public int GetParameterPoints(string parameterName)
    {
        return lvlParameters.Find(x => x.name == parameterName).playerValue;
    }

    public bool AreParametersSatisfied()
    {
        foreach (var parameter in lvlParameters)
        {
            if (parameter.playerValue < parameter.minValue)
            {
                return false;
            }
        }

        return true;
    }
}
