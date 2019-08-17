using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Crop crop;
    
    private SpriteRenderer mainSpriteRenderer;

    public Sprite fieldDrySprite;
    public Sprite fieldWetSprite;

    public SpriteRenderer cropOverlay;
    //public GameObject uIOverlay;

    private bool isWet = false;
    private bool hasCrop  = false;

    public void Start() 
    {
        mainSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interact(Crop c, bool hasCan, PlayerInteraction player)
    {
        if (c == null && hasCan && !isWet)
        {
            Water();
        }
        else if (c != null && !hasCrop && c.cropState == CropState.Stage0)
        {
            crop = c;
            PlantSeed();
            player.UnsetCrop(false);
        }
        else if (c == null && !hasCan && crop.hasCropAsset() && crop.cropState == CropState.Stage4)
        {
            Harvest(player);
        }
    }

    private void Water()
    {
        mainSpriteRenderer.sprite = fieldWetSprite;
        isWet = true;
        AudioManager.instance.Play("water");
    }

    private void Dry()
    {
        mainSpriteRenderer.sprite = fieldDrySprite;
        isWet = false;
    }

    private void PlantSeed()
    {
        crop.NextState();
        crop.SetField(this);
        cropOverlay.sprite = crop.GetSprite();
        hasCrop = true;
        AudioManager.instance.Play("plant");
    }

    private void Harvest(PlayerInteraction p)
    {
        crop.NextState();
        crop.UnsetField();
        p.SetCrop(crop);
        crop = null;
        cropOverlay.sprite = null;
        hasCrop = false;
        Dry();
    }

    public void UpdateSprite()
    {
        cropOverlay.sprite = crop.GetSprite();
    }

    private void Update() 
    {
        if (crop != null && crop.hasCropAsset() && isWet)
        {
            crop.Grow(Time.deltaTime);
        }
    }

    public bool GetIsWet()
    {
        return isWet;
    }
}
