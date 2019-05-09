using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Sprite canSprite;


    private void Start()
    {
        canSprite = spriteRenderer.sprite;
    }

    public void Interact(Crop crop, bool hasCan, PlayerInteraction player)
    {
        if (crop == null && !hasCan)
        {
            player.TakeCan(gameObject);
            HideCan();
        }
        else if (crop == null && hasCan)
        {
            player.PlaceCan();
            ShowCan(player);
        }
    }

    private void HideCan()
    {
        spriteRenderer.sprite = null;
    }

    /* Sets canSprite and transforms position to player position */
    private void ShowCan(PlayerInteraction p)
    {
        spriteRenderer.sprite = canSprite;
        transform.position = p.gameObject.transform.position;
    }

       public Sprite GetSprite()
    {
        return canSprite;
    }
}
