using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public void Interact(Crop c, Product p, bool hasCan, PlayerInteraction player)
    {
        if (c != null && !hasCan)
        {
            player.UnsetCrop();
        }
        if (p != null && !hasCan)
        {
            player.UnsetProduct();
        }
    }
}
