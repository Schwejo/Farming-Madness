using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBox : MonoBehaviour
{
    private SpriteRenderer backgroundSr;
    public SpriteRenderer overlaySr;

    private void Start ()
    {
        backgroundSr = GetComponent<SpriteRenderer>();
    }

    public void SetIcon(Sprite s)
    {
        overlaySr.sprite = s;
    }

    public void UnsetIcon()
    {
        overlaySr.sprite = null;
    }
}
