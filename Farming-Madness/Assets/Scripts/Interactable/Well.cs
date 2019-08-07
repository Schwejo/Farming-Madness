using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    public float timeToPullUp = 15f;
    public bool readyToTake;
    public Product water;
    
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("No well animator found.");
        StartCoroutine("PullUp");
    }

    private IEnumerator PullUp()
    {
        yield return new WaitForSeconds(timeToPullUp);

        animator.SetBool("Up", true);
        readyToTake = true;
    }

    public void Interact(Crop c, bool hasCan, PlayerInteraction player)
    {
        if (c == null && !hasCan && readyToTake)
        {
            player.SetProduct(water);

            readyToTake = false;
            animator.SetBool("Up", false);
            StartCoroutine("PullUp");
        }
    }
}
