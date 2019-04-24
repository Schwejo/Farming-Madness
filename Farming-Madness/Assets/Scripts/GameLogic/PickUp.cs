using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered");
        if (Input.GetButtonDown("P1Interact"))
        {
            Debug.Log("Button pressed");
            other.GetComponent<BasicMovement>().ToggleHolding();
        }
    }
}
