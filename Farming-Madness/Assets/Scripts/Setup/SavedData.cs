using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    //Game
    public bool firstStart = true;
    public bool playTutorial = true;

    //Highscores
    public LevelStats level01 = null;
    public LevelStats level02 = null;
    public LevelStats level03 = null;

    //Keys
    public KeyCode p1Up = KeyCode.W;
    public KeyCode p1Down = KeyCode.S;
    public KeyCode p1Left = KeyCode.A;
    public KeyCode p1Right = KeyCode.D;
    public KeyCode p1Interact = KeyCode.E;

    public KeyCode p2Up = KeyCode.I;
    public KeyCode p2Down = KeyCode.K;
    public KeyCode p2Left = KeyCode.J;
    public KeyCode p2Right = KeyCode.L;
    public KeyCode p2Interact = KeyCode.O;

    public KeyCode pauseKey = KeyCode.Escape;
}
