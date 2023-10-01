using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PicSelectionManager : MonoBehaviour
{
    public GameObject twoButtons;
    public GameObject fourButtons;

    public bool isTwoTimesTwo;

    private LevelManager levelManager;
    private GameManager gameManager;

    public List<string> correctWords;

    private WordListManager wordListManager;

    private GameObject leaderboardUI;

    public GameObject pointText;

    public TextMeshProUGUI PictureWordText;

    private void Awake()
    {
        wordListManager = FindObjectOfType<WordListManager>();

        levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();

        //correctWords = levelManager.wordsToSolve;

        correctWords = new List<string>(levelManager.wordsToSolve);

        twoButtons.SetActive(false);
        fourButtons.SetActive(false);

        //Debug.Log("WordsToSolve: " + levelManager.wordsToSolve.Count);
        //Debug.Log("WordsToSolveCache: " + levelManager.wordsToSolve.Count);
        leaderboardUI = GameObject.Find("GameUI").transform.GetChild(0).transform.Find("LeaderBoardPrefab").gameObject;
        leaderboardUI.SetActive(false);


        switch (correctWords[0])
        {
            case "BALL":
                PictureWordText.text = "(BALL)";
                break;

            case "CAKE":
                PictureWordText.text = "(SUN)";
                break;

            case "SHIRT":
                PictureWordText.text = "(TREES)";
                break;

            case "HOUSE":
                PictureWordText.text = "(HOUSE)";
                break;

            case "FLOWER":
                PictureWordText.text = "(FLOWER)";
                break;

            case "BREAD":
                PictureWordText.text = "(DOOR)";
                break;



        }
    }

    // Start is called before the first frame update
    void Start()
    {
            twoButtons.SetActive(true);
            Button[] buttonCount = new Button[2];
            buttonCount[0] = twoButtons.transform.GetChild(0).gameObject.GetComponent<Button>();
            buttonCount[1] = twoButtons.transform.GetChild(1).gameObject.GetComponent<Button>();

            var cachedWordButtonList = new Button[buttonCount.Length];

        
            //beginner
            if (correctWords[0] == "BALL")
            {
                int randomIndex = Random.Range(0, 11);

                if (randomIndex <=5 )
                {
                    buttonCount[0].GetComponent<Image>().sprite = levelManager.correctBallIMG;
                    buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongBallIMG;
                }
                else
                {
                    buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongBallIMG;
                    buttonCount[1].GetComponent<Image>().sprite = levelManager.correctBallIMG;
                }
                    
               
            }
            else if (correctWords[0] == "CAKE")
            {
                int randomIndex = Random.Range(0, 11);
                if (randomIndex <= 5)
                {
                    buttonCount[0].GetComponent<Image>().sprite = levelManager.correctSunIMG;
                    buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongSunIMG;
                }
                else
                {
                    buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongSunIMG;
                    buttonCount[1].GetComponent<Image>().sprite = levelManager.correctSunIMG;
                }
            }

            //Intermediate
            if (correctWords[0] == "SHIRT")
            {

                int randomIndex = Random.Range(0, 2);
                int randomIndex2 = Random.Range(0, 11);

                if (randomIndex <= 5)
                {

                    if (randomIndex2 <= 5)
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.correctTree1IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongTree1IMG;
                    }
                    else
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.correctTree2IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongTree2IMG;
                    }
                        
                }
                else
                {
                    if (randomIndex2 <= 5)
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongTree1IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.correctTree1IMG;
                    }
                    else
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongTree2IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.correctTree2IMG;
                    }
                           
                }

            }
             else if (correctWords[0] == "HOUSE")
            {
                int randomIndex = Random.Range(0, 2);
                int randomIndex2 = Random.Range(0, 11);

                if (randomIndex <= 5)
                {

                    if (randomIndex2 <= 5)
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.correctHouse1IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongHouse1IMG;
                    }
                    else
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.correctHouse2IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongHouse2IMG;
                    }

                }
                else
                {
                    if (randomIndex2 <= 5)
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongHouse1IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.correctHouse1IMG;
                    }
                    else
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongHouse2IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.correctHouse2IMG;
                    }

                }
            }

            //Advanced
            if (correctWords[0] == "FLOWER")
            {

                int randomIndex = Random.Range(0, 2);
                int randomIndex2 = Random.Range(0, 11);

                if (randomIndex <= 5)
                {

                    if (randomIndex2 <= 5)
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.correctFlower1IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongFlower1IMG;
                    }
                    else
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.correctFlower2IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongFlower2IMG;
                    }

                }
                else
                {
                    if (randomIndex2 <= 5)
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongFlower1IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.correctFlower1IMG;
                    }
                    else
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongFlower2IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.correctFlower2IMG;
                    }

                }

            }
            else if (correctWords[0] == "BREAD")
            {
                int randomIndex = Random.Range(0, 2);
                int randomIndex2 = Random.Range(0, 11);

                if (randomIndex <= 5)
                {

                    if (randomIndex2 <= 5)
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.correctDoor1IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongDoor1IMG;
                    }
                    else
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.correctDoor2IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.wrongDoor2IMG;
                    }

                }
                else
                {
                    if (randomIndex2 <= 5)
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongDoor1IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.correctDoor1IMG;
                    }
                    else
                    {
                        buttonCount[0].GetComponent<Image>().sprite = levelManager.wrongDoor2IMG;
                        buttonCount[1].GetComponent<Image>().sprite = levelManager.correctDoor2IMG;
                    }

                }
            }

            for (int i = 0; i < buttonCount.Length; i++)
            {
                cachedWordButtonList[i] = buttonCount[i];
                int x = i;
                cachedWordButtonList[x].onClick.AddListener(() => onButtonClick(cachedWordButtonList[x]));
            }
     
    }

    public void onButtonClick(Button buttonref)
    {

        switch (gameManager.gameData.levelDifficulty)
        {

            case LevelDifficulty.Beginner:
                if (correctWords[0] == "BALL")
                {
                    if (buttonref.GetComponent<Image>().sprite == levelManager.correctBallIMG)
                    {
                        correctEvent();
                    }
                    else
                    {
                        Debug.Log("wrong word");
                        GameManager.Instance.addScore(-5);
                    }
                }
                else if (correctWords[0] == "CAKE")
                {
                    if (buttonref.GetComponent<Image>().sprite == levelManager.correctSunIMG)
                    {
                        correctEvent();
                    }
                    else
                    {
                        Debug.Log("wrong word");
                        GameManager.Instance.addScore(-5);
                    }
                }

                break;



            case LevelDifficulty.Intermediate:
                if (correctWords[0] == "SHIRT")
                {
                    if (buttonref.GetComponent<Image>().sprite == levelManager.correctTree1IMG || buttonref.GetComponent<Image>().sprite == levelManager.correctTree2IMG)
                    {
                        correctEvent();


                    }
                    else
                    {
                        Debug.Log("wrong word");

                        GameManager.Instance.addScore(-5);
                    }
                }
                else if (correctWords[0] == "HOUSE")
                {
                    if (buttonref.GetComponent<Image>().sprite == levelManager.correctHouse1IMG || buttonref.GetComponent<Image>().sprite == levelManager.correctHouse2IMG)
                    {
                        correctEvent();
                    }
                    else
                    {
                        Debug.Log("wrong word");
                        GameManager.Instance.addScore(-5);
                    }
                }

                break;


            case LevelDifficulty.Advanced:
                if (correctWords[0] == "FLOWER")
                {
                    if (buttonref.GetComponent<Image>().sprite == levelManager.correctFlower1IMG || buttonref.GetComponent<Image>().sprite == levelManager.correctFlower2IMG)
                    {
                        correctEvent();


                    }
                    else
                    {
                        Debug.Log("wrong word");

                        GameManager.Instance.addScore(-5);
                    }
                }
                else if (correctWords[0] == "BREAD")
                {
                    if (buttonref.GetComponent<Image>().sprite == levelManager.correctDoor1IMG || buttonref.GetComponent<Image>().sprite == levelManager.correctDoor2IMG)
                    {
                        correctEvent();
                    }
                    else
                    {
                        Debug.Log("wrong word");
                        GameManager.Instance.addScore(-5);
                    }
                }

                break;

        }
    }

    private void correctEvent()
    {
        StartCoroutine(correctDelay());
    }

    IEnumerator correctDelay()
    {
        GameManager.Instance.playMagicSFX();
        Debug.Log("Correct");
        GameManager.Instance.addScore(20);

        switch (GameManager.Instance.gameData.levelDifficulty)
        {
            case LevelDifficulty.Beginner:
                GameManager.Instance.gameData.isLevelLock[0] = true;
                break;

            case LevelDifficulty.Intermediate:
                GameManager.Instance.gameData.isLevelLock[1] = true;
                break;
        }

        var gameDataTEMP = new GameData(GameManager.Instance.gameData);
        GameManager.Instance.gameDataList.Add(gameDataTEMP);

        SaveSystem.DeleteGameData(GameManager.Instance.gameData);

        yield return new WaitForSeconds(2.5f);


        leaderboardUI.SetActive(true);

        pointText.SetActive(false);

    
    }
}
