using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe", fileName = "new Recipe")]
public class RecipeAsset : ScriptableObject
{
    public int recipeTier;
    public float cookingTime;

    public Product[] inputProducts;

    public Product outputProduct;
}