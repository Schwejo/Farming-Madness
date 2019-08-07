using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tree", fileName = "New Tree")]
public class TreeAsset : ScriptableObject
{
    public int growingTimePerStage;

    public Sprite stage0;
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;
    public Sprite product;

    public Sprite GetSprite(TreeStage stage)
    {
        switch (stage)
        {
            case TreeStage.Stage0:
                return stage0;
            case TreeStage.Stage1:
                return stage1;
            case TreeStage.Stage2:
                return stage2;
            case TreeStage.Stage3:
                return stage3;
            case TreeStage.product:
                return product;
        }
        Debug.LogError("Could not return tree sprite");
        return null;
    }
}

public enum TreeStage
{
    Stage0, Stage1, Stage2, Stage3, product
}