/*using System.Collections;
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
    
    [Header("UI - Selection")]
    public GameObject recipeSelectionUI;
    public SpriteRenderer recipeSelectionProductUI;
    private bool recipeSelectionActive = false;
    private int selectedRecipe;

    [Header("UI - RecipeInput")]
    public GameObject recipeInputUI;
    public SpriteRenderer[] inputBG;
    public SpriteRenderer[] inputOverlay;
    public SpriteRenderer outputBG;
    public SpriteRenderer outputOverlay;

    private PlayerInteraction player;


    private void Start()
    {
        recipeSelectionUI.SetActive(false);
        recipeInputUI.SetActive(false);

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
    and adds them to the possibleRecipes list. */ /*
    
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

    /* Enables selection UI and selection input */ /*
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
            selectedRecipe -= 1;
            if (selectedRecipe < 0) 
            {
                selectedRecipe = possibleRecipes.Count - 1;
            }
        }
        if (Input.GetKeyDown(player.keyRight))
        {
            selectedRecipe += 1;
            if (selectedRecipe >= possibleRecipes.Count) 
            {
                selectedRecipe = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            activeRecipe = possibleRecipes[selectedRecipe];
            recipeSelectionActive = false;
            recipeSelectionUI.SetActive(false);
            ShowSelectedRecipe();
        }
        recipeSelectionProductUI.sprite = possibleRecipes[selectedRecipe].outputProduct.productSprite;
    }

    private void ShowSelectedRecipe() 
    {
        recipeInputUI.SetActive(true);

        outputOverlay.sprite = activeRecipe.outputProduct.productSprite;

        for (int i = 0; i < activeRecipe.inputProducts.Length; i++)
        {
            inputOverlay[i].sprite = activeRecipe.inputProducts[i].productSprite;
            if (true) return;
                //inputOverlay[i].color.a;
        }
    }
}
*/