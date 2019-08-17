using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public string horizontalAxis;
    public string verticalAxis;
    public float speed;

    private Rigidbody2D rb;
    private Vector3 movement;
    private Vector2 rbMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbMovement = new Vector2();
    }

    void Update()
    {
        rbMovement.x = Input.GetAxis(horizontalAxis);
        rbMovement.y = Input.GetAxis(verticalAxis);

        rbMovement *= speed;

        rb.velocity = rbMovement;

        //movement = movement * speed;
        //transform.position = transform.position + movement * Time.deltaTime;
    }
}
