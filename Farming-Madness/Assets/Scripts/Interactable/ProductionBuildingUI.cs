using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBuildingUI : MonoBehaviour
{
    [Header("Selection")]
    public GameObject recipeSelectionUI;
    public SpriteRenderer recipeSelectionProductUI;

    [Header("Input")]
    public GameObject recipeInputUI;
    public SpriteRenderer[] inputBG;
    public SpriteRenderer[] inputOverlay;
    public SpriteRenderer outputBG;
    public SpriteRenderer outputOverlay;

    [Header("Cooking")]
    public GameObject cookingCanvasObject;
    public Image cookingOverlay;
    public Image fillingBar;
    private bool cooking = false;
    private float cookingTime;
    private float timer;

    [Header("Notification Text")]
    public GameObject textObject;

    private Color zeroOpacity;
    private Color halfOpacity;
    private Color fullOpacity;


    private void Start() 
    {
        recipeSelectionUI.SetActive(false);
        recipeInputUI.SetActive(false);
        cookingCanvasObject.SetActive(false);

        zeroOpacity = new Color(255,255,255,0);
        halfOpacity = new Color(255,255,255,0.5f);
        fullOpacity = new Color(255,255,255,1);

        for (int i = 0; i < inputBG.Length; i++)
        {
            inputBG[i].color = zeroOpacity;
        }
    }

    private void Update() 
    {
        if (cooking)
        {
            timer += Time.deltaTime;
            fillingBar.fillAmount = timer / cookingTime;
            if (timer >= cookingTime)
                cooking = false;
        }
    }

    public void ShowRecipeSelection(RecipeAsset recipe)
    {
        recipeSelectionUI.SetActive(true);
        recipeSelectionProductUI.sprite = recipe.outputProduct.productSprite;
    }

    /* Shows the selected recipe and highlights the product, the player handed in. */
    public void ShowActiveRecipe(RecipeAsset recipe, Product firstProduct)
    {
        recipeSelectionUI.SetActive(false);
        recipeInputUI.SetActive(true);

        bool occurredOnce = false;

        //Shows input products
        for (int i = 0; i < recipe.inputProducts.Length; i++) 
        {
            inputBG[i].color = fullOpacity;
            inputOverlay[i].sprite = recipe.inputProducts[i].productSprite;

            if (recipe.inputProducts[i].Equals(firstProduct) && !occurredOnce)
            {
                inputOverlay[i].color = fullOpacity;
                occurredOnce = true;
            } 
            else 
            {
                inputOverlay[i].color = halfOpacity;
            }
        }

        //Shows output product
        outputOverlay.sprite = recipe.outputProduct.productSprite;
    }

    public void InputProduct(Product input)
    {
        for (int i = 0; i < inputOverlay.Length; i++)
        {
            if (inputOverlay[i].sprite == input.productSprite && inputOverlay[i].color != fullOpacity)
            {
                inputOverlay[i].color = fullOpacity;
                break;
            }
        }
    }

    public void StartCooking(RecipeAsset recipe)
    {
        cookingCanvasObject.SetActive(true);
        recipeSelectionUI.SetActive(false);
        recipeInputUI.SetActive(false);
        cookingOverlay.sprite = recipe.outputProduct.productSprite;
        cookingOverlay.color = halfOpacity;
        cookingTime = recipe.cookingTime;
        timer = 0;
        cooking = true;
        fillingBar.sprite = recipe.outputProduct.productSprite;
        fillingBar.fillAmount = 0;
    }

    public void ShowText(string text) 
    {
        //Dispay text
        Debug.Log(text);
    }

    public void Reset()
    {
        recipeSelectionUI.SetActive(false);
        recipeInputUI.SetActive(false);
        cookingCanvasObject.SetActive(false);
    }
}
