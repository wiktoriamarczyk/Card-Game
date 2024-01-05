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
        var temp = card.skinPath;
        card.skinPath = spritePathBegin + temp;
        UnityEngine.Debug.Log("card.skinPath: " + card.skinPath);

        //sprite`y przeniesione do Resources, mimo ¿e nie jest to optymalne rozwi¹zanie,
        //to nie wymaga u¿ycia Addressable Assetsów
        card.spriteRenderer.sprite = Resources.Load<Sprite>(card.skinPath);        

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
