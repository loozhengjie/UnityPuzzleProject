using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Assertions.Must;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine.UI;

//using Unity.VisualScripting.Dependencies.Sqlite;

public class LevelManager : MonoBehaviour
{
    [Header("PuzzleSet Image Library")]
    public GameObject[] twoTimesTwoPuzzleSetLibrary;
    public GameObject[] threeTimesThreePuzzleSetLibrary;
    public GameObject[] fourTimesfourPuzzleSetLibrary;

    private GameObject puzzleSet;

    private List<PuzzleSet> PuzzleSetInLevel = new List<PuzzleSet>();

    //public LevelDifficulty levelDifficulty;

    [HideInInspector]
    public List<string> wordsToSolve = new List<string>();
    [HideInInspector]
    public List<string> wordsToSolveCache = new List<string>();


    //public string[] wordsToSolve;

    private List<PuzzleSet> completedPuzzleSet = new List<PuzzleSet>();
    private List<WordSet> wordSetInLevel = new List<WordSet>();
    //public WordSet[] wordSetInLevel;

    private bool _camSpam = false;

    private GameManager gameManager;

    private Camera puzzleCamera;
    private Vector3 cameraLerpPosition;

    private Vector3 CameraInitialPoistion;

    [Header("Words Settings")]
    //public TextMeshProUGUI wordText;
    public TextMeshProUGUI wordTextToSolve;
    private List<TextMeshProUGUI> wordTextList= new List<TextMeshProUGUI>();
    private GameObject wITText;
    private GameObject wordsList;
  

    //public GameObject underScore;
    private GameObject Canvas;
    private GameObject TEMPUs;
    private int usCount;
    private int UsTextCount;

    [Header("Underscore Settings")]
    public GameObject threeUS;
    public GameObject fourUS;
    public GameObject fiveUS;
    public GameObject sixUS;
    private GameObject currentUS;

    [Header("WordImage Settings")]
    public Sprite ballIMG;
    public Sprite catIMG;
    public Sprite cakeIMG;
    public Sprite sunIMG;
    public Sprite shirtIMG;
    public Sprite treesIMG;
    public Sprite cloudIMG;
    public Sprite houseIMG;
    public Sprite pianoIMG;
    public Sprite pencilIMG;
    public Sprite flowerIMG;
    public Sprite cheeseIMG;
    public Sprite breadIMG;
    public Sprite butterIMG;
    public Sprite spoonIMG;
    public Sprite doorIMG;

    [Header("PicSelection Settings")]
    [Header("Beginner Pic Select Settings")]
    public Sprite correctSunIMG;
    public Sprite wrongSunIMG;
    public Sprite correctBallIMG;
    public Sprite wrongBallIMG;

    [Header("Intermediate Pic Select Settings")]
    public Sprite correctTree1IMG;
    public Sprite wrongTree1IMG;
    public Sprite correctTree2IMG;
    public Sprite wrongTree2IMG;

    public Sprite correctHouse1IMG;
    public Sprite wrongHouse1IMG;
    public Sprite correctHouse2IMG;
    public Sprite wrongHouse2IMG;

    [Header("Advanced Pic Select Settings")]
    public Sprite correctFlower1IMG;
    public Sprite wrongFlower1IMG;
    public Sprite correctFlower2IMG;
    public Sprite wrongFlower2IMG;

    public Sprite correctDoor1IMG;
    public Sprite wrongDoor1IMG;
    public Sprite correctDoor2IMG;
    public Sprite wrongDoor2IMG;

    private GameObject textImg;

    private TextMeshProUGUI scoreText;

    [Header("PuzzleImageSettings")]
    public PuzzleImageLibrary[] imageLibrary = new PuzzleImageLibrary[26];

    //public List<GameObject> underScores;

    private void Awake()
    {

        gameManager = FindObjectOfType<GameManager>();
        usCount= 0;
        UsTextCount = 0;

        wITText = GameObject.Find("GameUI").transform.GetChild(0).transform.Find("WITText").gameObject;
        wordsList = GameObject.Find("GameUI").transform.GetChild(0).transform.Find("WordList").gameObject;
        textImg = GameObject.Find("GameUI").transform.GetChild(0).transform.Find("TextIMGParent").gameObject;
        scoreText = GameObject.Find("GameUI").transform.GetChild(0).transform.Find("Panel").gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        wITText.SetActive(false);
        textImg.SetActive(false);

        setUpWordsTOSolve();


        setupCamera();
      
        spawnPuzzleSet();
        categoriseDragabblePieces();

        setUnderscore(usCount);
        //wordText.gameObject.SetActive(false);
    }

