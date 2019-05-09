using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public void Interact(Crop c, bool hasCan, PlayerInteraction player)
    {
        if (c != null && !hasCan)
        {
            player.UnsetCrop();
        }
    }
}
