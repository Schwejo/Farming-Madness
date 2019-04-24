using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;

    private Vector3 movement;
    private Vector3 lastMovement;

    public float speedMultiplier = 5.0f;
    public bool isMoving = false;
    public bool isHolding = false;

    private void Start()
    {
        movement = new Vector3(0,0,0);
    }


    private void Update()
    {
        ProcessInputs();
        Animate();
        Move();
    }

    private void ProcessInputs()
    {
        
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        //Store last movement vector while walking and set isMoving var.
        if(movement.magnitude != 0)
        {
            lastMovement = movement;
            isMoving = true;
        }   
        else 
        {
            isMoving = false;
        }
    }

    private void Animate()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        
        animator.SetBool("IsMoving", isMoving);

        //Set correct idle pose by comparing to last movemment vector.
        animator.SetFloat("HorizontalIdle", lastMovement.x);
        animator.SetFloat("VerticalIdle", lastMovement.y);
    }

    private void Move() 
    {
        movement = movement * speedMultiplier;
        transform.position = transform.position + movement * Time.deltaTime;
    }

    public void ToggleHolding()
    {
        if (isHolding)
            isHolding = false;
        else 
            isHolding = true;
    }
}
