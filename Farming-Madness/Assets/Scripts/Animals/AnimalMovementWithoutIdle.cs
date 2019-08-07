using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovementWithoutIdle : MonoBehaviour
{
    public AnimalCage cage;

    private float top;
    private float bottom;
    private float right;
    private float left;
    private Vector3 target;

    public float speed = 1.0f;


    
    void Start()
    {
        top = cage.GetTop();
        bottom = cage.GetBottom();
        left = cage.GetLeft();
        right = cage.GetRight();
        GetNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            GetNewTarget();
        }
    }

    private void GetNewTarget()
    {
        target.x = Random.Range(left, right);
        target.y = Random.Range(bottom, top);
    }
}