    private void setupCamera()
    {
        puzzleCamera = FindObjectOfType<Camera>();
        CameraInitialPoistion = puzzleCamera.transform.position;
        cameraLerpPosition = new Vector3(puzzleCamera.transform.position.x + 50, puzzleCamera.transform.position.y, puzzleCamera.transform.position.z);
    }
    private void setUpWordsTOSolve()
    {
        List<string[]> wordGroup;
        string[] wordGroup1;
        string[] wordGroup2;

        int random = Random.Range(0, 11);
        //`Debug.Log("RAndom is: " + random);

        GameManager.Instance.gameData.puzzlingPoints = 0;
        switch (gameManager.gameData.levelDifficulty)
        {
            case LevelDifficulty.Beginner:
                wordGroup = new List<string[]>();
                wordGroup1 = new string[] { "BALL", "CAT" };
                wordGroup2 = new string[] { "CAKE", "SUN" };


                wordGroup.Add(wordGroup1);
                wordGroup.Add(wordGroup2);



                if (random <= 5)
                {
                    wordsToSolve.Add(wordGroup[0][0]);
                    wordsToSolve.Add(wordGroup[0][1]);
                }
                else
                {
                    wordsToSolve.Add(wordGroup[1][0]);
                    wordsToSolve.Add(wordGroup[1][1]);
                }

                break;

            case LevelDifficulty.Intermediate:

                wordGroup = new List<string[]>();
                wordGroup1 = new string[] { "SHIRT", "TREES" };
                wordGroup2 = new string[] { "HOUSE", "PENCIL" };
                wordGroup.Add(wordGroup1);
                wordGroup.Add(wordGroup2);


                if (random <= 5)
                {
                    wordsToSolve.Add(wordGroup[0][0]);
                    wordsToSolve.Add(wordGroup[0][1]);
                }
                else
                {

                    wordsToSolve.Add(wordGroup[1][0]);
                    wordsToSolve.Add(wordGroup[1][1]);

                }
                break;

            case LevelDifficulty.Advanced:
                wordGroup = new List<string[]>();
                wordGroup1 = new string[] { "FLOWER", "CHEESE", "SUN" };
                wordGroup2 = new string[] { "BREAD", "BUTTER", "DOOR" };
                wordGroup.Add(wordGroup1);
                wordGroup.Add(wordGroup2);


                if (random <= 5)
                {
                    wordsToSolve.Add(wordGroup[0][0]);
                    wordsToSolve.Add(wordGroup[0][1]);
                    wordsToSolve.Add(wordGroup[0][2]);
                }
                else
                {
                    wordsToSolve.Add(wordGroup[1][0]);
                    wordsToSolve.Add(wordGroup[1][1]);
                    wordsToSolve.Add(wordGroup[1][2]);
                }
                break;
        }
        wordsToSolveCache = wordsToSolve;



    }

