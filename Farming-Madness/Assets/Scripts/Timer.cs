using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float timeStart = 300;
    public Text textBox;
    private string minutes;
    private string seconds;


    
    void Start()
    {
        textBox.text = timeStart.ToString();
    }


    void Update()
    {
        timeStart -= Time.deltaTime;
        UpdateTextBox();

    }

    void UpdateTextBox()
    {
        minutes = Mathf.RoundToInt(timeStart / 60).ToString();
        seconds = Mathf.RoundToInt(timeStart % 60).ToString();
        textBox.text = string.Format("{0}:{1}", minutes, seconds);
    }
}
