using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RezeptZaehler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        In [,] inArray = {
            {In.Bread, In.Flour, In.Water, In.Null},
            {In.Pizza, In.Flour, In.Tomato, In.Cheese},
            {In.Applepie, In.Flour, In.Butter, In.Apple},
            {In.StrawberryCake, In.Strawberry, In.Milk, In.Butter},
            {In.Flour, In.Wheat, In.Null, In.Null},
            {In.Croissont, In.Flour, In.Butter, In.Null},
            {In.ScrambledEggs, In.Egg, In.Egg,In.Null},
            {In.Marmelade, In.Water, In.Strawberry, In.Null},
            {In.Salad, In.Lettuce, In.Pepper, In.Tomato},
            {In.Lemonade, In.Water, In.Melon, In.Null},
            {In.Sandwhich, In.Bread, In.Salad, In.Cheese},
            {In.FruitSalad, In.Melon, In.Strawberry, In.Honey},
            {In.BakedPotato, In.Potato, In.Butter, In.Null},
            {In.Bruscetta, In.Tomato, In.Bread, In.Null},
            {In.Pencake, In.Flour, In.Egg, In.Milk},
            {In.Burger, In.Bread, In.Lettuce, In.Tomato},
            {In.Butter, In.Milk, In.Null, In.Null},
            {In.Cheese, In.Milk, In.Milk, In.Null},
            {In.Yoghurt, In.Milk, In.Honey, In.Null},
            {In.Milkshake, In.Milk, In.Strawberry, In.Null}
        };

        int count = Enum.GetNames(typeof(In)).Length -1;
        Debug.Log(count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum In
{
    Honey, Milk, Egg, Water, Wheat, Tomato, Potato, Apple, Lettuce, Strawberry, Melon, Carrot, Pepper, Flour, Bread, Pizza,
    Applepie, StrawberryCake, Croissont, ScrambledEggs, Marmelade, Salad, FruitSalad, BakedPotato, Lemonade, Bruscetta, Pencake,
    Burger, Butter, Cheese, Yoghurt, Milkshake, Sandwhich, Null
}