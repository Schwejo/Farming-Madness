using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCage : MonoBehaviour
{
    public GameObject top;
    public GameObject bottom;
    public GameObject right;
    public GameObject left;

    public float GetTop()
    {
        return top.transform.position.y;
    }

    public float GetBottom()
    {
        return bottom.transform.position.y;
    }

    public float GetLeft()
    {
        return left.transform.position.x;
    }

    public float GetRight()
    {
        return right.transform.position.x;
    }
}
