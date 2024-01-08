using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LevelSettings;

/// <summary>
/// Class representing a single level and its properties
/// </summary>
public class Level
{
    /// <summary>
    /// Level name
    /// </summary>
    public string name;
    /// <summary>
    /// Level's icon path
    /// </summary>
    public string iconPath;
    /// <summary>
    /// Level's icon
    /// </summary>
    public Sprite icon;
    /// <summary>
    /// List of parameter infos
    /// </summary>
    public List<LevelParameter> parameterInfos;
    /// <summary>
    /// Number of bombs
    /// </summary>
    public int numOfBombs;
    /// <summary>
    /// Number of people in the city
    /// </summary>
    public int population;
    /// <summary>
    /// List of cards assigned to the level
    /// </summary>
    public List<Card> cards = new List<Card>();


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">level name</param>
    /// <param name="icon">level icon</param>
    /// <param name="parameterInfos">list of parameter values</param>
    /// <param name="numOfBombs">number of bombs</param>
    public Level(string name, Sprite icon, List<LevelParameter> parameterInfos, int numOfBombs)
    {
        this.name = name;
        this.icon = icon;
        this.parameterInfos = parameterInfos;
        this.numOfBombs = numOfBombs;
    }

    public Level()
    {
    }

    //function to manually debug the correct serialisation of the list xDD
    public void ListAll()
    {
        UnityEngine.Debug.Log("NAME: " + name);
        UnityEngine.Debug.Log("POPULATION: " + population);
        UnityEngine.Debug.Log("BOMBs: " + numOfBombs);

        UnityEngine.Debug.Log("-------Parameter List-------");
        int cnt = 1;
        foreach (var p in parameterInfos)
        {
            UnityEngine.Debug.Log("Parameter ["+cnt+"]" + p.name);
            cnt++;
        }
        UnityEngine.Debug.Log("-------Cards List-------");
        cnt = 1;
        foreach (var c in cards)
        {
            UnityEngine.Debug.Log("Card [" + cnt + "]" + c.color + "/" + c.type);
            cnt++;
        }
    }
}

