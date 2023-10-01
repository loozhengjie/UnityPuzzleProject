using System;
using UnityEngine;

[Serializable]
public struct PuzzleImageLibrary
{
    public char letter;
    public Sprite image;

    public PuzzleImageLibrary(char letter, Sprite image)
    {
        this.letter = letter;
        this.image = image;
    }
}
