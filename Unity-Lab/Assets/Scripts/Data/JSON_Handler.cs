using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using Newtonsoft.Json;

using static Level;
using static Card;


public class JSON_Handler : MonoBehaviour
{
    public Level dataRead;
    string  saveFilePath;
    string  spritePathBegin;

    void Awake()
    {
        saveFilePath = Application.dataPath + "/JSONs/ktw.json";
        //spritePathBegin = Application.dataPath + "/Sprites/Cards/";
        spritePathBegin = "";
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
        Texture2D texture = Resources.Load<Texture2D>(card.skinPath);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        card.skin = sprite;

        if (card.skin != null)
        {
            UnityEngine.Debug.Log("Sprite loaded successfully: " + card.skinPath);
        }
        else
        {
            UnityEngine.Debug.LogError("Failed to load Sprite from path: " + card.skinPath);
        }
    }

}
