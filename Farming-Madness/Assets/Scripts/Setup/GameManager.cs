using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    [Header("Data")]
    public InitialGameValues defaultValuesAsset;
    public SavedData savedData;
    public string dataPath;

    [Header("Player1")]
    public GameObject player1;
    public KeyCode p1Up;
    public KeyCode p1Down;
    public KeyCode p1Left;
    public KeyCode p1Right;
    public KeyCode p1Interact;

    [Header("Player2")]
    public GameObject player2;
    public KeyCode p2Up;
    public KeyCode p2Down;
    public KeyCode p2Left;
    public KeyCode p2Right;
    public KeyCode p2Interact;

    [Header("Other")]
    public KeyCode pauseKey;
    
    public string levelToLoad;

    private int currentLevel = -1;


    private void Start()
    {
        //Singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        dataPath = Path.Combine(Application.persistentDataPath, "SavedData.txt");
        LoadData();
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }

    public void SetPlayer1(GameObject player)
    {
        player1 = player;
    }

    public void SetPlayer2(GameObject player)
    {
        player2 = player;
    }

    public void SetLevelStats(int levelNumber, LevelStats stats)
    {
        LevelStats savedStats;
        switch (levelNumber)
        {
            case 1:
                savedStats = savedData.level01;
                break;
            case 2:
                savedStats = savedData.level02;
                break;
            case 3:
                savedStats = savedData.level03;
                break;
            default:
                savedStats = null;
                Debug.LogError("Could not set level stats.");
                break;
        }

        if (savedStats.maxStars < stats.maxStars)
            savedStats.maxStars = stats.maxStars;
        if (savedStats.maxPoints < stats.maxPoints)
            savedStats.maxPoints = stats.maxPoints;
        if (savedStats.maxSoldItems < stats.maxSoldItems)
            savedStats.maxSoldItems = stats.maxSoldItems; 
        if (savedStats.maxCompletedOrders < stats.maxCompletedOrders)
            savedStats.maxCompletedOrders = stats.maxCompletedOrders; 
        if (savedStats.minFailedOrders > stats.minFailedOrders)
            savedStats.minFailedOrders = stats.minFailedOrders; 
    }
    
    public void SaveData()
    {
        string jsonString = JsonUtility.ToJson(savedData);
        Debug.Log(jsonString);
    }

    public void LoadData()
    {
        string jsonString = "";
        try
        {
            using (StreamReader streamReader = File.OpenText(dataPath))
            {
                jsonString = streamReader.ReadToEnd();
                savedData = JsonUtility.FromJson<SavedData>(jsonString);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.LogWarning("SavedData.txt not found - using default asset");
            jsonString = defaultValuesAsset.jsonString;
            savedData = JsonUtility.FromJson<SavedData>(jsonString);
        }
    }
}
