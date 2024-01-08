using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;


public class JSON_Handler
{
    public Level readLevel;
    string jsonPath;
    string architectureImagePath;
    
    public JSON_Handler(string jsonName)
    {
        jsonPath = jsonName;
        if (File.Exists(jsonPath))
        {
            try
            {
                string json = File.ReadAllText(jsonPath);
                readLevel = JsonConvert.DeserializeObject<Level>(json);
                string temp = $"LevelIcons/{readLevel.iconPath}";
                // Read image and load it as a sprite
                architectureImagePath = $"Architecture/{readLevel.name}/";
                Sprite sprite = Resources.Load<Sprite>(temp);
                readLevel.icon = sprite;
                foreach (var parameter in readLevel.parameterInfos)
                    parameter.icon = Resources.Load<Sprite>($"ParameterIcons/{parameter.iconPath}");
                for (int i = 0; i < readLevel.cards.Count; i++)
                    LoadSprite(readLevel.cards[i]);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading game data: " + e.Message);
            }
        }
        else Debug.LogWarning("JSON file does not exist.");
    }


    private void LoadSprite(Card card)
    {
        // load card skins from Resources
        card.skin = Resources.Load<Sprite>(card.skinPath);

        if (card.skin != null)
            Debug.Log("Sprite loaded successfully: " + card.skinPath);
        else
            Debug.LogError("Failed to load Sprite from path: " + card.skinPath);

        // load building images from Resources
        string path = architectureImagePath + card.buildingPath;
        card.buildingSprite = Resources.Load<Sprite>(path);
    }

}
