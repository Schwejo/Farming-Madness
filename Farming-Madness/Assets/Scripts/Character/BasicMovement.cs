using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;

    private Vector3 movement;
    private Vector3 lastMovement;

    public float speedMultiplier = 5.0f;


    private void Update()
    {
        ProcessInputs();
        Animate();
        Move();
    }

    private void ProcessInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        //Store last movement vector while walking.
        if(movement.magnitude != 0)
        {
            lastMovement = movement;
        }
    }

    private void Animate()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        
        animator.SetFloat("Magnitude", movement.magnitude);

        animator.SetFloat("HorizontalIdle", lastMovement.x);
        animator.SetFloat("VerticalIdle", lastMovement.y);
    }

    private void Move() 
    {
        movement = movement * speedMultiplier;
        transform.position = transform.position + movement * Time.deltaTime;
    }
}
