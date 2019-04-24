using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCow : MonoBehaviour
{
    private Vector3 movement;
    public float speedMultiplier = 5.0f;
    
    void Start()
    {
        movement = new Vector3(0,0,0);
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        movement = movement * speedMultiplier;
        transform.position = transform.position + movement * Time.deltaTime;
    }
}
