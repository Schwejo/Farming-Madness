using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TutorialManager : MonoBehaviour
{   
    public int tutorialStage = 0;

    private string tut0Text = "Welcome to Farming Madness! This is a tutorial level to teach you the basics of this game. Both players have an individual key to interact. \n Player 1: Press the E Key, Player 2: Press the O Key.";
    private string tut1Text = "You can plant crops by using your interaction keys. Both of you can pick up a seed from the highlighted seedbag. \n Both: Pick up a seed.";
    private string tut2Text = "Great! Both of you now hold a seed in your hands. You can plant them on the field using your interaction key. \n Both: Plant a seed.";
    private string tut3Text = "Both of you planted a seed. But these will only grow if you water them! One: Use the watering can to water the seeds.";
    private string tut4Text = "After you watered the seed they will take a small amount of time to grow, so you can harvest them. \n Both: Harvest the crops.";
    private string tut5Text = "You can sell crops to customers. Serving customers gives you points depending on the difficulity of the wanted item and how fast you completed the order. \n Both: Sell your crops.";
    private string tut6Text = "Customers only stay a certain amount of time, depending on their order. You can see the time left and the satisfaction in the top left corner. \n Both: Press your interaction key to continue.";
    private string tut7Text = "Once the bar is empty the customer will leave, giving you no chance to complete the order. A new customer will arrive at their place. \n Both: Press your interaction key to continue.";
    private string tut8Text = "Sometimes customers want to buy more than just a simple crop. There are different places around the map that spawn items. \n Both: Press your interaction key to continue.";
    private string tut9Text = "Additionally there are 3 different production buildings to craft items. The dairy, the bakery and the kitchen. \n Both: Insert the milk in the dairy to craft cheese.";
    private string tut10Text = "Awesome! You know all the basics of the game. Remember, in the normal levels you will have a countdown in top right corner. Watch it! \n Both: Press your interaction key to continue.";
    private string tut11Text = "You can only move on to the next level after achieving at least half star. If you did not make, you can try again! \n Both: Press your interaction key to continue.";
    private string tut12Text = "Good luck! Andn don't forget to teamwork! \n Both: Press your interaction key to start the game.";

    public GameObject stage1Fence; //Seedbag
    public GameObject stage2Fence; //Field
    public GameObject stage5Fence; //Customer
    public GameObject stage9Fence; //Production Building
    
    public Text text;

    public GameObject p1Checked;
    public GameObject p2Checked;

    public PlayerInteraction p1;
    public PlayerInteraction p2;

    public Field field1;
    public Field field2;

    public Product milk;
    private bool milkSet = false;

    public SpriteOutline seedbagShader;
    public SpriteOutline fieldShader;
    public SpriteOutline fieldShader2;
    public SpriteOutline canShader;
    //public SpriteOutline customerShader;
    public SpriteOutline itemspawnShader1;
    public SpriteOutline itemspawnShader2;
    public SpriteOutline bakeryShader;

    private bool p1Interaction = false;
    private bool p2Interaction = false;

    public static Action CustomerStart;
    public static Action<int> CustomerEnd;


    private void Start()
    {
        text.text = "";
        p1Checked.SetActive(false);
        p2Checked.SetActive(false);
        AudioManager.instance.Play("theme");
    }

    private void Update()
    {
        if (tutorialStage == 0)
        {
            text.text = tut0Text;
            bool done = WaitForInteractionKeys();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 1)
        {
            text.text = tut1Text;
            stage1Fence.SetActive(false);
            seedbagShader.outlineSize = 2;
            bool done = WaitForCropPickup();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 2)
        {
            text.text = tut2Text;
            stage2Fence.SetActive(false);
            seedbagShader.outlineSize = 0;
            fieldShader.outlineSize = 2;
            fieldShader2.outlineSize = 2;
            bool done = WaitForCropPutdown();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 3)
        {
            text.text = tut3Text;
    	    stage1Fence.SetActive(true);
            fieldShader.outlineSize = 0;
            fieldShader2.outlineSize = 0;
            canShader.outlineSize = 2;
            bool done = WaitForWatering();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 4)
        {
            text.text = tut4Text;
            canShader.outlineSize = 0;
            bool done = WaitForCropPickup();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 5)
        {
            text.text = tut5Text;
            stage5Fence.SetActive(false);
            bool done = WaitForCropPutdown();

            if (CustomerStart != null)
                CustomerStart();

            if (done)
            {
                NextStage();

                if (CustomerEnd != null)
                    CustomerEnd(1);
            }
        }
        else if (tutorialStage == 6)
        {
            text.text = tut6Text;
            stage2Fence.SetActive(true);
            bool done = WaitForInteractionKeys();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 7)
        {
            text.text = tut7Text;
            bool done = WaitForInteractionKeys();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 8)
        {
            text.text = tut8Text;
            itemspawnShader1.outlineSize = 2;
            itemspawnShader2.outlineSize = 2;
            bool done = WaitForInteractionKeys();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 9)
        {
            text.text = tut9Text;
            stage9Fence.SetActive(false);
            itemspawnShader1.outlineSize = 0;
            itemspawnShader2.outlineSize = 0;
            bakeryShader.outlineSize = 6;
            if (!milkSet)
            {
                p1.SetProduct(milk);
                p2.SetProduct(milk);
                milkSet = true;
            }
            bool done = WaitForProductPutdown();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 10)
        {
            text.text = tut10Text;
            bakeryShader.outlineSize = 0;
            stage5Fence.SetActive(true);
            bool done = WaitForInteractionKeys();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 11)
        {
            text.text = tut11Text;
            bool done = WaitForInteractionKeys();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 12)
        {
            text.text = tut12Text;
            bool done = WaitForInteractionKeys();

            if (done)
            {
                NextStage();
            }
        }
        else if (tutorialStage == 13)
        {
            StartCoroutine(NextLevel());
        }
    }

    private void NextStage()
    {
        tutorialStage++;
        p1Interaction = false;
        p2Interaction = false;
        p1Checked.SetActive(false);
        p2Checked.SetActive(false);
    }

    private IEnumerator NextLevel()
    {
        AudioSource s = AudioManager.instance.GetAudioSource("theme");
        AudioFadeOut.FadeOut(s, 2);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("PlayerSelection");
    }

    private bool WaitForInteractionKeys()
    {
        if (Input.GetKeyDown(p1.keyInteract))
        {
            p1Interaction = true;
            p1Checked.SetActive(true);
        }
        if (Input.GetKeyDown(p2.keyInteract))
        {
            p2Interaction = true;
            p2Checked.SetActive(true);
        }
        return p1Interaction && p2Interaction;
    }

    private bool WaitForCropPickup()
    {
        if (p1.GetCrop() != null)
        {
            p1Interaction = true; 
            p1Checked.SetActive(true);
        }   
        if (p2.GetCrop() != null)
        {
            p2Interaction = true;
            p2Checked.SetActive(true);
        } 
        return p1Interaction && p2Interaction;
    }

    private bool WaitForCropPutdown()
    {
        if (p1.GetCrop() == null)
        {
            p1Interaction = true;
            p1Checked.SetActive(true);
        } 
        if (p2.GetCrop() == null)
        {
            p2Interaction = true;
            p2Checked.SetActive(true);
        }
        return p1Interaction && p2Interaction;
    }

    private bool WaitForWatering()
    {
        if (field1.GetIsWet())
        {
            p1Interaction = true;
            p1Checked.SetActive(true);
        }   
        if (field2.GetIsWet())
        {
            p2Interaction = true;
            p2Checked.SetActive(true);
        }    
        return p1Interaction && p2Interaction;
    }

    private bool WaitForProductPutdown()
    {
        if (p1.GetProduct() == null)
        {
            p1Interaction = true;
            p1Checked.SetActive(true);
        } 
        if (p2.GetProduct() == null)
        {
            p2Interaction = true;
            p2Checked.SetActive(true);
        }
        return p1Interaction && p2Interaction;
    }
}
