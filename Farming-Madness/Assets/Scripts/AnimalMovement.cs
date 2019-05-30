using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public AnimalCage cage;

    private float top;
    private float bottom;
    private float right;
    private float left;
    private Vector3 target;
    private Vector3 lastIdlePos;

    [Header("Idle Time")]
    public float idleTime;
    public float idleMin = 8.0f;
    public float idleMax = 20.0f;

    [Header("Movement")]
    public float speed = 1.0f;
    private bool isMoving = false;

    private Animator anim;

    private void Start()
    {
        top = cage.GetTop();
        bottom = cage.GetBottom();
        left = cage.GetLeft();
        right = cage.GetRight();

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("No Animator on Animal");
        }

        idleTime = Random.Range(idleMin, idleMax);
        StartCoroutine("Idle");
    }

    private void Update()
    {
        if (isMoving) 
        {
            Move();
        }
        Animate();
    }

    /* Moves the object towards the target. Starts idle coroutine when position is near equal */
    private void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                StartCoroutine("Idle");
            }
    }

    /* Idle for random time between idleMin and idleMax */
    private IEnumerator Idle()
    {
        isMoving = false;
        idleTime = Random.Range(idleMin, idleMax);
        lastIdlePos = transform.position;
        yield return new WaitForSeconds(idleTime);
        GetNewTarget();
        isMoving = true;
    }

    private void GetNewTarget()
    {
        target.x = Random.Range(left, right);
        target.y = Random.Range(bottom, top);
    }

    private void Animate()
    {
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("Horizontal", target.x - lastIdlePos.x);
        anim.SetFloat("Vertical", target.y - lastIdlePos.y);
        //TODO: Idle animation is not set correct
    }
}
