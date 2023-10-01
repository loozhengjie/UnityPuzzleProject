using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public struct WordSet
{
    public int setNumber;
    public List<PuzzleSet> puzzleSets;

    public WordSet(int setNumber, List<PuzzleSet> puzzleSets)
    {
        this.setNumber = setNumber;
        this.puzzleSets = puzzleSets;
    }
}