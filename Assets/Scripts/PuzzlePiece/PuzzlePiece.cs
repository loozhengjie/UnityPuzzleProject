using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isPlaced = false;
    private bool isPuzzleAtPlace=false;
    private float puzzleDepth;
    private LevelManager levelManager;

    public Vector3 lerpPosition;

    [HideInInspector]
    public bool isMovable;

    public PuzzleSet puzzleSet;


    private void Awake()
    {
        puzzleDepth = this.transform.position.z;
        this.enabled= false;

        // Store the initial position of the puzzle piece  
        initialPosition = transform.position;;
    }

    private void OnMouseDown()
    {
        // Check if the puzzle piece is already placed
        if (!isMovable)
            return;

        // Convert the mouse position to world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the offset between the mouse position and the puzzle piece's position
        Vector3 offset = transform.position - mousePosition;

        // Start dragging the puzzle piece
        StartCoroutine(DragPiece(offset));

        this.enabled= true;
    }

    private IEnumerator DragPiece(Vector3 offset)
    {
        while (Input.GetMouseButton(0))
        {
            // Convert the mouse position to world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Move the puzzle piece to the new position
            transform.position = mousePosition + offset;

            yield return null;
        }
    }

    private void OnMouseUp()
    {
        // Check if the puzzle piece is already placed
        if (!isMovable)
            return;

        // Snap the puzzle piece to its correct position if it's close enough
        SnapToPlace();

    }

    private void SnapToPlace()
    {
       if (isPuzzleAtPlace)
       {
            transform.position = initialPosition;
            puzzleSet.addCompletedPuzzlePiece(this);
            //levelManager.GetComponent<LevelManager>().addCompletedPuzzlePiece(this);
            isMovable = false;
       }
    }

    private void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, initialPosition));
        if (Vector3.Distance(transform.position, initialPosition) < 2.3f)
        {
            isPuzzleAtPlace = true;
        }
        else
        {
            isPuzzleAtPlace = false;
        }
    }
}
