using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : MonoBehaviour
{
    [Header("Recipes")]
    public RecipeAsset[] recipes;
    private RecipeAsset activeRecipe;

    [Header("Input")]
    public List<Product> inputProducts; //set private if working
    public List<RecipeAsset> possibleRecipes; //set private if working
    private int possibleRecipesLength;
    
    [Header("Selection")]
    public GameObject recipeSelectionUI;
    public SpriteRenderer recipeSelectionProductUI;
    private bool recipeSelectionActive = false;
    private int selectedRecipe;

    [Header("UI - Recipe")]
    public GameObject recipeUI;

    private PlayerInteraction player;


    private void Start()
    {
        recipeSelectionUI.SetActive(false);

        inputProducts = new List<Product>();
        possibleRecipes = new List<RecipeAsset>();
    }

    private void Update()
    {
        if (recipeSelectionActive)
        {
            SelectRecipe();
        }
    }

    public void Interact(Product product, PlayerInteraction p)
    {
        if (product != null && activeRecipe == null)
        {
            player = p;
            player.AllowMovement(false);
            inputProducts.Add(product);
            SearchPossibleRecipes();
            ShowRecipeSelection();
        }
    }

    /* Searches all recipes, that include the first input product,
    and adds them to the possibleRecipes list. */
    private void SearchPossibleRecipes()
    {
        foreach (RecipeAsset recipe in recipes)
        {
            for (int i = 0; i < recipe.inputProducts.Length; i++)
            {
                if (recipe.inputProducts[i].Equals(inputProducts[0]))
                {
                    possibleRecipes.Add(recipe);
                }
            }
        }
        possibleRecipesLength = possibleRecipes.Count;
    }

    /* Enables selection UI and selection input */
    private void ShowRecipeSelection()
    {
        recipeSelectionActive = true;
        selectedRecipe = 0;
        recipeSelectionUI.SetActive(true);
        recipeSelectionProductUI.sprite = possibleRecipes[selectedRecipe].outputProduct.productSprite;
    }

    private void SelectRecipe()
    {
        if (Input.GetKeyDown(player.keyLeft))
        {
            //TODO Durchlaufen der Possible Recipe Liste
        }
        if (Input.GetKeyDown(player.keyRight))
        {

        }
        if (Input.GetKeyDown(player.interactKey))
        {
                
        }
    }
}
