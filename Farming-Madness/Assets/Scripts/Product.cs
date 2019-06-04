using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public Sprite productSprite;

    public Product (Sprite sprite)
    {
        productSprite = sprite;
    }

    public bool Equals(Product p)
    {
        return productSprite == p.productSprite;
    }
}