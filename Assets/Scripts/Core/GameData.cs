using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string playerName;
    public int puzzlingPoints;
    public LevelDifficulty levelDifficulty;
    public bool[] isLevelLock;


    public GameData()
    {
        playerName = "";
        puzzlingPoints = 0;
        levelDifficulty = LevelDifficulty.None;
        isLevelLock = new bool[2];
    }

    public GameData(GameData data)
    {
        playerName = data.playerName.ToString();
        puzzlingPoints = data.puzzlingPoints;
        levelDifficulty = data.levelDifficulty;
        isLevelLock= data.isLevelLock;
    }
}
