using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppleTree : MonoBehaviour
{
    public TreeAsset appleAsset;
    private TreeStage stage = TreeStage.Stage0;

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("Grow");
    }

    private IEnumerator Grow()
    {
        while(true)
        {
            yield return new WaitForSeconds(appleAsset.growingTimePerStage);
            
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
        spriteRenderer.sprite = appleAsset.GetSprite(stage);
    }

    public void Interact(Crop crop, bool hasCan, PlayerInteraction player)
    {
        if (crop == null && !hasCan)
        {
            Harvest();
        }
    }

    /* Harvest an Apple, set back the TreeStage by 1 */
    private void Harvest()
    {

    }
}
