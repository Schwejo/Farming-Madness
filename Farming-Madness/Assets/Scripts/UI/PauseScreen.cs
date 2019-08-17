using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
    }
    
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Recipe()
    {
        //Show recipe panel
    }

    public void Options()
    {
        //show options panel
    }
    
    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.Stop("theme5min");
        AudioManager.instance.Stop("theme7min");
        AudioManager.instance.Stop("theme10min");
    }

    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
