using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Game;

/// <summary>
///  Class responsible for storing difficulty settings.
/// </summary>
public class LevelSettings
{
    public static LevelSettings instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LevelSettings();
            }

            return _instance;
        }
    }
    static LevelSettings _instance;

    public string nickname { get; private set; }
    public Sprite lvlIcon { get; private set; }
    public int bombCount { get; private set; }
    public List<LevelParameter> levelParameters { get; private set; }

    public class LevelParameter
    {
        public string name;
        public int minValue;
        public int maxValue;
        public int playerValue;
        public Sprite icon;
    }

    /// <summary>
    ///  Sets difficulty settings of current level.
    /// </summary>
    /// <param name="nick">player's nick</param>
    /// <param name="lvlIcon">icon of the lvl</param>
    /// <param name="bombCount">number of bombs</param>
    /// <param name="parameters">level parameters</param>
    public void SetLevelParameters(string nick, Sprite lvlIcon, int bombCount, List<LevelParameter> parameters)
    {
        nickname = nick;
        this.lvlIcon = lvlIcon;
        this.bombCount = bombCount;
        levelParameters = parameters;
    }

}
