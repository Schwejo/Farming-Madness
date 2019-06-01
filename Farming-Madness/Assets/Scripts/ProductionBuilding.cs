using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : MonoBehaviour
{
    [Header("Recipes")]
    public RecipeAsset[] recipes;
    private RecipeAsset activeRecipe;
    private bool recipeIsActive = false;

    private List<Product> inputProducts;
    
    [Header("UI")]
    public GameObject recipeSelection;
    public SpriteRenderer productOverlay;


    private void Start()
    {
        recipeSelection.SetActive(false);

        inputProducts = new List<Product>();
    }

    public void Interact(Crop crop, Product product, bool hasCan, PlayerInteraction player)
    {
        if (crop != null || product != null && !hasCan)
        {
            recipeSelection.SetActive(true);
        }
    }

    private void SearchPossibleRecipes()
    {
        //sucht Rezepte je nach erstem Input
    }

    private void SelectRecipe()
    {
        //wähle Rezept aus possible Recipes aus
    }
}
