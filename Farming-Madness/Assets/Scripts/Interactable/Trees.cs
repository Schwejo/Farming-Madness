using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trees : MonoBehaviour
{
    public TreeAsset treeAsset;
    public Product appleProduct;
    private TreeStage stage = TreeStage.Stage0;

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("Grow");
    }

    /* Grows an apple every growingTime second, while it is not on max apples */
    private IEnumerator Grow()
    {
        while(true)
        {
            yield return new WaitForSeconds(treeAsset.growingTimePerStage);
            
            if (stage < TreeStage.Stage3)
            {
                stage++;
                UpdateSprite();
            }

            if (stage == TreeStage.Stage3)
            {
                StopCoroutine("Grow");
            }
        }
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = treeAsset.GetSprite(stage);
    }

    public void Interact(Crop crop, bool hasCan, PlayerInteraction player)
    {
        if (crop == null && !hasCan && stage > TreeStage.Stage0)
        {
            Harvest();
            player.SetProduct(appleProduct);
        }
    }

    /* Harvest the tree, set back the treeStage by 1, restart coroutine */
    private void Harvest()
    {
        if (stage != TreeStage.Stage3)
        {
            StopCoroutine("Grow");
        }

        stage--;
        UpdateSprite();
        StartCoroutine("Grow");

        //TODO: Set player tree crop
    }   
}
