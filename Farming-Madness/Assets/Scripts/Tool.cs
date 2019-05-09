using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : ScriptableObject
{
    public Sprite toolSprite;

    public Tooltype toolType;
}

public enum Tooltype 
{
    WateringCan
}