using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Class representing the display of a single parameter in the HUD.
/// </summary>
public class ParameterDisplay : MonoBehaviour
{
    [SerializeField] Image parameterIconDisplay;
    [SerializeField] TextMeshProUGUI minPointsDisplay;
    [SerializeField] TextMeshProUGUI maxPointsDisplay;
    [SerializeField] TextMeshProUGUI playerPointsDisplay;
    [SerializeField] TextMeshProUGUI info;

    public string parameterName;
    public Image parameterIcon => parameterIconDisplay;
    public int minPointsValue
    {
        get => int.Parse(minPointsDisplay.text);
        set => minPointsDisplay.text = value.ToString();
    }

    public int maxPointsValue
    {
        get => int.Parse(maxPointsDisplay.text);
        set => maxPointsDisplay.text = value.ToString();
    }

    public int playerPoints
    {
        get => _playerPoints;
        set
        {
            _playerPoints = value;
            playerPointsDisplay.text = value.ToString();
            if (value < int.Parse(minPointsDisplay.text))
            {
                playerPointsDisplay.color = Color.red;
                info.text = $"Brakuje Ci {int.Parse(minPointsDisplay.text) - playerPoints} pkt!";
            }
            else if (value == int.Parse(maxPointsDisplay.text))
            {
                playerPointsDisplay.color = Color.green;
                info.text = $"Osi¹gn¹³eœ cel!";
            }
            else
            {
                playerPointsDisplay.color = Color.red;
                info.text = $"Za du¿o punktów!";
            }
        }
    }
    int _playerPoints;
}
