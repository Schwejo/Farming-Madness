using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    
    [Header("Level")]
    public int level;
    public string nextLevel = "Level02";

    [Header("Time")]
    public float levelTime;
    public Timer timer;

    [Header("Points")]
    public int levelPoints = 0;
    public int maxPossiblePoints = 0;

    [Header("Stars")]   
    public float stars = 0;
    public float star1 = 0.4f;
    public float star2 = 0.5f;
    public float star3 = 0.6f;
    public float star4 = 0.7f;
    public float star5 = 0.8f;
    public float star6 = 0.9f;

    
    private void Start()
    {
        //Singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        GameManager.instance.SetCurrentLevel(level);
        timer.SetTimer(levelTime);
    }

    private void OnEnable()
    {
        Timer.OnTimeIsUp += TimeIsUp;
    }

    private void OnDisable()
    {
        Timer.OnTimeIsUp -= TimeIsUp;
    }

    public void AddPoints(int points)
    {
        levelPoints += points;
    }

    public int GetPoints()
    {
        return levelPoints;
    }

    public void AddMaxPossiblePoints(int points)
    {
        maxPossiblePoints += points;
    }

    public int GetMaxPoints()
    {
        return maxPossiblePoints;
    }

    public void TimeIsUp()
    {
        //TODO: Calc stars, show UI
        CalcStars();
    }

    private void CalcStars()
    {
        float pointPercentage = (float) levelPoints / maxPossiblePoints;
        Debug.Log(pointPercentage);

        if (pointPercentage < star1)
        {
            stars = 0;
        }
        else if (pointPercentage < star2)
        {
            stars = 0.5f;
        }
        else if (pointPercentage < star3)
        {
            stars = 1.0f;
        }
        else if (pointPercentage < star4)
        {
            stars = 1.5f;
        }
        else if (pointPercentage < star5)
        {
            stars = 2.0f;
        }
        else if (pointPercentage < star6)
        {
            stars = 2.5f;
        }
        else if (pointPercentage > star6)
        {
            stars = 3.0f;
        }
    }
}
