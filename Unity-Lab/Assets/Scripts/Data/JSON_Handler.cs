using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using Newtonsoft.Json;
using static LevelSettings;

using static Level;
using static Card;
using System.Text;


public class JSON_Handler : MonoBehaviour
{
    public Level dataRead;
    public static string levelName = "Katowice";

    string  saveFilePath;
    string architectureImagePath = $"Architecture/{LevelSettings.instance.levelName}/";

    void Awake()
    {
        saveFilePath = Application.dataPath + "/JSONs/ktw.json";
        LoadGame();
    }


    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);
                dataRead = JsonConvert.DeserializeObject<Level>(json);

                for (int i = 0; i < dataRead.cards.Count; i++)
                {
                    LoadSprite(dataRead.cards[i]);
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Error loading game data: " + e.Message);
            }

            UnityEngine.Debug.Log("Data have been loaded from: " + saveFilePath);
        }
        else
        {
            UnityEngine.Debug.LogWarning("JSON file does not exist.");
        }
    }

    private void LoadSprite(Card card)
    {
        // load card skins from Resources
        card.skin = Resources.Load<Sprite>(card.skinPath);

        if (card.skin != null)
            UnityEngine.Debug.Log("Sprite loaded successfully: " + card.skinPath);
        else
            UnityEngine.Debug.LogError("Failed to load Sprite from path: " + card.skinPath);

        // load building images from Resources
        string path = architectureImagePath + card.buildingPath;
        card.buildingSprite = Resources.Load<Sprite>(path);
    }

}
