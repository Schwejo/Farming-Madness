using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndscreenUI : MonoBehaviour
{
    public Text textBoxText;
    public Text textBoxPoints;
    public Text textMaxPoints;

    private Image background;

    private Color transparent;
    private Color nonTransparent;

    private void Start()
    {
        transparent = new Color(255,255,255,0);
        nonTransparent = new Color(255,255,255,1);

        background = GetComponent<Image>();

        background.color = transparent;
        textBoxText.text = "";
        textBoxPoints.text = "";
        textMaxPoints.text = "";
    }

    private void OnEnable()
    {
        Timer.OnTimeIsUp += ShowEndscreen;
    }

    private void OnDisable()
    {
        Timer.OnTimeIsUp -= ShowEndscreen;
    }

    private void ShowEndscreen()
    {
        background.color = nonTransparent;
        textBoxText.text = "Points";
        textBoxPoints.text = LevelManager.instance.GetPoints().ToString();
        textMaxPoints.text = LevelManager.instance.GetMaxPoints().ToString();
    }
}
