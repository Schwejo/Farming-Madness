using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public void Interact(Crop c, Product p, bool hasCan, PlayerInteraction player)
    {
        if (c != null && !hasCan)
        {
            player.UnsetCrop(false);
            AudioManager.instance.Play("trash");
        }
        if (p != null && !hasCan)
        {
            player.UnsetProduct(false);
            AudioManager.instance.Play("trash");
        }
    }
}
