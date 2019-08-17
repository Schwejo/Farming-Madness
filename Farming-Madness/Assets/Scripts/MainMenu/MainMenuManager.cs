using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public MainMenuUI ui;
    public Animator camAnimator;

    public string playScene;

    private bool initAnimFinished = false;
    

    private void Start()
    {
        AudioManager.instance.Play("theme");
    }
    
    private void OnEnable()
    {
        MoveTo.OnTargetReached += SetInitAnimFinished;
        MainMenuUI.OnPlay += Play;
        MainMenuUI.OnOptions += Options;
        MainMenuUI.OnReturn += Return;
    }

    private void OnDisable()
    {
        MoveTo.OnTargetReached -= SetInitAnimFinished;
        MainMenuUI.OnPlay -= Play;
        MainMenuUI.OnOptions -= Options;
        MainMenuUI.OnReturn -= Return;
    }
    
    private void Update()
    {
        if (initAnimFinished)
        {
            if (Input.anyKeyDown)
            {
                ui.ShowMenu();
            }
        }
    }
    
    public void Play()
    {
        camAnimator.SetBool("slide", true);
    }

    public void Options()
    {
        camAnimator.SetBool("slide", true);
    }

    public void Return()
    {
        camAnimator.SetBool("slide", false);
    }

    public void SetInitAnimFinished()
    {
        initAnimFinished = true;
    }
}
