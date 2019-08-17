using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("Customers")]
    public GameObject[] customers;
    public Transform customerParent;
    public int countCustomers;
    public int maxCustomers;
    private int lastCustomerIndex = -1;
    public float timeToNextCustomer;

    [Header("Times")]
    public float tier1BaseTime = 30;
    public float tier2BaseTime = 60;
    public float tier3BaseTime = 90;
    public float timeMultiplier = 0.3f; //used to calculate the time for the second and third item

    [Header("Positions")]
    public Transform spawnPoint;
    public Transform endPoint;
    public GameObject[] targetPoints;

    [Header("UI")]
    public CustomerUI customerUI;
    

    private RandomProducts randomProducts;

    private void Start() 
    {
        randomProducts = GetComponent<RandomProducts>();
        if (randomProducts == null)
        {
            Debug.LogError("Could not find random product script.");
        }
    }

    private void OnEnable()
    {
        TutorialManager.CustomerStart += StartGame;
        TutorialManager.CustomerEnd += EndGame;
        LevelManager.OnGameStart += StartGame;
        LevelManager.OnTimeIsUp += EndGame;
    }

    private void OnDisable()
    {
        TutorialManager.CustomerStart -= StartGame;
        TutorialManager.CustomerEnd -= EndGame;
        LevelManager.OnGameStart -= StartGame;
        LevelManager.OnTimeIsUp -= EndGame;
    }

    private void StartGame()
    {
        StartCoroutine(SpawnCustomer());
    }

    private void EndGame(int stars)
    {
        countCustomers = 100;
        StopAllCoroutines();
    }

    /* Spawns a new random customer every "timeForNextCustomer" seconds
     * as long maxCustomers is not reached.
     */
    private IEnumerator SpawnCustomer()
    {
        int index;

        while(true)
        {
            yield return new WaitForSeconds(timeToNextCustomer);

            if (countCustomers < maxCustomers)
            {
                //don't use the same customer twice in a row
                do 
                {
                    index = Random.Range(0, customers.Length);
                } while (index == lastCustomerIndex);

                lastCustomerIndex = index;
                countCustomers += 1;

                //get random products
                Product[] products = randomProducts.GetRandomProducts();

                //calculate time till customer leaves
                float time = TimeTillLeave(products);

                //get free target point
                TargetPoint point = null;
                int freePointIndex = -1;
                for (int i = 0; i < targetPoints.Length; i++)
                {
                    point = targetPoints[i].GetComponent<TargetPoint>();
                    if (!point.GetIsUsed())
                    {
                        point.SetIsUsed(true);
                        freePointIndex = i;
                        break; //if free point was found
                    }
                }

                //spawning...
                if (freePointIndex >= 0)
                {
                    GameObject cust = Instantiate(customers[index], spawnPoint.position, transform.rotation, customerParent);
                    Vector3 targetPos = targetPoints[freePointIndex].transform.position;
                    cust.GetComponent<Customer>().Setup(targetPos, endPoint.position, products, freePointIndex, time, this, customerUI);
                    point.SetCustomer(cust.GetComponent<Customer>());
                    freePointIndex = -1;
                }
            }
            else
            {
                StopCoroutine("SpawnCustomer");
            }
        }  
    }

    /* Calculates the time till the customer leaves again,
     * based on count and tier of the products he wants.
     */
    private float TimeTillLeave(Product[] products)
    {
        float time = 0;

        //get highest tier and its index
        int highestTier = 0;
        int highestTierIndex = 0;
        for (int i = 0; i < products.Length; i++)
        {
            if (highestTier < products[i].productTier)
            {
                highestTier = products[i].productTier;
                highestTierIndex = i;
            }
        }

        //add base time based on tier
        switch (highestTier)
        {
            case 1:
                time += tier1BaseTime;
                break;
            case 2:
                time += tier2BaseTime;
                break;
            case 3:
                time += tier3BaseTime;
                break;
        }

        //add additional time based on time multiplier
        for (int i = 0; i < products.Length; i++)
        {
            if (i != highestTierIndex)
            {
                switch (products[i].productTier)
                {
                    case 1:
                        time += tier1BaseTime * timeMultiplier;
                        break;
                    case 2:
                        time += tier2BaseTime * timeMultiplier;
                        break;
                    case 3:
                        time += tier3BaseTime * timeMultiplier;
                        break;
                }
            }
        }

        return time;
    }

    public void CustomerLeaves(int pointIndex)
    {
        countCustomers -= 1;
        targetPoints[pointIndex].GetComponent<TargetPoint>().Reset();
        StartCoroutine("SpawnCustomer");
    }
}
