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

    public void OnEnable()
    {
        Crop.OnStateChanged += UpdateSprite;
    }

    public void OnDisable()
    {
        Crop.OnStateChanged -= UpdateSprite;
    }

    public void Interact(Crop c, bool hasCan, PlayerInteraction player)
    {
        if (c == null && hasCan && !isWet)
        {
            Water();
        }
        else if (c != null && !hasCrop)
        {
            crop = c;
            PlantSeed(crop);
            player.UnsetCrop();
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
    }

    private void Dry()
    {
        mainSpriteRenderer.sprite = fieldDrySprite;
        isWet = false;
    }

    private void PlantSeed(Crop c)
    {
        c.NextState();
        cropOverlay.sprite = c.GetSprite();
        hasCrop = true;
    }

    private void Harvest(PlayerInteraction p)
    {
        crop.NextState();
        p.SetCrop(crop);
        crop = null;
        cropOverlay.sprite = null;
        hasCrop = false;
        Dry();
    }

    private void UpdateSprite()
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
}
