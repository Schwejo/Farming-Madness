using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    public OverlayManager[] overlays;
    public Sprite[] faces;

    public void SetupUI(CustomerName name, Product[] products, int index)
    {
        Sprite face = SearchFace(name);

        Sprite[] sprites = new Sprite[products.Length];
        for (int i = 0; i < products.Length; i++)
        {
            sprites[i] = products[i].productSprite;
        }

        overlays[index].Setup(sprites, face);
    }

    public void SetTimebar(int index, float fillAmount)
    {
        overlays[index].SetTimerbar(fillAmount);
    }

    public void CheckItem(int index, int itemIndex)
    {
        overlays[index].SetChecked(itemIndex);
    }

    public void Reset(int index)
    {
        overlays[index].Reset();
    }

    private Sprite SearchFace(CustomerName name)
    {
        switch (name)
        {
            case CustomerName.Gisela:
                return faces[0];
            case CustomerName.Gerda:
                return faces[1];
            case CustomerName.Gerd:
                return faces[2];
            case CustomerName.Gustav:
                return faces[3];
            case CustomerName.Gabriel:
                return faces[4];
            default:
                Debug.LogError("No customer face found.");
                return null;
        }
    }
}
