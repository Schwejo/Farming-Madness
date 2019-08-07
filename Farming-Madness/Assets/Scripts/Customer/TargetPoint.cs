using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    public bool isUsed = false;
    public Customer customer;
    private int[] soldItemsIndices;

    private void Start()
    {
        soldItemsIndices = new int[] {-1,-1,-1};
    }

    public void Interact(Product product, PlayerInteraction player)
    {
        if (product != null && customer != null)
        {
            for (int i = 0; i < customer.items.Length; i++)
            {
                if (customer.items[i].productType == product.productType)
                {
                    //for possible duplicate items
                    if (!Array.Exists(soldItemsIndices, element => element == i))
                    {
                        bool itemSold = customer.SellItem(i);
                        if (itemSold)
                            player.UnsetProduct();
                        else
                            Debug.Log("Not sold, no time left");

                        for (int j = 0; j < soldItemsIndices.Length; j++)
                        {
                            if (soldItemsIndices[j] == -1)
                            {
                                soldItemsIndices[j] = i;
                            }
                        }
                        break;
                    }
                }
            }
        }
    }

    public void Reset()
    {
        isUsed = false;
        customer = null;
        for (int i = 0; i < soldItemsIndices.Length; i++)
        {
            soldItemsIndices[i] = -1;
        }
    }

    public bool GetIsUsed() 
    {
        return isUsed;
    }

    public void SetIsUsed(bool value)
    {
        isUsed = value;
    }

    public Customer GetCustomer()
    {
        return customer;
    }
    
    public void SetCustomer(Customer c)
    {
        customer = c;
    }
}
