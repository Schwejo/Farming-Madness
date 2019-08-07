using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Crop")]
public class CropAsset : ScriptableObject
{
    public ProductType cropType;
    public int tier = 1;

    public Sprite spriteStage0; //Seedbag
    public Sprite spriteStage1; //Seed
    public Sprite spriteStage2;
    public Sprite spriteStage3;
    public Sprite spriteStage4; //Done
    public Sprite spriteStage5; //Product
}

/* List of all Crops and Products */
public enum ProductType 
{
    Null, Carrot, Potato, Wheat, Corn, Lettuce, Melon, Pepper, Tomato, Strawberry, Milk, Honey, Apple, Egg,
    Water, Flour, Bread, Pizza, Applepie, StrawberryCake, Croissant, ScrambledEggs, Jelly, Salad,
    FruitSalad, BakedPotato, Lemonade, Bruschetta, Pancake, Burger, Butter, Cheese, Yoghurt, Milkshake,
    Sandwich
}