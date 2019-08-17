using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Customer : MonoBehaviour
{
    public CustomerName customerName;
    
    [Header("Movement")]
    public float speed = 5.0f;
    private Vector3 target;
    private Vector3 finalTarget;
    private int targetIndex;
    private bool isMoving = false;
    private bool reachedTable = false;

    [Header("Items")]
    public Product[] items;
    public int soldItems = 0;

    [Header("Points")]
    private int points = 0;
    private int maxPoints = 0;
    public int t1Points = 100;
    public int t2Points = 200;
    public int t3Points = 300;
    public int completeOrderT1Bonus = 50;
    public int completeOrderT2Bonus = 100;
    public int completeOrderT3Bonus = 150;
    public int timeBonus = 200;
    public float timeToBonusMultiplier = 0.75f;

    [Header("Time")]
    public float timeToLeaveMax;
    public float timeLeft;

    private Animator animator;
    private CustomerUI customerUI;
    private CustomerManager customerManager;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("No animator for customer found!");
        }
    }

    private void Update()
    {
        if (isMoving)
            Move();

        if (reachedTable)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                customerUI.SetTimebar(targetIndex, timeLeft / timeToLeaveMax);
            }
            else
            {
                reachedTable = false;
                isMoving = true;

                target = finalTarget;
                CalcFinalPoints();
                CalcMaxPoints();
                WriteStats();
            }
        }

        Animate();
    }
    
    public void Setup(Vector3 targetPoint, Vector3 endPoint, Product[] i, int index, float time, CustomerManager manager, CustomerUI ui)
    {
        target = targetPoint;
        finalTarget = endPoint;
        targetIndex = index;
        isMoving = true;

        items = i;

        timeToLeaveMax = time;
        timeLeft = time;

        customerManager = manager;
        customerUI = ui;
        customerUI.SetupUI(customerName, items, targetIndex);
    }

    public bool SellItem(int soldItemIndex)
    {
        bool timeIsLeft = timeLeft > 0;
        if (timeIsLeft)
        {
            CalcPoints(items[soldItemIndex]);
            soldItems += 1;

            customerUI.CheckItem(targetIndex, soldItemIndex);

            //all items are sold
            if (soldItems == items.Length)
            {
                target = finalTarget;
                isMoving = true;
                reachedTable = false;
                if (LevelManager.instance != null)
                {
                    CalcFinalPoints();
                    CalcMaxPoints();
                    WriteStats();
                }
            }
        }

        return timeIsLeft;
    }

    private void CalcPoints(Product product)
    {
        switch (product.productTier)
        {
            case 1:
                points += t1Points;
                break;
            case 2:
                points += t2Points;
                break;
            case 3:
                points += t3Points;
                break;
        }
    }

    private void CalcFinalPoints()
    {
        int bonusPoints = 0;
        
        //Bonus points, if order completed (based on highest tier)
        if (soldItems == items.Length)
        {
            //get highest tier
            int highestTier = GetHighestTier();
            
            //add points based on tier
            switch (highestTier)
            {
                case 1:
                    bonusPoints += completeOrderT1Bonus;
                    break;
                case 2:
                    bonusPoints += completeOrderT2Bonus;
                    break;
                case 3:
                    bonusPoints += completeOrderT3Bonus;
                    break;
            }

            //Extra bonus points, if fast delivered
            if (timeLeft >= timeToLeaveMax * timeToBonusMultiplier)
            {
                bonusPoints += timeBonus;
            }
        }

        points += bonusPoints;
        LevelManager.instance.AddPoints(points);
    }

    /*
     * Max points include all bonus points (time bonus + complete order bonus)
     */
    private void CalcMaxPoints()
    {
        //base points
        for (int i = 0; i < items.Length; i++)
        {
            switch (items[i].productTier)
            {
                case 1:
                    maxPoints += t1Points;
                    break;
                case 2:
                    maxPoints += t2Points;
                    break;
                case 3:
                    maxPoints += t3Points;
                    break;
            }
        }

        //complete order bonus
        int highestTier = GetHighestTier();
        switch (highestTier)
            {
                case 1:
                    maxPoints += completeOrderT1Bonus;
                    break;
                case 2:
                    maxPoints += completeOrderT2Bonus;
                    break;
                case 3:
                    maxPoints += completeOrderT3Bonus;
                    break;
            }

        //time bonus
        maxPoints += timeBonus;

        LevelManager.instance.AddMaxPossiblePoints(maxPoints);
    }

    private int GetHighestTier()
    {
        int tier = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (tier < items[i].productTier)
            {
                tier = items[i].productTier;
            }
        }
        return tier;
    }

    private void WriteStats()
    {
        if (soldItems == 0)
        {
            AudioManager.instance.Play("customer_angry");
            LevelManager.instance.AddFailedOrder();
        }
        else if (soldItems == items.Length)
        {
            AudioManager.instance.Play("customer_complete");
            LevelManager.instance.AddCompletedOrder();
            LevelManager.instance.AddSoldItems(soldItems);
        }
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            isMoving = false;
            if (target != finalTarget)
            {
                AudioManager.instance.Play("customer_new");
                reachedTable = true;
            } 
            else
                FinalDestination();
        }
    }

    /* Calls customer manager to notify he is leaving and to reset target point.
     * Calls customer ui to reset it.
     */
    private void FinalDestination()
    {
        customerManager.CustomerLeaves(targetIndex);
        customerUI.Reset(targetIndex);
        Destroy(this.gameObject);
    }

    private void Animate()
    {
        animator.SetBool("IsMoving", isMoving);
    }

    public void SetItems(Product[] i)
    {
        items = i;
    }

    public Product[] GetItems()
    {
        return items;
    }
}


public enum CustomerName 
{
    Gisela, Gerda, Gustav, Gerd, Gabriel
}