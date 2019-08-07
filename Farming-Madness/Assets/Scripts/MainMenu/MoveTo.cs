using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform target;
    public float speed = 5.0f;
    public bool destroyAtTarget = false;
    
    private bool isMoving = true;
    private Vector3 targetPoint;
    private Animator animator;

    private void Start()
    {
        targetPoint = target.position;
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (isMoving)
        {
           Move();
           animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, step);

        //reached target
        if (Vector3.Distance(transform.position, targetPoint) < 0.001f)
        {
            isMoving = false;

            if (destroyAtTarget)
            {
                Destroy(gameObject);
            }
        }
    }
}
