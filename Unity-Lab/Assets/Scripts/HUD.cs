using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static LevelSettings;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// Class representing the HUD of the game displaying current game informations.
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nickname;
    [SerializeField] Image lvlIcon;
    [SerializeField] TextMeshProUGUI bombsLeft;
    [SerializeField] GameObject parameterPrefab;
    [SerializeField] GameObject parametersContener;
    [SerializeField] Button endGameButton;

    List<LevelParameter> lvlParameters = new List<LevelParameter>();

    public int bombsLeftValue
    {
        get => int.Parse(bombsLeft.text);
        set => bombsLeft.text = value.ToString();
    }

    private void Awake()
    {
        LevelSettings lvlSettings = LevelSettings.instance;

        foreach (var param in lvlSettings.levelParameters)
        {
            param.playerValue = 0;
        }


        Initialize(lvlSettings.nickname, lvlSettings.lvlIcon, lvlSettings.bombCount, lvlSettings.levelParameters);
        endGameButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void OnDestroy()
    {
        endGameButton.onClick.RemoveListener(ReturnToMainMenu);
    }

    /// <summary>
    /// Initializes HUD panel
    /// </summary>
    /// <param name="nick">player's nickname</param>
    /// <param name="icon">icon of chosen level</param>
    /// <param name="bombsCount">number of bombs of this level</param>
    /// <param name="parameters">level parameters</param>
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
            parameterDisplay.parameterIcon.sprite = parameter.icon;
        }
    }

    /// <summary>
    /// Returns specific parameter points value
    /// </summary>
    /// <param name="parameterName">name of the parameter</param>
    /// <returns></returns>
    public int GetParameterPoints(string parameterName)
    {
        return lvlParameters.Find(x => x.name == parameterName).playerValue;
    }

    public bool AreParametersSatisfied()
    {
        foreach (var parameter in lvlParameters)
        {
            if (parameter.playerValue < parameter.minValue || parameter.playerValue > parameter.maxValue)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Adds points to player's score from specific parameter points value
    /// </summary>
    /// <param name="cardParameters">card parameters</param>
    public void AddPointsForParameter(CardParamValue cardParameters)
    {
        LevelParameter parameter = lvlParameters.Find(x => x.name == cardParameters.paramName);
        if (parameter != null)
        {
            parameter.playerValue += cardParameters.paramValue;
            List<ParameterDisplay> parameterDisplays = parametersContener.GetComponentsInChildren<ParameterDisplay>().ToList();
            ParameterDisplay parameterDisplay = parameterDisplays.Find(x => x.parameterName == cardParameters.paramName);
            if (parameterDisplay != null)
            {
                parameterDisplay.playerPoints = parameter.playerValue;
            }
        }
    }

    /// <summary>
    /// Returns to main menu
    /// </summary>
    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
