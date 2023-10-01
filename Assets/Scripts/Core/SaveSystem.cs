using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void DeleteGameData(GameData gameData)
    {
        if (File.Exists(getPath()))
        {
           File.Delete(getPath());
            //loadGame();
        }
        else
        {
            gameData.playerName= null;
            gameData.levelDifficulty = LevelDifficulty.None;
            gameData.puzzlingPoints= 0;
        }
    }

    public static void saveGame(List<GameData> gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(getPath(), FileMode.Create);
        formatter.Serialize(fs, gameData);
        fs.Close();
    }

    public static List<GameData> loadGame()
    {
        if (!File.Exists(getPath()))
        {
            List<GameData> newData = new List<GameData>();
            saveGame(newData);
            //Debug.Log("New Game");
            return newData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(getPath(), FileMode.Open);
        List<GameData> deserializedGameData = formatter.Deserialize(fs) as List<GameData>;
        fs.Close();
        //Debug.Log("Load Game");
        return deserializedGameData;
    }

    private static string getPath()
    {
        return Path.Combine( Application. persistentDataPath , "data.qnd");
    }
}
  
 
