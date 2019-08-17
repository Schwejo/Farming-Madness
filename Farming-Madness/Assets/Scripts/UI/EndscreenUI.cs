using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndscreenUI : MonoBehaviour
{
    public GameObject panel;
    
    public string nextLevel;

    public Text scoreValue;
    public Text maxPointsValue;
    public Text soldItemsValue;
    public Text completeOrdersValue;
    public Text failedOrdersValue;
    
    public Image star1;
    public Image star2;
    public Image star3;
    public Image star4;
    public Image star5;
    public Image star6;

    private Color transparent;
    private Color nonTransparent;

    private int stars;

    private void Start()
    {
        transparent = new Color(255,255,255,0);
        nonTransparent = new Color(255,255,255,1);

        panel.SetActive(false);
        scoreValue.text = "";
        maxPointsValue.text = "";
        soldItemsValue.text = "";
        completeOrdersValue.text = "";
        failedOrdersValue.text = "";

        star1.color = transparent;
        star2.color = transparent;
        star3.color = transparent;
        star4.color = transparent;
        star5.color = transparent;
        star6.color = transparent;
    }

    private void OnEnable()
    {
        LevelManager.OnTimeIsUp += ShowEndscreen;
    }

    private void OnDisable()
    {
        LevelManager.OnTimeIsUp -= ShowEndscreen;
    }

    private void ShowEndscreen(int s)
    {
        panel.SetActive(true);
        //AudioManager.instance.Play("stats");
        scoreValue.text = LevelManager.instance.GetPoints().ToString();
        maxPointsValue.text = LevelManager.instance.GetMaxPoints().ToString();
        soldItemsValue.text = LevelManager.instance.GetSoldItems().ToString();
        completeOrdersValue.text = LevelManager.instance.GetCompletedOrders().ToString();
        failedOrdersValue.text = LevelManager.instance.GetFailedOrders().ToString();
        
        //stars
        stars = s;
        switch (stars)
        {
            case 1:
                star1.color = nonTransparent;
                break;
            case 2:
                star2.color = nonTransparent;
                break;
            case 3:
                star2.color = nonTransparent;
                star3.color = nonTransparent;
                break;
            case 4:
                star2.color = nonTransparent;
                star4.color = nonTransparent;
                break;
            case 5:
                star2.color = nonTransparent;
                star4.color = nonTransparent;
                star5.color =  nonTransparent;
                break;
            case 6: 
                star2.color = nonTransparent;
                star4.color = nonTransparent;
                star6.color = nonTransparent;
                break;
        }
    }

    public void NextLevel()
    {
        if (stars > 0)
        {
            LevelManager.instance.SaveLevelStats();
            SceneManager.LoadScene(nextLevel);
        }
        else
            AudioManager.instance.Play("customer_angry");
        
    }

    public void Retry()
    {
        AudioManager.instance.Play("button");
        LevelManager.instance.SaveLevelStats();
        string currentLevelName = GameManager.instance.levelToLoad;
        SceneManager.LoadScene(currentLevelName);
    }
}
