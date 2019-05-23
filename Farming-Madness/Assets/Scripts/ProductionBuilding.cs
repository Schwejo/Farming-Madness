using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : MonoBehaviour
{
    public RecipeAsset[] recipes;
    private RecipeAsset activeRecipe;
    private bool recipeIsActive = false;

    private List<Crop> inputCrops;
    private List<Product> inputProducts;
    
    /* UI Overlay */
    public GameObject recipeSelection;
    public SpriteRenderer productOverlay;


    private void Start()
    {
        recipeSelection.SetActive(false);

        inputCrops = new List<Crop>();
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
        
    }
}
