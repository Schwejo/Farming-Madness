using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public static Action<int> OnTimeIsUp;
    public static Action OnGameStart;
    
    [Header("Spawn Points")]
    public Transform spawnP1;
    public Transform spawnP2;

    [Header("Players")]
    public GameObject player1;
    public GameObject player2;
    
    [Header("Level")]
    public int level;
    public string nextLevel = "Level02";
    public string audioTheme;

    [Header("Time")]
    public float levelTime;
    public Timer timer;

    [Header("Stats")]
    public int levelPoints = 0;
    public int maxPossiblePoints = 0;
    public int soldItems = 0;
    public int completedOrders = 0;
    public int failedOrders = 0; //failed = order with nothing sold

    [Header("Stars")]   
    public int stars = 0;
    public float star1 = 0.4f;
    public float star2 = 0.5f;
    public float star3 = 0.6f;
    public float star4 = 0.7f;
    public float star5 = 0.8f;
    public float star6 = 0.9f;

    public PauseScreen pauseScreen;
    public GameObject countdown;

    
    private void Start()
    {
        //Singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        GameManager.instance.SetCurrentLevel(level);
        timer.SetTimer(levelTime);

        //Spawn Player1
        if (GameManager.instance.player1 != null)
        {
            player1 = Instantiate(GameManager.instance.player1, spawnP1.position, spawnP1.rotation);
            player1.GetComponent<PlayerInteraction>().player = "p1";
            player1.GetComponent<BasicMovement>().movementEnabled = false;
        }
        else
            Debug.LogWarning("No Player1 in GameManager");
        
        //Spawn Player2
        if (GameManager.instance.player1 != null)
        {
            player2 = Instantiate(GameManager.instance.player2, spawnP2.position, spawnP2.rotation);
            player2.GetComponent<PlayerInteraction>().player = "p2";
            player2.GetComponent<BasicMovement>().movementEnabled = false;
        }
        else 
            Debug.LogWarning("No Player2 in GameManager");

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameManager.instance.pauseKey))
        {
            if (pauseScreen != null)
            {
                pauseScreen.Pause();
            }
        }
    }

    private IEnumerator StartGame()
    {
        countdown.SetActive(true);
        AudioManager.instance.Play("countdown");
        yield return new WaitForSeconds(11);
        countdown.SetActive(false);
        if (OnGameStart != null && player1 != null && player2 != null)
        {
            OnGameStart();
            player1.GetComponent<BasicMovement>().movementEnabled = true;
            player2.GetComponent<BasicMovement>().movementEnabled = true;
            timer.StartTimer();
            AudioManager.instance.Play("atmosphere");
            AudioManager.instance.Play(audioTheme);
        }
        else
        {
            Debug.LogError("Could not start game, no players.");
        }            
        
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

    public void AddSoldItems(int items)
    {
        soldItems += items;
    }

    public int GetSoldItems()
    {
        return soldItems;
    }

    public void AddCompletedOrder()
    {
        completedOrders += 1;
    }

    public int GetCompletedOrders()
    {
        return completedOrders;
    }

    public void AddFailedOrder()
    {
        failedOrders += 1;
    }

    public int GetFailedOrders()
    {
        return failedOrders;
    }

    public void TimeIsUp()
    {
        CalcStars();
        if (stars > 0)
            AudioManager.instance.Play("success");
        else
            AudioManager.instance.Play("fail");
        if (OnTimeIsUp != null)
            OnTimeIsUp(stars);
    }

    private void CalcStars()
    {
        float pointPercentage;
        if (maxPossiblePoints == 0)
        {
            pointPercentage = 0;
        }
        else
        {
            pointPercentage = (float) levelPoints / maxPossiblePoints;
        }

        if (pointPercentage < star1)
        {
            stars = 0;
        }
        else if (pointPercentage < star2 && pointPercentage >= star1)
        {
            stars = 1;
        }
        else if (pointPercentage < star3 && pointPercentage >= star2)
        {
            stars = 2;
        }
        else if (pointPercentage < star4 && pointPercentage >= star3)
        {
            stars = 3;
        }
        else if (pointPercentage < star5 && pointPercentage >= star4)
        {
            stars = 4;
        }
        else if (pointPercentage < star6 && pointPercentage >= star5)
        {
            stars = 5;
        }
        else if (pointPercentage > star6)
        {
            stars = 6;
        }
    }

    public int GetStars()
    {
        return stars;
    }

    public void SaveLevelStats()
    {
        LevelStats stats = new LevelStats(level, stars, levelPoints, soldItems, completedOrders, failedOrders);
        GameManager.instance.SetLevelStats(level, stats);
    }
}
