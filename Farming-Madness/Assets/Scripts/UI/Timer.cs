using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    private float timer;
    private string minutes;
    private string seconds;
    private bool timerRunning = false;

    public Text textBox;
    public static Action OnTimeIsUp;

    void Start()
    {  
        textBox.text = timer.ToString();
    }


    void Update()
    {
        if (timerRunning)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                FormatTime();
                ShowTimer();
            }
            else 
            {
                timerRunning = false;
                if (OnTimeIsUp != null)
                    OnTimeIsUp();
            }
        }
    }

    /* Formats the time to show minutes and seconds.
     * 
     */
    private void FormatTime()
    {
        minutes = Mathf.RoundToInt(timer / 60).ToString();
        seconds = Mathf.RoundToInt(timer % 60).ToString();
    }

    private void ShowTimer()
    {
        textBox.text = string.Format("{0}:{1}", minutes, seconds);
    }

    public void SetTimer(float time)
    {
        timer = time;
        timerRunning = true;
    }
}
