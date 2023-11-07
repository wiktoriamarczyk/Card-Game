using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DifficultySettings;

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
}