    private void setUnderscore(int wordToSolve)
    {
        switch (wordsToSolve[wordToSolve].Length)
        {
            default:
                TEMPUs = null;
                break;

            case 3:
                TEMPUs = threeUS;
                break;

            case 4:
                TEMPUs = fourUS;
                break;

            case 5:
                TEMPUs = fiveUS;
                break;

            case 6:
                TEMPUs = sixUS;
                break;
        }

        if (TEMPUs != null)
        {
            Canvas = GameObject.Find("GameUI").transform.Find("Canvas").transform.Find("UnderScoreParent").gameObject;
            var wordSpawnPoint = Canvas.transform.position;
            var newWordSpawnnPoint = new Vector3(wordSpawnPoint.x, wordSpawnPoint.y, wordSpawnPoint.z);
            var uS = Instantiate(TEMPUs, newWordSpawnnPoint, Quaternion.identity);
            uS.transform.SetParent(Canvas.transform, true);
            uS.transform.localScale = new Vector3(1, 1, 1);
            currentUS = uS;
        }

        
        for (int i =0; i < currentUS.transform.childCount;i ++)
        {
            currentUS.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void spawnPuzzleSet()
    {
        int random = Random.Range(0, 11);

        switch (gameManager.gameData.levelDifficulty)
        {
            case LevelDifficulty.Beginner:
                if (random <=5)
                {
                    
                }
                else
                {

                }
                puzzleSet = twoTimesTwoPuzzleSetLibrary[0];
                break;

            case LevelDifficulty.Intermediate:
                if (random <= 5)
                {

                }
                else
                {

                }
                puzzleSet = threeTimesThreePuzzleSetLibrary[0];
                break;

            case LevelDifficulty.Advanced:
                if (random <= 5)
                {

                }
                else
                {

                }
                puzzleSet = fourTimesfourPuzzleSetLibrary[0];
                break;
        }



        for (int i =0; i < wordsToSolve.Count;i++)
        {
            WordSet set = new WordSet();
            set.setNumber= i;
            set.puzzleSets = new List<PuzzleSet>();

            var letterCount = wordsToSolve[i].Length;

            for (int j = 0; j < letterCount; j++)
            {
                var initialSpawnPoint = puzzleSet.transform.position;
                var newSpawnPoint = new Vector3((initialSpawnPoint.x + (j * 50) * 1), (initialSpawnPoint.y - (i * 20)), initialSpawnPoint.z);
                var newPuzzleSet = Instantiate(puzzleSet, newSpawnPoint, Quaternion.identity);
                newPuzzleSet.GetComponent<PuzzleSet>().puzzleSetAlphabet = wordsToSolve[i][j];
                newPuzzleSet.GetComponent<PuzzleSet>().setupPuzzle();
                newPuzzleSet.GetComponent<PuzzleSet>().setID = i;
                PuzzleSetInLevel.Add(newPuzzleSet.GetComponent<PuzzleSet>());
            }

            wordSetInLevel.Add(set);           
        }
    }

    private void categoriseDragabblePieces()
    {
        foreach (var WordSet in wordSetInLevel)
        {
            for (int i = 0; i < PuzzleSetInLevel.Count; i++)
            {
                if (PuzzleSetInLevel[i].setID == WordSet.setNumber)
                {
                    if (PuzzleSetInLevel[i] == null)
                    {
                        Debug.Log("troll");
                        break;
                    }
                    else
                    {
                        WordSet.puzzleSets.Add(PuzzleSetInLevel[i]);
                    }

                }
            }
        }
    }


    public void addCompletedSet(PuzzleSet set)
    {
        completedPuzzleSet.Add(set);
        if (completedPuzzleSet.Count < PuzzleSetInLevel.Count)
        {
            if (puzzleCamera != null)
            {
                var cameraPosition = puzzleCamera.transform.position;
                puzzleCamera.transform.position = cameraLerpPosition;
                cameraLerpPosition = new Vector3(puzzleCamera.transform.position.x + 50, puzzleCamera.transform.position.y, puzzleCamera.transform.position.z);
            }
        }
        
    }

    public void removeSetFromGroup(PuzzleSet set)
    {
        for (int i =0; i < wordSetInLevel.Count; i++)
        {
            for (int j = 0; j < wordSetInLevel[i].puzzleSets.Count; j++)
            {
                if (set == wordSetInLevel[i].puzzleSets[j])
                {
                    wordSetInLevel[i].puzzleSets.RemoveAt(j);

                    //puzzleChecking
                    if (wordSetInLevel[i].puzzleSets.Count != 0)
                    {
                        StartCoroutine(CameraMoveRightEvent());
                    }
                    else 
                    {
                        StartCoroutine(CameraMoveDownEvent(i));
                    }
                }
            }

        }
    }

    IEnumerator CameraMoveRightEvent()
    {
        currentUS.transform.GetChild(UsTextCount).transform.GetChild(1).gameObject.SetActive(true);
        currentUS.transform.GetChild(UsTextCount).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = wordsToSolve[usCount][UsTextCount].ToString();
        UsTextCount++;

        yield return new WaitForSeconds(0.5f);
        cameraLerpPosition = new Vector3(puzzleCamera.transform.position.x + 50, puzzleCamera.transform.position.y, puzzleCamera.transform.position.z);
        puzzleCamera.transform.position = cameraLerpPosition;


        GameManager.Instance.addScore(1);
        //yield return new WaitForSeconds(0.5f);


    }

    IEnumerator CameraMoveDownEvent(int i)
    {
        currentUS.transform.GetChild(UsTextCount).transform.GetChild(1).gameObject.SetActive(true);
        currentUS.transform.GetChild(UsTextCount).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = wordsToSolve[usCount][UsTextCount].ToString();

        GameManager.Instance.addScore(5);

        yield return new WaitForSeconds(0.5f);
        if (GameObject.Find("GameUI").transform.Find("Canvas").transform.Find("UnderScoreParent").transform.childCount != 0)
        {
            var previousUS = GameObject.Find("GameUI").transform.Find("Canvas").transform.Find("UnderScoreParent").transform.GetChild(0).gameObject;
            Destroy(previousUS);
        }

        cameraLerpPosition = new Vector3(puzzleCamera.transform.position.x + 50, puzzleCamera.transform.position.y, puzzleCamera.transform.position.z);
        puzzleCamera.transform.position = cameraLerpPosition;


        wITText.SetActive(true);
        textImg.SetActive(true);
        
        switch (wordsToSolve[i])
        {
            case "BALL":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = ballIMG;
                break;

            case "CAT":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = catIMG;
                break;

            case "CAKE":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = cakeIMG;
                break;

            case "SUN":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = sunIMG;
                break;

            case "SHIRT":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = shirtIMG;
                break;

            case "TREES":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = treesIMG;
                break;

            case "CLOUD":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = cloudIMG;
                break;


            case "HOUSE":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = houseIMG;
                break;

            case "PIANO":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = pianoIMG;
                break;

            case "PENCIL":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = pencilIMG;
                break;

            case "FLOWER":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = flowerIMG;
                break;

            case "CHEESE":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = cheeseIMG;
                break;

            case "BREAD":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = breadIMG;
                break;

            case "BUTTER":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = butterIMG;
                break;

            case "SPOON":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = spoonIMG;
                break;

            case "DOOR":
                textImg.transform.GetChild(0).GetComponent<Image>().sprite = doorIMG;
                break;







        }

        for (int j = 0; j < wordsToSolve[i].Length; j++)
        {
            if (wordsToSolve[i][j] == 'A' || wordsToSolve[i][j] == 'E' || wordsToSolve[i][j] == 'I' || wordsToSolve[i][j] == 'O' ||  wordsToSolve[i][j] == 'U')
            {
                StartCoroutine(spawnVowels(i, j));
            }
            else
            {
                var parent = GameObject.Find("GameUI").transform.Find("Canvas").transform.Find("WITParent").gameObject;
                var newWordText = Instantiate(wordTextToSolve, parent.transform, false);
                var initialSpawnPoint = newWordText.transform.position;
                var newSpawnPoint = new Vector3((initialSpawnPoint.x + (j * 100) * 1), (initialSpawnPoint.y), initialSpawnPoint.z);
                newWordText.transform.position = newSpawnPoint;
                newWordText.transform.localScale = new Vector3(1f,1,1);

                newWordText.text = wordsToSolve[i][j].ToString();
                wordTextList.Add(newWordText);

            }
        }
   

        yield return new WaitForSeconds(0.5f);

        UsTextCount = 0;

        yield return new WaitForSeconds(1f);
        GameManager.Instance.playMagicSFX();



        StartCoroutine(setCompleteEvents(i));

    }

    IEnumerator spawnVowels(int i, int j)
    {
        yield return new WaitForSeconds(3f);

        var parent = GameObject.Find("GameUI").transform.Find("Canvas").transform.Find("WITParent").gameObject;
        var newWordText = Instantiate(wordTextToSolve, parent.transform, false);
        var initialSpawnPoint = newWordText.transform.position;
        var newSpawnPoint = new Vector3((initialSpawnPoint.x + (j * 100) * 1), (initialSpawnPoint.y), initialSpawnPoint.z);
        newWordText.transform.position = newSpawnPoint;
        newWordText.transform.localScale = new Vector3(1f, 1, 1);
        newWordText.text = wordsToSolve[i][j].ToString();
        wordTextList.Add(newWordText);

    }

    IEnumerator setCompleteEvents(int index)
    {
        yield return new WaitForSeconds(6f);

        var isLevelCompleted = false;

        wITText.SetActive(false);
        textImg.SetActive(false);

        for (int i = 0; i < wordsToSolve.Count; i++) 
        {
            if (wordSetInLevel[i].puzzleSets.Count==0)
            {
                if (i == (wordsToSolve.Count -1)) 
                {
                    isLevelCompleted= true;
                }
            }
        
        }

        if (isLevelCompleted == false) 
        {
            usCount++;
            setUnderscore(usCount);

            cameraLerpPosition = new Vector3(CameraInitialPoistion.x, CameraInitialPoistion.y + ((index + 1) * -20), puzzleCamera.transform.position.z);
            puzzleCamera.transform.position = cameraLerpPosition;
            

            for (int i = 0; i < wordTextList.Count; i++)
            {
                if (wordTextList[i] != null)
                {
                    Destroy(wordTextList[i].gameObject);
                }
                else
                {
                    wordTextList.Remove(wordTextList[i]);
      
                }

            }

            wordTextList.Clear();
        }
        else if (isLevelCompleted)
        {
            StartCoroutine(jigsawPuzzleCompletedEvent());
        }


    }


    private void puzzleLevelCompletedEvent()
    {
        //   var allPuzzleSetsInGame = FindObjectsOfType<PuzzleSet>();

        foreach (PuzzleSet puzzleSets in PuzzleSetInLevel) 
        { 
            Destroy(puzzleSets.gameObject);
        
        }

        for(int i = 0; i < wordTextList.Count; i++)
        {
            if (wordTextList[i]!=null)
            {
                Destroy(wordTextList[i].gameObject);
                //Debug.Log("Destroy: " + wordTextList[i]);
            }
            else
            {
                wordTextList.Remove(wordTextList[i]);
            }

        }

        wordTextList.Clear();

        wordsList.SetActive(true);
    }

    IEnumerator jigsawPuzzleCompletedEvent()
    {
        yield return new WaitForSeconds(0f);

        puzzleLevelCompletedEvent();
    }


    private void Update()
    {
        scoreText.text = GameManager.Instance.gameData.puzzlingPoints.ToString();
    }



    public void restartPuzzle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
