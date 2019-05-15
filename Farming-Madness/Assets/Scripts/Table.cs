using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private SpriteRenderer sr;
    
    private Crop crop;
    private bool isEmpty = true;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact(Crop c, bool hasCan, PlayerInteraction player)
    {
        if (c != null && c.cropState == CropState.Stage5 && !hasCan && isEmpty)
        {
            SetCrop(c);
            player.UnsetCrop();
        }
        else if (c == null && !hasCan && !isEmpty)
        {
            player.SetCrop(crop);
            UnsetCrop();
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
}
