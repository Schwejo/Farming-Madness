using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;

    public BoxCollider2D upCol;
    public BoxCollider2D leftCol;
    public BoxCollider2D downCol;
    public BoxCollider2D rightCol;

    public float speedMultiplier = 5.0f;
    public bool movementEnabled = true;

    private KeyCode up;
    private KeyCode down;
    private KeyCode left;
    private KeyCode right;
    
    private Vector2 movement;
    private Vector2 lastMovement;
    
    private bool isMoving = false;
    private bool isHolding = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(0,0);
    }

    private void OnEnable()
    {
        LevelManager.OnTimeIsUp += EndGame;
    }

    private void OnDisable()
    {
        LevelManager.OnTimeIsUp -= EndGame;
    }

    private void Update()
    {
        if (movementEnabled)
        {
            ProcessInputs();
            Animate();
            Move();
        }
    }

    public void SetupKeys(KeyCode kUp, KeyCode kDown, KeyCode kLeft, KeyCode kRight)
    {
        up = kUp;
        down = kDown;
        left = kLeft;
        right = kRight;
    }

    private void ProcessInputs()
    {
        if (Input.GetKey(up))
        {
            movement.y = 1;
        }
        else if (Input.GetKey(down))
        {
            movement.y = -1;
        }
        else 
        {
            movement.y = 0;
        }

        if (Input.GetKey(left))
        {
            movement.x = -1;
        }
        else if (Input.GetKey(right))
        {
            movement.x = 1;
        }
        else
        {
            movement.x = 0;
        }
        
        
        //movement.x = Input.GetAxis(horizontalAxis);
        //movement.y = Input.GetAxis(verticalAxis);

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

        movement.Normalize();
    }

    private void Animate()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        
        animator.SetBool("IsMoving", isMoving);

        //Set correct idle pose by comparing to last movemment vector.
        animator.SetFloat("HorizontalIdle", lastMovement.x);
        animator.SetFloat("VerticalIdle", lastMovement.y);

        animator.SetBool("IsHolding", isHolding);
    }

    /*
     * Moves the rigidbody with its velocity param.
     * Sets correct collider based on movement direction.
     */
    private void Move() 
    {
        movement = movement * speedMultiplier;
        rb.velocity = movement;
        //transform.position = transform.position + movement * Time.deltaTime;

        if (movement.y > 0.1f)
		{
			upCol.enabled = true;
			downCol.enabled = false;
			rightCol.enabled = false;
			leftCol.enabled = false;
		}
		else if (movement.y < -0.1f)
		{
			upCol.enabled = false;
			downCol.enabled = true;
			rightCol.enabled = false;
			leftCol.enabled = false;
		}

		if (movement.x > 0.1f)
		{
			upCol.enabled = false;
			downCol.enabled = false;
			rightCol.enabled = true;
			leftCol.enabled = false;
		} 
        else if (movement.x < -0.1f)
		{
			upCol.enabled = false;
			downCol.enabled = false;
			rightCol.enabled = false;
			leftCol.enabled = true;
		}
    }

    public void SetIsHolding(bool value)
    {
        isHolding = value;
    }

    private void EndGame(int stars)
    {
        movementEnabled = false;
    }
}
