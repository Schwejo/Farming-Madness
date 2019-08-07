using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Crop
{
    public CropAsset cropAsset;
    public CropState cropState;

    public Field field;

    private float growLevel = 0;


    public Crop (CropAsset asset, CropState state)
    {
        cropAsset = asset;
        cropState = state;
    }

    /* Grows a crop by amount (=Time.deltaTime) */
    public void Grow(float amount)
    {
        growLevel += amount / 10f;

        if (growLevel >= 1f){
            SetState(CropState.Stage4);
        }
        else if (growLevel > 0.66f)
        {
            SetState(CropState.Stage3);
        }
        else if (growLevel > 0.33f)
        {
            SetState(CropState.Stage2);
        }
    }

    public void NextState()
    {
        cropState = cropState + 1;
    }

    /* Changes CropState if it has changed. If changed, field sprite gets updated. */
    public void SetState(CropState state)
    {
        if (cropState != state)
        {
            cropState = state;
            if (field != null)
            {
                field.UpdateSprite();
            }
        }
        
    }

    public Sprite GetSprite()
    {
        if (cropAsset == null)
        {
            Debug.LogError("No CropAsset");
            return null;
        }

        switch (cropState)
        {
            case CropState.Stage0:
                return cropAsset.spriteStage0;
            case CropState.Stage1:
                return cropAsset.spriteStage1;
            case CropState.Stage2:
                return cropAsset.spriteStage2;
            case CropState.Stage3:
                return cropAsset.spriteStage3;
            case CropState.Stage4:
                return cropAsset.spriteStage4;
            case CropState.Stage5:
            return cropAsset.spriteStage5;
        }

        Debug.LogError("Could not find CropStage");
        return null;
    }

    public bool hasCropAsset()
    {
        if (cropAsset == null)
            return false;
        else
            return true;
    }

    public void SetField(Field f)
    {
        field = f;
    }

    public void UnsetField()
    {
        field = null;
    }

    /* Makes a product out of this crop to interact with production buildings */
    public Product MakeProductFromCrop()
    {
        if (cropState == CropState.Stage5)
        {
            Product product = new Product(GetSprite(), cropAsset.cropType, cropAsset.tier);
            return product;
        }
        else
        {
            Debug.LogError("Could not make product from crop, wrong stage!");
            return null;
        }
    }
}

 public enum CropState 
{
    Stage0, Stage1, Stage2, Stage3, Stage4, Stage5
}