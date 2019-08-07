using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProducts : MonoBehaviour
{
    [Header("Products")]
    public Product[] possibleT1Products;
    public Product[] possibleT2Products;
    public Product[] possibleT3Products;

    [Header("Tier")]
    public int[] tierProbability;
    private int tier;

    [Header("Items")]
    public int[] itemCountProbability;
    private int numberOfItems;


    public Product[] GetRandomProducts()
    {
        numberOfItems = RandomNumberOfItems();
        Product[] randomProducts = new Product[numberOfItems];

        for (int i = 0; i < numberOfItems; i++)
        {
            randomProducts[i] = RandomProductAndTier();
        }
        return randomProducts;
    }

    private int RandomNumberOfItems() 
    {
        int i = Random.Range(0, itemCountProbability.Length - 1);
        return itemCountProbability[i];
    }

    private Product RandomProductAndTier()
    {
        int randomNumber = Random.Range(0, tierProbability.Length - 1);
        tier = tierProbability[randomNumber];

        switch (tier) 
        {
            case 1:
                randomNumber = Random.Range(0, possibleT1Products.Length);
                return possibleT1Products[randomNumber];
            case 2:
                randomNumber = Random.Range(0, possibleT2Products.Length);
                return possibleT2Products[randomNumber];
            case 3:
                randomNumber = Random.Range(0, possibleT3Products.Length);
                return possibleT3Products[randomNumber];
        }

        Debug.LogError("Could not get a random product and/or tier");
        return null;
    }
}
