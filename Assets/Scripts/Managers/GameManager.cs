using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

    public bool isResetData;

    //[HideInInspector]
    public GameData gameData;

    public List<GameData> gameDataList = new List<GameData>();

    public AudioSource self;
    public AudioClip BGMusic;
    public AudioClip ClickSFX;
    public AudioClip MagicSFX;



    private void Awake()
    {
        resetData();
        self = this.GetComponent<AudioSource>();
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
      

        
    }


    private void resetData()
    {
        if (isResetData)
        {
            SaveSystem.DeleteGameData(gameData);
        }
        else
        {
            gameData = new GameData();
            gameDataList = SaveSystem.loadGame();
        }
    }

    public void addScore(int score)
    {
        if (gameData.puzzlingPoints >= 0)
        {
            gameData.puzzlingPoints += score;
            if (gameData.puzzlingPoints <0)
            {
                gameData.puzzlingPoints = 0;
            }
        }
    }

    public void setDiffuclty (LevelDifficulty level)
    {
        gameData.levelDifficulty = level;
    }

    public void addNewDataToList (GameData data)
    {
        gameDataList.Add(data);
        
    }

    public void playBG()
    {
        self.loop = true;
        self.PlayOneShot(BGMusic);
       
    }

    public void playClick()
    {
        self.loop = false;
        self.PlayOneShot(ClickSFX);
    }

    public void playMagicSFX()
    {
        self.loop = false;
        self.PlayOneShot(MagicSFX);
        Debug.Log("Play MaficSFX");
    }

}
