using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PuzzleSet : MonoBehaviour
{
    [HideInInspector]
    public char puzzleSetAlphabet;
    public int setID;
   
    private PuzzlePiece[] puzzlePieceInSet;
    private SpriteRenderer puzzleImage;
    private GameManager gameManager;
    private LevelManager levelManager;
    private bool isSetCompleted;
    public List<PuzzlePiece> completedPuzzlePieces = new List<PuzzlePiece>();

    private Camera puzzleCamera;


    private bool isScatteringPuzzle = false;
    public List<GameObject> draggablePuzzlePieces = new List<GameObject>();

    private void Awake()
    {
        this.gameObject.SetActive(true);

        puzzleCamera = FindObjectOfType<Camera>();
        isSetCompleted = false;

        this.enabled = false;
        isScatteringPuzzle = false;

        levelManager = FindObjectOfType<LevelManager>();
    }

    //---------Main Function Called by Level Manager---------------//
    public void setupPuzzle()
    {
        var puzzleImageGO = this.transform.Find("PuzzleImage");
        puzzleImage = puzzleImageGO.GetComponent<SpriteRenderer>();

        SetupImage();

        var board = this.transform.Find("PuzzleBoard");
        puzzlePieceInSet = board.transform.GetComponentsInChildren<PuzzlePiece>();


        foreach (PuzzlePiece piece in puzzlePieceInSet)
        {
            piece.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            piece.gameObject.GetComponent<SpriteMask>().sprite = piece.GetComponent<SpriteRenderer>().sprite;

            if (piece.gameObject.GetComponent<SpriteMask>().sprite == null)
            {
                Debug.Log("sprite not found");
            }

            //spawn image as mask for puzzle
            SpriteRenderer newImage = Instantiate(puzzleImage, piece.transform, true);
            newImage.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

            spawnDraggablePieces(piece);
            
        }
        

        StartCoroutine(startPuzzleSet());


    }
    //------------------------------------------------------------//


    //-------------------Sub functions called in setupPuzzle-----------------------------------//
    IEnumerator startPuzzleSet()
    {
        yield return new WaitForSeconds(0.5f);

        //Destroy(puzzleImage.gameObject);
        var imageCol = puzzleImage.GetComponent<SpriteRenderer>().color;
        puzzleImage.GetComponent<SpriteRenderer>().color = new Color(imageCol.r, imageCol.g, imageCol.b, 0.2f);

        foreach (PuzzlePiece piece in puzzlePieceInSet)
        {
            disablePuzzlePieceSet(piece);
        }

        scatterDraggablePuzzlePiece();
        //this.enabled= true;
    }
    private void SetupImage()
    {
        //gameManager = FindObjectOfType<GameManager>();
        //for (int i =0; i < gameManager.imageLibrary.Length; i++) 
        //{
        //    if (puzzleSetAlphabet == gameManager.imageLibrary[i].letter)
        //    {
        //        puzzleImage.sprite = gameManager.imageLibrary[i].image;
        //    }
        //}

        levelManager = FindObjectOfType<LevelManager>();
        for (int i = 0; i < levelManager.imageLibrary.Length; i++)
        {
            if (puzzleSetAlphabet == levelManager.imageLibrary[i].letter)
            {
                puzzleImage.sprite = levelManager.imageLibrary[i].image;
            }
        }
    }
    private void spawnDraggablePieces(PuzzlePiece spawnPuzzlePiece)
    {
        //spawn draggable puzzle pieces
        PuzzlePiece moveablepiece = Instantiate(spawnPuzzlePiece, this.gameObject.transform.Find("DraggablePieces").transform, true);
        moveablepiece.isMovable = true;
        moveablepiece.puzzleSet = this;
        draggablePuzzlePieces.Add(moveablepiece.gameObject);

        switch (GameManager.Instance.gameData.levelDifficulty)
        {
            case LevelDifficulty.Beginner:

                var xRandom = (Random.Range(-15, 15));
                if (xRandom == 0 || xRandom == 1 || xRandom == 2)
                {
                    xRandom += 3;
                }
                else if (xRandom == -1 || xRandom == -2 || xRandom == -3)
                {
                    xRandom -= 3;
                }

                var yRandom = (Random.Range(-3, 3));

                var lerpPosition = new Vector3(moveablepiece.transform.position.x + xRandom, moveablepiece.transform.position.y + yRandom, -1);
                moveablepiece.lerpPosition = lerpPosition;


                break;

            case LevelDifficulty.Intermediate:
            case LevelDifficulty.Advanced:
                var xRandom1 = (Random.Range(-15, 15));
                if (xRandom1 == 0 || xRandom1 == 1 || xRandom1 == 2)
                {
                    xRandom1 += 2;
                }
                else if (xRandom1 == -1 || xRandom1 == -2 || xRandom1 == -3)
                {
                    xRandom1 -= 2;
                }

                var yRandom1 = (Random.Range(-2, 2));

                var lerpPosition1 = new Vector3(moveablepiece.transform.position.x + xRandom1, moveablepiece.transform.position.y + yRandom1, -1);
                moveablepiece.lerpPosition = lerpPosition1;


                break;

        }

  
    }

    private void disablePuzzlePieceSet(PuzzlePiece piece)
    {
        if (piece != null)
        {
            piece.gameObject.SetActive(false);
            //var pieceColor = piece.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
            //piece.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(pieceColor.r, pieceColor.g, pieceColor.b, 0.2f);
        }
    }
    private void scatterDraggablePuzzlePiece()//(float time)
    {
        if (isScatteringPuzzle == false)
        {
            foreach (var piece in draggablePuzzlePieces)
            {
                piece.GetComponent<PuzzlePiece>().isMovable = true;
                piece.transform.position = piece.GetComponent<PuzzlePiece>().lerpPosition;
                //piece.transform.position = Vector3.MoveTowards(piece.transform.position, piece.GetComponent<PuzzlePiece>().lerpPosition, time * 5f);
                //Debug.Log("Lerping " + piece.transform.position);
            }
            isScatteringPuzzle = true; 
        }
       
    }

    private void Update()
    {
        //scatterDraggablePuzzlePiece(Time.deltaTime);
        //Debug.Log("Boolean is" + isScatteringPuzzle);
    }
    //--------------------------------------------------------------------------------------------//



    //-----------------Main Function called by puzzle pieces--------------------------------------//
    public void addCompletedPuzzlePiece(PuzzlePiece piece)
    {
        completedPuzzlePieces.Add(piece);
        checkPuzzleSet();
    }
    //--------------------------------------------------------------------------------------------//


    //-----------------Sub Function called by addCompletedPuzzlePIece--------------------------------------//
    private void checkPuzzleSet()
    {
        if (completedPuzzlePieces.Count == draggablePuzzlePieces.Count)
        {
            //levelManager.addCompletedSet(this);
            levelManager.removeSetFromGroup(this);
        }
    }
    //--------------------------------------------------------------------------------------------//

}
