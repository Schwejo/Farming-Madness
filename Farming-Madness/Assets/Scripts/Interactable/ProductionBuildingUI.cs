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
    public SpriteRenderer[] inputOverlays;
    public SpriteRenderer oneItemBG;
    public SpriteRenderer twoItemBG;
    public SpriteRenderer threeItemBG;
    private int recipeInputLength;

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
    private Color quarterOpacity;
    private Color halfOpacity;
    private Color fullOpacity;


    private void Start() 
    {
        recipeSelectionUI.SetActive(false);
        recipeInputUI.SetActive(false);
        cookingCanvasObject.SetActive(false);

        zeroOpacity = new Color(255,255,255,0);
        quarterOpacity = new Color(255,255,255,0.25f);
        halfOpacity = new Color(255,255,255,0.5f);
        fullOpacity = new Color(255,255,255,1);

        oneItemBG.color = zeroOpacity;
        twoItemBG.color = zeroOpacity;
        threeItemBG.color = zeroOpacity;
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

        recipeInputLength = recipe.inputProducts.Length;

        //Select correct background
        switch (recipeInputLength)
        {
            case 1:
                oneItemBG.color = fullOpacity;
                break;
            case 2:
                twoItemBG.color = fullOpacity;
                break;
            case 3:
                threeItemBG.color = fullOpacity;
                break;
        }

        //Shows input products
        bool occurredOnce = false;
        int indexOutput = 0;
        for (int i = 0; i < recipeInputLength; i++) 
        {
            inputOverlays[i].sprite = recipe.inputProducts[i].productSprite;
            indexOutput = i+1;

            //Full opacity only, if item was already put in (=fist product)
            if (recipe.inputProducts[i].Equals(firstProduct) && !occurredOnce)
            {
                inputOverlays[i].color = fullOpacity;
                occurredOnce = true;
            } 
            else 
            {
                inputOverlays[i].color = quarterOpacity;
            }
        }

        //Show output product
        inputOverlays[indexOutput].sprite = recipe.outputProduct.productSprite;
        inputOverlays[indexOutput].color = quarterOpacity;
    }

    public void InputProduct(Product input)
    {
        for (int i = 0; i < recipeInputLength; i++)
        {
            if (inputOverlays[i].sprite == input.productSprite && inputOverlays[i].color != fullOpacity)
            {
                inputOverlays[i].color = fullOpacity;
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
        cookingOverlay.color = quarterOpacity;
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
