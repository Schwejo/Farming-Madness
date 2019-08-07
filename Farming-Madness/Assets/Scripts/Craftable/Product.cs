using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public Sprite productSprite;
    public ProductType productType;
    public int productTier;

    public Product (Sprite sprite, ProductType type, int tier)
    {
        productSprite = sprite;
        productType = type;
        productTier = tier;
    }

    public bool Equals(Product p)
    {
        return productSprite == p.productSprite;
    }
}