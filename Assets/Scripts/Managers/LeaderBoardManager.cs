using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class LeaderBoardManager : MonoBehaviour
{

    public List<LeaderBoardDisplay> leaderboardDisplay = new List<LeaderBoardDisplay>();
    public List<GameData> gameDataLists = new List<GameData>();
    public ScrollView scrollView;
    public GameObject dataContent;
    public TMP_Dropdown levelDropdown;
    private LevelDifficulty TEMPlev;

    public TMP_InputField inputF;

    public List<GameData> gameDataListsTEMP;
    int val; 


    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<LeaderBoardDisplay>() != null)
            {
                leaderboardDisplay.Add(transform.GetChild(i).gameObject.GetComponent<LeaderBoardDisplay>());
            }
        }

        if (GameManager.Instance.gameDataList != null )
        {
            for (int i = 0; i < GameManager.Instance.gameDataList.Count; i++)
            {
                gameDataLists.Add(GameManager.Instance.gameDataList[i]);
            }
        }

        SaveSystem.saveGame(GameManager.Instance.gameDataList);


        //AddGameDataList(new GameData { playerName = "J", levelDifficulty = LevelDifficulty.Beginner, puzzlingPoints = 10000 });

        spawnnViewportContent();

        if (GameManager.Instance.gameData.levelDifficulty == LevelDifficulty.None || GameManager.Instance.gameData.levelDifficulty == LevelDifficulty.Beginner)
        {
            levelDropdown.value = 0;
            updateLeaderboard(FilterData(LevelDifficulty.Beginner));
        }
        else if (GameManager.Instance.gameData.levelDifficulty == LevelDifficulty.Intermediate)
        {
            levelDropdown.value = 1;
            updateLeaderboard(FilterData(LevelDifficulty.Intermediate));
        }
        else
        {
            levelDropdown.value = 2;
            updateLeaderboard(FilterData(LevelDifficulty.Advanced));
        }


    }

    private void spawnnViewportContent()
    {
        for (int i =0; i < gameDataLists.Count; i++) 
        {
            var newDataContent = Instantiate(dataContent, this.transform, false);
            leaderboardDisplay.Add(newDataContent.GetComponent<LeaderBoardDisplay>());
        }
    }

    private void updateLeaderboard(List<GameData> data)
    {
        data.Sort((GameData x, GameData y) => y.puzzlingPoints.CompareTo(x.puzzlingPoints));

        for (int i =0; i < leaderboardDisplay.Count; i++) 
        {
            if (i < data.Count)
            {
                leaderboardDisplay[i].DisplayHighScore(data[i], i+1);
            }
            else
            {
                leaderboardDisplay[i].HideEntryDisplay();
            }
        
        }
    }

    private void AddGameDataList(GameData data)
    {
        gameDataLists.Add(data);
    }


    public void filterListByLevel()
    {
        val = levelDropdown.value;

        //Debug.Log("dropdown value is: " + val);

        if (val == 0)
        {
            TEMPlev = LevelDifficulty.Beginner;
        }
        else if (val == 1) 
        {
            TEMPlev = LevelDifficulty.Intermediate;
        }
        else if (val==2)
        {
            TEMPlev = LevelDifficulty.Advanced;
        }

        Debug.Log("Level is: " + TEMPlev);
        updateLeaderboard(FilterData(TEMPlev));
    }


    public List<GameData> FilterData(LevelDifficulty level)
    {
        List<GameData> displayList = (from item in gameDataLists
                                  where item.levelDifficulty == level
                                      select item).ToList();
        return displayList;
    }

    public void MainMenu()
    {
        StartCoroutine(loadMenu());
    }

    IEnumerator loadMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("01_MainMenu");
    }

    public List<GameData> FilterdataByName(List<GameData> dataByLevel)
    {
        string tempVal = inputF.text;

        if (string.IsNullOrWhiteSpace(tempVal) == true)
        {
            Debug.Log("null");
            return dataByLevel;
        }
        else
        {
            List<GameData> displayList = (from item in dataByLevel
                                          where item.playerName.Equals(inputF.textComponent.text)
                                          select item).ToList();
            return displayList;
        }
        
       
    }

    public void updateLeaderboardByName()
    {
        val = levelDropdown.value;

        if (val == 0)
        {
            TEMPlev = LevelDifficulty.Beginner;
        }
        else if (val == 1)
        {
            TEMPlev = LevelDifficulty.Intermediate;
        }
        else if (val == 2)
        {
            TEMPlev = LevelDifficulty.Advanced;
        }
   
       updateLeaderboard(FilterdataByName((FilterData(TEMPlev))));
    }
}
