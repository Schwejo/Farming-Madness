using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{
    public Image face;
    public Image[] items;
    public Image[] itemCheck;
    public Image timebar;

    private Color transparent;
    private Color nonTransparent;

    public void Start()
    {
        transparent = new Color(255,255,255,0);
        nonTransparent = new Color(255,255,255,1);
        
        Reset();
    }

    /* Parameters: 
     * @s Sprite array of items sprites
     * @f Sprite of the customer face
     */
    public void Setup(Sprite[] s, Sprite f)
    {        
        for (int i = 0; i < s.Length; i++)
        {
            items[i].sprite = s[i];
            items[i].color = nonTransparent;
        }

        face.sprite = f;
        face.color = nonTransparent;

        timebar.fillAmount = 1;
    }

    public void SetTimerbar(float fillAmount)
    {
        timebar.fillAmount = fillAmount;
    }

    public void SetChecked(int index)
    {
        itemCheck[index].color = nonTransparent;
    }

    public void Reset()
    {
        face.sprite = null;
        face.color = transparent;

        itemCheck = new Image[items.Length];

        for (int i = 0; i < items.Length; i++)
        {
            items[i].sprite = null;
            items[i].color = transparent;

            Transform child = items[i].gameObject.transform.GetChild(0);
            itemCheck[i] = child.gameObject.GetComponent<Image>();
            itemCheck[i].color = transparent;
        }

        timebar.fillAmount = 0;
    }
}
