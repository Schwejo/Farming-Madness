using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStats
{
    public int number;
    public int maxStars;
    public int maxPoints;
    public int maxSoldItems;
    public int maxCompletedOrders;
    public int minFailedOrders;

    public LevelStats(int lvNumber, int stars, int points, int soldItems, int ordersComplete, int ordersFailed)
    {
        number = lvNumber;
        maxStars = stars;
        maxPoints = points;
        maxSoldItems = soldItems;
        maxCompletedOrders = ordersComplete;
        minFailedOrders = ordersFailed;
    }
}
