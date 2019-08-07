using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public string nextSceneName = "Level01";

    public Animator logoAnimator;
    public GameObject buttonPanel;


    private void Start()
    {
        buttonPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            logoAnimator.SetBool("OnClick", true);
            buttonPanel.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
