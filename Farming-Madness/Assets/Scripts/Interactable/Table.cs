using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private SpriteRenderer sr;
    
    private Crop crop;
    private Product product;
    private bool isEmpty = true;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact(Crop c, Product p, bool hasCan, PlayerInteraction player)
    {
        if (c != null && c.cropState == CropState.Stage5 && !hasCan && isEmpty)
        {
            SetCrop(c);
            player.UnsetCrop();
        }
        else if (p != null && !hasCan && isEmpty)
        {
            SetProduct(p);
            player.UnsetProduct();
        }
        else if (c == null && p == null && !hasCan && !isEmpty)
        {
            if (crop != null)
            {
                player.SetCrop(crop);
                UnsetCrop();
            }
            else
            {
                player.SetProduct(product);
                UnsetProduct();
            }
        }
    }

    private void SetCrop(Crop c)
    {
        crop = c;
        sr.sprite = crop.GetSprite();
        isEmpty = false;
    }

    private void UnsetCrop()
    {
        crop = null;
        sr.sprite = null;
        isEmpty = true;
    }

    private void SetProduct(Product p)
    {
        product = p;
        sr.sprite = p.productSprite;
        isEmpty = false;
    }

    private void UnsetProduct()
    {
        product = null;
        sr.sprite = null;
        isEmpty = true;
    }
}
