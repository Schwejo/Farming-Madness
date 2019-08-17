using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    public bool selection = true;
    public bool p1Selecting = true;

    public GameObject malePlayer;
    public GameObject femalePlayer;

    public GameObject darkenLeft;
    public GameObject darkenRight;

    public Text text;

    private KeyCode p1Left;
    private KeyCode p1Right;
    private KeyCode p1Interact;

    private KeyCode p2Left;
    private KeyCode p2Right;
    private KeyCode p2Interact;

    private bool selectedLeft = true;

    private void Start()
    {
        p1Left = GameManager.instance.p1Left;
        p1Right = GameManager.instance.p1Right;
        p1Interact = GameManager.instance.p1Interact;

        p2Left = GameManager.instance.p2Left;
        p2Right = GameManager.instance.p2Right;
        p2Interact = GameManager.instance.p2Interact;

        ShowSelection();
    }

    private void Update()
    {
        if (selection)
        {
            if (p1Selecting)
            {
                if (Input.GetKeyDown(p1Left) || Input.GetKeyDown(p1Right))
                {
                    selectedLeft = !selectedLeft;
                    ShowSelection();
                }
                else if (Input.GetKeyDown(p1Interact))
                {
                    if (selectedLeft)
                        GameManager.instance.SetPlayer1(malePlayer);
                    else
                        GameManager.instance.SetPlayer1(femalePlayer);
                    p1Selecting = false;
                    text.text = "Selection right player";
                }
            }
            else 
            {
                if (Input.GetKeyDown(p2Left) || Input.GetKeyDown(p2Right))
                {
                    selectedLeft = !selectedLeft;
                    ShowSelection();
                }
                else if (Input.GetKeyDown(p2Interact))
                {
                    if (selectedLeft)
                        GameManager.instance.SetPlayer2(malePlayer);
                    else
                        GameManager.instance.SetPlayer2(femalePlayer);
                    selection = false;
                    StartCoroutine(StartGame());
                }
            }
        }
    }

    private void ShowSelection()
    {
        if (selectedLeft)
        {
            darkenRight.SetActive(true);
            darkenLeft.SetActive(false);
        }
        else
        {
            darkenRight.SetActive(false);
            darkenLeft.SetActive(true);
        }
    }

    private IEnumerator StartGame()
    {
        text.text = "Starting Game...";
        AudioSource s = AudioManager.instance.GetAudioSource("theme");
        StartCoroutine(AudioFadeOut.FadeOut(s, 3));
        yield return new WaitForSeconds(3);
        if (GameManager.instance.levelToLoad != "")
            SceneManager.LoadScene(GameManager.instance.levelToLoad);
        else
        {
            SceneManager.LoadScene("Level01");
            GameManager.instance.levelToLoad = "Level01";
        }
    }       
}
