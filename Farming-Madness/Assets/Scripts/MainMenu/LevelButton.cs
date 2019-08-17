using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string levelName;
    public string playerSelection = "PlayerSelection";
    
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject star5;
    public GameObject star6;

    private void Start()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        star4.SetActive(false);
        star5.SetActive(false);
        star6.SetActive(false);
    }

    public void SetStars(int stars)
    {
        switch (stars)
        {
            case 1:
                star1.SetActive(true);
                break;
            case 2:
                star2.SetActive(true);
                break;
            case 3:
                star2.SetActive(true);
                star3.SetActive(true);
                break;
            case 4:
                star2.SetActive(true);
                star4.SetActive(true);
                break;
            case 5:
                star2.SetActive(true);
                star4.SetActive(true);
                star5.SetActive(true);
                break;
            case 6:
                star2.SetActive(true);
                star4.SetActive(true);
                star6.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Play()
    {
        GameManager.instance.levelToLoad = levelName;
        SceneManager.LoadScene(playerSelection);
    }
}
