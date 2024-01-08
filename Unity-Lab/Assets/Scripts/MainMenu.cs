using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static LevelSettings;

/// <summary>
/// Class responsible for the main menu behaviour
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// User's name
    /// </summary>
    private string userName;
    /// <summary>
    /// Selected level name
    /// </summary>
    private JSON_Handler selectedLevel = null;
    /// <summary>
    /// Reference to the level button prefab
    /// </summary>
    [SerializeField]
    private GameObject levelButtonPrefab;
    /// <summary>
    /// Reference to the easy level icon
    /// </summary>
    [SerializeField]
    private Sprite easyIcon;
    /// <summary>
    /// Reference to the medium level icon
    /// </summary>
    [SerializeField]
    private Sprite mediumIcon;
    /// <summary>
    /// Reference to the hard level icon
    /// </summary>
    [SerializeField]
    private Sprite hardIcon;
    /// <summary>
    /// List of levels
    /// </summary>
    private List<JSON_Handler> levels = new List<JSON_Handler>();

    [SerializeField]
    TMP_InputField nickNameInput;

    [SerializeField]
    List<Sprite> lvlIcons;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Find all JSON files in the JSONs folder and store them in a list
        string[] files = System.IO.Directory.GetFiles(Application.dataPath + "/JSONs", "*.json");

        int i = 0;
        // Displaying buttons for each level
        foreach (string jsonFile in files)
        {
            JSON_Handler jsonHandler = new JSON_Handler(jsonFile);
            levels.Add(jsonHandler);
            // Creating button
            GameObject button = Instantiate(levelButtonPrefab);
            // Set game object's name
            button.name = "Level" + i + "Button";
            button.transform.SetParent(GameObject.Find("Levels").transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = System.IO.Path.GetFileNameWithoutExtension(jsonFile);
            button.GetComponentsInChildren<Image>()[1].sprite = jsonHandler.readLevel.icon;
            button.GetComponent<Button>().onClick.AddListener(delegate { LevelButtonListener(jsonHandler); });
            i++;
        }

        var startButton = GameObject.Find("StartButton");
        startButton.GetComponent<Button>().onClick.AddListener(delegate { StartButtonListener(); });

        nickNameInput.onValueChanged.AddListener(SetNickname);
    }

    void SetNickname(string value)
    {
        userName = value;
    }


    /// <summary>
    /// Method responsible for handling level button click
    /// </summary>
    /// <param name="level">level of the button that has been clicked</param>
    private void LevelButtonListener(JSON_Handler level)
    {
        ClearErrorMessage();
        int i = 0;
        foreach (JSON_Handler level1 in levels)
        {
            var button = GameObject.Find("Level" + i + "Button");
            if (level1.readLevel.name == level.readLevel.name)
            {
                // Make the button green
                button.GetComponent<Image>().color = Color.green;
                selectedLevel = level;

            }
            else
            {
                // Make the button white
                button.GetComponent<Image>().color = Color.white;
            }
            i++;
        }
    }

    /// <summary>
    /// Method responsible for handling start button click
    /// </summary>
    private void StartButtonListener()
    {
        if (selectedLevel != null && userName != null && userName.Length >= 2)
        {
            // Clear error message
            ClearErrorMessage();
            // Load the game scene
            LevelSettings.instance.SetLevelParameters(selectedLevel.readLevel.name, userName, selectedLevel.readLevel.icon, selectedLevel.readLevel.numOfBombs, selectedLevel.readLevel.parameterInfos);
            Game.jsonHandler = selectedLevel;
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            // Display error message
            GameObject errMsg = GameObject.Find("ErrorMessage");
            errMsg.GetComponent<TextMeshProUGUI>().text = "Wybierz poziom i podaj imiê!";
        }
    }

    /// <summary>
    /// Clears the error message
    /// </summary>
    public void ClearErrorMessage()
    {
        GameObject errMsg = GameObject.Find("ErrorMessage");
        errMsg.GetComponent<TextMeshProUGUI>().text = "";
    }

}
