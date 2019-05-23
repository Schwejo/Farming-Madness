using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Crop")]
public class CropAsset : ScriptableObject
{
    public CropType cropType;

    public Sprite spriteStage0; //Seedbag
    public Sprite spriteStage1; //Seed
    public Sprite spriteStage2;
    public Sprite spriteStage3;
    public Sprite spriteStage4; //Done
    public Sprite spriteStage5; //Product
}

/* List of all Crops */
public enum CropType 
{
    Carrot, Potato, Wheat, Corn, Lettuce, Melon, Pepper, Tomato, Strawberry, Milk, Honey, Apple, Egg
}