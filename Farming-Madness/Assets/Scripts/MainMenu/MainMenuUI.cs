using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Animator logoAnimator;
    public Animator optionsPanelAnimator;
    public Animator mainPanelAnimator;
    public Animator levelPanelAnimator;

    public GameObject buttonPanel;

    public LevelButton levelButton1;
    public LevelButton levelButton2;
    public LevelButton levelButton3;

    public static Action OnPlay;
    public static Action OnOptions;
    public static Action OnReturn;

    private void Start()
    {
        buttonPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        logoAnimator.SetBool("OnClick", true);
        buttonPanel.SetActive(true);
    }

    public void Play()
    {
        AudioManager.instance.Play("button");
        
        if (GameManager.instance.savedData.firstStart || GameManager.instance.savedData.playTutorial)
        {
            GameManager.instance.savedData.firstStart = false;
            GameManager.instance.savedData.playTutorial = false;
            GameManager.instance.levelToLoad = "Level01";
            SceneManager.LoadScene("Tutorial");
        }
        else 
        {
            AudioManager.instance.Play("screen_switch");

            levelPanelAnimator.SetBool("slide", true);
            mainPanelAnimator.SetBool("slide", true);

            levelButton1.SetStars(GameManager.instance.savedData.level01.maxStars);
            levelButton2.SetStars(GameManager.instance.savedData.level02.maxStars);
            levelButton3.SetStars(GameManager.instance.savedData.level03.maxStars);

            if (OnPlay != null)
                OnPlay();
        }
    }

    public void Options()
    {
        AudioManager.instance.Play("button");
        AudioManager.instance.Play("screen_switch");

        mainPanelAnimator.SetBool("slide", true);
        optionsPanelAnimator.SetBool("slide", true);
        
        if (OnOptions != null)
            OnOptions();
    }

    public void Return()
    {
        AudioManager.instance.Play("button");
        AudioManager.instance.Play("screen_switch");

        mainPanelAnimator.SetBool("slide", false);
        optionsPanelAnimator.SetBool("slide", false);
        levelPanelAnimator.SetBool("slide", false);

        if (OnReturn != null)
            OnReturn();
    }

    public void Quit()
    {
        AudioManager.instance.Play("button");
        Application.Quit();
    }
}
