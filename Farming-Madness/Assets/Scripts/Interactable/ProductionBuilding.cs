using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductionBuilding : MonoBehaviour
{
    [Header("Recipes")]
    public RecipeAsset[] recipes;
    public List<RecipeAsset> possibleRecipes; //set private if working
    private RecipeAsset activeRecipe;

    [Header("Input")]
    public List<Product> inputProducts; //set private if working
    
    [Header("Selection")]
    private bool recipeSelectionActive = false;
    private int selectedRecipe = 0;

    [Header("Cooking")]
    public bool cookingFinished = false;
    public GameObject smoke;

    private PlayerInteraction player;
    private ProductionBuildingUI ui;


    private void Start()
    {
        ui = GetComponent<ProductionBuildingUI>();

        smoke.SetActive(false);
        
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
        //initial product and recipe selection
        if (product != null && activeRecipe == null)
        {
            player = p;
            player.AllowMovement(false);

            inputProducts.Add(product);
            SearchPossibleRecipes();
            
            //check if there are possible recipes
            if (possibleRecipes.Any()) 
            {
                recipeSelectionActive = true;
                ui.ShowRecipeSelection(possibleRecipes[selectedRecipe]);
                p.UnsetCrop();
                p.UnsetProduct();
            }
            //no recipes found
            else
            {
                ui.ShowText("No possible recipes found.");
                p.AllowMovement(true);
                inputProducts.Remove(product);
            }
        }
        //add an item
        else if (product != null && activeRecipe != null) 
        {
            if (AddProduct(product))
            {
                p.UnsetCrop();
                p.UnsetProduct();
            } 
        }
        //finished cooking, taking up product
        else if (product == null && cookingFinished)
        {
            TakeUpProduct(p);
        }
    }

    /* Searches all recipes, that include the first input product,
     * and adds them to the possibleRecipes list. 
     */
    private void SearchPossibleRecipes()
    {
        foreach (RecipeAsset recipe in recipes)
        {
            for (int i = 0; i < recipe.inputProducts.Length; i++)
            {
                if (recipe.inputProducts[i].Equals(inputProducts[0]))
                {
                    possibleRecipes.Add(recipe);
                    break;
                }
            }
        }
    }

    /* Select a recipe using the movement keys. */
    private void SelectRecipe()
    {
        if (Input.GetKeyDown(player.keyLeft))
        {
            selectedRecipe -= 1;
            if (selectedRecipe < 0) 
            {
                selectedRecipe = possibleRecipes.Count - 1;
            }

            ui.ShowRecipeSelection(possibleRecipes[selectedRecipe]);
        }
        if (Input.GetKeyDown(player.keyRight))
        {
            selectedRecipe += 1;
            if (selectedRecipe >= possibleRecipes.Count) 
            {
                selectedRecipe = 0;
            }

            ui.ShowRecipeSelection(possibleRecipes[selectedRecipe]);
        }
        if (Input.GetKeyDown(player.keyInteract))
        {
            activeRecipe = possibleRecipes[selectedRecipe];
            recipeSelectionActive = false;

            player.AllowMovement(true);

            ui.ShowActiveRecipe(activeRecipe, inputProducts[0]);

            // if only one input is needed, start cooking immediatly
            if (activeRecipe.inputProducts.Length == 1)
            {
                StartCoroutine(WaitBeforeCooking());
                return;
            }
        }
    }

    /* Tries to add an product to the input products array. 
     * Returns true if product could be used.
     */
    private bool AddProduct(Product product) 
    {
        bool productNeeded = false;

        bool searchSecondOccurrence = false;
        int productCount = 0;
        
        //check input products for (possible) duplicate inputs
        for (int j = 0; j < inputProducts.Count; j++)
        {
            if (inputProducts[j].Equals(product)) 
            {
                searchSecondOccurrence = true;
                productCount += 1;
            }
        }

        if (productCount <= 1) 
        {
            for (int i = 0; i < activeRecipe.inputProducts.Length; i++) 
            {
                if (activeRecipe.inputProducts[i].Equals(product))
                {
                    if (!searchSecondOccurrence) 
                    {
                        productNeeded = true;
                        inputProducts.Add(product);
                        ui.InputProduct(product);
                        break;
                    } 
                    else if (searchSecondOccurrence)
                    {
                        searchSecondOccurrence = false;
                    }
                }
            }
        }

        if (!productNeeded) 
        {
            ui.ShowText("Can't use this item here.");
        }

        //start cooking, if all ingredients are in
        if (inputProducts.Count == activeRecipe.inputProducts.Length) 
        {
            StartCoroutine(WaitBeforeCooking());
        }

        return productNeeded;
    }

    /*
     * Waits for 1 second before starting to cook, so you can see the filled out recipe.
     */
    private IEnumerator WaitBeforeCooking()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(Cook());
    }
    
    private IEnumerator Cook() 
    {
        ui.StartCooking(activeRecipe);
        smoke.SetActive(true);
        yield return new WaitForSeconds(activeRecipe.cookingTime);
        cookingFinished = true;
        AudioManager.instance.Play("item_ready");
        smoke.SetActive(false);
    }

    private void TakeUpProduct(PlayerInteraction p) 
    {
        p.SetProduct(activeRecipe.outputProduct);
        Reset();
    }

    /* Resets everything back to normal. */
    private void Reset()
    {
        recipeSelectionActive = false;
        selectedRecipe = 0;
        activeRecipe = null;
        possibleRecipes.Clear();
        inputProducts.Clear();
        cookingFinished = false;
        ui.Reset();
    }
}
