using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordListManager : MonoBehaviour
{
    private LevelManager levelManager;
    private GameManager gameManager;


    private List<string> correctWords;

    private List<string> wordChoices = new List<string>();
    public Button[] wordButtons;
    private GameObject gameOverScreen;
    private GameObject picSelectionScreen;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        correctWords = new List<string>(levelManager.wordsToSolve);
        gameOverScreen = GameObject.Find("GameUI").transform.GetChild(0).transform.Find("GameOverScreen").gameObject;
        picSelectionScreen = GameObject.Find("GameUI").transform.GetChild(0).transform.Find("PicSelectionScreen").gameObject;

        gameManager = FindObjectOfType<GameManager>();

        switch (gameManager.gameData.levelDifficulty)
        {
            case LevelDifficulty.Beginner:
                wordChoices.Add("BALL");
                wordChoices.Add("CAT");
                wordChoices.Add("CAKE");
                wordChoices.Add("SUN");
                break;

            case LevelDifficulty.Intermediate:
                wordChoices.Add("SHIRT");
                wordChoices.Add("TREES");
                wordChoices.Add("CLOUD");
                wordChoices.Add("HOUSE");
                wordChoices.Add("PIANO");
                wordChoices.Add("PENCIL");
                break;

            case LevelDifficulty.Advanced:
                wordChoices.Add("FLOWER");
                wordChoices.Add("CHEESE");
                wordChoices.Add("CLOUD");
                wordChoices.Add("SUN");
                wordChoices.Add("BREAD");
                wordChoices.Add("BUTTER");
                wordChoices.Add("SPOON");
                wordChoices.Add("DOOR");
                break;
        }

 

        
        gameOverScreen.SetActive(false);

        for (int i = 0; i < wordButtons.Length; i++)
        {
            wordButtons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < wordChoices.Count; i++)
        {
            wordButtons[i].gameObject.SetActive(true);
            wordButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = wordChoices[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var cachedWordButtonList = new Button[wordButtons.Length];

        for (int i = 0; i < wordChoices.Count; i++)
        {
            cachedWordButtonList[i] = wordButtons[i];
            int x = i;
            cachedWordButtonList[x].onClick.AddListener(() => onButtonClick(cachedWordButtonList[x]));
        }
    }

    public void onButtonClick(Button buttonref)
    {
        
        for (int i =0; i < correctWords.Count; i++)
        {
            //int x = i;
            //Debug.Log("Correct word is: " + correctWords[i]);

            if (buttonref.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == correctWords[i])
            {
                GameManager.Instance.addScore(10);
                Debug.Log("Correct word count is : " + correctWords.Count);
                Debug.Log("Level Manager List count is: " + levelManager.wordsToSolve.Count);
                correctWords.RemoveAt(i);

            }
            else
            {
                var neverdone = false;

                if (neverdone == false)
                {
                    GameManager.Instance.addScore(-5);
                    neverdone= true;
                }
                //Debug.Log("wrong word when current loop is: " + correctWords[i]);
            }
        }



        if (correctWords.Count == 0)
        {
            Debug.Log("whole level completed");
            StartCoroutine(wordCorrectEvent());


        }
        else
        {

            //Debug.Log("what???? List count is: " + levelManager.wordsToSolve.Count);
        }
    }

    IEnumerator wordCorrectEvent()
    {
        yield return new WaitForSeconds(0f);
        GameManager.Instance.playMagicSFX();

        yield return new WaitForSeconds(3.5f);
        picSelectionScreen.SetActive(true);
        this.gameObject.SetActive(false);

    }
}
