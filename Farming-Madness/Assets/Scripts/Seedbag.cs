using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seedbag : MonoBehaviour
{
    public Crop crop;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = crop.cropAsset.spriteStage0;
    }

    public void Interact(Crop c, bool hasCan, PlayerInteraction player)
    {
        if (c == null && !hasCan)
        {
            player.SetCrop(new Crop(crop.cropAsset, CropState.Stage0));
        }
    }
}
