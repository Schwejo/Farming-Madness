using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    private float timer;
    private int minutes;
    private int seconds;
    private bool timerRunning = false;
    
    private bool endCountdownPlayed = false;

    public Text textBox;

    void Update()
    {
        if (timerRunning)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                FormatTime();
                ShowTimer();
                if (timer <= 11 && !endCountdownPlayed)
                {
                    endCountdownPlayed = true;
                    AudioManager.instance.Play("countdown_end");
                    string sourceName = LevelManager.instance.audioTheme;
                    AudioSource s = AudioManager.instance.GetAudioSource(sourceName);
                    StartCoroutine(AudioFadeOut.FadeOut(s, 2));
                }
            }
            else 
            {
                timerRunning = false;
                LevelManager.instance.TimeIsUp();
            }
        }
    }

    /* Formats the time to show minutes and seconds.
     * 
     */
    private void FormatTime()
    {
        minutes = (int) timer / 60;
        seconds = (int) timer % 60;
    }

    private void ShowTimer()
    {
        textBox.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetTimer(float time)
    {
        timer = time;
        FormatTime();
        ShowTimer();
    }

    public void StartTimer()
    {
        timerRunning = true;
    }
}
